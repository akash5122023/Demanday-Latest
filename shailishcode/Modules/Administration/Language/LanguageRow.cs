
namespace AdvanceCRM.Administration
{
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), DisplayName("Languages"), InstanceName("Language"), TwoLevelCached]
    [ReadPermission(PermissionKeys.Translation)]
    [ModifyPermission(PermissionKeys.Translation)]
    public sealed class LanguageRow : Row<LanguageRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Language Id"), Size(10), NotNull, QuickSearch]
        public String LanguageId
        {
            get { return Fields.LanguageId[this]; }
            set { Fields.LanguageId[this] = value; }
        }

        [DisplayName("Language Name"), Size(50), NotNull, QuickSearch,NameProperty]
        public String LanguageName
        {
            get { return Fields.LanguageName[this]; }
            set { Fields.LanguageName[this] = value; }
        }

       

        public LanguageRow()
            : base(Fields)
        {
        }
        
        public LanguageRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField LanguageId;
            public StringField LanguageName;

            public RowFields()
                : base("Languages")
            {
                LocalTextPrefix = "Administration.Language";
            }
        }
    }
}