
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[MailChimp]")]
    [DisplayName("Mail Chimp Configuration"), InstanceName("Mail Chimp Configuration")]
    [ReadPermission("Settings:MailChimp")]
    [ModifyPermission("Settings:MailChimp")]
    public sealed class MailChimpConfigurationRow : Row<MailChimpConfigurationRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Api Key"), Column("APIKey"), Size(200), QuickSearch,NameProperty]
        public String ApiKey
        {
            get { return Fields.ApiKey[this]; }
            set { Fields.ApiKey[this] = value; }
        }

        [DisplayName("Company Name"), Size(200)]
        public String CompanyName
        {
            get { return Fields.CompanyName[this]; }
            set { Fields.CompanyName[this] = value; }
        }

        [DisplayName("Address"), Size(500), TextAreaEditor(Rows = 4)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("City"), Size(100)]
        public String City
        {
            get { return Fields.City[this]; }
            set { Fields.City[this] = value; }
        }

        [DisplayName("State"), Size(100)]
        public String State
        {
            get { return Fields.State[this]; }
            set { Fields.State[this] = value; }
        }

        [DisplayName("Zip"), Size(100)]
        public String Zip
        {
            get { return Fields.Zip[this]; }
            set { Fields.Zip[this] = value; }
        }

        [DisplayName("Country")]
        public Int32? Country
        {
            get { return Fields.Country[this]; }
            set { Fields.Country[this] = value; }
        }

        [DisplayName("Phone"), Size(100)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Reminder"), Size(500), TextAreaEditor(Rows = 4)]
        public String Reminder
        {
            get { return Fields.Reminder[this]; }
            set { Fields.Reminder[this] = value; }
        }

        [DisplayName("Contact From Email"), Size(50)]
        public String ContactFromEmail
        {
            get { return Fields.ContactFromEmail[this]; }
            set { Fields.ContactFromEmail[this] = value; }
        }

        [DisplayName("Contact From Name"), Size(50)]
        public String ContactFromName
        {
            get { return Fields.ContactFromName[this]; }
            set { Fields.ContactFromName[this] = value; }
        }

        [DisplayName("Contact Subject"), Size(50)]
        public String ContactSubject
        {
            get { return Fields.ContactSubject[this]; }
            set { Fields.ContactSubject[this] = value; }
        }

        [DisplayName("Enquiry From Email"), Size(50)]
        public String EnquiryFromEmail
        {
            get { return Fields.EnquiryFromEmail[this]; }
            set { Fields.EnquiryFromEmail[this] = value; }
        }

        [DisplayName("Enquiry From Name"), Size(50)]
        public String EnquiryFromName
        {
            get { return Fields.EnquiryFromName[this]; }
            set { Fields.EnquiryFromName[this] = value; }
        }

        [DisplayName("Enquiry Subject"), Size(50)]
        public String EnquirySubject
        {
            get { return Fields.EnquirySubject[this]; }
            set { Fields.EnquirySubject[this] = value; }
        }

        [DisplayName("Quotation From Email"), Size(50)]
        public String QuotationFromEmail
        {
            get { return Fields.QuotationFromEmail[this]; }
            set { Fields.QuotationFromEmail[this] = value; }
        }

        [DisplayName("Quotation From Name"), Size(50)]
        public String QuotationFromName
        {
            get { return Fields.QuotationFromName[this]; }
            set { Fields.QuotationFromName[this] = value; }
        }

        [DisplayName("Quotation Subject"), Size(50)]
        public String QuotationSubject
        {
            get { return Fields.QuotationSubject[this]; }
            set { Fields.QuotationSubject[this] = value; }
        }

        [DisplayName("Sale From Email"), Size(50)]
        public String SaleFromEmail
        {
            get { return Fields.SaleFromEmail[this]; }
            set { Fields.SaleFromEmail[this] = value; }
        }

        [DisplayName("Sale From Name"), Size(50)]
        public String SaleFromName
        {
            get { return Fields.SaleFromName[this]; }
            set { Fields.SaleFromName[this] = value; }
        }

        [DisplayName("Sale Subject"), Size(50)]
        public String SaleSubject
        {
            get { return Fields.SaleSubject[this]; }
            set { Fields.SaleSubject[this] = value; }
        }

      

        public MailChimpConfigurationRow()
            : base(Fields)
        {
        }
        
        public MailChimpConfigurationRow(RowFields fields )
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField ApiKey;
            public StringField CompanyName;
            public StringField Address;
            public StringField City;
            public StringField State;
            public StringField Zip;
            public Int32Field Country;
            public StringField Phone;
            public StringField Reminder;
            public StringField ContactFromEmail;
            public StringField ContactFromName;
            public StringField ContactSubject;
            public StringField EnquiryFromEmail;
            public StringField EnquiryFromName;
            public StringField EnquirySubject;
            public StringField QuotationFromEmail;
            public StringField QuotationFromName;
            public StringField QuotationSubject;
            public StringField SaleFromEmail;
            public StringField SaleFromName;
            public StringField SaleSubject;
        }
    }
}
