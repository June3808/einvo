using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Sys.AuditLogs
{
    public class EntityPropertyChangeDto : EntityDto<Guid>
    {
        public Guid? TenantId { get; set; }

        public Guid EntityChangeId { get; set; }

        public string NewValue { get; set; }

        public string OriginalValue { get; set; }

        public string PropertyName { get; set; }

        public string PropertyTypeFullName { get; set; }
    }
}
