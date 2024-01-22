using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.Timing;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.ComponentModel;

namespace Sys.Web.Pages.AuditLogs.ViewModels
{
    public class QueryViewModel
    {
        [Display(Name = "Query String")]
        public string QueryString { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Start Time")]
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; } = DateTime.Now.Date;

        [Display(Name = "End Time")]
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; } = DateTime.Now;

        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Display(Name = "Application Name")]
        public string ApplicationName { get; set; }

        [Display(Name = "Browser")]
        public string Browser { get; set; }

        [Display(Name = "HttpMethod")]
        public string HttpMethod { get; set; }

        [Display(Name = "HttpStatusCode")]
        public int? HttpStatusCode { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "Exceptions")]
        public string Exceptions { get; set; }

        [Display(Name = "Has Exception")]
        public bool? hasException { get; set; }
    }
}
