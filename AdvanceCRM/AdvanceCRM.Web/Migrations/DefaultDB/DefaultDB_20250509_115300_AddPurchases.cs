using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20250509115300)]
    public class DefaultDB_20250509_115300_AddPurchase : AutoReversingMigration
    {

        public override void Up()
        {

            Alter.Table("PurchaseProducts")
                   .AddColumn("From").AsString(100).Nullable()
                   .AddColumn("To").AsString(100).Nullable()
                   .AddColumn("Date").AsDateTime().Nullable()
                   .AddColumn("Adults").AsString(50).Nullable()
                   .AddColumn("Childrens").AsString(50).Nullable()
                   .AddColumn("Destination").AsString(100).Nullable()
                  .AddColumn("Nights").AsString(50).Nullable()
                   .AddColumn("HotelName").AsString(100).Nullable()
                  .AddColumn("MealPlan").AsString(100).Nullable();

            Alter.Table("PurchaseOrderProducts")
                    .AddColumn("From").AsString(100).Nullable()
                    .AddColumn("To").AsString(100).Nullable()
                    .AddColumn("Date").AsDateTime().Nullable()
                    .AddColumn("Adults").AsString(50).Nullable()
                    .AddColumn("Childrens").AsString(50).Nullable()
                    .AddColumn("Destination").AsString(100).Nullable()
                   .AddColumn("Nights").AsString(50).Nullable()
                    .AddColumn("HotelName").AsString(100).Nullable()
                   .AddColumn("MealPlan").AsString(100).Nullable();

            Alter.Table("PurchaseReturnProducts")
                    .AddColumn("From").AsString(100).Nullable()
                    .AddColumn("To").AsString(100).Nullable()
                    .AddColumn("Date").AsDateTime().Nullable()
                    .AddColumn("Adults").AsString(50).Nullable()
                    .AddColumn("Childrens").AsString(50).Nullable()
                    .AddColumn("Destination").AsString(100).Nullable()
                   .AddColumn("Nights").AsString(50).Nullable()
                    .AddColumn("HotelName").AsString(100).Nullable()
                   .AddColumn("MealPlan").AsString(100).Nullable();

            Alter.Table("ChallanProducts")
                  .AddColumn("From").AsString(100).Nullable()
                  .AddColumn("To").AsString(100).Nullable()
                  .AddColumn("Date").AsDateTime().Nullable()
                  .AddColumn("Adults").AsString(50).Nullable()
                  .AddColumn("Childrens").AsString(50).Nullable()
                  .AddColumn("Destination").AsString(100).Nullable()
                 .AddColumn("Nights").AsString(50).Nullable()
                  .AddColumn("HotelName").AsString(100).Nullable()
                 .AddColumn("MealPlan").AsString(100).Nullable();

            Alter.Table("InvoiceProducts")
                  .AddColumn("From").AsString(100).Nullable()
                  .AddColumn("To").AsString(100).Nullable()
                  .AddColumn("Date").AsDateTime().Nullable()
                  .AddColumn("Adults").AsString(50).Nullable()
                  .AddColumn("Childrens").AsString(50).Nullable()
                  .AddColumn("Destination").AsString(100).Nullable()
                 .AddColumn("Nights").AsString(50).Nullable()
                  .AddColumn("HotelName").AsString(100).Nullable()
                 .AddColumn("MealPlan").AsString(100).Nullable();

            Alter.Table("SalesProducts")
                  .AddColumn("From").AsString(100).Nullable()
                  .AddColumn("To").AsString(100).Nullable()
                  .AddColumn("Date").AsDateTime().Nullable()
                  .AddColumn("Adults").AsString(50).Nullable()
                  .AddColumn("Childrens").AsString(50).Nullable()
                  .AddColumn("Destination").AsString(100).Nullable()
                 .AddColumn("Nights").AsString(50).Nullable()
                  .AddColumn("HotelName").AsString(100).Nullable()
                 .AddColumn("MealPlan").AsString(100).Nullable();

            Alter.Table("SalesReturnProducts")
                  .AddColumn("From").AsString(100).Nullable()
                  .AddColumn("To").AsString(100).Nullable()
                  .AddColumn("Date").AsDateTime().Nullable()
                  .AddColumn("Adults").AsString(50).Nullable()
                  .AddColumn("Childrens").AsString(50).Nullable()
                  .AddColumn("Destination").AsString(100).Nullable()
                 .AddColumn("Nights").AsString(50).Nullable()
                  .AddColumn("HotelName").AsString(100).Nullable()
                 .AddColumn("MealPlan").AsString(100).Nullable();


        }
    }
}