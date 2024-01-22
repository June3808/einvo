using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Timing;
using Volo.Abp.Validation;

namespace Sys.AuditLogs
{
    [DisableDateTimeNormalization]
    public class GetAuditLogListDto : PagedAndSortedResultRequestDto
    {
        [DynamicStringLength(typeof(AuditLogDtoCommonConsts), "UrlFilterMaxLength", null)]
        public string? Url { get; set; }

        [DynamicStringLength(typeof(AuditLogDtoCommonConsts), "UserNameFilterMaxLength", null)]
        public string? UserName { get; set; }

        [DynamicStringLength(typeof(AuditLogDtoCommonConsts), "UserNameFilterMaxLength", null)]
        public string? UserId { get; set; }

        public string? ApplicationName { get; set; }

        public string? CorrelationId { get; set; }

        [DynamicStringLength(typeof(AuditLogDtoCommonConsts), "HttpMethodFilterMaxLength", null)]
        public string? HttpMethod { get; set; }

        public HttpStatusCode? HttpStatusCode { get; set; }

        public int? MaxExecutionDuration { get; set; }

        public int? MinExecutionDuration { get; set; }

        public bool? HasException { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
