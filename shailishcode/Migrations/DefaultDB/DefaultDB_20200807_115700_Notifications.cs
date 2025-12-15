using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200807115800)]
    public class DefaultDB_20200807_115700_Notifications : AutoReversingMigration
    {

        public override void Up()
        {
            Create.Table("Notifications").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Module").AsInt32().NotNullable()
                .WithColumn("Text").AsString(int.MaxValue).NotNullable()
                .WithColumn("Url").AsString(int.MaxValue).NotNullable()
                .WithColumn("InsertUserId").AsInt32().NotNullable().ForeignKey("FK_NotIUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("InsertDate").AsDateTime().NotNullable();

            Create.Table("NotificationUsers")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("IsChecked").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("FK_NotificationUsers_UserId", "dbo", "Users", "UserId")
                .WithColumn("NotificationsId").AsInt32().NotNullable().ForeignKey("FK_NotificationsAssigned_NotificationsId", "dbo", "Notifications", "Id")
                ;
        }
    }
}