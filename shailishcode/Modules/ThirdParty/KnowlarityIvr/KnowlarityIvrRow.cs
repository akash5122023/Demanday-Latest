
namespace AdvanceCRM.ThirdParty
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("ThirdParty"), TableName("[dbo].[KnowlarityIVR]")]
    [DisplayName("Knowlarity Ivr"), InstanceName("Knowlarity Ivr")]
    [ReadPermission("KnowlarityIVR:Inbox")]
    [ModifyPermission("KnowlarityIVR:Inbox")]
    public sealed class KnowlarityIvrRow : Row<KnowlarityIvrRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), Size(50), QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Mobile"), Size(100)]
        public String Mobile
        {
            get { return Fields.Mobile[this]; }
            set { Fields.Mobile[this] = value; }
        }

        [DisplayName("Emp Mobile"), Size(2048)]
        public String EmpMobile
        {
            get { return Fields.EmpMobile[this]; }
            set { Fields.EmpMobile[this] = value; }
        }

        [DisplayName("Ivr No"), Column("IVRNo"), Size(2048)]
        public String IvrNo
        {
            get { return Fields.IvrNo[this]; }
            set { Fields.IvrNo[this] = value; }
        }

        [DisplayName("Recording"), Size(2000)]
        public String Recording
        {
            get { return Fields.Recording[this]; }
            set { Fields.Recording[this] = value; }
        }

        [DisplayName("Date"), Size(200)]
        public String Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
        }

        [DisplayName("Duration"), Size(10)]
        public String Duration
        {
            get { return Fields.Duration[this]; }
            set { Fields.Duration[this] = value; }
        }

      

        public KnowlarityIvrRow()
            : base(Fields)
        {
        }
        
        public KnowlarityIvrRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField Mobile;
            public StringField EmpMobile;
            public StringField IvrNo;
            public StringField Recording;
            public StringField Date;
            public StringField Duration;
        }
    }
}
