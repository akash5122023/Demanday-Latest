using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using System.Threading.Tasks;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayTeleMarketingEnquirySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayTeleMarketingEnquirySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayTeleMarketingEnquirySaveHandler
    {
        private readonly ISqlConnections sqlConnections;
        public DemandayTeleMarketingEnquirySaveHandler(IRequestContext context, ISqlConnections sqlConnections)
             : base(context)
        {
            this.sqlConnections = sqlConnections;
        }
        protected override void BeforeSave()
        {
            base.BeforeSave();

            // For Create only
            if (IsCreate)
                Row.OwnerId = int.Parse(User.GetIdentifier());

        }
        protected override void AfterSave()
        {
            base.AfterSave();
            if (IsCreate)
            {
                // Insert into TeamLeader table
                var demandaytelemarketingteamleader = new DemandayTeleMarketingTeamLeaderRow
                {
                    //EnquiryID = Row.EnquiryId,
                    CompanyName = Row.CompanyName,
                    FirstName = Row.FirstName,
                    LastName = Row.LastName,
                    Title = Row.Title,
                    Email = Row.Email,
                    WorkPhone = Row.WorkPhone,
                    CampaignId = Row.CampaignId,
                    AlternativeNumber = Row.AlternativeNumber,
                    Street = Row.Street,
                    City = Row.City,
                    State = Row.State,
                    ZipCode = Row.ZipCode,
                    Country = Row.Country,
                    Industry = Row.Industry,
                    SubIndustry = Row.SubIndustry,
                    Revenue = Row.Revenue,
                    CompanyEmployeeSize = Row.CompanyEmployeeSize,
                    ZoomInfoIndustry = Row.ZoomInfoIndustry,
                    Date = Row.Date,
                    ZoomInfoEmployeeSize = Row.ZoomInfoEmployeeSize,
                    CallStatus = Row.CallStatus,
                    AdditionalNotes = Row.AdditionalNotes,
                    Asset = Row.Asset,
                    ProfileLink = Row.ProfileLink,
                    CompanyLink = Row.CompanyLink,
                    RevenueLink = Row.RevenueLink,
                    AddressLink = Row.AddressLink,
                    Tenurity = Row.Tenurity,
                    Code = Row.Code,
                    Link = Row.Link,
                    Md5 = Row.Md5,
                    Attachments = Row.Attachments,
                    OwnerId = Row.OwnerId
                };

                // Insert TeamLeader on a separate connection and retrieve the auto-generated Id
                using var teamLeaderConn = sqlConnections.NewFor<DemandayTeleMarketingTeamLeaderRow>();
                teamLeaderConn.Insert(demandaytelemarketingteamleader);

                // Serenity Insert does not populate identity; retrieve via @@IDENTITY
                // @@IDENTITY is connection-scoped (safe on this dedicated connection)
                using (var cmd = teamLeaderConn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CAST(@@IDENTITY AS INT)";
                    var newId = cmd.ExecuteScalar();
                    if (newId != null && newId != DBNull.Value)
                        demandaytelemarketingteamleader.Id = Convert.ToInt32(newId);
                }

                // Copy QADetails using the current Connection (same transaction) to avoid deadlock
                var qaFlds = DemandayTeleMarketingEnquiryQADetailsRow.Fields;
                var qaDetails = Connection.List<DemandayTeleMarketingEnquiryQADetailsRow>(q =>
                    q.Select(qaFlds.Id, qaFlds.EnquiryId, qaFlds.QuestionId, qaFlds.AnswerId)
                     .Where(qaFlds.EnquiryId == Row.Id.Value));

                foreach (var qa in qaDetails)
                {
                    Connection.Insert(new DemandayTeleMarketingEnquiryQADetailsRow
                    {
                        EnquiryId = demandaytelemarketingteamleader.Id,
                        QuestionId = qa.QuestionId,
                        AnswerId = qa.AnswerId
                    });
                    Connection.DeleteById<DemandayTeleMarketingEnquiryQADetailsRow>(qa.Id.Value);
                }

                Connection.DeleteById<DemandayTeleMarketingEnquiryRow>(Row.Id.Value);
            }
        }
    }
}