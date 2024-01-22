using System;
using System.IO;
using Medallion.Threading;
//using Medallion.Threading.Redis;
using Medallion.Threading.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Sys.Localization;
using Sys.MultiTenancy;
using Sys.Web.Menus;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Authentication.OpenIdConnect;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Http.Client.Web;
using Volo.Abp.Identity.Web;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using EasyAbp.Abp.SettingUi.Web;
using EInvoice.Web;
using EInvoice;
using EasyAbp.FileManagement.Web;
using EInvoice.EntityFrameworkCore;
using Sys.EntityFrameworkCore;
using EasyAbp.FileManagement;
using Volo.Abp.BlobStoring;
using EasyAbp.FileManagement.Options;
using EasyAbp.FileManagement.Files;
using EasyAbp.FileManagement.Containers;
using Volo.Abp.BlobStoring.FileSystem;
using TaskScheduler.Web;
using TaskScheduler;
using TaskScheduler.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskScheduler.TaskSchedulers;
using Quartz;
using TaskScheduler.Listener;

namespace Sys.Web;

[DependsOn(
    typeof(FileManagementWebModule),
    typeof(EInvoiceWebModule),
    typeof(EInvoiceApplicationModule),
    typeof(EInvoiceEntityFrameworkCoreModule),
    typeof(SysEntityFrameworkCoreModule),
    typeof(SysHttpApiClientModule),
    typeof(SysHttpApiModule),
    typeof(AbpAspNetCoreAuthenticationOpenIdConnectModule),
    typeof(AbpAspNetCoreMvcClientModule),
    typeof(AbpHttpClientWebModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpSettingManagementWebModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpSettingUiWebModule)
    )]
[DependsOn(typeof(TaskSchedulerWebModule),
    typeof(TaskSchedulerApplicationModule),
    typeof(TaskSchedulerEntityFrameworkCoreModule)
    )]
public class SysWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(SysResource),
                typeof(SysDomainSharedModule).Assembly,
                typeof(SysApplicationContractsModule).Assembly,
                typeof(SysWebModule).Assembly
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureBundles();
        ConfigureCache();
        ConfigureDataProtection(context, configuration, hostingEnvironment);
        ConfigureDistributedLocking(context, configuration);
        ConfigureUrls(configuration);
        ConfigureAuthentication(context, configuration);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices(configuration);
        ConfigureMultiTenancy();
        ConfigureSwaggerServices(context.Services);


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

    private void ConfigureBundles()
    {
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
    }

    private void ConfigureCache()
    {
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Sys:";
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureMultiTenancy()
    {
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc";
        })
            .AddCookie("Cookies", options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
                options.CheckTokenExpiration();
            })
            .AddAbpOpenIdConnect("oidc", options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.ResponseType = OpenIdConnectResponseType.CodeIdToken;

                options.ClientId = configuration["AuthServer:ClientId"];
                options.ClientSecret = configuration["AuthServer:ClientSecret"];

                options.UsePkce = true;
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.Scope.Add("roles");
                options.Scope.Add("email");
                options.Scope.Add("phone");
                options.Scope.Add("Sys");
            });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SysWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<SysDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Sys.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<SysApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Sys.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<SysWebModule>(hostingEnvironment.ContentRootPath);

                var sharedpath = Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EInvoice{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EInvoice.Domain.Shared");
                options.FileSets.ReplaceEmbeddedByPhysical<EInvoiceDomainSharedModule>(sharedpath);
                options.FileSets.ReplaceEmbeddedByPhysical<EInvoiceApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EInvoice{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EInvoice.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<EInvoiceWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EInvoice{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EInvoice.Web"));

                var sharedpath2 = Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}TaskScheduler{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}TaskScheduler.Domain.Shared");
                options.FileSets.ReplaceEmbeddedByPhysical<TaskSchedulerDomainSharedModule>(sharedpath2);
                options.FileSets.ReplaceEmbeddedByPhysical<TaskSchedulerApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}TaskScheduler{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}TaskScheduler.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<TaskSchedulerWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}TaskScheduler{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}TaskScheduler.Web"));

            });
        }
    }

    private void ConfigureNavigationServices(IConfiguration configuration)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new SysMenuContributor(configuration));
        });

        Configure<AbpToolbarOptions>(options =>
        {
            options.Contributors.Add(new SysToolbarContributor());
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Sys API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    private void ConfigureDataProtection(
        ServiceConfigurationContext context,
        IConfiguration configuration,
        IWebHostEnvironment hostingEnvironment)
    {
        var dataProtectionBuilder = context.Services.AddDataProtection().SetApplicationName("Sys");
        if (!hostingEnvironment.IsDevelopment())
        {
            //var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            //dataProtectionBuilder.PersistKeysToStackExchangeRedis(redis, "Sys-Protection-Keys");
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
        });
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}