using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Masters
{
    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[DemandayMasterAccount]")]
    [DisplayName("Master Account"), InstanceName("Master Account")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.DemandayMasterAccount", Permission = "?")]
    public sealed class DemandayMasterAccountRow : Row<DemandayMasterAccountRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Account Number"), Size(15), QuickSearch, NameProperty]
        public String AccountNumber
        {
            get => fields.AccountNumber[this];
            set => fields.AccountNumber[this] = value;
        }

        public DemandayMasterAccountRow()
            : base()
        {
        }

        public DemandayMasterAccountRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField AccountNumber;
        }
    }
}
