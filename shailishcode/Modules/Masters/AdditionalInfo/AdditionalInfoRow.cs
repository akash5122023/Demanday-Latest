
namespace AdvanceCRM.Masters
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Masters"), TableName("[dbo].[AdditionalInfo]")]
    [DisplayName("Additional Info"), InstanceName("Additional Info")]
    [ReadPermission("Masters:Read")]
    [ModifyPermission("Masters:Modify")]
    [LookupScript("Masters.AdditionalInfo", Permission = "?")]
    public sealed class AdditionalInfoRow : Row<AdditionalInfoRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Additional Info"), Size(2000), NotNull, QuickSearch,NameProperty]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }

        [DisplayName("Type"), NotNull, LookupInclude]
        public AddInfoTypeMaster? Type
        {
            get { return (AddInfoTypeMaster?)Fields.Type[this]; }
            set { Fields.Type[this] = (Int32?)value; }
        }

       

        public AdditionalInfoRow()
            : base(Fields)
        {
        }
        public AdditionalInfoRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField AdditionalInfo;
            public Int32Field Type;
        }
    }
}
