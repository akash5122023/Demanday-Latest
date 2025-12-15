




namespace AdvanceCRM.Common
{
    using AdvanceCRM.Administration;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), Module("Common"), TableName("Timeline")]
    [DisplayName("Timeline"), InstanceName("Timeline")]
    [ReadPermission("")] 
    [ModifyPermission("")]
    public sealed class TimelineRow : Row<TimelineRow.RowFields>, IIdRow, INameRow, IInsertLogRow
    {
        [DisplayName("Timeline Id"), Identity, Column("TimelineID"),IdProperty]
        public Int64? TimelineId
        {
            get { return Fields.TimelineId[this]; }
            set { Fields.TimelineId[this] = value; }
        }

        [DisplayName("Entity Type"), Size(100), NotNull, Updatable(false),NameProperty]
        public String EntityType
        {
            get { return Fields.EntityType[this]; }
            set { Fields.EntityType[this] = value; }
        }

        [DisplayName("Entity Id"), Column("EntityID"), Size(100), NotNull, Updatable(false)]
        public Int64? EntityId
        {
            get { return Fields.EntityId[this]; }
            set { Fields.EntityId[this] = value; }
        }

        [DisplayName("Text"), NotNull, QuickSearch]
        public String Text
        {
            get { return Fields.Text[this]; }
            set { Fields.Text[this] = value; }
        }

        [DisplayName("Type"), NotNull]
        public Int32? Type
        {
            get { return Fields.Type[this]; }
            set { Fields.Type[this] = value; }
        }

        [DisplayName("Insert User Id"), NotNull, Insertable(false), Updatable(false)]
        public Int32? InsertUserId
        {
            get { return Fields.InsertUserId[this]; }
            set { Fields.InsertUserId[this] = value; }
        }

        [DisplayName("Insert User"), NotMapped]
        public String InsertUserDisplayName
        {
            get { return Fields.InsertUserDisplayName[this]; }
            set { Fields.InsertUserDisplayName[this] = value; }
        }

        [DisplayName("Insert Date"), NotNull, Insertable(false), Updatable(false)]
        public DateTime? InsertDate
        {
            get { return Fields.InsertDate[this]; }
            set { Fields.InsertDate[this] = value; }
        }

        public Int32Field InsertUserIdField
        {
            get { return Fields.InsertUserId; }
        }

       
        //public IIdField InsertUserIdField
        //{
        //    get
        //    {
        //        return Fields.InsertUserId;
        //    }
        //}

        public DateTimeField InsertDateField
        {
            get
            {
                return Fields.InsertDate;
            }
        }
        Field IInsertLogRow.InsertUserIdField
        {
            get
            {
                return Fields.InsertUserId;
            }
        }
        //public static readonly RowFields Fields = new RowFields().Init();

        public TimelineRow()
            : base(Fields)
        {
        } public TimelineRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int64Field TimelineId;
            public StringField EntityType;
            public Int64Field EntityId;
            public StringField Text;
            public Int32Field Type;
            public Int32Field InsertUserId;
            public DateTimeField InsertDate;
            public StringField InsertUserDisplayName;
        }
    }
}