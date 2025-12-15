
namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("ThirdParty.TradeIndiaDetails")]
    [BasedOnRow(typeof(TradeIndiaDetailsRow), CheckNames = true)]
    public class TradeIndiaDetailsForm
    {
        [Category("Enquiry Details")]
        [OneThirdWidth(UntilNext = true)]
        public Int32 RfiId { get; set; }
        public String Source { get; set; }
        public String ProductSource { get; set; }
        public DateTime GeneratedDateTime { get; set; }
        public String InquiryType { get; set; }
        public String ProductName { get; set; }
        public Int32 Quantity { get; set; }
        public Int32 OrderValueMin { get; set; }
        public String MonthSlot { get; set; }
        [FullWidth]
        public String Subject { get; set; }
        [TextAreaEditor(Rows = 4)]
        public String Message { get; set; }
        [Category("Sender Details")]
        [OneThirdWidth(UntilNext = true)]
        public String SenderCo { get; set; }
        public String SenderName { get; set; }
        public String SenderMobile { get; set; }
        public String SenderEmail { get; set; }
        public String SenderAddress { get; set; }
        public String SenderCity { get; set; }
        public String SenderState { get; set; }
        public String SenderCountry { get; set; }
        public String LandlineNumber { get; set; }
        public String PrefSuppLocation { get; set; }
        [TextAreaEditor(Rows = 4), FullWidth]
        public String Feedback { get; set; }
        [Hidden]
        public Boolean IsMoved { get; set; }
    }
}