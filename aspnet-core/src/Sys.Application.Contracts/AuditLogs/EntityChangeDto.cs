﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace Sys.AuditLogs
{
    public class EntityChangeDto : ExtensibleEntityDto<Guid>
    {
        public Guid AuditLogId { get; set; }

        public Guid? TenantId { get; set; }

        public DateTime ChangeTime { get; set; }

        public EntityChangeType ChangeType { get; set; }

        public string EntityId { get; set; }

        public string EntityTypeFullName { get; set; }

        public List<EntityPropertyChangeDto> PropertyChanges { get; set; }
    }
}
