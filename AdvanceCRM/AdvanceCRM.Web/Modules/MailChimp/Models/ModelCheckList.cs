using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdvanceCRM.Modules.MailChimp.Models
{
    public class ModelCheckList
    {
        public string id;
        public string campaignId;
        public string TemplateResolveId;
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ScheduleDate;
        public string templateId;
        public bool CheckReady;
        public IEnumerable<SubCheckList> CheckLists { get; set; }
        //public string Details { get; set; }
        //public string Heading { get; set; }
        //public SubResult Type { get; set; }
        //public string Type { get; set; }
        public bool IsReady { get; set; }

    }
    public class SubCheckList
    {

        public string Details { get; set; }
        public string Heading { get; set; }
        public SubResult Type { get; set; }
    }
    public enum SubResult
    {
        Success = 0,
        Warning = 1,
        Error = 2
    }

}