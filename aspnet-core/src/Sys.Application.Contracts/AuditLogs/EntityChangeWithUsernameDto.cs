using System;
using System.Collections.Generic;
using System.Text;

namespace Sys.AuditLogs
{
    public class EntityChangeWithUsernameDto
    {
        public EntityChangeDto EntityChange { get; set; }

        public string UserName { get; set; }
    }
}
