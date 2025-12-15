using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20260701015000)]
    public class DefaultDB_20260701_015000_InwardProducts : Migration
    {
        public override void Up()
        {
            if (Schema.Table("Inward").Exists() && !Schema.Table("Inward").Column("OutwardId").Exists())
            {
                Alter.Table("Inward")
                    .AddColumn("OutwardId").AsInt32().Nullable();
            }

            if (Schema.Table("InwardProducts").Exists())
                return;

            if (!Schema.Table("OutwardProducts").Exists())
                return;

            Execute.Sql(@"
                SELECT *
                INTO InwardProducts
                FROM OutwardProducts
                WHERE 1 = 0;
            ");

            Execute.Sql(@"
                ALTER TABLE InwardProducts
                ADD CONSTRAINT PK_InwardProducts PRIMARY KEY (Id);
            ");

            if (Schema.Table("InwardProducts").Column("OutwardId").Exists())
            {
                Rename.Column("OutwardId").OnTable("InwardProducts").To("InwardId");
            }
        }

        public override void Down()
        {
            if (Schema.Table("InwardProducts").Exists())
                Delete.Table("InwardProducts");
        }
    }
}
