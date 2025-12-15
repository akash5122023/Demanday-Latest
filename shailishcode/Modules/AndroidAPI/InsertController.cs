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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.UI;
using System.IO;
//using System.Web.UI.WebControls;
using Microsoft.AspNetCore.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;


namespace AdvanceCRM.Modules.AndroidAPI
{
    public class InsertController : ApiController
    {
        private readonly ISqlConnections _connections;

        public InsertController(ISqlConnections connections)
        {
            _connections = connections;
        }
        [System.Web.Http.HttpPost]
        public string Quotation(string Date, int Contact, int Source, int status, int Stage, int Owner, int Assign, string Description, int QuotationNo, string QuotationN)
        {
            // try
            {
                string str = "Insert into Quotation([ContactsId],[Date],[Status],[SourceId],[StageId],[OwnerId],[AssignedId],[AdditionalInfo],[QuotationNo],[QuotationN]) values " +
                    "(" + Contact + ",'" + Date + "'," + status + "," + Source + "," + Stage + "," + Owner + "," + Assign + "," + Description + "," + QuotationNo + "," + QuotationN + ")";
                using (var innerConnection = _connections.NewFor<QuotationRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

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
        public string CMS(string Date, string ProjectId, string ContactsId, int ComplaintId,string ExDate,string SerialNo, string ProductsId, int AssignedBy, int AssignedTo,int Category,int Status,string AdditionalInfo,string CMSN,int CMSNo)
        {
            //try
            //{
            //     [Date],[ProductsId],[SerialNo] ,[ComplaintId],[Category],[Amount],[ExpectedCompletion],[AssignedBy],[AssignedTo],[Instructions],[BranchId]
            //,[Status],[CompletionDate],[Feedback],[AdditionalInfo],[Image],[Phone],[Address],[StageId],[Priority],[Attachment],[PMRClosed],[InvestigationBy],[ActionBy]
            //,[SupervisedBy] ,[Observation],[Action],[Comments],[CMSNo],[DealerId],[PurchaseDate],[InvoiceNo],[EmployeeId],[ProjectId],[CMSN],[TicketNo]
               string str = string.Empty;
           
                if (ProjectId !=null)
                {
                   if(ContactsId != null)
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
                            if(ProductsId != null)
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
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "CMS added Successfully";
        }


        

        //[HttpPost]
        //public string Upload(HttpPostedFileBase postedFile)
        //{
        //    if (postedFile != null)
        //    {
        //       // string path = .MapPath("~/Uploads/");
        //        if (!Directory.Exists("Uploads/Visit/"))
        //        {
        //            Directory.CreateDirectory("Uploads/Visit/");
        //        }

        //        postedFile.SaveAs("Uploads/Visit/" + Path.GetFileName(postedFile.FileName));

        //    }

        //    return "File uploaded successfully.";
        //}


        [HttpPost]
        public string Visit(String VisitDate, String CompanyName, String ContactPerson, String MobileNumber, String EmailId, String CompanyAddress, String Location, String Reason, String Purpose, String FileName, int UserId)
        {
           // try
            {
                // string folderPath = Server.MapPath("~/Images/");
                //var datetimestr = date + " " + time;
                // DateTime datetime = DateTime.ParseExact(VisitDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                //if (FileName != null)
                //{
                //    // string path = .MapPath("~/Uploads/");
                //    if (!Directory.Exists("Uploads/Visit/"))
                //    {
                //        Directory.CreateDirectory("Uploads/Visit/");
                //    }

                //    FileName.SaveAs("Uploads/Visit/" + Path.GetFileName(FileName.FileName));

                //}

              //  FileUpload fileUpload = new FileUpload();
                //fileUpload.FileName = "abcd";"Uploads/Visit/" + Path.GetFileName(FileName.FileName)
                string abc = "";
              //  fileUpload.SaveAs("/Images/" + CompanyName + "_" + VisitDate);

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
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //    return "Error While Inserting Visit Details";
            //}

            return "Successfully Added Visit Record";
        }

        //[HttpPost]
        //public string EnqUpClose(int EnquiryId, string Date, int Closure)
        //{
        //    // try
        //    {
        //        string str = "Update Enquiry Set [Status]=2,[ClosingType]=2,[ClosureId]='" + Closure + "',[ClosingDate]='" + Date + "' where Id=" + EnquiryId;
        //        //string str = "Update Enquiry([Status],[ClosingDate]) values " +"(2,'"+Date+"') where 
        //        using (var innerConnection = _connections.NewFor<EnquiryRow>())
        //        {
        //            innerConnection.Execute(str);
        //        }
        //    }
        //    return "Enquiry Status Updated Successfully";
        //}

        //[HttpPost]
        //public string EnquiryUpdate(int EnquiryId, string Date)
        //{
        //    // try
        //    {
        //        string str = "Update Enquiry Set [Status]=2,[ClosingType]=1,[ClosingDate]='" + Date + "' where Id=" + EnquiryId;
        //        //string str = "Update Enquiry([Status],[ClosingDate]) values " +"(2,'"+Date+"') where 
        //        using (var innerConnection = _connections.NewFor<EnquiryRow>())
        //        {
        //            innerConnection.Execute(str);
        //        }
        //    }
        //    return "Enquiry Status Updated Successfully";
        //}

        [HttpPost]
        public string TelecallUpdate(int TelecallId, string Feedback)
        {
            // try
            {
                string str = "Update RawTelecall Set [Feedback]='" + Feedback + "' where Id=" + TelecallId;               
                using (var innerConnection = _connections.NewFor<RawTelecallRow>())
                {
                    innerConnection.Execute(str);
                }
            }
           

            return "Telecall Record Updated Successfully";
        }

        [HttpPost]
        public string EnqFupUpdate(int EnqFupId, string Date)
        {
            // try
            {
                string str = "Update EnquiryFollowups Set [Status]=2,[ClosingDate]='" + Date + "' where Id=" + EnqFupId;
                //string str = "Update Enquiry([Status],[ClosingDate]) values " +"(2,'"+Date+"') where 
                using (var innerConnection = _connections.NewFor<EnquiryFollowupsRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "Enquiry-Followup Status Updated Successfully";
        }

        public string QuoFupUpdate(int QuoFupId, string Date)
        {
            // try
            {
                string str = "Update QuotationFollowups Set [Status]=2,[ClosingDate]='" + Date + "' where Id=" + QuoFupId;
                //string str = "Update Enquiry([Status],[ClosingDate]) values " +"(2,'"+Date+"') where 
                using (var innerConnection = _connections.NewFor<QuotationFollowupsRow>())
                {
                    innerConnection.Execute(str);
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "Quotation-Followup Status Updated Successfully";
        }


    }
}