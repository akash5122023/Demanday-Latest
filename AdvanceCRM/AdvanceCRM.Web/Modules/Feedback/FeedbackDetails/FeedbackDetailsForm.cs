
namespace AdvanceCRM.Feedback.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Feedback.FeedbackDetails")]
    [BasedOnRow(typeof(FeedbackDetailsRow), CheckNames = true)]
    public class FeedbackDetailsForm
    {
        public String Name { get; set; }
        public String Phone { get; set; }
        public Int32 Service { get; set; }
        public Boolean Refer { get; set; }
        public String Details { get; set; }
    }
}