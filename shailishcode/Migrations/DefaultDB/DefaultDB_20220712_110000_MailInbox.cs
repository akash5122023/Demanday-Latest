using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20220712110000)]
    public class DefaultDB_20220712_110000_MailInbox : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("MailInbox")
                .AddColumn("SHost").AsString(200).Nullable()
                .AddColumn("SPort").AsInt32().Nullable()
                .AddColumn("SSSL").AsBoolean().Nullable()
                .AddColumn("SEmailId").AsString(200).Nullable()
                .AddColumn("SEmailPassword").AsString(200).Nullable();

            Alter.Table("Ticket")
                .AddColumn("AdditionalDetails").AsString(500).Nullable();

            Alter.Table("CMS")
                .AddColumn("TicketNo").AsInt32().Nullable();
                

           
        }
    }
}