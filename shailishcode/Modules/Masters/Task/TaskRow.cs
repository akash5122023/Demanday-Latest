
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Task]")]
    [DisplayName("Task"), InstanceName("Task")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Task", Permission = "?")]
    public sealed class TaskRow : Row<TaskRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Task"), Size(200), NotNull, QuickSearch,NameProperty]
        public String Task
        {
            get { return Fields.Task[this]; }
            set { Fields.Task[this] = value; }
        }

    

        public TaskRow()
            : base(Fields)
        {
        }
        public TaskRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Task;
        }
    }
}
