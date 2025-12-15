
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
    [Migration(20230401130600)]
    public class DefaultDB_20230401_130600_Template : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("OtherTemplates").Row(new
            {
                TicketSMSTemplate = "Dear #customername thankyou for showing interest in us",
                FeedbackSMSTemplate = "Dear #customername thankyou for showing interest in us",
                TaskSMSTemplate = "Dear #customername thankyou for showing interest in us",
                OTPSMSTemplate = "Dear #customername thankyou for showing interest in us"
            }); 

          
            
        }

        public override void Down()
        {
        }
    }
}