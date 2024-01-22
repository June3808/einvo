using AutoMapper;
using Sys.AuditLogs;
using Volo.Abp.AuditLogging;
using Volo.Abp.AutoMapper;

namespace Sys;

public class SysApplicationAutoMapperProfile : Profile
{
    public SysApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<AuditLog, AuditLogDto>();
        CreateMap<EntityChange, EntityChangeDto>();
        CreateMap<AuditLogAction, AuditLogActionDto>();
    }
}
