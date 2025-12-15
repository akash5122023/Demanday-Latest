
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.FacebookDetails")]
    [BasedOnRow(typeof(FacebookDetailsRow), CheckNames = true)]
    public class FacebookDetailsForm
    {
        [HalfWidth(UntilNext = true)]
        public String LeadId { get; set; }
        public String Campaignid { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String CompaignName { get; set; }
        public String AdSetName { get; set; }
        [DateTimeEditor]
        public DateTime CreatedTime { get; set; }
        public String Company { get; set; }
        public String AdId { get; set; }
        public String AdName { get; set; }
        public String AdSetId { get; set; }
        public String AdditionalDetails { get; set; }
        [TextAreaEditor(Rows = 4), FullWidth]
        public String Feedback { get; set; }
        [Hidden]
        public Boolean IsMoved { get; set; }
    }
}