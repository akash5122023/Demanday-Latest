
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[ComplaintType]")]
    [DisplayName("Complaint Type"), InstanceName("Complaint Type")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.ComplaintType", Permission = "?")]
    public sealed class ComplaintTypeRow : Row<ComplaintTypeRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Complaint Type"), Size(150), QuickSearch,NameProperty]
       public String ComplaintType
        {
            get { return Fields.ComplaintType[this]; }
            set { Fields.ComplaintType[this] = value; }
        }

       
        public ComplaintTypeRow()
            : base(Fields)
        {
        }
        
        public ComplaintTypeRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField ComplaintType;
        }
    }
}
