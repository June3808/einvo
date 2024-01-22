using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.Collections.Generic;
using Sys.AuditLogs;

namespace Sys.Web.Pages.AuditLogs.ViewModels
{
    public class DetailViewModel
    {
        [DisabledInput]
        [Display(Name = "User Name")]
        public string userName { get; set; }

        [DisabledInput]
        [Display(Name = "Execution Time")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime executionTime { get; set; }

        [DisabledInput]
        [Display(Name = "Execution Duration")]
        //[TextArea(Rows = 12)]
        public long executionDuration { get; set; }

        [DisabledInput]
        [Display(Name = "Client IpAddress")]
        public string clientIpAddress { get; set; }

        [DisabledInput]
        [Display(Name = "Client Name")]
        public string clientName { get; set; }

        [DisabledInput]
        [Display(Name = "Application Name")]
        public string applicationName { get; set; }

        [DisabledInput]
        [Display(Name = "Browser")]
        public string browser { get; set; }

        [DisabledInput]
        [Display(Name = "HttpMethod")]
        public string hHttpMethod { get; set; }

        [DisabledInput]
        [Display(Name = "HttpStatusCode")]
        public int? httpStatusCode { get; set; }

        [DisabledInput]
        [Display(Name = "Url")]
        public string url { get; set; }

        [DisabledInput]
        [Display(Name = "Exceptions")]
        [TextArea(Rows = 4)]
        public string exceptions { get; set; }

        [DisabledInput]
        [TextArea(Rows = 4)]
        public List<EntityChangeDto> entityChanges { get; set; }

        [DisabledInput]
        [TextArea(Rows = 4)]
        public string actions { get; set; }
    }
}
