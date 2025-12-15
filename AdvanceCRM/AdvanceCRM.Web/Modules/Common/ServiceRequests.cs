
namespace AdvanceCRM
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Masters;
    using Serenity.Services;
    using System;
    using System.Collections.Generic;

    public class StandardRequest : ServiceRequest
    {
        public Int32 Id { get; set; }
    }

    public class BulkRequest : ServiceRequest
    {
        public string[] EnqIds { get; set; }
        public String[] Ids { get; set; }
    }

    public class SendMailRequest : ServiceRequest
    {
        public Int32 Id { get; set; }
        public String MailType { get; set; }
        public String EmailId { get; set; }
        public String Subject { get; set; }
        public String Message { get; set; }
    }

    public class SendSMSRequest : ServiceRequest
    {
        public Int32 Id { get; set; }
        public String Phone { get; set; }
        public String SMSType { get; set; }
        public Int32 EngineerID { get; set; }
        public String TemplateID { get; set; }
    }

    public class SendIntractSMSRequest : ServiceRequest
    {
        public Int32 Id { get; set; }
        public String Phone { get; set; }
        public String Variable { get; set; }
        public String Template { get; set; }
        public String ImageUrl { get; set; }

    }


    public class SendEmailRequest : ServiceRequest
    {
        public Int32 Id { get; set; }
        public String Email { get; set; }
        public String Subject { get; set; }
        public String EmailType { get; set; }
        public DateTime Senddate { get; set; }
    }
    public class CallRequest : ServiceRequest
    {
        public String IVRNumber { get; set; }
        public Int32 AgentNumber { get; set; }
        public String CustomerNumber { get; set; }
    }

    public class AddToContactsRequest : ServiceRequest
    {
        public String ContactType { get; set; }
        public String CompanyName { get; set; }
        public String ContactPerson { get; set; }
        public String Source { get; set; }
        public String Requirement { get; set; }
        public String Country { get; set; }
        public String Date { get; set; }
        public String Phone { get; set; }
        public String Address { get; set; }
        public String AdditionalInfo { get; set; }
        public String Email { get; set; }
    }

    public class TransferRequest : ServiceRequest
    {
        public String Type { get; set; }
        public Int32 FromID { get; set; }
        public Int32 ToID { get; set; }
    }

    //public class BulkMoveRequest : ServiceRequest
    //{
    //    public string[] UIds { get; set; }
    //}

    public class MailChimpRequest : ServiceRequest
    {
        public string[] MailChimpIds { get; set; }
        public string ListName { get; set; }
    }

    // Used by import dialogs that require assigning imported records to users
    public class ExcelImportWithUsersRequest : ServiceRequest
    {
        public String FileName { get; set; }
        public string[] UIds { get; set; }
    }
}