
namespace AdvanceCRM.Template.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Template.AppointmentTemplate")]
    [BasedOnRow(typeof(AppointmentTemplateRow), CheckNames = true)]
    public class AppointmentTemplateForm
    {
        [Category("Email")]
        public String Sender { get; set; }
        public String Subject { get; set; }
        [HtmlContentEditor]
        public String EmailTemplate { get; set; }
        [Hint("This is optional field, you can select any file to be sent as by default as attachment with Appointment thank you email")]
        public String Attachment { get; set; }
        [Category("SMS")]
        public String SMSTemplate { get; set; }
        public String SmsTempId { get; set; }       
       
        public String MondaySMS { get; set; }
        public String MonTempId { get; set; }
        public String TuesdaySMS { get; set; }
        public String TueTempId { get; set; }
        public String WednesdaySMS { get; set; }
        public String WedTempId { get; set; }
        public String ThursdaySMS { get; set; }
        public String ThurTempId { get; set; }
        public String FridaySMS { get; set; }
        public String FriTempId { get; set; }
        public String SaturdaySMS { get; set; }
        public String SatTempId { get; set; }
        public String SundaySMS { get; set; }
        public String SunTempId { get; set; }
        [Category("WhatsApp")]
        public String WaTemplate { get; set; }
        public String WaTemplateId { get; set; }
        public String WaMonTemplate { get; set; }
        public String WaMonTemplateId { get; set; }
        public String WaTueTemplate { get; set; }
        public String WaTueTemplateId { get; set; }
        public String WaWedTemplate { get; set; }
        public String WaWebTemplateId { get; set; }
        public String WaThurTemplate { get; set; }
        public String WaThurTemplateId { get; set; }
        public String WaFriTemplate { get; set; }
        public String WaFriTemplateId { get; set; }
        public String WaSatTemplate { get; set; }
        public String WaSatTemplateId { get; set; }
        public String WaSunTemplate { get; set; }
        public String WaSunTemplateId { get; set; }

        [Category("Mail Configuration")]
        [HalfWidth, Hint("Configure this if you want to use this mail id by all users to send mail, keep blank to send mail, by users respective mail Id's")]
        public String Host { get; set; }
        [QuarterWidth]
        public Int32 Port { get; set; }
        [QuarterWidth]
        public Boolean SSL { get; set; }
        [HalfWidth]
        public String EmailId { get; set; }
        [HalfWidth]
        public String EmailPassword { get; set; }
    }
}