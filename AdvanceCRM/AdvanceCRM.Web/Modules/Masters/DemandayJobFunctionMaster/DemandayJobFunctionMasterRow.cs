using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Masters
{
    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[DemandayJobFunctionMaster]")]
    [DisplayName("Job Function Master"), InstanceName("Job Function Master")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.DemandayJobFunctionMaster", Permission = "?")]
    public sealed class DemandayJobFunctionMasterRow : Row<DemandayJobFunctionMasterRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Job Function"), Size(200), QuickSearch, NameProperty]
        public String Name
        {
            get => fields.Name[this];
            set => fields.Name[this] = value;
        }

        public DemandayJobFunctionMasterRow()
            : base()
        {
        }

        public DemandayJobFunctionMasterRow(RowFields fields)
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
