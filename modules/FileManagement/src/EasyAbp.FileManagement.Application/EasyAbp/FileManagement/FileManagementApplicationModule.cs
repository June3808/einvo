using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Domain.Entities.Events.Distributed;
using EasyAbp.FileManagement.Files;

namespace EasyAbp.FileManagement
{
    [DependsOn(
        typeof(FileManagementDomainModule),
        typeof(FileManagementApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class FileManagementApplicationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<File, FileEto>(typeof(FileManagementApplicationModule));
                options.AutoEventSelectors.Add<File>();
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<FileManagementApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FileManagementApplicationModule>(validate: true);
            });
        }
    }
}
