using System;
using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170807170500)]
    public class DefaultDB_20170807_170500_SalesFollowups : Migration
    {
        public override void Up()
        {
            Create.Table("SalesFollowups")
            .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
            .WithColumn("FollowupNote").AsString(200).NotNullable()
            .WithColumn("Details").AsString(200)
            .WithColumn("FollowupDate").AsDateTime().NotNullable()
            .WithColumn("Status").AsInt32().NotNullable()
            .WithColumn("SalesId").AsInt32().NotNullable().ForeignKey("FK_Followups_SalesId", "dbo", "Sales", "Id")
            .WithColumn("RepresentativeId").AsInt32().Nullable().ForeignKey("FK_SalesFollowups_UserId", "dbo", "Users", "UserId")
            ;
        }

        public override void Down()
        {

        }
    }
}