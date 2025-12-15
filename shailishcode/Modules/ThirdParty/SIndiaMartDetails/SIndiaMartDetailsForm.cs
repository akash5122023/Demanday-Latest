
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.SIndiaMartDetails")]
    [BasedOnRow(typeof(SIndiaMartDetailsRow), CheckNames = true)]
    public class SIndiaMartDetailsForm
    {
        [HalfWidth(UntilNext = true)]
        public String SenderName { get; set; }
        public String SenderEmail { get; set; }

        public String Subject { get; set; }
        public String ProductName { get; set; }
        public Int32 Source { get; set; }
        [DateTimeEditor]
        public DateTime DateTimeRe { get; set; }
        public String GlUserCompanyName { get; set; }
        public String Mob { get; set; }
        [TextAreaEditor(Rows = 4)]
        public String EnqMessage { get; set; }
        [TextAreaEditor(Rows = 4)]
        public String EnqAddress { get; set; }
        public String EnqCallDuration { get; set; }
        public String EnqReceiverMob { get; set; }
        public String EnqCity { get; set; }
        public String EnqState { get; set; }
        public String EmailAlt { get; set; }
        public String MobileAlt { get; set; }
        public String Phone { get; set; }
        public String PhoneAlt { get; set; }
        [TextAreaEditor(Rows = 4), FullWidth]
        public String Feedback { get; set; }
        [Hidden]
        public Boolean IsMoved { get; set; }
    }
}