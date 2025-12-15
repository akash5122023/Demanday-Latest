
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[TradeIndiaDetails]")]
    [DisplayName("Trade India Details"), InstanceName("Trade India Details")]
    [ReadPermission("TradeIndia:Inbox")]
    [ModifyPermission("TradeIndia:Inbox")]
    public sealed class TradeIndiaDetailsRow : Row<TradeIndiaDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Feedback"), Size(500)]
        public String Feedback
        {
            get { return Fields.Feedback[this]; }
            set { Fields.Feedback[this] = value; }
        }

        [DisplayName("Rfi Id"), NotNull]
        public Int32? RfiId
        {
            get { return Fields.RfiId[this]; }
            set { Fields.RfiId[this] = value; }
        }

        [DisplayName("Source"), Size(200), QuickSearch,NameProperty]
        public String Source
        {
            get { return Fields.Source[this]; }
            set { Fields.Source[this] = value; }
        }

        [DisplayName("Product Source"), Size(200), NotNull]
        public String ProductSource
        {
            get { return Fields.ProductSource[this]; }
            set { Fields.ProductSource[this] = value; }
        }

        [DisplayName("Date Time")]
        [DateTimeEditor]
        public DateTime? GeneratedDateTime
        {
            get { return Fields.GeneratedDateTime[this]; }
            set { Fields.GeneratedDateTime[this] = value; }
        }

        [DisplayName("Inquiry Type"), Size(255)]
        public String InquiryType
        {
            get { return Fields.InquiryType[this]; }
            set { Fields.InquiryType[this] = value; }
        }

        [DisplayName("Subject"), Size(500)]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Product Name"), Size(500), QuickSearch]
        public String ProductName
        {
            get { return Fields.ProductName[this]; }
            set { Fields.ProductName[this] = value; }
        }

        [DisplayName("Quantity"), Size(500)]
        public String Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("Order Value Min")]
        public Int32? OrderValueMin
        {
            get { return Fields.OrderValueMin[this]; }
            set { Fields.OrderValueMin[this] = value; }
        }

        [DisplayName("Message")]
        public String Message
        {
            get { return Fields.Message[this]; }
            set { Fields.Message[this] = value; }
        }

        [DisplayName("Sender Co"), Size(500), QuickSearch]
        public String SenderCo
        {
            get { return Fields.SenderCo[this]; }
            set { Fields.SenderCo[this] = value; }
        }

        [DisplayName("Sender Name"), Size(500), QuickSearch]
        public String SenderName
        {
            get { return Fields.SenderName[this]; }
            set { Fields.SenderName[this] = value; }
        }

        [DisplayName("Sender Mobile"), Size(500)]
        public String SenderMobile
        {
            get { return Fields.SenderMobile[this]; }
            set { Fields.SenderMobile[this] = value; }
        }

        [DisplayName("Sender Email"), Size(500)]
        public String SenderEmail
        {
            get { return Fields.SenderEmail[this]; }
            set { Fields.SenderEmail[this] = value; }
        }

        [DisplayName("Sender Address")]
        public String SenderAddress
        {
            get { return Fields.SenderAddress[this]; }
            set { Fields.SenderAddress[this] = value; }
        }

        [DisplayName("Sender City"), Size(50)]
        public String SenderCity
        {
            get { return Fields.SenderCity[this]; }
            set { Fields.SenderCity[this] = value; }
        }

        [DisplayName("Sender State"), Size(50)]
        public String SenderState
        {
            get { return Fields.SenderState[this]; }
            set { Fields.SenderState[this] = value; }
        }

        [DisplayName("Sender Country"), Size(50)]
        public String SenderCountry
        {
            get { return Fields.SenderCountry[this]; }
            set { Fields.SenderCountry[this] = value; }
        }

        [DisplayName("Month Slot"), Size(500)]
        public String MonthSlot
        {
            get { return Fields.MonthSlot[this]; }
            set { Fields.MonthSlot[this] = value; }
        }

        [DisplayName("Landline Number"), Size(500)]
        public String LandlineNumber
        {
            get { return Fields.LandlineNumber[this]; }
            set { Fields.LandlineNumber[this] = value; }
        }

        [DisplayName("Pref Supp Location")]
        public String PrefSuppLocation
        {
            get { return Fields.PrefSuppLocation[this]; }
            set { Fields.PrefSuppLocation[this] = value; }
        }

        [DisplayName("Is Moved"), NotNull]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

       
        public TradeIndiaDetailsRow()
            : base(Fields)
        {
        }
       
        public TradeIndiaDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field RfiId;
            public StringField Source;
            public StringField ProductSource;
            public DateTimeField GeneratedDateTime;
            public StringField InquiryType;
            public StringField Subject;
            public StringField ProductName;
            public StringField Quantity;
            public Int32Field OrderValueMin;
            public StringField Message;
            public StringField SenderCo;
            public StringField SenderName;
            public StringField SenderMobile;
            public StringField SenderEmail;
            public StringField SenderAddress;
            public StringField SenderCity;
            public StringField SenderState;
            public StringField SenderCountry;
            public StringField MonthSlot;
            public StringField LandlineNumber;
            public StringField PrefSuppLocation;
            public StringField Feedback;
            public BooleanField IsMoved;
        }
    }
}
