using AdvanceCRM.Administration;
using FluentMigrator;
using Serenity.Data;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250205183700)]
    public class DefaultDB_20250205_183700_AddRepresentativeInCashbook : Migration
    {
        public override void Up()
        {
            Alter.Table("Cashbook")
                .AddColumn("RepresentativeId").AsInt32().Nullable()
                .ForeignKey("FK_CashbookRepresentacccctive_UserId", "dbo", "Users", "UserId");


        }

        public override void Down()
        {

        }
    }
}
