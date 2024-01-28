using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Medallion.Threading;
using Medallion.Threading.Redis;
using Medallion.Threading.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sys.EntityFrameworkCore;
using Sys.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.BlobStoring;
using EasyAbp.FileManagement;
using EasyAbp.FileManagement.Options;
using EasyAbp.FileManagement.Files;
using EasyAbp.FileManagement.Containers;
using Volo.Abp.BlobStoring.FileSystem;
using EasyAbp.FileManagement.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using System.Security.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Sys;

[DependsOn(
    typeof(FileManagementApplicationModule),
    typeof(FileManagementDomainModule),
    typeof(FileManagementEntityFrameworkCoreModule),
    typeof(FileManagementHttpApiModule),
    typeof(SysHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(SysApplicationModule),
    typeof(SysEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpBlobStoringFileSystemModule)
)]
public class SysHttpApiHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        PreConfigure<OpenIddictServerBuilder>(options =>
        {
            if (hostingEnvironment.IsProduction())
            {
                options.AddEncryptionCertificate(LoadCert(
                    configuration["AuthServer:EncryptionCertificateThumbprint"]));
                options.AddSigningCertificate(LoadCert(
                    configuration["AuthServer:SigningCertificateThumbprint"]));
            };

            //options.RegisterScopes(["SysScope"]);
            //options.AllowClientCredentialsFlow();
            //options.AllowAuthorizationCodeFlow();
            //options.AllowClientCredentialsFlow();
            //options.SetTokenEndpointUris("token");
            //options.UseAspNetCore().EnableTokenEndpointPassthrough();
            //options.DisableAccessTokenEncryption();
        });
    }

    private X509Certificate2 LoadCert(string thumbprint)
    {
        //var thumbprint = "xxxxxxxxxxxxxx";

        X509Certificate2 retVal = null;

        X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        certStore.Open(OpenFlags.ReadOnly);

        X509Certificate2Collection certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

        if (certCollection.Count > 0)
        {
            retVal = certCollection[0];
        }

        certStore.Close();

        return retVal;
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        ConfigureConventionalControllers();
        ConfigureCache(configuration);
        ConfigureVirtualFileSystem(context);
        ConfigureDataProtection(context, configuration, hostingEnvironment);
        ConfigureDistributedLocking(context, configuration);
        ConfigureCors(context, configuration);
        ConfigureAuthentication(context, configuration);
        ConfigureSwaggerServices(context, configuration);

        Configure<AbpBlobStoringOptions>(options =>
        {
            options.Containers.Configure<LocalFileSystemBlobContainer>(container =>
            {
                container.IsMultiTenant = true;
                container.UseFileSystem(fileSystem =>
                {
                    // fileSystem.BasePath = "C:\\my-files";
                    fileSystem.BasePath = Path.Combine(hostingEnvironment.ContentRootPath, "my-files");
                });
            });
        });

        Configure<FileManagementOptions>(options =>
        {
            options.DefaultFileDownloadProviderType = typeof(LocalFileDownloadProvider);
            options.Containers.Configure<CommonFileContainer>(container =>
            {
                // private container never be used by non-owner users (except user who has the "File.Manage" permission).
                container.FileContainerType = FileContainerType.Public;
                container.AbpBlobContainerName = BlobContainerNameAttribute.GetContainerName<LocalFileSystemBlobContainer>();
                container.AbpBlobDirectorySeparator = "/";

                container.RetainUnusedBlobs = false;
                container.EnableAutoRename = true;

                container.MaxByteSizeForEachFile = 5 * 1024 * 1024;
                container.MaxByteSizeForEachUpload = 10 * 1024 * 1024;
                container.MaxFileQuantityForEachUpload = 2;

                container.AllowOnlyConfiguredFileExtensions = true;
                container.FileExtensionsConfiguration.Add(".txt", true);
                container.FileExtensionsConfiguration.Add(".csv", true);
                container.FileExtensionsConfiguration.Add(".xlsx", true);
                //container.FileExtensionsConfiguration.Add(".jpg", true);
                //container.FileExtensionsConfiguration.Add(".PNG", true);
                // container.FileExtensionsConfiguration.Add(".tar.gz", true);
                // container.FileExtensionsConfiguration.Add(".exe", false);

                container.GetDownloadInfoTimesLimitEachUserPerMinute = 10;
            });
        });
    }

    private void ConfigureCache(IConfiguration configuration)
    {
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Sys:"; });
    }

    private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<SysDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Sys.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<SysDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Sys.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<SysApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Sys.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<SysApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Sys.Application"));
            });
        }
    }

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(SysApplicationModule).Assembly);
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = configuration["AuthServer:Authority"];
            options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
            options.Audience = "Sys";
            //options.BackchannelHttpHandler = GetHandler();
            //options.MetadataAddress = configuration["AuthServer:Authority"] + "/.well-known/openid-configuration";
            //options.TokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //    ValidateIssuerSigningKey = true,
            //    ValidateLifetime = true,
            //    ValidIssuer = "Sys",//configuration["Jwt:Issuer"],
            //    ValidAudience = "Sys", //configuration["Jwt:Audience"],
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("7B19E8634F02EAB26B3723241E961D76BF4F8289")),//new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
            //    ClockSkew = TimeSpan.Zero
            //};
        });//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
           //.AddOAuthIntrospection(options =>
           //{
           //    options.Authority = new Uri("http://localhost:54540/"); //<-please mind the URI.
           //    options.Audiences.Add("resource-server-1");
           //    options.ClientId = "resource-server-1";
           //    options.ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654"; //Assume you assign this secret on your Auth server
           //    options.RequireHttpsMetadata = false;
           //}); 
    }

    private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAbpSwaggerGenWithOidc(
            configuration["AuthServer:Authority"]!,
            ["Sys", "Sys API"], null, null
            ,
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sys API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);

            });
    }

    private static HttpClientHandler GetHandler()
    {
        var handler = new HttpClientHandler();
        handler.ClientCertificateOptions = ClientCertificateOption.Manual;
        handler.SslProtocols = SslProtocols.Tls12;
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        return handler;
    }

    private void ConfigureDataProtection(
    ServiceConfigurationContext context,
    IConfiguration configuration,
    IWebHostEnvironment hostingEnvironment)
    {
        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("SysTiered");
        if (!hostingEnvironment.IsDevelopment())
        {
            //var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            //dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "SysTiered-Protection-Keys");
            dataProtectionBuilder.PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory()));
        }
    }

    private void ConfigureDistributedLocking(
        ServiceConfigurationContext context,
        IConfiguration configuration)
    {
        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            //var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            //return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
            return new SqlDistributedSynchronizationProvider(configuration["ConnectionStrings:Default"]!);
        });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(configuration["App:CorsOrigins"]?
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.RemovePostFix("/"))
                        .ToArray() ?? Array.Empty<string>())
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseAuthorization();

        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Sys API");

            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthScopes("Sys");
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }
}
