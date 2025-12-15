using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180911130500)]
    public class DefaultDB_20180911_130500_ExpenseManagement : Migration
    {
        public override void Up()
        {
            Create.Table("ExpenseManagement")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("RepresentativeId").AsInt32().NotNullable().ForeignKey("FK_ExpenseManagementRepresentative_UserId", "dbo", "Users", "UserId")
                .WithColumn("HeadId").AsInt32().NotNullable().ForeignKey("FK_ExpenseManagement_AccountingHeadsId", "dbo", "AccountingHeads", "Id")
                .WithColumn("Amount").AsDouble().NotNullable()
                .WithColumn("Attachment").AsString(1000).Nullable()
                .WithColumn("AdditionalInfo").AsString(1000).Nullable()
                .WithColumn("ApprovedBy").AsInt32().Nullable().ForeignKey("FK_ExpenseManagementApprovedBy_UserId", "dbo", "Users", "UserId")
                .WithColumn("Date").AsDateTime().NotNullable().WithDefaultValue(DateTime.Now)
                ;
        }

        public override void Down()
        {

        }
    }
}