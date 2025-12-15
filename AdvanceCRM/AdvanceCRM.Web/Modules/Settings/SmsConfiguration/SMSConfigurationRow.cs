
namespace AdvanceCRM.Settings
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Settings"), TableName("[dbo].[SMSConfiguration]")]
    [DisplayName("SMS Configuration"), InstanceName("SMS Configuration")]
    [ReadPermission("Settings:SMS")]
    [ModifyPermission("Settings:SMS")]

    public sealed class SMSConfigurationRow : Row<SMSConfigurationRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Username"), Size(50), NotNull, QuickSearch,NameProperty]
        public String Username
        {
            get { return Fields.Username[this]; }
            set { Fields.Username[this] = value; }
        }

        [DisplayName("Password"), Size(50), NotNull, PasswordEditor]
        public String Password
        {
            get { return Fields.Password[this]; }
            set { Fields.Password[this] = value; }
        }

        [DisplayName("Sender Id"), Size(50), NotNull]
        public String SenderId
        {
            get { return Fields.SenderId[this]; }
            set { Fields.SenderId[this] = value; }
        }

        [DisplayName("Key(Optional)"), Size(50)]
        public String Key
        {
            get { return Fields.Key[this]; }
            set { Fields.Key[this] = value; }
        }

        [DisplayName("API"), Column("API"), Size(2048), NotNull]
        public String API
        {
            get { return Fields.API[this]; }
            set { Fields.API[this] = value; }
        }

        [DisplayName("Bulk API"), Column("BulkAPI"), Size(2048), NotNull]
        public String BulkAPI
        {
            get { return Fields.BulkAPI[this]; }
            set { Fields.BulkAPI[this] = value; }
        }

        [DisplayName("Schedule API"), Column("ScheduleAPI"), Size(2048), NotNull]
        public String ScheduleAPI
        {
            get { return Fields.ScheduleAPI[this]; }
            set { Fields.ScheduleAPI[this] = value; }
        }

        [DisplayName("Success Response"), Column("SuccessResponse"), Size(2048), NotNull]
        public String SuccessResponse
        {
            get { return Fields.SuccessResponse[this]; }
            set { Fields.SuccessResponse[this] = value; }
        }

       

        public SMSConfigurationRow()
            : base(Fields)
        {
        }
        

        public SMSConfigurationRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Username;
            public StringField Password;
            public StringField SenderId;
            public StringField Key;
            public StringField API;
            public StringField BulkAPI;
            public StringField ScheduleAPI;
            public StringField SuccessResponse;
        }
    }
}
