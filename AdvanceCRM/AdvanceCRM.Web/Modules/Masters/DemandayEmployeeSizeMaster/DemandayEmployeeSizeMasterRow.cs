using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Masters
{
    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[DemandayEmployeeSizeMaster]")]
    [DisplayName("Employee Size Master"), InstanceName("Employee Size Master")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.DemandayEmployeeSizeMaster", Permission = "?")]
    public sealed class DemandayEmployeeSizeMasterRow : Row<DemandayEmployeeSizeMasterRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Employee Size"), Size(200), QuickSearch, NameProperty]
        public String Name
        {
            get => fields.Name[this];
            set => fields.Name[this] = value;
        }

        public DemandayEmployeeSizeMasterRow()
            : base()
        {
        }

        public DemandayEmployeeSizeMasterRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
        }
    }
}
