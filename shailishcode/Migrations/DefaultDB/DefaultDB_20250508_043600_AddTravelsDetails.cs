using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250508043600)]
    public class DefaultDB_20250508_043600_AddTravelsDetails : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("CompanyDetails")
                  .AddColumn("Capacity").AsBoolean().Nullable()
                  .AddColumn("MRP").AsBoolean().Nullable()
                  .AddColumn("SellingPrice").AsBoolean().Nullable();

            Alter.Table("Products")
                   .AddColumn("From").AsString(100).Nullable()
                   .AddColumn("To").AsString(100).Nullable()
                   .AddColumn("Date").AsDateTime().Nullable()
                   .AddColumn("Adults").AsString(50).Nullable()
                   .AddColumn("Childrens").AsString(50).Nullable()
                   .AddColumn("Destination").AsString(100).Nullable()
                  .AddColumn("Nights").AsString(50).Nullable()
                   .AddColumn("HotelName").AsString(100).Nullable()
                  .AddColumn("MealPlan").AsString(100).Nullable();

            Alter.Table("QuotationProducts")
                   .AddColumn("FileAttachments").AsString(500).Nullable()
            .AddColumn("From").AsString(100).Nullable()
            .AddColumn("To").AsString(100).Nullable()
            .AddColumn("Date").AsDateTime().Nullable()
            .AddColumn("Adults").AsString(50).Nullable()
            .AddColumn("Childrens").AsString(50).Nullable()
            .AddColumn("Destination").AsString(100).Nullable()
            .AddColumn("Nights").AsString(50).Nullable()
            .AddColumn("HotelName").AsString(100).Nullable()
            .AddColumn("MealPlan").AsString(100).Nullable();

            Alter.Table("EnquiryProducts")
            .AddColumn("FileAttachments").AsString(500).Nullable()
            .AddColumn("From").AsString(100).Nullable()
            .AddColumn("To").AsString(100).Nullable()
            .AddColumn("Date").AsDateTime().Nullable()
            .AddColumn("Adults").AsString(50).Nullable()
            .AddColumn("Childrens").AsString(50).Nullable()
            .AddColumn("Destination").AsString(100).Nullable()
            .AddColumn("Nights").AsString(50).Nullable()
            .AddColumn("HotelName").AsString(100).Nullable()
            .AddColumn("MealPlan").AsString(100).Nullable();

            Alter.Table("SubContacts")
                  .AddColumn("PassportNumber").AsString(50).Nullable()
                  .AddColumn("FirstName").AsString(100).Nullable()
                  .AddColumn("LastName").AsString(100).Nullable()
                  .AddColumn("ExpiryDate").AsDateTime().Nullable();



        }
    }
}