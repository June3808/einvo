using System;
using System.IO;
using System.Linq;
using Localization.Resources.AbpUi;
using Medallion.Threading;
//using Medallion.Threading.Redis;
using Medallion.Threading.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sys.EntityFrameworkCore;
using Sys.Localization;
using Sys.MultiTenancy;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.OpenIddict;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp.Security.Claims;

namespace Sys;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(SysEntityFrameworkCoreModule),
    typeof(AbpAspNetCoreSerilogModule)
    )]
public class SysAuthServerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("Sys");
                options.UseLocalServer();
                options.UseAspNetCore();
                //options.SetClientId("Sys_Web");
                //options.AddEncryptionCertificate(certificate);
                //options.AddSigningCertificate(certificate2);
            });
        });

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


        //var INSERT_PASSWORD_HERE = "P@ssw0rd1!";

        //using var algorithm = RSA.Create(keySizeInBits: 2048);

        //var subject = new X500DistinguishedName("CN=Fabrikam Encryption Certificate");
        //var request = new CertificateRequest(
        //    subject, algorithm, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        //request.CertificateExtensions.Add(
        //    new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment, critical: true));

        //var certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));

        //File.WriteAllBytes("encryption-certificate.pfx",
        //    certificate.Export(X509ContentType.Pfx, INSERT_PASSWORD_HERE));


        //using var algorithm2 = RSA.Create(keySizeInBits: 2048);

        //var subject2 = new X500DistinguishedName("CN=Fabrikam Signing Certificate");
        //var request2 = new CertificateRequest(
        //    subject2, algorithm2, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        //request2.CertificateExtensions.Add(
        //    new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, critical: true));

        //var certificate2 = request2.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));

        //File.WriteAllBytes("signing-certificate.pfx",
        //    certificate2.Export(X509ContentType.Pfx, INSERT_PASSWORD_HERE));
    }


    //private X509Certificate2 LoadCertificate(string thumbprint)
    //{
    //    var bytes = File.ReadAllBytes($"C:\\appservice\\certificates\\private\\{thumbprint}.p12");
    //    return new X509Certificate2(bytes);
    //}

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

    //private void ConfigureAuthentication(ServiceConfigurationContext context)
    //{
    //    context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
    //}


    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        //context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SysResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });

        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });

        Configure<AbpAuditingOptions>(options =>
        {
            //options.IsEnabledForGetRequests = true;
            options.ApplicationName = "AuthServer";
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<SysDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Sys.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<SysDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Sys.Domain"));
            });
        }

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"]?.Split(',') ?? Array.Empty<string>());

            options.Applications["Angular"].RootUrl = configuration["App:ClientUrl"];
            options.Applications["Angular"].Urls[AccountUrlNames.PasswordReset] = "account/reset-password";
        });

        Configure<AbpBackgroundJobOptions>(options =>
        {
            options.IsJobExecutionEnabled = false;
        });

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Sys:";
        });

        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("SysTiered");
        if (!hostingEnvironment.IsDevelopment())
        {
            //var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            //dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "SysTiered-Protection-Keys");
            dataProtectionBuilder.PersistKeysToFileSystem(new DirectoryInfo(Directory.GetCurrentDirectory()));
        }

        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            //var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            //return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
            return new SqlDistributedSynchronizationProvider(configuration["ConnectionStrings:Default"]!);
        });

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray() ?? Array.Empty<string>()
                    )
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

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
