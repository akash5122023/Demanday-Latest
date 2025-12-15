using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20230703014505)]
    public class DefaultDB_20230703_014505_Sulekha : AutoReversingMigration
    {

        public override void Up()
        {
            
            Create.Table("SulekhaDetails").WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("UserName").AsString(255).Nullable()               
                .WithColumn("Mobile").AsString(50).Nullable()                
                .WithColumn("Email").AsString(50).Nullable()
                .WithColumn("City").AsString(255).Nullable()
                .WithColumn("Localities").AsString(5000).Nullable()
                .WithColumn("Comments").AsString(5000).Nullable()
                .WithColumn("Keywords").AsString(255).Nullable()
                .WithColumn("Feedback").AsString(5000).Nullable()                
                .WithColumn("IsMoved").AsBoolean().WithDefaultValue(false);

            Create.Table("Sulekha")
               .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
               .WithColumn("Username").AsString(20).Nullable()
               .WithColumn("Password").AsString(20).Nullable()
               ;

            Insert.IntoTable("Sulekha").Row(new
            {
                Username = "sulekha",
                Password = "sulekhaapi"
            });
        }
    }
}