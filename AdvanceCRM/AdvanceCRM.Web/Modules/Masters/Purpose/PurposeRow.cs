
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[Purpose]")]
    [DisplayName("Purpose"), InstanceName("Purpose")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.Purpose", Permission = "?")]
    public sealed class PurposeRow : Row<PurposeRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Purpose"), Size(2000), NotNull, QuickSearch,NameProperty]
        public String Purpose
        {
            get { return Fields.Purpose[this]; }
            set { Fields.Purpose[this] = value; }
        }

      

        public PurposeRow()
            : base(Fields)
        {
        }
        
        public PurposeRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Purpose;
        }
    }
}
