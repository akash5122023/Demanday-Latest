
using AdvanceCRM.Administration;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201229143800)]
    public class DefaultDB_20201229_143800_KnowlarityAgents : Migration
    {

        public override void Up()
        {

            Create.Table("KnowlarityAgents").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("KnowlarityId").AsInt32().NotNullable().ForeignKey("FK_IVRKnowlarityAgents_KnowlarityAgentsId", "dbo", "IVRConfiguration", "Id")
                .WithColumn("Name").AsString(2000).NotNullable()
                .WithColumn("Number").AsString(2000).NotNullable().Unique()
            ;
        }

        public override void Down()
        {
        }
    }
}