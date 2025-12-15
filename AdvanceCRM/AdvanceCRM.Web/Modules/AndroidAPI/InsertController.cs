using AdvanceCRM.Settings;
using AdvanceCRM.ThirdParty;
using AdvanceCRM.Attendance;
using AdvanceCRM.Contacts;
using AdvanceCRM.Quotation;
using AdvanceCRM.Enquiry;
using AdvanceCRM.Products;
using AdvanceCRM.Services;
using AdvanceCRM.Enquiry.Endpoints;
using AdvanceCRM.Administration;
using Serenity.Data;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceCRM.Modules.AndroidAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsertController : ControllerBase
    {
        private readonly ISqlConnections _connections;

        public InsertController(ISqlConnections connections)
        {
            _connections = connections;
        }

        [HttpPost]
        public string Quotation(string Date, int Contact, int Source, int status, int Stage, int Owner, int Assign, string Description, int QuotationNo, string QuotationN)
        {
            string str = "Insert into Quotation([ContactsId],[Date],[Status],[SourceId],[StageId],[OwnerId],[AssignedId],[AdditionalInfo],[QuotationNo],[QuotationN]) values " +
                "(" + Contact + ",'" + Date + "'," + status + "," + Source + "," + Stage + "," + Owner + "," + Assign + "," + Description + "," + QuotationNo + "," + QuotationN + ")";
            using (var innerConnection = _connections.NewFor<QuotationRow>())
            {
                innerConnection.Execute(str);
            }

            return "Quotation added Successfully";
        }

        [HttpPost]
        public string QuotationProducts(int Product, double Quantity, double MRP, double SellingPrice, double Price, double Discount, string Tax1, double Percentage1, string Tax2, double Percentage2, int QuotationId, double DisAmt)
        {
            try
            {
                string str = "Insert into QuotationProducts([ProductsId],[Quantity],[MRP],[SellingPrice],[Price],[Discount],[TaxType1],[Percentage1],[TaxType2],[Percentage2],[QuotationId],[DiscountAmount]) values " +
                    "('" + Product + "','" + Quantity + "','" + MRP + "','" + SellingPrice + "','" + Price + "','" + Discount + "','" + Tax1 + "','" + Percentage1 + "','" + Tax2 + "','" + Percentage2 + "','" + QuotationId + "','" + DisAmt + "')";
                using (var innerConnection = _connections.NewFor<QuotationProductsRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "Quotation added Successfully";
        }

        [HttpPost]
        public string CMS(string Date, string ProjectId, string ContactsId, int ComplaintId, string ExDate, string SerialNo, string ProductsId, int AssignedBy, int AssignedTo, int Category, int Status, string AdditionalInfo, string CMSN, int CMSNo)
        {
            string str = string.Empty;

            if (ProjectId != null)
            {
                if (ContactsId != null)
                {
                    if (ProductsId != null)
                    {
                        str = "Insert into CMS([Date],[ProjectId],[ContactsId],[ComplaintId],[ExpectedCompletion],[SerialNo],[ProductsId],[AssignedBy],[AssignedTo],[Category],[Status],[AdditionalInfo],[CMSN],[CMSNo]) values " +
                         "('" + Date + "','" + ProjectId + "','" + ContactsId + "','" + ComplaintId + "','" + ExDate + "','" + SerialNo + "','" + ProductsId + "','" + AssignedBy + "','" + AssignedTo + "','" + Category + "','" + Status + "','" + AdditionalInfo + "','" + CMSN + "'," + CMSNo + ")";
                    }
                    else
                    {
                        str = "Insert into CMS([Date],[ProjectId],[ContactsId],[ComplaintId],[ExpectedCompletion],[SerialNo],[AssignedBy],[AssignedTo],[Category],[Status],[AdditionalInfo],[CMSN],[CMSNo]) values " +
                         "('" + Date + "','" + ProjectId + "','" + ContactsId + "','" + ComplaintId + "','" + ExDate + "','" + SerialNo + "','" + AssignedBy + "','" + AssignedTo + "','" + Category + "','" + Status + "','" + AdditionalInfo + "','" + CMSN + "'," + CMSNo + ")";
                    }
                }
                else
                {
                    if (ProductsId != null)
                    {
                        str = "Insert into CMS([Date],[ProjectId],[ComplaintId],[ExpectedCompletion],[SerialNo],[ProductsId],[AssignedBy],[AssignedTo],[Category],[Status],[AdditionalInfo],[CMSN],[CMSNo]) values " +
                             "('" + Date + "','" + ProjectId + "','" + ComplaintId + "','" + ExDate + "','" + SerialNo + "','" + ProductsId + "','" + AssignedBy + "','" + AssignedTo + "','" + Category + "','" + Status + "','" + AdditionalInfo + "','" + CMSN + "'," + CMSNo + ")";

                    }
                    else
                    {
                        str = "Insert into CMS([Date],[ProjectId],[ComplaintId],[ExpectedCompletion],[SerialNo],[AssignedBy],[AssignedTo],[Category],[Status],[AdditionalInfo],[CMSN],[CMSNo]) values " +
                        "('" + Date + "','" + ProjectId + "','" + ComplaintId + "','" + ExDate + "','" + SerialNo + "','" + AssignedBy + "','" + AssignedTo + "','" + Category + "','" + Status + "','" + AdditionalInfo + "','" + CMSN + "'," + CMSNo + ")";
                    }
                }
            }
            else
            {
                if (ContactsId != null)
                {
                    if (ProductsId != null)
                    {
                        str = "Insert into CMS([Date],[ContactsId],[ComplaintId],[ExpectedCompletion],[SerialNo],[ProductsId],[AssignedBy],[AssignedTo],[Category],[Status],[AdditionalInfo],[CMSN],[CMSNo]) values " +
                         "('" + Date + "','" + ContactsId + "','" + ComplaintId + "','" + ExDate + "','" + SerialNo + "','" + ProductsId + "','" + AssignedBy + "','" + AssignedTo + "','" + Category + "','" + Status + "','" + AdditionalInfo + "','" + CMSN + "'," + CMSNo + ")";
                    }
                    else
                    {
                        str = "Insert into CMS([Date],[ContactsId],[ComplaintId],[ExpectedCompletion],[SerialNo],[AssignedBy],[AssignedTo],[Category],[Status],[AdditionalInfo],[CMSN],[CMSNo]) values " +
                         "('" + Date + "','" + ContactsId + "','" + ComplaintId + "','" + ExDate + "','" + SerialNo + "','" + AssignedBy + "','" + AssignedTo + "','" + Category + "','" + Status + "','" + AdditionalInfo + "','" + CMSN + "'," + CMSNo + ")";
                    }
                }
                else
                {
                    if (ProductsId != null)
                    {
                        str = "Insert into CMS([Date],[ComplaintId],[ExpectedCompletion],[SerialNo],[ProductsId],[AssignedBy],[AssignedTo],[Category],[Status],[AdditionalInfo],[CMSN],[CMSNo]) values " +
                        "('" + Date + "','" + ComplaintId + "','" + ExDate + "','" + SerialNo + "','" + ProductsId + "','" + AssignedBy + "','" + AssignedTo + "','" + Category + "','" + Status + "','" + AdditionalInfo + "','" + CMSN + "'," + CMSNo + ")";
                    }
                    else
                    {
                        str = "Insert into CMS([Date],[ComplaintId],[ExpectedCompletion],[SerialNo],[AssignedBy],[AssignedTo],[Category],[Status],[AdditionalInfo],[CMSN],[CMSNo]) values " +
                        "('" + Date + "','" + ComplaintId + "','" + ExDate + "','" + SerialNo + "','" + AssignedBy + "','" + AssignedTo + "','" + Category + "','" + Status + "','" + AdditionalInfo + "','" + CMSN + "'," + CMSNo + ")";
                    }
                }
            }

            using (var innerConnection = _connections.NewFor<CMSRow>())
            {
                innerConnection.Execute(str);
            }

            return "CMS added Successfully";
        }

        [HttpPost]
        public string Visit(string VisitDate, string CompanyName, string ContactPerson, string MobileNumber, string EmailId, string CompanyAddress, string Location, string Reason, string Purpose, string FileName, int UserId)
        {
            string abc = "";
            string str = "INSERT INTO Visit ([DateNTime], [CompanyName], [Name], [MobileNo], [Email], [Address],[Location], [Requirements], [Purpose], [Attachments],[CreatedBy],[IsMoved]) Values " +
            "('" + VisitDate + "','"
             + CompanyName + "','"
             + ContactPerson + "','"
             + MobileNumber + "','"
             + EmailId + "','"
             + CompanyAddress + "','"
             + Location + "','"
             + Reason + "','"
             + Purpose + "','"
             + abc + "'," +
              +UserId + ",0)";

            using (var innerConnection = _connections.NewFor<VisitRow>())
            {
                innerConnection.Execute(str);
            }

            return "Successfully Added Visit Record";
        }

        [HttpPost]
        public string TelecallUpdate(int TelecallId, string Feedback)
        {
            string str = "Update RawTelecall Set [Feedback]='" + Feedback + "' where Id=" + TelecallId;
            using (var innerConnection = _connections.NewFor<RawTelecallRow>())
            {
                innerConnection.Execute(str);
            }

            return "Telecall Record Updated Successfully";
        }

        [HttpPost]
        public string EnqFupUpdate(int EnqFupId, string Date)
        {
            string str = "Update EnquiryFollowups Set [Status]=2,[ClosingDate]='" + Date + "' where Id=" + EnqFupId;
            using (var innerConnection = _connections.NewFor<EnquiryFollowupsRow>())
            {
                innerConnection.Execute(str);
            }

            return "Enquiry-Followup Status Updated Successfully";
        }

        [HttpPost]
        public string QuoFupUpdate(int QuoFupId, string Date)
        {
            string str = "Update QuotationFollowups Set [Status]=2,[ClosingDate]='" + Date + "' where Id=" + QuoFupId;
            using (var innerConnection = _connections.NewFor<QuotationFollowupsRow>())
            {
                innerConnection.Execute(str);
            }

            return "Quotation-Followup Status Updated Successfully";
        }
    }
}