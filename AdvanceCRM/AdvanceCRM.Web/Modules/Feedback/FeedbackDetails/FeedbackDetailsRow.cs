
namespace AdvanceCRM.Feedback
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Feedback"), TableName("[dbo].[FeedbackDetails]")]
    [DisplayName("Feedback Details"), InstanceName("Feedback Details")]
    [ReadPermission("Feedback:Read")]
    [InsertPermission("Feedback:Insert")]
    [UpdatePermission("Feedback:Update")]
    [DeletePermission("Feedback:Delete")]
    [LookupScript("Feedback.Feedback", Permission = "?")]
    public sealed class FeedbackDetailsRow : Row<FeedbackDetailsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Name"), Size(150), NotNull, QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Phone"), Size(30), NotNull]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Service")]
        public Int32? Service
        {
            get { return Fields.Service[this]; }
            set { Fields.Service[this] = value; }
        }

        [DisplayName("Refer"), NotNull]
        public Boolean? Refer
        {
            get { return Fields.Refer[this]; }
            set { Fields.Refer[this] = value; }
        }

        [DisplayName("Details"), Size(500), NotNull]
        public String Details
        {
            get { return Fields.Details[this]; }
            set { Fields.Details[this] = value; }
        }

      

        public FeedbackDetailsRow()
            : base(Fields)
        {
        }
        

        public FeedbackDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField Phone;
            public Int32Field Service;
            public BooleanField Refer;
            public StringField Details;
        }
    }
}
