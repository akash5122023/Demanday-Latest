using _Ext.Pages;
using AdvanceCRM.Accounting.Pages;
using AdvanceCRM.Administration;
using AdvanceCRM.Administration.Pages;
using AdvanceCRM.Attendance.Pages;
using AdvanceCRM.BizMail.Pages;
using AdvanceCRM.Contacts.Pages;
using AdvanceCRM.Demanday.Pages;
using AdvanceCRM.DMS.Pages;
using AdvanceCRM.Employee.Pages;
using AdvanceCRM.Enquiry.Pages;
using AdvanceCRM.Feedback.Pages;
using AdvanceCRM.Masters.Pages;
using AdvanceCRM.Premium.Pages;
using AdvanceCRM.Products.Pages;
using AdvanceCRM.Purchase.Pages;
using AdvanceCRM.Quotation.Pages;
using AdvanceCRM.Reports.Pages;
using AdvanceCRM.Sales.Pages;
using AdvanceCRM.Services.Pages;
using AdvanceCRM.Settings.Pages;
using AdvanceCRM.Tasks.Pages;
using AdvanceCRM.Template.Pages;
using AdvanceCRM.ThirdParty.Pages;
//using MyPages = AdvanceCRM.Pages;
using Serenity.Navigation;

//[assembly: NavigationLink(900, "My Profile", url: "~/Account/Profile", permission: "", icon: "fa-user-circle")]
//[assembly: NavigationLink(1000, "Dashboard", url: "~/", permission: "", icon: "fa-tachometer")]
//[assembly: NavigationLink(1000, "Dashboard/My Dashboard", url: "~/", permission: "", icon: "fa-home text-orange")]
//[assembly: NavigationLink(1001, "Dashboard/Team Dashboard", url: "~/TeamDashboard", permission: "", icon: "fa-users text-green")]
//[assembly: NavigationLink(6001, "Dashboard/DSR Dashboard", url: "~/DSR", permission: "", icon: "fa-line-chart text-red")]
//[assembly: NavigationLink(6002, "Dashboard/Accounts Dashboard", url: "~/Accounts", permission: "Contacts:Read", icon: "fa-calculator text-yellow")]
//[assembly: NavigationLink(6003, "Dashboard/Attendance Dashboard", url: "~/AttendanceDashboard", permission: "", icon: "fa-calendar-check-o text-blue")]
//[assembly: NavigationLink(6004, "Dashboard/Inventory Dashboard", url: "~/InventoryDashboard", permission: "", icon: "fa-cubes fa-bounce text-green")]
//[assembly: NavigationLink(1002, "Dashboard/IVR Dashboard", url: "~/IvrDashboard", permission: "KnowlarityDetails:inbox", icon: "fa-phone text-purple")]


//Addon
//[assembly: NavigationMenu(1100, "CEO Dashboards", icon: "fa-bar-chart", Title = "CEO Dashboards", Permission = "Premium:Dashboards")]
//[assembly: NavigationLink(1101, "CEO Dashboards/Enquiry Dashboard", url: "~/Premium/Enquiry", permission: "Premium:Dashboards", icon: "fa-search text-blue", Title = "Enquiry Dashboard")]
//[assembly: NavigationLink(1102, "CEO Dashboards/Quotation Dashboard", url: "~/Premium/Quotation", permission: "Premium:Dashboards", icon: "fa-file-text text-orange", Title = "Quotation Dashboard")]
//[assembly: NavigationLink(1103, "CEO Dashboards/Sales Dashboard", url: "~/Premium/Sales", permission: "Premium:Dashboards", icon: "fa-line-chart text-green", Title = "Sales Dashboard")]
//[assembly: NavigationLink(1104, "CEO Dashboards/Representative Dashboard", url: "~/Premium/Representative", permission: "Premium:Dashboards", icon: "fa-users text-purple", Title = "Representative Dashboard")]
//[assembly: NavigationLink(1105, "CEO Dashboards/Representative Performance", url: "~/Premium/RepresentativePerformance", permission: "Premium:Dashboards", icon: "fa-user-circle text-yellow", Title = "Representative Performance")]
//[assembly: NavigationLink(1107, "CEO Dashboards/Target Setting", typeof(TargetSettingController), icon: "fa-bullseye text-red", Permission = "Premium:Target", Title = "Target Setting")]

[assembly: NavigationMenu(1000, "Demanday EMail", icon: "fa-gift", Title = "Products")]
[assembly: NavigationLink(1451, "Demanday/Demanday Enquiry", typeof(DemandayEnquiryController), icon: "fa-sms", Permission = "DemandayEnquiry:Read", Title = "Enquiry")]
[assembly: NavigationLink(1452, "Demanday/TeamLeader", typeof(DemandayTeamLeaderController), icon: "fa-mail-bulk", Permission = "TeamLeader:Read", Title = "Team Leader")]
[assembly: NavigationLink(1453, "Demanday/Quality", typeof(DemandayQualityController), icon: "fa-mail-bulk", Permission = "Quality:Read", Title = "Quality")]
[assembly: NavigationLink(1454, "Demanday/MIS", typeof(DemandayMisController), icon: "fa-mail-bulk", Permission = "MIS:Read", Title = "MIS")]
//[assembly: NavigationLink(1456, "Demanday/EMailTeam", typeof(EMailTeamController), icon: "fa-mail-bulk", Permission = "EMailTeam:Read", Title = "EMailTeam")]
[assembly: NavigationLink(1457, "Demanday/Enquiry Contacts", typeof(EnquiryContactsController), icon: "fa-mail-bulk", Permission = "EnquiryContacts:Read", Title = "Enquiry Contacts")]


[assembly: NavigationLink(1461, "Demanday/TM Enquiry", typeof(DemandayTeleMarketingEnquiryController), icon: "fa-phone", Permission = "TeleMarketingEnquiry:Read", Title = "TM Enquiry")]
[assembly: NavigationLink(1462, "Demanday/TM TeamLeader", typeof(DemandayTeleMarketingTeamLeaderController), icon: "fa-phone", Permission = "TeleMarketingTeamLeader:Read", Title = "TM TeamLeader")]
[assembly: NavigationLink(1463, "Demanday/TM Quality", typeof(DemandayTeleMarketingQualiltyController), icon: "fa-phone", Permission = "TeleMarketingQualilty:Read", Title = "TM Quality")]
[assembly: NavigationLink(1464, "Demanday/TM MIS", typeof(DemandayTeleMarketingMISController), icon: "fa-phone", Permission = "TeleMarketingMis:Read", Title = "TM MIS")]
[assembly: NavigationLink(1465, "Demanday/TM Contacts", typeof(DemandayTeleMarketingContactsController), icon: "fa-phone", Permission = "TeleMarketingContacts:Read", Title = "TM Contacts")]
//[assembly: NavigationLink(1466, "Demanday/TM Team", typeof(TeleMarketingTeammController), icon: "fa-phone", Permission = "TeleMarketingTeam:Read", Title = "TM Team")]

[assembly: NavigationLink(1455, "Demanday/EmailTeam Contacts", typeof(DemandayContactsController), icon: "fa-mail-bulk", Permission = "FLS:Read", Title = "ET Contacts")]
[assembly: NavigationLink(1473, "Demanday/Verification", typeof(DemandayVerificationController), icon: "fa-mail-bulk", Permission = "Verification:Read", Title = "Verification")]


[assembly: NavigationLink(1200, "Enquiry", typeof(EnquiryController), icon: "fa-group", Permission = "Enquiry:Read", Title = "New Lead")]
//[assembly: NavigationLink(1201, "Quotation", typeof(QuotationController), icon: "fa-file-text", Permission = "Quotation:Read", Title = "Quotation")]
//[assembly: NavigationLink(1202, "Tasks", typeof(TasksController), icon: "fa-edit", Permission = "Tasks:Read", Title = "Tasks")]
[assembly: NavigationLink(1204, "Contacts", typeof(ContactsController), icon: "fa-address-book", Permission = "Contacts:Read", Title = "Lead Contacts")]
//[assembly: NavigationMenu(1400, "Products", icon: "fa-gift", Title = "Products")]
//[assembly: NavigationLink(1203, "Products/Products", typeof(ProductsController), icon: "fa-gift", Permission = "Products:Read", Title = "Products")]
//[assembly: NavigationLink(1454, "Products/Stock Transfer", typeof(StockTransferController), icon: "fa-truck", Permission = "StockTransfer:Read", Title = "Stock Transfer")]
//[assembly: NavigationLink(1455, "Products/Bom", typeof(BomController), icon: "fa-bar-chart", Permission = "Bom:Read", Title = "Bom")]
//[assembly: NavigationLink(1460, "Products/Inventory", typeof(InventoryController), icon: "fa-server", Permission = "Inventory:Read", Title = "Inventory")]
//[assembly: NavigationLink(1456, "Products/Itinerary", typeof(ItineraryController), icon: "fa-truck", Permission = "Itinerary:Read", Title = "Itinerary")]
//[assembly: NavigationLink(1205, "Attendance", typeof(AttendanceController), icon: "fa-calendar-check-o", Permission = "Attendance:Read", Title = "Attendance")]
//[assembly: NavigationLink(1205, "DMS", typeof(DMSController), icon: "fa-folder-o", Permission = "DMS:Read", Title = "DMS")]
//[assembly: NavigationLink(1206, "Employee", typeof(EmployeeController), icon: "fa-address-book", Permission = "Employee:Read", Title = "Employee")]

//[assembly: NavigationMenu(1400, "Sales", icon: "fa-exchange", Title = "Sales")]
//[assembly: NavigationLink(1401, "Sales/Proforma Invoice", typeof(InvoiceController), icon: "icon-book-open", Permission = "Proforma:Read", Title = "Proforma")]
//[assembly: NavigationLink(1404, "Sales/Delivery Challan", typeof(ChallanController), icon: "fa-file-text-o", Permission = "Challan:Read", Title = "Challan")]
//[assembly: NavigationLink(1406, "Sales/Indent", typeof(IndentController), icon: "fa-list-alt", Permission = "Indent:Read", Title = "Indent")]
//[assembly: NavigationLink(1407, "Sales/Inward", typeof(InwardController), icon: "fa-file-text-o", Permission = "Inward:Read", Title = "Inward")]
//[assembly: NavigationLink(1408, "Sales/Outward", typeof(OutwardController), icon: "fa-file-text-o", Permission = "Outward:Read", Title = "Outward")]
//[assembly: NavigationLink(1402, "Sales/Sales", typeof(SalesController), icon: "icon-docs", Permission = "Sales:Read", Title = "Invoice")]
//[assembly: NavigationLink(1405, "Sales/Sales Return", typeof(SalesReturnController), icon: "fa-reply-all", Permission = "SalesReturn:Read", Title = "Sales Return")]

//[assembly: NavigationMenu(1450, "Purchase", icon: "fa-cart-plus", Title = "Purchase")]
//[assembly: NavigationLink(1451, "Purchase/Purchase Order", typeof(PurchaseOrderController), icon: "fa-cart-arrow-down", Permission = "PurchaseOrder:Read", Title = "Purchase Order")]
//[assembly: NavigationLink(1452, "Purchase/Purchase", typeof(PurchaseController), icon: "icon-basket-loaded", Permission = "Purchase:Read", Title = "Purchase")]
//[assembly: NavigationLink(1453, "Purchase/Purchase Return", typeof(PurchaseReturnController), icon: "fa-reply-all", Permission = "PurchaseReturn:Read", Title = "Purchase Return")]
//[assembly: NavigationLink(1454, "Purchase/GrnTwo", typeof(GrnTwoController), icon: "fa-truck", Permission = "GrnTwo:Read", Title = "GRN")]
//[assembly: NavigationLink(1455, "Purchase/QualityCheck", typeof(QualityCheckController), icon: "fa-check-circle", Permission = "QualityCheck:Read", Title = "Quality Check")]
//[assembly: NavigationLink(1456, "Purchase/RejectionOutward", typeof(RejectionOutwardController), icon: "fa-times-circle", Permission = "RejectionOutward:Read", Title = "Rejection Outward")]
//[assembly: NavigationLink(1457, "Purchase/Rorder", typeof(RorderController), icon: "fa-retweet", Permission = "Rorder:Read", Title = "Reorder")]

//[assembly: NavigationMenu(1800, "Services", icon: "fa-briefcase", Title = "Services")]
//[assembly: NavigationLink(1801, "Services/Tele Calling", typeof(TeleCallingController), icon: "fa-phone", Permission = "TeleCalling:Read", Title = "Tele Calling")]
//[assembly: NavigationLink(1802, "Services/CMS", typeof(CMSController), icon: "fa-support", Permission = "CMS:Read", Title = "CMS")]
//[assembly: NavigationLink(1803, "Services/AMC", typeof(AMCController), icon: "fa-file-code-o", Permission = "AMC:Read", Title = "AMC")]
//[assembly: NavigationLink(1805, "Services/Ticket", typeof(TicketController), icon: "fa-ticket", Permission = "Ticket:Read", Title = "Ticket")]
//[assembly: NavigationLink(1805, "Services/Feedback", typeof(FeedbackDetailsController), icon: "fa-comment", Permission = "Feedback:Read", Title = "Feedback")]


////Addon MailChimp
//[assembly: NavigationMenu(1810, "Services/MailChimp", icon: "fa-bullhorn", Title = "MailChimp")]
//[assembly: NavigationLink(1811, "Services/MailChimp/Home", url: ("~/MailChimp/GetList"), permission: "MailChimp:Inbox", icon: "fa-home", Title = "Home")]
//[assembly: NavigationLink(1812, "Services/MailChimp/Campaign", url: ("~/MailChimp/SentCampaigns"), permission: "MailChimp:Inbox", icon: "fa-envelope-open", Title = "Campaign")]
//[assembly: NavigationLink(1813, "Services/MailChimp/Template", url: ("~/MailChimp/GetTemplateList"), permission: "MailChimp:Inbox", icon: "fa-trello", Title = "Template")]
//[assembly: NavigationLink(1814, "Services/MailChimp/Add Subscriber", url: ("~/MailChimp/NewSubscriber"), permission: "MailChimp:Inbox", icon: "fa-user-plus", Title = "Add Subscriber")]
//[assembly: NavigationLink(1815, "Services/MailChimp/Campaign Reports", url: ("~/MailChimp/Reports"), permission: "MailChimp:Inbox", icon: "fa-file-text-o", Title = "Campaign Reports")]

//[assembly: NavigationMenu(1900, "Accounting", icon: "icon-credit-card", Title = "Accounting")]
//[assembly: NavigationLink(1901, "Accounting/Cashbook", typeof(CashbookController), icon: "fa-money", Permission = "Cashbook:Read", Title = "Cashbook")]
//[assembly: NavigationLink(1902, "Accounting/Expense Management", typeof(ExpenseManagementController), icon: "fa-file-text-o", Permission = "ExpenseManagement:Read", Title = "Expense Management")]

//[assembly: NavigationMenu(2000, "Integrations", icon: "fa-compass", Title = "Integrations")]

//[assembly: NavigationLink(2001, "Integrations/Website Enquiry", typeof(WebsiteEnquiryController), icon: "fa-globe", Permission = "WebsiteEnquiry:Inbox", Title = "Website Enquiry")]
////Addon
////[assembly: NavigationMenu(2030, "Integrations/IndiaMART", icon: "fa-maxcdn", Title = "IndiaMart")]
//[assembly: NavigationLink(2002, "Integrations/IndiaMART", typeof(IndiaMartDetailsController), icon: "fa-maxcdn", Permission = "IndiaMART:Inbox", Title = "India MART")]
////[assembly: NavigationLink(2003, "Integrations/IndiaMART2", typeof(SIndiaMartDetailsController), icon: "fa-maxcdn", Permission = "IndiaMART:Inbox", Title = "India MART2")]

////Addon
//[assembly: NavigationLink(2004, "Integrations/JustDial", typeof(JustDialDetailsController), icon: "fa-circle-o", Permission = "JustDial:Inbox", Title = "Just Dial")]
////Addon
//[assembly: NavigationLink(2005, "Integrations/TradeIndia", typeof(TradeIndiaDetailsController), icon: "fa-tumblr", Permission = "TradeIndia:Inbox", Title = "Trade India")]
////Addon
//[assembly: NavigationLink(2006, "Integrations/Facebook", typeof(FacebookDetailsController), icon: "fa-facebook", Permission = "Facebook:Inbox", Title = "Facebook")]

//[assembly: NavigationLink(2008, "Integrations/Visit", typeof(VisitController), icon: "fa-vimeo", Permission = "Visit:Inbox", Title = "Visit")]
////[assembly: NavigationMenu(2100, "Integrations/RazorPay", icon: "fa-solid fa-r", Title = "RazorPay")]
//[assembly: NavigationLink(2101, "Integrations/RazorPay", typeof(RpPaymentDetailsController), icon: "fa-credit-card", Permission = "RazorPay:Inbox", Title = "RazorPay")]


////Addon
////[assembly: NavigationLink(2007, "Integrations/IVR", url: "~/IVR", permission: "IVR:Inbox", icon: "fa-headphones", Title = "IVR")]
//[assembly: NavigationLink(2008, "Integrations/IVRDetails", typeof(KnowlarityDetailsController), icon: "fa-headphones", Permission = "KnowlarityDetails:inbox", Title = "IVRDetails")]
//[assembly: NavigationLink(2009, "Integrations/Instamojo", typeof(InstamojoDetailsController), icon: "fa-info", Permission = "Instamojo:Inbox", Title = "Instamojo")]
//[assembly: NavigationMenu(2010, "Integrations/WooCommerce", icon: "fa-wordpress", Title = "WooCommerce")]
//[assembly: NavigationLink(2011, "Integrations/WooCommerce/Customer", typeof(WoocommerceDetailsController), icon: "fa-wordpress", Permission = "Woocommerce:Inbox", Title = "Customer")]
//[assembly: NavigationLink(2012, "Integrations/WooCommerce/Orders", typeof(WcOrderDetailsController), icon: "fa-wordpress", Permission = "Woocommerce:Inbox", Title = "Order")]

//[assembly: NavigationMenu(2016, "Integrations/Interakt", icon: "fa-info-circle", Title = "Interakt")]
//[assembly: NavigationLink(2017, "Integrations/Interakt/Customer", typeof(InteraktUserController), icon: "fa-user", Permission = "Interakt:Inbox", Title = "Customer")]

////[assembly: NavigationLink(2013, "Integrations/Mails", typeof(MailInboxDetailsController), icon: "fa-medium", Permission = "MailInbox:Inbox", Title = "MailDetails")]
//[assembly: NavigationLink(2014, "Integrations/Telecall", typeof(RawTelecallController), icon: "fa-phone", Permission = "MailInbox:Inbox", Title = "Telecall")]
////[assembly: NavigationLink(2015, "Integrations/Wati Contacts", typeof(WatiContactsController), icon: "fa-wikipedia-w", Permission = "WatiContacts:Inbox", Title = "Wati_Contacts")]
//[assembly: NavigationLink(2016, "Integrations/SulekhaDetails", typeof(SulekhaDetailsController), icon: "fa-superpowers", Permission = "SulekhaDetails:Inbox", Title = "Sulekha")]

//[assembly: NavigationLink(2015, "Integrations/TikcetWebGenerated", typeof(TicketWebDetailsController), icon: "fa-ticket", Permission = "WebsiteEnquiry:Inbox", Title = "Ticket-Web-Generated")]

//////BizMail

//[assembly: NavigationMenu(3000, "BizMail", icon: "fa-compass", Title = "BizMail")]
//[assembly: NavigationLink(3001, "BizMail/Lists", typeof(BmListController), icon: "fa-info", Title = "Lists")]
//[assembly: NavigationLink(3002, "BizMail/Subscribers", typeof(BmSubscribersController), icon: "fa-info", Title = "Subscribers")]
//[assembly: NavigationLink(3003, "BizMail/Campaign", typeof(BmCampaignController), icon: "fa-info", Title = "Campaign")]
//[assembly: NavigationLink(3004, "BizMail/CreateCampaign", typeof(CampaignBmController), icon: "fa-info", Title = "Create Campaign")]
//[assembly: NavigationLink(3005, "BizMail/Template", typeof(BmTemplateController), icon: "fa-info", Title = "Template")]


////Addon
//[assembly: NavigationMenu(3100, "BizMail/Setting", icon: "ion-android-archive", Title = "Settigs")]
//[assembly: NavigationLink(3101, "BizMail/Setting/Enquiry", typeof(BizMailEnquiryController), icon: "fa-info", Title = "Enquiry")]
//[assembly: NavigationLink(3102, "BizMail/Setting/Quotation", typeof(BizMailQuotationController), icon: "fa-info", Title = "Quotation")]
//[assembly: NavigationLink(3103, "BizMail/Setting/Facebook", typeof(BizMailFacebookController), icon: "fa-info", Title = "Facebook")]
//[assembly: NavigationLink(3104, "BizMail/Setting/WooCommerce", typeof(BizMailWooComController), icon: "fa-info", Title = "WooCommerce")]
////[assembly: NavigationLink(3105, "BizMail/Setting/Contact", typeof(BizMailContactController), icon: "fa-info", Title = "Contact")]
////[assembly: NavigationLink(3106, "BizMail/Setting/CMS", typeof(BizMailCmsController), icon: "fa-info", Title = "CMS")]
//[assembly: NavigationLink(3107, "BizMail/Setting/IndiaMart", typeof(BizMailIdiaMartController), icon: "fa-info", Title = "IndiaMart")]
//[assembly: NavigationLink(3108, "BizMail/Setting/Instamojo", typeof(BizMailIstamojoController), icon: "fa-info", Title = "Instamojo")]
//[assembly: NavigationLink(3109, "BizMail/Setting/Task", typeof(BizMailTaskController), icon: "fa-info", Title = "Task")]
////[assembly: NavigationLink(3110, "BizMail/Setting/IVR", typeof(BizMailIvrController), icon: "fa-info", Title = "IVR")]
//[assembly: NavigationLink(3111, "BizMail/Setting/Visit", typeof(BizMailVisitController), icon: "fa-info", Title = "Visit")]
//[assembly: NavigationLink(3112, "BizMail/Setting/TradeIndia", typeof(BizMailTradeIdiaController), icon: "fa-info", Title = "TradeIndia")]
//[assembly: NavigationLink(3113, "BizMail/Setting/Web", typeof(BizMailWebController), icon: "fa-info", Title = "Web")]


////Addon
//[assembly: NavigationMenu(6000, "Reports", icon: "ion-android-archive", Title = "Reports")]
////[assembly: NavigationLink(6001, "Reports/Enquiry Reports", typeof(UserDetailReportController), icon: "fa-group", Permission = "Reports:Enquiry", Title = "Detailed")]
//[assembly: NavigationLink(6002, "Reports/Enquiry Reports", typeof(EnquiryReportController), icon: "fa-group", Permission = "Reports:Enquiry", Title = "Enquiry")]
//[assembly: NavigationLink(6003, "Reports/Quotation Reports", typeof(QuotationReportController), icon: "fa-file-text", Permission = "Reports:Quotation", Title = "Quotation")]
//[assembly: NavigationLink(6004, "Reports/Inventory Reports", typeof(InventoryReportController), icon: "icon-basket-loaded", Permission = "Reports:Accounting", Title = "Inventory")]
//[assembly: NavigationLink(6005, "Reports/Accounting", typeof(AccountingReportController), icon: "fa-money", Permission = "Reports:Accounting", Title = "Accounting")]
//[assembly: NavigationLink(6006, "Reports/SignInDetails", typeof(SignInReportController), icon: "fa-user-circle", Permission = "Reports:Accounting", Title = "SignIn")]
//[assembly: NavigationLink(6007, "Reports/Tasks", typeof(TasksReportController), icon: "fa-edit", Permission = "Reports:Accounting", Title = "Tasks")]
//[assembly: NavigationLink(6008, "Reports/Visit", typeof(VisitReportController), icon: "fa-location-arrow", Permission = "Reports:Accounting", Title = "Visit")]
//[assembly: NavigationLink(6009, "Reports/Sales", typeof(SalesReportController), icon: "fa-tag", Permission = "Reports:Accounting", Title = "Sales")]

//[assembly: NavigationLink(6020, "Reports/CMS Reports", typeof(CMSReportController), icon: "fa-group", Permission = "Reports:Enquiry", Title = "CMS")]

//[assembly: NavigationMenu(6010, "Reports/IVRDetails", icon: "fa-icon-basket-loaded", Title = "IVR", Permission = "Reports:IVRReport")]
//[assembly: NavigationLink(6019, "Reports/IVRDetails/IVRrep", url: ("~/Reports/IVRReport"), permission: "Reports:IVRReport", icon: "fa-file", Title = "IVR")]


////GST Reports

//[assembly: NavigationMenu(6500, "Reports/GST Reports", icon: "icon-basket-loaded")]
//[assembly: NavigationMenu(6500, "Reports/GST Reports/GSTR-1", icon: "fa-icon-basket-loaded", Title = "GSTR-1", Permission = "Reports:GSTR1")]
//[assembly: NavigationLink(6501, "Reports/GST Reports/GSTR-1/B2B", url: ("~/Reports/GSTR1Report"), permission: "Reports:GSTR1", icon: "fa-file", Title = "B2B")]
//[assembly: NavigationLink(6501, "Reports/GST Reports/GSTR-1/B2CS", url: ("~/Reports/GSTR1B2CSReport"), permission: "Reports:GSTR1", icon: "fa-file", Title = "B2CS")]
//[assembly: NavigationLink(6501, "Reports/GST Reports/GSTR-1/B2CL", url: ("~/Reports/GSTR1B2CLReport"), permission: "Reports:GSTR1", icon: "fa-file", Title = "B2CL")]

////GSTR-2
//[assembly: NavigationMenu(6500, "Reports/GST Reports/GSTR-2", icon: "icon-basket-loaded", Title = "GSTR-2", Permission = "Reports:GSTR2")]
//[assembly: NavigationLink(6501, "Reports/GST Reports/GSTR-2/B2B", url: ("~/Reports/GSTR2B2BReport"), permission: "Reports:GSTR2", icon: "fa-file", Title = "B2B")]
//[assembly: NavigationLink(6501, "Reports/GST Reports/GSTR-2/B2BUR", url: ("~/Reports/GSTR2B2BURReport"), permission: "Reports:GSTR2", icon: "fa-file", Title = "B2BUR")]

////Attendance Report
//[assembly: NavigationLink(6552, "Reports/Attendance", typeof(AttendanceReportController), icon: "fa-calendar-check-o", Permission = "Reports:Attendance", Title = "Attendance")]


////Followup Report
//[assembly: NavigationMenu(6560, "Reports/Followups", icon: "ion-android-archive")]
//[assembly: NavigationLink(6561, "Reports/Followups/Enquiry", typeof(EnquiryFollowupsController), icon: "fa-group", Permission = "Reports:Enquiry:Followups")]
//[assembly: NavigationLink(6562, "Reports/Followups/Quotation", typeof(QuotationFollowupsController), icon: "fa-file-text", Permission = "Reports:Quotation:Followups")]
//[assembly: NavigationLink(6564, "Reports/Followups/Proforma", typeof(InvoiceFollowupsController), icon: "icon-book-open", Permission = "Reports:Proforma:Followups")]
//[assembly: NavigationLink(6565, "Reports/Followups/Sales", typeof(SalesFollowupsController), icon: "icon-docs", Permission = "Reports:Sales:Followups")]
//[assembly: NavigationLink(6566, "Reports/Followups/TeleCalling", typeof(TeleCallingFollowupsController), icon: "fa-phone", Permission = "Reports:TeleCalling:Followups")]

////Appointment Report
//[assembly: NavigationMenu(6570, "Reports/Appointments", icon: "ion-android-archive")]
//[assembly: NavigationLink(6571, "Reports/Appointments/Enquiry", typeof(EnquiryAppointmentsController), icon: "fa-group", Permission = "Reports:Enquiry:Appointments")]
//[assembly: NavigationLink(6572, "Reports/Appointments/Quotation", typeof(QuotationAppointmentsController), icon: "fa-file-text", Permission = "Reports:Quotation:Appointments")]
//[assembly: NavigationLink(6573, "Reports/Appointments/AMC", typeof(AMCVisitPlannerController), icon: "fa-file-code-o", Permission = "Reports:AMC:Visits")]
//[assembly: NavigationLink(6574, "Reports/Appointments/Proforma", typeof(InvoiceAppointmentsController), icon: "icon-book-open", Permission = "Reports:Proforma:Appointments")]
//[assembly: NavigationLink(6575, "Reports/Appointments/TeleCalling", typeof(TeleCallingAppointmentsController), icon: "fa-phone", Permission = "Reports:TeleCalling:Appointments")]

//[assembly: NavigationMenu(6580, "Reports/Products", icon: "ion-android-archive")]
//[assembly: NavigationLink(6581, "Reports/Products/Enquiry", typeof(EnquiryProductsController), icon: "fa-group", Permission = "Reports:Enquiry:Followups")]
//[assembly: NavigationLink(6582, "Reports/Products/Quotation", typeof(QuotationProductsController), icon: "fa-file-text", Permission = "Reports:Quotation:Followups")]


//Masters
[assembly: NavigationMenu(7000, "Masters", icon: "fa-anchor", Title = "Masters", Permission = "Masters:Read")]
[assembly: NavigationLink(7001, "Masters/Area", typeof(AreaController), icon: "fa-map-signs")]
[assembly: NavigationLink(7001, "Masters/Purpose", typeof(PurposeController), icon: "fa-medium")]
[assembly: NavigationLink(7003, "Masters/Category", typeof(CategoryController), icon: "fa-houzz")]
[assembly: NavigationLink(7004, "Masters/State", typeof(StateController), icon: "fa-map")]
[assembly: NavigationLink(7005, "Masters/City", typeof(CityController), icon: "fa-building")]
[assembly: NavigationLink(7006, "Masters/Tehsil", typeof(TehsilController), icon: "icon-direction")]
[assembly: NavigationLink(7006, "Masters/Village", typeof(VillageController), icon: "fa-home")]
[assembly: NavigationLink(7007, "Masters/Product Division", typeof(ProductsDivisionController), icon: "fa-arrows-alt")]
[assembly: NavigationLink(7007, "Masters/Product Unit", typeof(ProductsUnitController), icon: "fa-balance-scale")]
[assembly: NavigationLink(7007, "Masters/Product Group", typeof(ProductsGroupController), icon: "fa-object-group")]
[assembly: NavigationLink(7008, "Masters/Tax", typeof(TaxController), icon: "fa-cut")]
[assembly: NavigationLink(7009, "Masters/Source", typeof(SourceController), icon: "fa-medium")]
[assembly: NavigationLink(7010, "Masters/Stage", typeof(StageController), icon: "fa-align-right")]
[assembly: NavigationLink(7011, "Masters/Terms", typeof(QuotationTermsMasterController), icon: "fa-gg")]
[assembly: NavigationLink(7011, "Masters/Message", typeof(MessageMasterController), icon: "fa-comment")]
[assembly: NavigationLink(7011, "Masters/Additional Charges", typeof(AdditionalChargesController), icon: "fa-plus-circle")]
[assembly: NavigationLink(7011, "Masters/Additional Concession", typeof(AdditionalConcessionController), icon: "fa-minus-circle")]
[assembly: NavigationLink(7012, "Masters/Task Status", typeof(TaskStatusController), icon: "fa-file-text")]
[assembly: NavigationLink(7012, "Masters/Task Types", typeof(TaskTypeController), icon: "fa-sliders")]
[assembly: NavigationLink(7013, "Masters/Complaint Types", typeof(ComplaintTypeController), icon: "fa-support", Title = "Complaint Types")]
[assembly: NavigationLink(7014, "Masters/Banks", typeof(BankMasterController), icon: "fa-bank", Title = "Bank Master")]
[assembly: NavigationLink(7015, "Masters/Accounting Heads", typeof(AccountingHeadsController), icon: "fa-money", Title = "Accounting Heads")]
[assembly: NavigationLink(7015, "Masters/Trasportation", typeof(TransportationController), icon: "fa-truck", Title = "Trasportation")]
[assembly: NavigationLink(7024, "Masters/Grade", typeof(GradeController), icon: "fa-star")]
[assembly: NavigationLink(7025, "Masters/Department", typeof(DepartmentController), icon: "fa-th-large")]

[assembly: NavigationLink(7026, "Masters/Dealer", typeof(DealerController), icon: "fa-briefcase")]
[assembly: NavigationLink(7026, "Masters/Teams", typeof(TeamsController), icon: "fa-sitemap")]

[assembly: NavigationLink(7027, "Masters/Task", typeof(TaskController), icon: "fa fa-tasks")]
[assembly: NavigationLink(7028, "Masters/Days", typeof(DaysController), icon: "fa fa-Days")]



//[assembly: NavigationMenu(8000, "Templates", icon: "icon-doc", Title = "Templates")]
//[assembly: NavigationLink(8001, "Templates/Enquiry Template", typeof(EnquiryTemplateController), icon: "fa-file-text", Title = "Enquiry Template", Permission = "Template:Enquiry")]
//[assembly: NavigationLink(8002, "Templates/Quotation Template", typeof(QuotationTemplateController), icon: "fa-file-text", Title = "Quotation Template", Permission = "Template:Quotation")]
////Addon
//[assembly: NavigationLink(8003, "Templates/CMS Template", typeof(CmsTemplateController), icon: "fa-file-text", Title = "CMS Template", Permission = "Template:CMS")]
////Addon
//[assembly: NavigationLink(8004, "Templates/AMC Template", typeof(AMCTemplateController), icon: "fa-file-text", Title = "AMC Template", Permission = "Template:AMC")]
////Addon
//[assembly: NavigationLink(8005, "Templates/Invoice Template", typeof(InvoiceTemplateController), icon: "fa-file-text", Title = "Invoice Template", Permission = "Template:Invoice")]
////Addon
//[assembly: NavigationLink(8006, "Templates/Challan Template", typeof(ChallanTemplateController), icon: "fa-file-text", Title = "Challan Template", Permission = "Template:Challan")]
////Addon
//[assembly: NavigationLink(8007, "Templates/PO Template", typeof(PurchaseOrderTemplateController), icon: "fa-file-text", Title = "Purchase Template", Permission = "Template:Purchase")]
////Addon
//[assembly: NavigationLink(8008, "Templates/TeleCalling Template", typeof(TeleCallingTemplateController), icon: "fa-file-text", Title = "TeleCalling Template", Permission = "Template:TeleCalling")]
////Addon
//[assembly: NavigationLink(8009, "Templates/Appointment Template", typeof(AppointmentTemplateController), icon: "fa-file-text", Title = "Appointment Template", Permission = "Template:Appointment")]
////Addon
//[assembly: NavigationLink(8010, "Templates/Daily Wishes Template", typeof(DailyWishesTemplateController), icon: "fa-file-text", Title = "Daily Wishes Template", Permission = "Template:DailyWishes")]
//[assembly: NavigationLink(8011, "Templates/Other Templates", typeof(OtherTemplatesController), icon: "fa-file-text", Title = "Other Templates")]

//[assembly: NavigationLink(8012, "Templates/Intract Templates", typeof(IntractTemplateController), icon: "fa-file-text", Title = "Intract Templates")]
//[assembly: NavigationLink(8014, "Templates/QuickMail Templates", typeof(QuickMailTemplateController), icon: "fa-file-text", Title = "QuickMail Templates", Permission = "Template:QuickMailTemplate")]



[assembly: NavigationMenu(9000, "Administration", icon: "icon-screen-desktop")]
[assembly: NavigationLink(9001, "Administration/Company Setup", typeof(CompanyDetailsController), icon: "fa-hospital-o", Permission = "Administration:General")]
[assembly: NavigationLink(9002, "Administration/Languages", typeof(LanguageController), icon: "icon-bubbles", Permission = "Administration:Translation")]
[assembly: NavigationLink(9003, "Administration/Translations", typeof(TranslationController), icon: "icon-speech", Permission = "Administration: Translation")]
[assembly: NavigationLink(9004, "Administration/Roles", typeof(RoleController), icon: "icon-lock", Permission = "Administration:Security")]
[assembly: NavigationLink(9005, "Administration/User Management", typeof(UserController), icon: "icon-people", Permission = "Administration:Security")]
// SaaS-only: hide client license monitor for single-tenant deployments
//[assembly: NavigationLink(9006, "Administration/Client License Monitor", typeof(TenantLicenseController), icon: "fa-id-badge", Permission = PermissionKeys.Security, Title = "Client License Monitor")]
[assembly: NavigationLink(9006, "Administration/Bulk Transfer", url: "javascript:new AdvanceCRM.Common.BulkTransferDialog().dialogOpen()", permission: "Administration:Bulk Transfer", icon: "fa-calendar-check-o", Title = "Bulk Transfer")]

[assembly: NavigationMenu(9007, "Administration/Logs", icon: "icon-doc", Permission = "Administration:Logs")]
//[assembly: NavigationLink(9006, "Administration/Logs/Audit Log", typeof(AuditLogController), icon: "fa-pencil-square-o")]
//[assembly: NavigationLink(9008, "Administration/Logs/Daily Wishes Log", typeof(DailyWishesLogController), icon: "fa-calendar")]
//[assembly: NavigationLink(9009, "Administration/Logs/OTP Log", typeof(OptLogController), icon: "fa-file-text")]
//[assembly: NavigationLink(9010, "Administration/Logs/Appointment SMS Log", typeof(AppointmentSmsLogController), icon: "fa-calendar")]
[assembly: NavigationLink(9011, "Administration/Logs/LogInOut Log", typeof(LogInOutLogController), icon: "fa-calendar")]

[assembly: NavigationMenu(9100, "Settings", icon: "icon-settings")]
[assembly: NavigationLink(9101, "Settings/CRM Setup", typeof(CRMController), icon: "fa-gears", Permission = "Administration:General")]

//[assembly: NavigationLink(9102, "Settings/SMS Configuration", typeof(SMSConfigurationController), icon: "fa-envelope", Permission = "Settings:SMS")]
//[assembly: NavigationLink(9103, "Settings/Website Configuration", typeof(WebsiteEnquiryConfigurationController), icon: "fa-globe", Permission = "Settings:WebsiteEnquiry")]

////Addon
//[assembly: NavigationLink(9104, "Settings/IVR Configuration", typeof(IVRConfigurationController), icon: "fa-headphones", Permission = "Settings:IVR")]
////Addon
//[assembly: NavigationLink(9105, "Settings/IndiaMART Configuration", typeof(IndiaMartConfigurationController), icon: "fa-maxcdn", Permission = "Settings:IndiaMART")]
////Addon
//[assembly: NavigationLink(9106, "Settings/JustDial Configuration", typeof(JustDialConfigurationController), icon: "fa-circle-o", Permission = "Settings:JustDial")]
////Addon
//[assembly: NavigationLink(9107, "Settings/TradeIndia Configuration", typeof(TradeIndiaConfigurationController), icon: "fa-tumblr", Permission = "Settings:TradeIndia")]
////Addon
//[assembly: NavigationLink(9108, "Settings/Facebook Configuration", typeof(FacebookConfigurationController), icon: "fa-facebook", Permission = "Settings:Facebook")]
////Addon
////[assembly: NavigationLink(9109, "Settings/MailChimp Configuration", typeof(MailChimpConfigurationController), icon: "fa-bullhorn", Permission = "Settings:MailChimp")]
//////Addon
//[assembly: NavigationLink(9110, "Settings/Interakt Configuration", typeof(InteraktConfigController), icon: "fa-info-circle", Permission = "Settings:Interakt")]

//[assembly: NavigationLink(9114, "Settings/RazorPay", typeof(RazorPayController), icon: "fa-credit-card", Permission = "Settings:RazorPay")]

////[assembly: NavigationLink(9110, "Settings/Whatsapp Configuration", typeof(WaConfigrationController), icon: "fa-brands fa-whatsapp", Permission = "Settings:WA")]
////[assembly: NavigationLink(9111, "Settings/BulkMail Configuration", typeof(BulkMailConfigController), icon: "fa-medium", Permission = "Settings:WA")]
//[assembly: NavigationLink(9111, "Settings/Instamojo Configuration", typeof(InstamojoController), icon: "fa-info", Permission = "Settings:WA")]

//[assembly: NavigationLink(9112, "Settings/Woocommerce Configuration", typeof(WoocommerceController), icon: "fa-wordpress", Permission = "Settings:WA")]
////[assembly: NavigationLink(9113, "Settings/Mail Configuration", typeof(MailInboxController), icon: "fa-medium", Permission = "Settings:MailInbox")]
////[assembly: NavigationLink(9114, "Settings/Wati Configuration", typeof(WatiConfigController), icon: "fa-wikipedia-w", Permission = "Settings:SMS")]
//[assembly: NavigationLink(9115, "Settings/BizMail Configuration", typeof(BizMailConfigController), icon: "fa-globe")]
//[assembly: NavigationLink(9116, "Settings/BizWA Configuration", typeof(BizWaConfigController), icon: "fa-globe")]
//[assembly: NavigationLink(9117, "Settings/Sulekha Configuration", typeof(SulekhaController), icon: "fa-superpowers")]

//[assembly: NavigationLink(9118, "Settings/Ticket Web Configuration", typeof(TicketWebController), icon: "fa-envelope", Permission = "Settings:SMS")]
//[assembly: NavigationLink(9119, "Settings/Ai Configuration", typeof(AiConfigurationController), icon: "fa-envelope", Permission = "Settings:Ai Configuration")]
// SaaS-only product/plan configuration screens are hidden in single-tenant mode
//[assembly: NavigationLink(9120, "Settings/Product Plans", typeof(ProductPlansController), icon: "fa-list", Permission = "Settings:ProductPlans")]
//[assembly: NavigationLink(9121, "Settings/Product Modules", typeof(ProductModulesController), icon: "fa-puzzle-piece", Permission = "Settings:ProductPlans")]
//[assembly: NavigationLink(9122, "Settings/Default Features", typeof(DefaultFeaturesPage), icon: "fa-star", Permission = "Settings:ProductPlans")]
//[assembly: NavigationLink(9123, "Settings/Module Pricing", typeof(ModulePricingController), icon: "fa-tags", Permission = "Settings:ProductPlans")]
//[assembly: NavigationLink(9124, "Settings/Coupon Codes", typeof(CouponCodesController), icon: "fa-ticket", Permission = "Settings:ProductPlans")]
