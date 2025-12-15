
namespace AdvanceCRM.Administration
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Administration"), TableName("[dbo].[AppointmentSMSLog]")]
    [DisplayName("Appointment Sms Log"), InstanceName("Appointment Sms Log")]
    [ReadPermission(PermissionKeys.Logs)]

    public sealed class AppointmentSmsLogRow : Row<AppointmentSmsLogRow.RowFields>,
    IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => Fields.Id[this];
            set => Fields.Id[this] = value;
        }

        [DisplayName("Date"), NotNull]
        public DateTime? Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
        }

        [DisplayName("Log"), Size(2000), QuickSearch,NameProperty]
        public String Log
        {
            get { return Fields.Log[this]; }
            set { Fields.Log[this] = value; }
        }

     

        

        public AppointmentSmsLogRow()
            : base(Fields)
        {
        }
        public AppointmentSmsLogRow(RowFields fields)
           : base(fields)
        {
        }
        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public DateTimeField Date;
            public StringField Log;
        }
    }
}
