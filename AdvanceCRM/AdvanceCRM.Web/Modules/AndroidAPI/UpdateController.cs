using AdvanceCRM.Services;
using AdvanceCRM.ThirdParty;
using Microsoft.AspNetCore.Mvc;
using Serenity.Data;

namespace AdvanceCRM.Modules.AndroidAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly ISqlConnections _connections;

        public UpdateController(ISqlConnections connections)
        {
            _connections = connections;
        }

        [HttpPost]
        public string CMS(int CMSId, string Date, string ProjectId, string ContactsId, int ComplaintId, string ExDate, string SerialNo, string ProductsId, int AssignedBy, int AssignedTo, int Category, int Status, string AdditionalInfo, string CMSN, int CMSNo, string ComDate)
        {
            string str = string.Empty;

            if (ProjectId != null)
            {
                if (ContactsId != null)
                {
                    if (ProductsId != null)
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
                    if (ProductsId != null)
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

            return "CMS Updated Successfully";
        }

        [HttpPost]
        public string Visit(int visitId, string VisitDate, string CompanyName, string ContactPerson, string MobileNumber, string EmailId, string CompanyAddress, string Location, string Reason, string Purpose, string FileName, int UserId)
        {
            string str = "Update Visit set [DateNTime]='" + VisitDate + "', [CompanyName]='" + CompanyName + "', [Name]='" + ContactPerson + "', [MobileNo]='" + MobileNumber + "', [Email]= '" + EmailId + "', [Address]= '" + CompanyAddress + "',[Location]= '" + Location + "', [Requirements]='" + Reason + "', [Purpose]='" + Purpose + "', [Attachments]= '" + FileName + "',[CreatedBy]= " + UserId + ",[IsMoved]=0 where Id=" + visitId;

            using (var innerConnection = _connections.NewFor<VisitRow>())
            {
                innerConnection.Execute(str);
            }

            return "Successfully Updated Visit Record";
        }
    }
}