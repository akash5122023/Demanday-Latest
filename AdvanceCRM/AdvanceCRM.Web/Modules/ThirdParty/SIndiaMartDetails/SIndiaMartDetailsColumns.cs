
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using Serenity.Data.Mapping;
    using System.IO;

    [ColumnsScript("ThirdParty.SIndiaMartDetails")]
    [BasedOnRow(typeof(SIndiaMartDetailsRow), CheckNames = true)]
    public class SIndiaMartDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink,QuickSearch]
        public String SenderName { get; set; }

        [EditLink]
        public String SenderEmail { get; set; }

        [QuickFilter]
        public Int32 Source { get; set; }
        public String Subject { get; set; }
        [QuickFilter]
        public Boolean IsMoved { get; set; }
        [QuickFilter, DateTimeEditor, DateTimeFormatter(DisplayFormat = "dd/MM/yyyy HH:mm")]
        public DateTime DateTimeRe { get; set; }
        public String GlUserCompanyName { get; set; }
        public String Mob { get; set; }
        public String CountryFlag { get; set; }
        public String EnqMessage { get; set; }
        public String EnqAddress { get; set; }
        public String EnqCallDuration { get; set; }
        public String EnqReceiverMob { get; set; }
        public String EnqCity { get; set; }
        public String EnqState { get; set; }
        public String EmailAlt { get; set; }
        public String MobileAlt { get; set; }
        public String Phone { get; set; }
        public String PhoneAlt { get; set; }
        public String Feedback { get; set; }
    }
}