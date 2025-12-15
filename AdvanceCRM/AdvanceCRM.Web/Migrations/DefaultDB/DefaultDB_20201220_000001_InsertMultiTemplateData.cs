
using AdvanceCRM.Administration;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Serenity.Data;
using Serenity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serenity.Extensions.DependencyInjection;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20201220000002)]
    public class DefaultDB_20201220_000001_InsertMultiTemplateData : Migration
    {

        public override void Up()
        {

            using (var connection = Dependency.Resolve<ISqlConnections>().NewFor<CompanyDetailsRow>())
            {
                var companyList = connection.List<CompanyDetailsRow>(c => c
                    
                    .Select(CompanyDetailsRow.Fields.Id)
                    .Where(CompanyDetailsRow.Fields.Id != 1));

                foreach (var c in companyList)
                {
                    Insert.IntoTable("DailyWishesTemplate").Row(new
                    {
                        From = "Your company name",
                        Subject = "Subject",
                        BirthdaySMS = "We wish you a happy birthday!!! dear #customername",
                        MarriageSMS = "We wish you a happy marriage anniversary!!! dear #customername",
                        DOFAnniversarySMS = "We wish you a happy anniversary!!!",
                        BirthdayEmail = "We wish you a happy birthday!!! dear #customername<br/><br/><br/>Regards<br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        MarriageEmail = "We wish you a happy marriage anniversary!!! dear #customername<br/><br/><br/>Regards<br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        DOFAnniversaryEmail = "We wish you a happy anniversary!!!<br/><br/><br/>Regards<br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        CompanyId = c.Id
                    });

                    Insert.IntoTable("AppointmentTemplate").Row(new
                    {
                        Sender = "Your Company Name",
                        Subject = "Appointment",
                        EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>Your appointment for date #appointmentdate is been booked<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        SMSTemplate = "Dear #customername, your appointment for date #appointmentdate is been booked",
                        CompanyId = c.Id
                    });

                    Insert.IntoTable("TeleCallingTemplate").Row(new
                    {
                        From = "Your company name",
                        Subject = "Subject",
                        CustomerSMS = "Dear #customername, appointment by your time convinience is been scheduled on #appointmentdate, for demonstration of #product",
                        ExecutiveSMS = "Dear #executive, new appointment for customer #customername is been scheduled on #appointmentdate, for demonstration of #product",
                        CustomerEmail = "Dear #customername,<br/><br/>appointment by your time convinience is been scheduled on #appointmentdate, for demonstration of #product<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        ExecutiveEmail = "Dear #executive,<br/><br/>new appointment for customer #customername is been scheduled on #appointmentdate, for demonstration of #product<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        CustomerReminderSMS = "Dear #customername, our executive will be reaching your office by #appointmentdate, thankyou",
                        ExecutiveReminderSMS = "Dear #executive, your appointment with #customername will be at #appointmentdate",
                        CompanyId = c.Id
                    });

                    Insert.IntoTable("PurchaseOrderTemplate").Row(new
                    {
                        Sender = "Your Company Name",
                        Subject = "Purchase Order",
                        EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached Purchase Order with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        CompanyId = c.Id
                    });

                    Insert.IntoTable("EnquiryTemplate").Row(new
                    {
                        Sender = "Your Company Name",
                        Subject = "Enquiry",
                        EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        SMSTemplate = "Dear #customername thankyou for showing interest in us",
                        CompanyId = c.Id
                    });

                    Insert.IntoTable("QuotationTemplate").Row(new
                    {
                        Sender = "Your Company Name",
                        Subject = "Quotation",
                        EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached quotation with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        SMSTemplate = "Dear #customername thankyou for showing interest in us",
                        CompanyId = c.Id
                    });

                    Insert.IntoTable("CMSTemplate").Row(new
                    {
                        Sender = "Your Company Name",
                        Subject = "CMS",
                        EmailTemplate = "Dear customer #customername,<br/>Greetings!!!<br/><p>Your complaint regarding product #product is been logged on #complaintdate,<br/>Complaint no is #complaintno, your complaint is assigned to #representative and shall be closed by #expecteddate<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        EmailTemplateReceipt = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached receipt with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        ClosedEmailTemplate = "Dear customer #customername,<br/>Greetings!!!<br/><p>Your complaint regarding product #product is been closed on #completiondate,<br/>Complaint no was #complaintno, if you are having any query feel free to connect us <br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        EngineerEmailTemplate = "Dear #representative,<br/>Greetings!!!<br/><p>New complaint #complaintno is been logged by customer #customername - #phone,<br/>Address - #address,<br/>For product #product,<br/>Complaint details are \"#complaintdetails\", expected closing is on #expecteddate <br/> #instructions<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        SMSTemplate = "Dear customer #customername, your complaint regarding product #product is been logged on #complaintdate, Complaint no is #complaintno, your complaint is assigned to #representative and shall be closed by #expecteddate",
                        ClosedSMSTemplate = "Dear customer #customername, your complaint regarding product #product is been closed, if you have any query feel free to connect us",
                        EngineerSMSTemplate = "New complaint #complaintno is been logged by customer #customername - #phone, address - #address, for product #product, complaint details #complaintdetails, expected closing is on #expecteddate - #instructions",
                        CompanyId = c.Id
                    });

                    Insert.IntoTable("AMCTemplate").Row(new
                    {
                        Sender = "Your Company Name",
                        Subject = "AMC Invoice",
                        EmailTemplateReceipt = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached Invoice with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        SMSTemplate = "Dear customer #customername, your AMC invoice of amount #amount is created, Invoice no is #amcno",
                        VisitSMSTemplate = "Dear #customername, AMC visit is done by our engineer #engineername on #completiondate",
                        CompanyId = c.Id
                    });

                    Insert.IntoTable("InvoiceTemplate").Row(new
                    {
                        Sender = "Your Company Name",
                        Subject = "Invoice",
                        EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached invoice with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        SMSTemplate = "Dear #customername thankyou for purchasing, your invoice amount is #amount for invoice no. #invoiceno ",
                        CompanyId = c.Id
                    });

                    Insert.IntoTable("ChallanTemplate").Row(new
                    {
                        Sender = "Your Company Name",
                        Subject = "Delivery Challan",
                        EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached delivery challan with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                        SMSTemplate = "Dear #customername thankyou for purchasing, your challan amount is #amount for challan no. #challanno ",
                        CompanyId = c.Id
                    });
                }
            }
        }

        public override void Down()
        {
        }
    }
}