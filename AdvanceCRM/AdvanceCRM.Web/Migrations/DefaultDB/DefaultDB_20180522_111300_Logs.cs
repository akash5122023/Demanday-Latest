using System;
using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180522111300)]
    public class DefaultDB_20180522_111300_DailyWishes : Migration
    {
        public override void Up()
        {

            Create.Table("DailyWishesLog")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Date").AsDate().NotNullable()
                .WithColumn("Log").AsString(2000).Nullable()
                ;

            this.CreateTableIfNotExists("OPTLog", table => table
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Phone").AsString(30).NotNullable()
                .WithColumn("OPT").AsDouble().NotNullable()
                .WithColumn("Validity").AsDateTime().NotNullable());

            Insert.IntoTable("DailyWishesLog").Row(new
            {
                Date = DateTime.Now.Date.AddDays(-1),

            });

            this.CreateTableWithId64("AuditLog", "Id", s => s
                .WithColumn("VersionNo").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("ActionType").AsInt32().NotNullable()
                .WithColumn("ActionDate").AsDateTime().NotNullable()
                .WithColumn("TableName").AsString(100).NotNullable()
                .WithColumn("EntityId").AsInt64().NotNullable()
                .WithColumn("OldEntity").AsString(int.MaxValue).Nullable()
                .WithColumn("NewEntity").AsString(int.MaxValue).Nullable()
                .WithColumn("IpAddress").AsString(100).Nullable()
                .WithColumn("SessionId").AsString(100).Nullable()
                );

            Create.Table("AppointmentSMSLog")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Date").AsDate().NotNullable()
                .WithColumn("Log").AsString(2000).Nullable()
                ;

            Insert.IntoTable("AppointmentSMSLog").Row(new
            {
                Date = DateTime.Now.Date.AddDays(-1),

            });

            Create.Table("LogInOutLog")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Date").AsDateTime().NotNullable()
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("FK_LogInOutLog_UserId", "dbo", "Users", "UserId")
            ;
        }

        public override void Down()
        {

        }
    }
}