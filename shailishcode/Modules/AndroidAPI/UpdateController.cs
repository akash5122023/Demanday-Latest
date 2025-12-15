using AdvanceCRM.ThirdParty;
using AdvanceCRM.Services;
using Serenity.Data;
using System;
using System.Web.Http;

namespace AdvanceCRM.Modules.AndroidAPI
{
    public class UpdateController : ApiController
    {
        private readonly ISqlConnections _connections;

        public UpdateController(ISqlConnections connections)
        {
            _connections = connections;
        }
        [HttpPost]
        public string CMS(int CMSId, string Date, string ProjectId, string ContactsId, int ComplaintId, string ExDate, string SerialNo, string ProductsId, int AssignedBy, int AssignedTo, int Category, int Status, string AdditionalInfo, string CMSN, int CMSNo, string ComDate)
        {
            //try
            //{
            //     [Date],[ProductsId],[SerialNo] ,[ComplaintId],[Category],[Amount],[ExpectedCompletion],[AssignedBy],[AssignedTo],[Instructions],[BranchId]
            //,[Status],[CompletionDate],[Feedback],[AdditionalInfo],[Image],[Phone],[Address],[StageId],[Priority],[Attachment],[PMRClosed],[InvestigationBy],[ActionBy]
            //,[SupervisedBy] ,[Observation],[Action],[Comments],[CMSNo],[DealerId],[PurchaseDate],[InvoiceNo],[EmployeeId],[ProjectId],[CMSN],[TicketNo]
            string str = string.Empty;
            if (ProjectId !=null)
            {
                if(ContactsId!=null)
                {
                    if(ProductsId!=null)
                    {
                        str = "update CMS set [Date]='" + Date + "',[ProjectId]='" + ProjectId + "',[ContactsId]='" + ContactsId + "',[ComplaintId]='" + ComplaintId + "',[ExpectedCompletion]='" + ExDate + "',[SerialNo]='" + SerialNo + "',[ProductsId]='" + ProductsId + "',[AssignedBy]='" + AssignedBy + "',[AssignedTo]='" + AssignedTo + "',[Category]='" + Category + "',[Status]='" + Status + "',[AdditionalInfo]='" + AdditionalInfo + "',[CMSN]='" + CMSN + "',[CMSNo]='" + CMSNo + "',[CompletionDate]='" + ComDate + "' where Id=" + CMSId;
                    }
                    else 
                    {
                        str = "update CMS set [Date]='" + Date + "',[ProjectId]='" + ProjectId + "',[ContactsId]='" + ContactsId + "',[ComplaintId]='" + ComplaintId + "',[ExpectedCompletion]='" + ExDate + "',[SerialNo]='" + SerialNo + "',[AssignedBy]='" + AssignedBy + "',[AssignedTo]='" + AssignedTo + "',[Category]='" + Category + "',[Status]='" + Status + "',[AdditionalInfo]='" + AdditionalInfo + "',[CMSN]='" + CMSN + "',[CMSNo]='" + CMSNo + "',[CompletionDate]='" + ComDate + "' where Id=" + CMSId;
                    }
                }
                else
                {
                    if (ProductsId != null)
                    {
                        str = "update CMS set [Date]='" + Date + "',[ProjectId]='" + ProjectId + "',[ComplaintId]='" + ComplaintId + "',[ExpectedCompletion]='" + ExDate + "',[SerialNo]='" + SerialNo + "',[ProductsId]='" + ProductsId + "',[AssignedBy]='" + AssignedBy + "',[AssignedTo]='" + AssignedTo + "',[Category]='" + Category + "',[Status]='" + Status + "',[AdditionalInfo]='" + AdditionalInfo + "',[CMSN]='" + CMSN + "',[CMSNo]='" + CMSNo + "',[CompletionDate]='" + ComDate + "' where Id=" + CMSId;
                    }
                    else
                    {
                        str = "update CMS set [Date]='" + Date + "',[ProjectId]='" + ProjectId + "',[ComplaintId]='" + ComplaintId + "',[ExpectedCompletion]='" + ExDate + "',[SerialNo]='" + SerialNo + "',[AssignedBy]='" + AssignedBy + "',[AssignedTo]='" + AssignedTo + "',[Category]='" + Category + "',[Status]='" + Status + "',[AdditionalInfo]='" + AdditionalInfo + "',[CMSN]='" + CMSN + "',[CMSNo]='" + CMSNo + "',[CompletionDate]='" + ComDate + "' where Id=" + CMSId;
                    }

                }
            }
            else
            {
                if (ContactsId != null)
                {
                    if(ProductsId!=null)
                    {
                        str = "update CMS set [Date]='" + Date + "',[ContactsId]='" + ContactsId + "',[ComplaintId]='" + ComplaintId + "',[ExpectedCompletion]='" + ExDate + "',[SerialNo]='" + SerialNo + "',[ProductsId]='" + ProductsId + "',[AssignedBy]='" + AssignedBy + "',[AssignedTo]='" + AssignedTo + "',[Category]='" + Category + "',[Status]='" + Status + "',[AdditionalInfo]='" + AdditionalInfo + "',[CMSN]='" + CMSN + "',[CMSNo]='" + CMSNo + "',[CompletionDate]='" + ComDate + "' where Id=" + CMSId;
                    }
                    else
                    {
                        str = "update CMS set [Date]='" + Date + "',[ContactsId]='" + ContactsId + "',[ComplaintId]='" + ComplaintId + "',[ExpectedCompletion]='" + ExDate + "',[SerialNo]='" + SerialNo + "',[AssignedBy]='" + AssignedBy + "',[AssignedTo]='" + AssignedTo + "',[Category]='" + Category + "',[Status]='" + Status + "',[AdditionalInfo]='" + AdditionalInfo + "',[CMSN]='" + CMSN + "',[CMSNo]='" + CMSNo + "',[CompletionDate]='" + ComDate + "' where Id=" + CMSId;
                    }
                }
                else
                {
                    if (ProductsId != null)
                    {
                        str = "update CMS set [Date]='" + Date + "',[ComplaintId]='" + ComplaintId + "',[ExpectedCompletion]='" + ExDate + "',[SerialNo]='" + SerialNo + "',[ProductsId]='" + ProductsId + "',[AssignedBy]='" + AssignedBy + "',[AssignedTo]='" + AssignedTo + "',[Category]='" + Category + "',[Status]='" + Status + "',[AdditionalInfo]='" + AdditionalInfo + "',[CMSN]='" + CMSN + "',[CMSNo]='" + CMSNo + "',[CompletionDate]='" + ComDate + "' where Id=" + CMSId;
                    }
                    else
                    {
                        str = "update CMS set [Date]='" + Date + "',[ComplaintId]='" + ComplaintId + "',[ExpectedCompletion]='" + ExDate + "',[SerialNo]='" + SerialNo + "',[AssignedBy]='" + AssignedBy + "',[AssignedTo]='" + AssignedTo + "',[Category]='" + Category + "',[Status]='" + Status + "',[AdditionalInfo]='" + AdditionalInfo + "',[CMSN]='" + CMSN + "',[CMSNo]='" + CMSNo + "',[CompletionDate]='" + ComDate + "' where Id=" + CMSId;
                    }
                }
              
            }
            using (var innerConnection = _connections.NewFor<CMSRow>())
            {
                innerConnection.Execute(str);
            }

            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            return "CMS Updated Successfully";
        }




        [HttpPost]
        public string Visit(int visitId,String VisitDate, String CompanyName, String ContactPerson, String MobileNumber, String EmailId, String CompanyAddress, String Location, String Reason, String Purpose, String FileName, int UserId)
        {
           // try
            {
                //var datetimestr = date + " " + time;
                // DateTime datetime = DateTime.ParseExact(VisitDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                string str = "Update Visit set [DateNTime]='" + VisitDate + "', [CompanyName]='" + CompanyName + "', [Name]='" + ContactPerson + "', [MobileNo]='" + MobileNumber + "', [Email]= '" + EmailId + "', [Address]= '" + CompanyAddress + "',[Location]= '" + Location + "', [Requirements]='" + Reason + "', [Purpose]='" + Purpose + "', [Attachments]= '" + FileName + "',[CreatedBy]= " + UserId + ",[IsMoved]=0 where Id=" + visitId;
       
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

            return "Successfully Updated Visit Record";
        }



    }
}