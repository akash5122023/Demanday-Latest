
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[IndiaMartDetails]")]
    [DisplayName("India Mart Details"), InstanceName("India Mart Details")]
    [ReadPermission("IndiaMART:Inbox")]
    [ModifyPermission("IndiaMART:Inbox")]
    public sealed class IndiaMartDetailsRow : Row<IndiaMartDetailsRow.RowFields>, IIdRow, INameRow
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
        [DisplayName("Rn")]
        public Int32? Rn
        {
            get { return Fields.Rn[this]; }
            set { Fields.Rn[this] = value; }
        }

        [DisplayName("Query Id"), Size(100)]
        public String QueryId
        {
            get { return Fields.QueryId[this]; }
            set { Fields.QueryId[this] = value; }
        }

        [DisplayName("Query Type"), Size(10)]
        public String QueryType
        {
            get { return Fields.QueryType[this]; }
            set { Fields.QueryType[this] = value; }
        }

        [DisplayName("Sender Name"), Size(200), QuickSearch,NameProperty]
        public String SenderName
        {
            get { return Fields.SenderName[this]; }
            set { Fields.SenderName[this] = value; }
        }

        [DisplayName("Sender Email"), Size(200)]
        public String SenderEmail
        {
            get { return Fields.SenderEmail[this]; }
            set { Fields.SenderEmail[this] = value; }
        }
      

        [DisplayName("Requirement"), Size(500), QuickSearch]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Date"), NotNull]
        [DateEditor, SortOrder(1, descending: true)]
        public DateTime? DateRe
        {
            get { return Fields.DateRe[this]; }
            set { Fields.DateRe[this] = value; }
        }

        [DisplayName("Date R"), Size(100)]
        public String DateR
        {
            get { return Fields.DateR[this]; }
            set { Fields.DateR[this] = value; }
        }

        [DisplayName("Date Time"), Size(100)]
        [DateTimeEditor]
        public DateTime? DateTimeRe
        {
            get { return Fields.DateTimeRe[this]; }
            set { Fields.DateTimeRe[this] = value; }
        }

        [DisplayName("Company Name"), Size(500), QuickSearch]
        public String GlUserCompanyName
        {
            get { return Fields.GlUserCompanyName[this]; }
            set { Fields.GlUserCompanyName[this] = value; }
        }

        [DisplayName("Read Status")]
        public Int32? ReadStatus
        {
            get { return Fields.ReadStatus[this]; }
            set { Fields.ReadStatus[this] = value; }
        }

        [DisplayName("Sender Gl User Id"), Column("SenderGLUserId"), Size(100)]
        public String SenderGlUserId
        {
            get { return Fields.SenderGlUserId[this]; }
            set { Fields.SenderGlUserId[this] = value; }
        }

        [DisplayName("Mobile"), Size(1024), QuickSearch]
        public String Mob
        {
            get { return Fields.Mob[this]; }
            set { Fields.Mob[this] = value; }
        }

        [DisplayName("Country"), Size(500)]
        public String CountryFlag
        {
            get { return Fields.CountryFlag[this]; }
            set { Fields.CountryFlag[this] = value; }
        }

        [DisplayName("Query Mod Id"), Size(100)]
        public String QueryModId
        {
            get { return Fields.QueryModId[this]; }
            set { Fields.QueryModId[this] = value; }
        }

        [DisplayName("Log Time"), Size(100)]
        public String LogTime
        {
            get { return Fields.LogTime[this]; }
            set { Fields.LogTime[this] = value; }
        }

        [DisplayName("Query Mod Ref Id"), Size(100)]
        public String QueryModRefId
        {
            get { return Fields.QueryModRefId[this]; }
            set { Fields.QueryModRefId[this] = value; }
        }

        [DisplayName("Dir Query Modref Type"), Column("DIRQueryModrefType")]
        public Int16? DirQueryModrefType
        {
            get { return Fields.DirQueryModrefType[this]; }
            set { Fields.DirQueryModrefType[this] = value; }
        }

        [DisplayName("Org Sender Gl User Id"), Column("ORGSenderGLUserId"), Size(100)]
        public String OrgSenderGlUserId
        {
            get { return Fields.OrgSenderGlUserId[this]; }
            set { Fields.OrgSenderGlUserId[this] = value; }
        }

        [DisplayName("Message"), Size(1000)]
        public String EnqMessage
        {
            get { return Fields.EnqMessage[this]; }
            set { Fields.EnqMessage[this] = value; }
        }

        [DisplayName("Address"), Size(1000)]
        public String EnqAddress
        {
            get { return Fields.EnqAddress[this]; }
            set { Fields.EnqAddress[this] = value; }
        }

        [DisplayName("Call Duration"), Size(100)]
        public String EnqCallDuration
        {
            get { return Fields.EnqCallDuration[this]; }
            set { Fields.EnqCallDuration[this] = value; }
        }

        [DisplayName("Receiver"), Size(100)]
        public String EnqReceiverMob
        {
            get { return Fields.EnqReceiverMob[this]; }
            set { Fields.EnqReceiverMob[this] = value; }
        }

        [DisplayName("City"), Size(100)]
        public String EnqCity
        {
            get { return Fields.EnqCity[this]; }
            set { Fields.EnqCity[this] = value; }
        }

        [DisplayName("State"), Size(100)]
        public String EnqState
        {
            get { return Fields.EnqState[this]; }
            set { Fields.EnqState[this] = value; }
        }

        [DisplayName("Product Name"), Size(500)]
        public String ProductName
        {
            get { return Fields.ProductName[this]; }
            set { Fields.ProductName[this] = value; }
        }

        [DisplayName("Country Iso"), Column("CountryISO"), Size(100)]
        public String CountryIso
        {
            get { return Fields.CountryIso[this]; }
            set { Fields.CountryIso[this] = value; }
        }

        [DisplayName("Email Alternate"), Size(100)]
        public String EmailAlt
        {
            get { return Fields.EmailAlt[this]; }
            set { Fields.EmailAlt[this] = value; }
        }

        [DisplayName("Mobile Alternate"), Size(100)]
        public String MobileAlt
        {
            get { return Fields.MobileAlt[this]; }
            set { Fields.MobileAlt[this] = value; }
        }

        [DisplayName("Phone"), Size(100)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Phone Alternate"), Size(100)]
        public String PhoneAlt
        {
            get { return Fields.PhoneAlt[this]; }
            set { Fields.PhoneAlt[this] = value; }
        }

        [DisplayName("Immember Since"), Size(100)]
        public String ImmemberSince
        {
            get { return Fields.ImmemberSince[this]; }
            set { Fields.ImmemberSince[this] = value; }
        }

        [DisplayName("Total Cnt")]
        public Int32? TotalCnt
        {
            get { return Fields.TotalCnt[this]; }
            set { Fields.TotalCnt[this] = value; }
        }

        [DisplayName("Moved")]
        public Boolean? IsMoved
        {
            get { return Fields.IsMoved[this]; }
            set { Fields.IsMoved[this] = value; }
        }

        [DisplayName("Source"), NotNull]
        public Masters.IndiaMARTSource? Source
        {
            get { return (Masters.IndiaMARTSource?)Fields.Source[this]; }
            set { Fields.Source[this] = (Int32?)value; }
        }

      

        public IndiaMartDetailsRow()
            : base(Fields)
        {
        }
        public IndiaMartDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field Rn;
            public StringField QueryId;
            public StringField QueryType;
            public StringField SenderName;
            public StringField SenderEmail;
            public StringField Subject;
            public DateTimeField DateRe;
            public StringField DateR;
            public DateTimeField DateTimeRe;
            public StringField GlUserCompanyName;
            public Int32Field ReadStatus;
            public StringField SenderGlUserId;
            public StringField Mob;
            public StringField Feedback;
            public StringField CountryFlag;
            public StringField QueryModId;
            public StringField LogTime;
            public StringField QueryModRefId;
            public Int16Field DirQueryModrefType;
            public StringField OrgSenderGlUserId;
            public StringField EnqMessage;
            public StringField EnqAddress;
            public StringField EnqCallDuration;
            public StringField EnqReceiverMob;
            public StringField EnqCity;
            public StringField EnqState;
            public StringField ProductName;
            public StringField CountryIso;
            public StringField EmailAlt;
            public StringField MobileAlt;
            public StringField Phone;
            public StringField PhoneAlt;
            public StringField ImmemberSince;
            public Int32Field TotalCnt;
            public BooleanField IsMoved;
            public Int32Field Source;
            
        }
    }
}
