
namespace AdvanceCRM.Common
{
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), DisplayName("User Preferences"), InstanceName("UserPreference"), TwoLevelCached]
    [ReadPermission("")]
    [ModifyPermission("")]
    public sealed class UserPreferenceRow : Row<UserPreferenceRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("ID"), Identity,IdProperty]
        public Int32? UserPreferenceId
        {
            get { return Fields.UserPreferenceId[this]; }
            set { Fields.UserPreferenceId[this] = value; }
        }

        [DisplayName("User ID")]
        public Int32? UserId
        {
            get { return Fields.UserId[this]; }
            set { Fields.UserId[this] = value; }
        }

        [DisplayName("PreferenceType"), Size(100), NotNull]
        public String PreferenceType
        {
            get { return Fields.PreferenceType[this]; }
            set { Fields.PreferenceType[this] = value; }
        }

        [DisplayName("Name"), Size(100), NotNull, QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Value")]
        public String Value
        {
            get { return Fields.Value[this]; }
            set { Fields.Value[this] = value; }
        }

     
        public UserPreferenceRow()
            : base(Fields)
        {
        }
        
        public UserPreferenceRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public readonly Int32Field UserPreferenceId;
            public readonly Int32Field UserId;
            public readonly StringField PreferenceType;
            public readonly StringField Name;
            public readonly StringField Value;

            public RowFields()
                : base("UserPreferences")
            {
                LocalTextPrefix = "Common.UserPreference";
            }
        }
    }
}