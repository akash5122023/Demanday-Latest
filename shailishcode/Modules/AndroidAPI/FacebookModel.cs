using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class FacebookModel
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String CompaignName { get; set; }
        public String LeadId { get; set; }
        public String Campaignid { get; set; }
        public String AdSetName { get; set; }
        public String Address { get; set; }
        public DateTime CreatedTime { get; set; }
        public String Company { get; set; }
        public String AdId { get; set; }      
        public String AdName { get; set; }    
        public String AdSetId { get; set; }
        public String AdditionalDetails { get; set; }
        public String Feedback { get; set; }      
        public Boolean IsMoved { get; set; }
    }
}