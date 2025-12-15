
namespace AdvanceCRM.Template.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Template.DailyWishesTemplate")]
    [BasedOnRow(typeof(DailyWishesTemplateRow), CheckNames = true)]
    public class DailyWishesTemplateForm
    {
        [Category("Mmail Templates")]
        public String From { get; set; }
        public String Subject { get; set; }
        [HtmlContentEditor]
        public String BirthdayEmail { get; set; }
        [HtmlContentEditor]
        public String MarriageEmail { get; set; }
        [HtmlContentEditor]
        public String DofAnniversaryEmail { get; set; }
        [Category("SMS Templates")]
        public String BirthdaySMS { get; set; }
        public String BirthTempId { get; set; }
        public String MarriageSMS { get; set; }
        public String MarriageTempId { get; set; }
        public String DofAnniversarySMS { get; set; }
        public String DofTempId { get; set; }
        [Category("WhatsApp Templates")]
        public String WaBirTemplate { get; set; }
        public String WaBirTemplateId { get; set; }
        public String WaMarTemplate { get; set; }
        public String WaMarTemplateId { get; set; }
        public String WaAnnTemplate { get; set; }
        public String WaAnnTemplateId { get; set; }
    }
}