using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20230712111000)]
    public class DefaultDB_20230712_111000_Sulekha : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("SulekhaDetails")
            .AddColumn("PostUrl").AsString(2000).Nullable();



           
        }
    }
}