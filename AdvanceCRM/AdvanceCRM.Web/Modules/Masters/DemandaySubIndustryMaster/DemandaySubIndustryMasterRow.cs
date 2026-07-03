using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Masters
{
    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[DemandaySubIndustryMaster]")]
    [DisplayName("Sub Industry Master"), InstanceName("Sub Industry Master")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.DemandaySubIndustryMaster", Permission = "?")]
    public sealed class DemandaySubIndustryMasterRow : Row<DemandaySubIndustryMasterRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Sub Industry"), Size(200), QuickSearch, NameProperty]
        public String Name
        {
            get => fields.Name[this];
            set => fields.Name[this] = value;
        }

        public DemandaySubIndustryMasterRow()
            : base()
        {
        }

        public DemandaySubIndustryMasterRow(RowFields fields)
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
