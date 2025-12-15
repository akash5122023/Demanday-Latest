using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250510050600)]
    public class DefaultDB_20250510_050600_Itinerary : AutoReversingMigration
    {

        public override void Up()
        {

           


            Create.Table("Itinerary").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Headline").AsString(50).Nullable()
                .WithColumn("Date").AsDateTime().Nullable()
                .WithColumn("DaysId").AsInt32().Nullable()
                .WithColumn("From").AsString(100).Nullable()
                .WithColumn("To").AsString(100).Nullable()
                .WithColumn("Adults").AsString(50).Nullable()
                .WithColumn("Childrens").AsString(50).Nullable()
                .WithColumn("Destination").AsString(100).Nullable()
                .WithColumn("Nights").AsString(50).Nullable()
                .WithColumn("HotelName").AsString(100).Nullable()
                .WithColumn("MealPlan").AsString(100).Nullable()
                .WithColumn("Amount").AsDecimal().Nullable()
                .WithColumn("TermsAndConditions").AsString(1000).Nullable()
                ;
            Create.Table("ItineraryTerm").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
             .WithColumn("DaysId").AsInt32().Nullable().ForeignKey("FK_IDaysMastes_DaysId", "dbo", "Days", "Id")
             .WithColumn("ItineraryId").AsInt32().Nullable().ForeignKey("FK_IItinerary_ItineraryId", "dbo", "Itinerary", "Id")
             ;

            // Add FK from Itinerary.DaysId → ItineraryTerm.Id AFTER both tables exist
            Create.ForeignKey("FK_IDaysTerm_DaysId")
                .FromTable("Itinerary").ForeignColumn("DaysId")
                .ToTable("ItineraryTerm").PrimaryColumn("Id");
        }
    }
}