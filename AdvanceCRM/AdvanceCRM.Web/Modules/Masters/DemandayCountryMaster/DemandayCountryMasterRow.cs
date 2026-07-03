using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Masters
{
    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[DemandayCountryMaster]")]
    [DisplayName("Country Master"), InstanceName("Country Master")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.DemandayCountryMaster", Permission = "?")]
    public sealed class DemandayCountryMasterRow : Row<DemandayCountryMasterRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Country"), Size(200), QuickSearch, NameProperty]
        public String Name
        {
            get => fields.Name[this];
            set => fields.Name[this] = value;
        }

        public DemandayCountryMasterRow()
            : base()
        {
        }

        public DemandayCountryMasterRow(RowFields fields)
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
