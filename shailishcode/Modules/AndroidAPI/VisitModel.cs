using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class VisitModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string CompanyAddress { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string Location { get; set; }
        public DateTime VisitDate { get; set; }
        public string Reason { get; set; }
        public string Purpose { get; set; }
        public string FileName { get; set; }
        public string Feedback { get; set; }
        public byte IsMoved { get; set; }

        public int UserId { get; set; }
    }
}