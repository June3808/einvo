using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using EInvoice.Localization;
using EInvoice.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using EInvoice.Permissions;
using EasyAbp.FileManagement;
using EasyAbp.FileManagement.Options;
using EasyAbp.FileManagement.Files;
using EasyAbp.FileManagement.Containers;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.FileSystem;
using System.IO;

namespace EInvoice.Web;

[DependsOn(
    typeof(FileManagementApplicationContractsModule),
    typeof(EInvoiceApplicationContractsModule),
    typeof(AbpBlobStoringFileSystemModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class EInvoiceWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(EInvoiceResource), typeof(EInvoiceWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(EInvoiceWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new EInvoiceMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EInvoiceWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<EInvoiceWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EInvoiceWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
                //Configure authorization.
        });

    }
}
