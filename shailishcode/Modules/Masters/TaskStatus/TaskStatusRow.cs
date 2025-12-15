
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[TaskStatus]")]
    [DisplayName("Task Status"), InstanceName("Task Status")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.TaskStatus", Permission = "?")]
    
    public sealed class TaskStatusRow : Row<TaskStatusRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty ]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Status"), Size(200), NotNull, QuickSearch, LookupInclude,NameProperty]
            public String Status
        {
            get { return Fields.Status[this]; }
            set { Fields.Status[this] = value; }
        }

      

        public TaskStatusRow()
            : base(Fields)
        {
        }
        
        public TaskStatusRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Status;
        }
    }
}
