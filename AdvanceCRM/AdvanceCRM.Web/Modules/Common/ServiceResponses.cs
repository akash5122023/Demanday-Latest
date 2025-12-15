
namespace AdvanceCRM
{
    using Serenity.Services;
    using System;
    using System.Collections.Generic;

    public class StandardResponse : ServiceResponse
    {
        public int Id { get; set; }
        public String Status { get; set; }
    }

    public class BulkMailResponse : ServiceResponse
    {
        public string Ids { get; set; }
       
    }

    public class SendMailResponse : ServiceResponse
    {
        public String Status { get; set; }
        public List<string> ErrorList { get; set; }
        public int Id { get; set; }
    }

    public class AddToContactsResponse : ServiceResponse
    {
        public Int32 EnquriyId { get; set; }
        public String Status { get; set; }
        public String ReturnType { get; set; }
    }

    public class MailChimpResponse : ServiceResponse
    {
        public string MailChimpReturnResponse { get; set; }
    }
    public class BulkImportResponse : ServiceResponse
    {
        public int Inserted { get; set; }
        public int Updated { get; set; }
        public String Status { get; set; }
        public List<string> ErrorList { get; set; }
    }
    //public class ExcelImportResponse : ServiceResponse
    //{
    //    public int Inserted { get; set; }
    //    public int Updated { get; set; }
    //    public List<string> ErrorList { get; set; }
    //}
}