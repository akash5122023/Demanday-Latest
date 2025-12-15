using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200206111600)]
    public class DefaultDB_20200206_111600_AppointmentsFeedback : AutoReversingMigration
    {

        public override void Up()
        {
            Alter.Table("EnquiryAppointments")
                .AddColumn("MinutesOfMeeting").AsString(1024).Nullable()
                .AddColumn("Reason").AsString(1024).Nullable()
                ;
            Alter.Table("InvoiceAppointments")
                .AddColumn("MinutesOfMeeting").AsString(1024).Nullable()
                .AddColumn("Reason").AsString(1024).Nullable()
                ;
            Alter.Table("QuotationAppointments")
                .AddColumn("MinutesOfMeeting").AsString(1024).Nullable()
                .AddColumn("Reason").AsString(1024).Nullable()
                ;
            Alter.Table("TeleCallingAppointments")
                .AddColumn("MinutesOfMeeting").AsString(1024).Nullable()
                .AddColumn("Reason").AsString(1024).Nullable()
                ;
        }
    }
}