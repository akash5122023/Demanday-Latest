using Serenity.Services;
using System;

namespace AdvanceCRM.Toolkit
{
    // Master Suppression is uploaded account-wise. The user can pick a Master Account in the
    // dialog as the default every row falls back to; a row may instead (or also) name its own
    // Account Number / Campaign Id, created automatically if it doesn't already exist, so one
    // file can carry data for several accounts.
    public class MasterSupressionExcelImportRequest : ServiceRequest
    {
        public String FileName { get; set; }
        public Int32? MasterAccountId { get; set; }
    }
}
