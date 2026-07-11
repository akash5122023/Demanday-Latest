using Serenity.Services;
using System;

namespace AdvanceCRM.Toolkit
{
    // Master Suppression is uploaded account-wise. The user picks a Master Account in the dialog,
    // which every row falls back to; a row may name its own Account Number to override it, so one
    // file can carry data for several accounts.
    public class MasterSupressionExcelImportRequest : ServiceRequest
    {
        public String FileName { get; set; }
        public Int32? MasterAccountId { get; set; }
    }
}
