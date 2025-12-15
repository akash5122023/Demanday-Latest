using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class CMSModel
    {
        public int id { get; set; }
        public Int32 ProjectId { get; set; }       
        public DateTime Date { get; set; }        
        public Int32 TicketNo { get; set; }        
        public String SerialNo { get; set; }       
        public Int32 CMSNo { get; set; }
        public String CMSN{ get; set; }
        public String StatusValue { get; set; }
        public String CategoryValue { get; set; }
        public String ComplaintValue { get; set; }
        public String PriorityValue { get; set; }

        public String StageValue { get; set; }

        public String AssignedbyUsername { get; set; }
        public String AssignedtoUsername { get; set; }
        public String AssignedbyDisplayname { get; set; }
        public String AssignedToDisplayname { get; set; }
        public String Project { get; set; }
        public String Product { get; set; }
        public Int32 ContactsId { get; set; }      
        public String ContactsName { get; set; }
        public String ContactsPhone { get; set; }       
        public String ContactsAddress { get; set; }       
        public String ContactsEmail { get; set; }
        public Int32 DealerId { get; set; }
        public String DealerPhone { get; set; }
        public String DealerEmail { get; set; }
        public String DealerName { get; set; }
        public String DealerAddress { get; set; }
        public Int32 EmployeeId { get; set; }
        public String EmployeePhone { get; set; }
        public String EmployeeEmail { get; set; }
        public String EmployeeName { get; set; }
        public String EmployeeAddress { get; set; }
        public Int32 ProductsId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public String InvoiceNo { get; set; }
        public Int32 Priority { get; set; }       
        public Int32 Status { get; set; }
        public Int32 Category { get; set; }
        public Int32 StageId { get; set; }
        public Int32 BranchId { get; set; }
        public Double Amount { get; set; }
        public DateTime ExpectedCompletion { get; set; }
        public DateTime CompletionDate { get; set; }
        
        public String Feedback { get; set; }

        public String AdditionalInfo { get; set; }
        public String Image { get; set; }
        public Int32 ComplaintId { get; set; }
        public String Instructions { get; set; }
        public Int32 InvestigationBy { get; set; }
        public String Observation { get; set; }
        public Int32 ActionBy { get; set; }
        public String Action { get; set; }
        public Int32 SupervisedBy { get; set; }
        public String Comments { get; set; }
       
        public Int32 AssignedBy { get; set; }
        public Int32 AssignedTo { get; set; }


    }
}