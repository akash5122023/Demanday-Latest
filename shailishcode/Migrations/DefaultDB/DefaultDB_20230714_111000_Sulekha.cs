using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20230714111000)]
    public class DefaultDB_20230714_111000_Sulekha : Migration
    {
        public override void Up()
        {
            if (Schema.Table("SulekhaDetails").Column("PostUrl").Exists())
            {
                Delete.Column("PostUrl").FromTable("SulekhaDetails");
            }
            Alter.Table("Sulekha")
            .AddColumn("PostUrl").AsString(int.MaxValue).Nullable();
        }
        public override void Down()
        {
        }
    }
}