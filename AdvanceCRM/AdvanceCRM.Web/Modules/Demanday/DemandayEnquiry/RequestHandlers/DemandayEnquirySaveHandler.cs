using Serenity;
using Serenity.Data;
using Serenity.Services;
using System;
using System.Data;
using System.Threading.Tasks;
using MyRequest = Serenity.Services.SaveRequest<AdvanceCRM.Demanday.DemandayEnquiryRow>;
using MyResponse = Serenity.Services.SaveResponse;
using MyRow = AdvanceCRM.Demanday.DemandayEnquiryRow;

namespace AdvanceCRM.Demanday
{
    public interface IDemandayEnquirySaveHandler : ISaveHandler<MyRow, MyRequest, MyResponse> {}

    public class DemandayEnquirySaveHandler : SaveRequestHandler<MyRow, MyRequest, MyResponse>, IDemandayEnquirySaveHandler
    {
        private readonly ISqlConnections sqlConnections;
        public DemandayEnquirySaveHandler(IRequestContext context, ISqlConnections sqlConnections)
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
                var demandayteamLeader = new DemandayTeamLeaderRow
                {
                    //EnquiryID = Row.EnquiryId,
                    CompanyName = Row.CompanyName,
                    FirstName = Row.FirstName,
                    LastName = Row.LastName,
                    Title = Row.Title,
                    Email = Row.Email,
                    WorkPhone = Row.WorkPhone,
                    AlternativeNumber = Row.AlternativeNumber,
                    Street = Row.Street,
                    City = Row.City,
                    State = Row.State,
                    ZipCode = Row.ZipCode,
                    Country = Row.Country,
                    Industry = Row.Industry,
                    Revenue = Row.Revenue,
                    CompanyEmployeeSize = Row.CompanyEmployeeSize,
                    ProfileLink = Row.ProfileLink,
                    CompanyLink = Row.CompanyLink,
                    RevenueLink = Row.RevenueLink,
                    AddressLink = Row.AdressLink,
                    Tenurity = Row.Tenurity,
                    Code = Row.Code,
                    Link = Row.Link,
                    Md5 = Row.Md5,
                    OwnerId = Row.OwnerId
                };

                var demandayteamleaderConn = sqlConnections.NewFor<DemandayTeamLeaderRow>();
                demandayteamleaderConn.Insert(demandayteamLeader);

                Task.Run(() =>
                {
                    using var newConn = sqlConnections.NewFor<DemandayEnquiryRow>();
                    try
                    {
                        newConn.DeleteById<DemandayEnquiryRow>(Row.Id.Value);
                    }
                    catch (Exception ex)
                    {
                        // Log error if deletion fails
                        Console.WriteLine("Error deleting Enquiry: " + ex.Message);
                    }
                });
            }
        }
    }
}