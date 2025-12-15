using System;
using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180603095300)]
    public class DefaultDB_20180603_095300_AddUsersBranch : Migration
    {
        public override void Up()
        {
            Alter.Table("CompanyDetails")
                .AddColumn("StateId").AsInt32().Nullable().ForeignKey("FK_CompanyState_StateId", "dbo", "State", "Id")
                ;

            Alter.Table("Users")
                .AddColumn("BranchId").AsInt32().Nullable().ForeignKey("FK_Users_BranchId", "dbo", "Branch", "Id")
                ;
        }

        public override void Down()
        {

        }
    }
}