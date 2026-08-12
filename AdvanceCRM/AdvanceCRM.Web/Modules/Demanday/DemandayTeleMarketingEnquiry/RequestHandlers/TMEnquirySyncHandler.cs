using Serenity.Data;
using System;
using System.Data;
using AdvanceCRM.Toolkit;
using MyRow = AdvanceCRM.Demanday.DemandayTeleMarketingEnquiryRow;

namespace AdvanceCRM.Demanday
{
    /// <summary>
    /// Copies a TM Enquiry into ToolkitTMEnquiry at the moment it is moved to Team Leader.
    /// The move deletes the enquiry, so the copy is keyed on the Team Leader record it became.
    /// </summary>
    public interface ITMEnquirySyncHandler
    {
        void SyncToTMEnquiry(IDbConnection connection, MyRow enquiry, int teamLeaderId);
    }

    public class TMEnquirySyncHandler : ITMEnquirySyncHandler
    {
        public void SyncToTMEnquiry(IDbConnection connection, MyRow enquiry, int teamLeaderId)
        {
            if (enquiry == null || teamLeaderId <= 0)
                return;

            var fld = ToolkitTMEnquiryRow.Fields;

            // Only SrNo is selected - TryFirst does not select any field on its own, and
            // selecting the whole row would also pull in the joined expression fields.
            var existing = connection.TryFirst<ToolkitTMEnquiryRow>(q => q
                .Select(fld.SrNo)
                .Where(fld.TeamLeaderId == teamLeaderId));

            var row = new ToolkitTMEnquiryRow
            {
                MasterAccountId = enquiry.MasterAccountId,
                CampaignId = ResolveCampaignId(connection, enquiry.MasterAccountId, enquiry.CampaignId),
                FirstName = enquiry.FirstName,
                LastName = enquiry.LastName,
                Email = enquiry.Email,
                CompanyName = enquiry.CompanyName,
                Timestamp = enquiry.Date ?? DateTime.Now
            };

            if (existing != null)
            {
                // Re-running the move for the same Team Leader record refreshes it rather than
                // leaving a second copy behind.
                row.SrNo = existing.SrNo;
                row.UpdatedOn = DateTime.Now;
                row.UpdatedBy = "MOVE";
                connection.UpdateById(row);
            }
            else
            {
                row.TeamLeaderId = teamLeaderId;
                row.CreatedBy = "MOVE";
                // CreatedOn is left to the database default.
                connection.Insert(row);
            }
        }

        /// <summary>
        /// Clears the Team Leader link on the toolkit copies of a Team Leader record that is
        /// about to disappear - moved on to Quality, or deleted outright. The copy itself is the
        /// toolkit's own data and stays; only the back-reference goes.
        ///
        /// Without this the delete fails on FK_ToolkitTMEnquiry_TeamLeaderId. The link only ever
        /// existed so that re-running the same move refreshes the copy instead of duplicating it
        /// (see SyncToTMEnquiry), and that meaning ends with the Team Leader record.
        /// </summary>
        public static void UnlinkTeamLeader(IDbConnection connection, int teamLeaderId)
        {
            if (connection == null || teamLeaderId <= 0)
                return;

            var fld = ToolkitTMEnquiryRow.Fields;
            new SqlUpdate(fld.TableName)
                .SetNull(fld.TeamLeaderId.Name)
                .Where(fld.TeamLeaderId == teamLeaderId)
                .Execute(connection, ExpectedRows.Ignore);
        }

        /// <summary>
        /// Maps a Campaign ID text ("79580") to its DemandayCampaignId key. Scoped to the
        /// enquiry's Master Account - the same Campaign ID text may exist under more than one
        /// account, and a copied row must never point at another account's campaign.
        /// Returns null when the text is blank or has no match, which just leaves the column empty.
        /// </summary>
        private static int? ResolveCampaignId(IDbConnection connection, int? masterAccountId, string campaignText)
        {
            if (masterAccountId == null || string.IsNullOrWhiteSpace(campaignText))
                return null;

            var fld = Masters.DemandayCampaignIdRow.Fields;
            var match = connection.TryFirst<Masters.DemandayCampaignIdRow>(q => q
                .Select(fld.Id)
                .Where(fld.DemandayMasterAccountId == masterAccountId.Value &
                       fld.CampaignId == campaignText.Trim()));

            return match?.Id;
        }
    }
}
