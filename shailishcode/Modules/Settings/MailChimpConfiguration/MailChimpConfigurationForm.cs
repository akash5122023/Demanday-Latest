
namespace AdvanceCRM.Settings.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Settings.MailChimpConfiguration")]
    [BasedOnRow(typeof(MailChimpConfigurationRow), CheckNames = true)]
    public class MailChimpConfigurationForm
    {
        [Category("General")]
        public String ApiKey { get; set; }
        public String CompanyName { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public Int32 Country { get; set; }
        public String Reminder { get; set; }
        [Category("Contact")]
        public String ContactFromEmail { get; set; }
        public String ContactFromName { get; set; }
        public String ContactSubject { get; set; }
        [Category("Enquiry")]
        public String EnquiryFromEmail { get; set; }
        public String EnquiryFromName { get; set; }
        public String EnquirySubject { get; set; }
        [Category("Quotation")]
        public String QuotationFromEmail { get; set; }
        public String QuotationFromName { get; set; }
        public String QuotationSubject { get; set; }
        [Category("Sale")]
        public String SaleFromEmail { get; set; }
        public String SaleFromName { get; set; }
        public String SaleSubject { get; set; }
    }
}