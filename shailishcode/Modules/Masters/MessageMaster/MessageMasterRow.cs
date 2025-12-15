
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;


    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[MessageMaster]")]
    [DisplayName("Terms Master"), InstanceName("Message Master")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.MessageMaster", Permission = "?")]
    public sealed class MessageMasterRow : Row<MessageMasterRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Message"), NotNull, QuickSearch,NameProperty]
        public String Message
        {
            get { return Fields.Message[this]; }
            set { Fields.Message[this] = value; }
        }

       

        public MessageMasterRow()
            : base(Fields)
        {
        }
        

        public MessageMasterRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Message;
        }
    }
}
