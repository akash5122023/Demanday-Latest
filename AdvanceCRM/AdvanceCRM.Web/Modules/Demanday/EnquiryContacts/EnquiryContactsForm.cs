using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Demanday.Forms
{
    [FormScript("Demanday.EnquiryContacts")]
    [BasedOnRow(typeof(EnquiryContactsRow), CheckNames = true)]
    public class EnquiryContactsForm
    {
        [Category("Contact Information")]
        [HalfWidth]
        public string FirstName { get; set; }
        [HalfWidth]
        public string LastName { get; set; }
        [HalfWidth]
        public string Title { get; set; }
        [HalfWidth]
        public string Email { get; set; }
        [HalfWidth]
        public string WorkPhone { get; set; }
        [HalfWidth]
        public string AlternativeNumber { get; set; }

        [Category("Company Information")]
        [HalfWidth]
        public string CompanyName { get; set; }
        [HalfWidth]
        public string Industry { get; set; }
        [HalfWidth]
        public string Revenue { get; set; }
        [HalfWidth, DisplayName("Employee Size")]
        public string CompanyEmployeeSize { get; set; }

        [Category("Address Information")]
        [FullWidth]
        public string Street { get; set; }
        [OneThirdWidth]
        public string City { get; set; }
        [OneThirdWidth]
        public string State { get; set; }
        [OneThirdWidth]
        public string ZipCode { get; set; }
        [HalfWidth]
        public string Country { get; set; }

        [Category("Links & References")]
        [HalfWidth]
        public string ProfileLink { get; set; }
        [HalfWidth]
        public string CompanyLink { get; set; }
        [HalfWidth]
        public string RevenueLink { get; set; }
        [HalfWidth]
        public string AdressLink { get; set; }


        [Category("Additional Details")]
        public string Tenurity { get; set; }
        [HalfWidth]
        public string Code { get; set; }
        [HalfWidth]
        public string Md5 { get; set; }
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
        //public String CompanyName { get; set; }
        //public String FirstName { get; set; }
        //public String LastName { get; set; }
        //public String Title { get; set; }
        //public String Email { get; set; }
        //public String WorkPhone { get; set; }
        //public String AlternativeNumber { get; set; }
        //public String Street { get; set; }
        //public String City { get; set; }
        //public String State { get; set; }
        //public String ZipCode { get; set; }
        //public String Country { get; set; }
        //public String Industry { get; set; }
        //public Decimal Revenue { get; set; }
        //public Int32 CompanyEmployeeSize { get; set; }
        //public String ProfileLink { get; set; }
        //public String CompanyLink { get; set; }
        //public String RevenueLink { get; set; }
        //public String EmailFormat { get; set; }
        //public String AdressLink { get; set; }
        //public String Tenurity { get; set; }
        //public String Code { get; set; }
        //public String Link { get; set; }
        //public String Md5 { get; set; }
        //public Int32 OwnerId { get; set; }
    }
}