using AdvanceCRM.Demanday;
using Serenity.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AdvanceCRM.Web.Modules.Common.AppServices
{
    /// <summary>
    /// Loads the QA Details of a TeleMarketing module (the question / answer rows shown in the
    /// dialog) so an export can write them out under the record they belong to.
    ///
    /// The detail rows always live in DemandayTeleMarketingEnquiryQADetails and are re-pointed
    /// at whichever module currently owns the record: every "Move to ..." rewrites EnquiryId to
    /// the new row's id (see the Move handlers). So for a TM Quality export, EnquiryId is the
    /// TM Quality record's own Id.
    /// </summary>
    public static class QADetailsExportHelper
    {
        public class QaEntry
        {
            public int QuestionId { get; set; }
            public string Question { get; set; }
            public string Answer { get; set; }
        }

        /// <summary>
        /// Loads the QA details of the given records, keyed by record id. Ids are queried in
        /// chunks because an export can easily carry more records than SQL Server allows
        /// parameters in a single IN (...).
        /// </summary>
        public static Dictionary<int, List<QaEntry>> LoadByRecordId(IDbConnection connection,
            IEnumerable<int> recordIds)
        {
            var result = new Dictionary<int, List<QaEntry>>();
            if (connection == null || recordIds == null)
                return result;

            var ids = recordIds.Distinct().ToList();
            if (ids.Count == 0)
                return result;

            var fld = DemandayTeleMarketingEnquiryQADetailsRow.Fields;

            foreach (var chunk in ids.Chunk(1000))
            {
                // Selecting QuestionText / AnswerText brings their joins along, so the readable
                // texts come back with the same query instead of a lookup per row.
                var details = connection.List<DemandayTeleMarketingEnquiryQADetailsRow>(q => q
                    .Select(fld.Id)
                    .Select(fld.EnquiryId)
                    .Select(fld.QuestionId)
                    .Select(fld.QuestionText)
                    .Select(fld.AnswerText)
                    .Where(fld.EnquiryId.In(chunk))
                    .OrderBy(fld.QuestionId)
                    .OrderBy(fld.Id));

                foreach (var d in details)
                {
                    if (!d.EnquiryId.HasValue)
                        continue;

                    if (!result.TryGetValue(d.EnquiryId.Value, out var list))
                        result[d.EnquiryId.Value] = list = new List<QaEntry>();

                    list.Add(new QaEntry
                    {
                        QuestionId = d.QuestionId ?? 0,
                        Question = d.QuestionText,
                        Answer = d.AnswerText
                    });
                }
            }

            return result;
        }

    }
}
