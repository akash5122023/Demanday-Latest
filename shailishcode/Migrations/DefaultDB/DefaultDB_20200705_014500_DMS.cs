using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20200705014500)]
    public class DefaultDB_20200705_014500_DMS : AutoReversingMigration
    {

        public override void Up()
        {
            Create.Table("DMS").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("ParentId").AsInt32().Nullable()
                .WithColumn("Title").AsString(100).NotNullable()
                .WithColumn("Files").AsString(1000).Nullable()
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_DMSO_UserId", "dbo", "Users", "UserId")
                .WithColumn("LastUpdatedId").AsInt32().NotNullable().ForeignKey("FK_DMSU_UserId", "dbo", "Users", "UserId")
                .WithColumn("CreateDate").AsDateTime().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable();
        }
    }
}