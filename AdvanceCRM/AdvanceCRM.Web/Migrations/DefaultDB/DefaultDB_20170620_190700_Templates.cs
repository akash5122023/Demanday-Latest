using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170620190700)]
    public class DefaultDB_20170620_190700_Templates : Migration
    {
        public override void Up()
        {

            Create.Table("EnquiryTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Sender").AsString(250).NotNullable()
                .WithColumn("Subject").AsString(250).NotNullable()
                .WithColumn("EmailTemplate").AsString(2000).NotNullable()
                .WithColumn("Attachment").AsString(1000).Nullable()
                .WithColumn("SMSTemplate").AsString(1000).NotNullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable();

            Create.Table("QuotationTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Sender").AsString(250).NotNullable()
                .WithColumn("Subject").AsString(250).NotNullable()
                .WithColumn("EmailTemplate").AsString(2000).NotNullable()
                .WithColumn("SMSTemplate").AsString(1000).NotNullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable()
                .WithColumn("Attachment").AsString(1000).Nullable()
                .WithColumn("CCEmails").AsString(1000).Nullable()
                .WithColumn("CCSMSs").AsString(1000).Nullable()
                .WithColumn("BCCEmails").AsString(500).Nullable()
                ;

            Create.Table("CMSTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Sender").AsString(250).NotNullable()
                .WithColumn("Subject").AsString(250).NotNullable()
                .WithColumn("EmailTemplate").AsString(2000).NotNullable()
                .WithColumn("EmailTemplateReceipt").AsString(2000).NotNullable()
                .WithColumn("ClosedEmailTemplate").AsString(2000).NotNullable()
                .WithColumn("EngineerEmailTemplate").AsString(2000).NotNullable()
                .WithColumn("SMSTemplate").AsString(1000).NotNullable()
                .WithColumn("ClosedSMSTemplate").AsString(1000).NotNullable()
                .WithColumn("EngineerSMSTemplate").AsString(1000).NotNullable()
                .WithColumn("CCEmails").AsString(1000).Nullable()
                .WithColumn("CCSMSs").AsString(1000).Nullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable()
                .WithColumn("BCCEmails").AsString(500).Nullable()
                ;
            Create.Table("AMCTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Sender").AsString(250).NotNullable()
                .WithColumn("Subject").AsString(250).NotNullable()
                .WithColumn("EmailTemplateReceipt").AsString(2000).NotNullable()
                .WithColumn("SMSTemplate").AsString(1000).NotNullable()
                .WithColumn("CCEmails").AsString(1000).Nullable()
                .WithColumn("TermsConditions").AsString(2000).Nullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable()
                .WithColumn("BCCEmails").AsString(500).Nullable()
                .WithColumn("VisitSMSTemplate").AsString(1000).Nullable()
                ;

            Create.Table("InvoiceTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Sender").AsString(250).NotNullable()
                .WithColumn("Subject").AsString(250).NotNullable()
                .WithColumn("EmailTemplate").AsString(2000).NotNullable()
                .WithColumn("SMSTemplate").AsString(1000).NotNullable()
                .WithColumn("TermsConditions").AsString(2000).Nullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable()
                .WithColumn("CCEmails").AsString(1000).Nullable()
                .WithColumn("CCSMSs").AsString(1000).Nullable()
                .WithColumn("Attachment").AsString(1000).Nullable()
                .WithColumn("BCCEmails").AsString(500).Nullable()
                ;

            Create.Table("ChallanTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Sender").AsString(250).NotNullable()
                .WithColumn("Subject").AsString(250).NotNullable()
                .WithColumn("EmailTemplate").AsString(2000).NotNullable()
                .WithColumn("CCEmails").AsString(1000).Nullable()
                .WithColumn("SMSTemplate").AsString(1000).NotNullable()
                .WithColumn("CCSMSs").AsString(1000).Nullable()
                .WithColumn("TermsConditions").AsString(2000).Nullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable()
                .WithColumn("BCC").AsString(500).Nullable()
                ;

            Create.Table("PurchaseOrderTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Sender").AsString(250).NotNullable()
                .WithColumn("Subject").AsString(250).NotNullable()
                .WithColumn("EmailTemplate").AsString(2000).NotNullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable()
                .WithColumn("CCEmails").AsString(1000).Nullable()
                .WithColumn("CCSMSs").AsString(1000).Nullable()
                .WithColumn("BCC").AsString(500).Nullable()
                ;

            Create.Table("TeleCallingTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("From").AsString(500).NotNullable()
                .WithColumn("Subject").AsString(500).NotNullable()
                .WithColumn("CustomerSMS").AsString(500).NotNullable()
                .WithColumn("ExecutiveSMS").AsString(500).NotNullable()
                .WithColumn("CustomerEmail").AsString(500).NotNullable()
                .WithColumn("ExecutiveEmail").AsString(500).NotNullable()
                .WithColumn("CustomerReminderSMS").AsString(500).Nullable()
                .WithColumn("ExecutiveReminderSMS").AsString(500).Nullable()
                ;

            Create.Table("AppointmentTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Sender").AsString(250).NotNullable()
                .WithColumn("Subject").AsString(250).NotNullable()
                .WithColumn("EmailTemplate").AsString(2000).NotNullable()
                .WithColumn("Attachment").AsString(1000).Nullable()
                .WithColumn("SMSTemplate").AsString(1000).NotNullable()
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable()
                .WithColumn("MondaySMS").AsString(500).Nullable()
                .WithColumn("TuesdaySMS").AsString(500).Nullable()
                .WithColumn("WednesdaySMS").AsString(500).Nullable()
                .WithColumn("ThursdaySMS").AsString(500).Nullable()
                .WithColumn("FridaySMS").AsString(500).Nullable()
                .WithColumn("SaturdaySMS").AsString(500).Nullable()
                .WithColumn("SundaySMS").AsString(500).Nullable();

            Create.Table("DailyWishesTemplate")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("From").AsString(500).NotNullable()
                .WithColumn("Subject").AsString(500).NotNullable()
                .WithColumn("BirthdaySMS").AsString(500).NotNullable()
                .WithColumn("MarriageSMS").AsString(500).NotNullable()
                .WithColumn("DOFAnniversarySMS").AsString(500).NotNullable()
                .WithColumn("BirthdayEmail").AsString(500).NotNullable()
                .WithColumn("MarriageEmail").AsString(500).NotNullable()
                .WithColumn("DOFAnniversaryEmail").AsString(500).NotNullable()

                ;

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
            });

            Insert.IntoTable("AppointmentTemplate").Row(new
            {
                Sender = "Your Company Name",
                Subject = "Appointment",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>Your appointment for date #appointmentdate is been booked<br/><br/><br/>Regards,<br/> #username </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername, your appointment for date #appointmentdate is been booked",
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
                ExecutiveReminderSMS= "Dear #executive, your appointment with #customername will be at #appointmentdate"
            });

            Insert.IntoTable("PurchaseOrderTemplate").Row(new
            {
                Sender = "Your Company Name",
                Subject = "Purchase Order",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached Purchase Order with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username <br/> #Phone </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
            });

            Insert.IntoTable("EnquiryTemplate").Row(new
            {
                Sender = "Your Company Name",
                Subject = "Enquiry",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We thank you for showing your interest in our products/services<br/><br/><br/>Regards,<br/> #username <br/> #Phone </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            });

            Insert.IntoTable("QuotationTemplate").Row(new
            {
                Sender = "Your Company Name",
                Subject = "Quotation",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached quotation with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username <br/> #Phone </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for showing interest in us",
            });

            Insert.IntoTable("CMSTemplate").Row(new
            {
                Sender = "Your Company Name",
                Subject = "CMS",
                EmailTemplate = "Dear customer #customername,<br/>Greetings!!!<br/><p>Your complaint regarding product #product is been logged on #complaintdate,<br/>Complaint no is #complaintno, your complaint is assigned to #representative and shall be closed by #expecteddate<br/><br/><br/>Regards,<br/> #username <br/> #Phone </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                EmailTemplateReceipt = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached receipt with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username <br/> #Phone</p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                ClosedEmailTemplate = "Dear customer #customername,<br/>Greetings!!!<br/><p>Your complaint regarding product #product is been closed on #completiondate,<br/>Complaint no was #complaintno, if you are having any query feel free to connect us <br/><br/><br/>Regards,<br/> #username <br/> #Phone </p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                EngineerEmailTemplate = "Dear #representative,<br/>Greetings!!!<br/><p>New complaint #complaintno is been logged by customer #customername - #phone,<br/>Address - #address,<br/>For product #product,<br/>Complaint details are \"#complaintdetails\", expected closing is on #expecteddate <br/> #instructions<br/><br/><br/>Regards,<br/> #username <br/> #Phone</p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear customer #customername, your complaint regarding product #product is been logged on #complaintdate, Complaint no is #complaintno, your complaint is assigned to #representative and shall be closed by #expecteddate",
                ClosedSMSTemplate = "Dear customer #customername, your complaint regarding product #product is been closed, if you have any query feel free to connect us",
                EngineerSMSTemplate = "New complaint #complaintno is been logged by customer #customername - #phone, address - #address, for product #product, complaint details #complaintdetails, expected closing is on #expecteddate - #instructions",
            });

            Insert.IntoTable("AMCTemplate").Row(new
            {
                Sender = "Your Company Name",
                Subject = "AMC Invoice",
                EmailTemplateReceipt = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached Invoice with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username <br/> #Phone</p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear customer #customername, your AMC invoice of amount #amount is created, Invoice no is #amcno",
                VisitSMSTemplate = "Dear #customername, AMC visit is done by our engineer #engineername on #completiondate"
            });

            Insert.IntoTable("InvoiceTemplate").Row(new
            {
                Sender = "Your Company Name",
                Subject = "Invoice",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached invoice with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username <br/> #Phone</p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for purchasing, your invoice amount is #amount for invoice no. #invoiceno ",
            });

            Insert.IntoTable("ChallanTemplate").Row(new
            {
                Sender = "Your Company Name",
                Subject = "Delivery Challan",
                EmailTemplate = "Dear #customername,<br/>Greetings!!!<br/><p>We have attached delivery challan with this mail as requested by you, please find the attachment<br/><br/><br/>Regards,<br/> #username <br/> #Phone</p><br/><br/><br/><br/>-<br/>Sent from " + Texts.Site.Layout.WhiteLabel + " CRM",
                SMSTemplate = "Dear #customername thankyou for purchasing, your challan amount is #amount for challan no. #challanno ",
            });

        }

        public override void Down()
        {

        }
    }
}