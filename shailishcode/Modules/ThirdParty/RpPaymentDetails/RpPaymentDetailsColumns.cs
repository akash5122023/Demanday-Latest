
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.RpPaymentDetails")]
    [BasedOnRow(typeof(RpPaymentDetailsRow), CheckNames = true)]
    public class RpPaymentDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String PaymentId { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Entity { get; set; }
        public String Amount { get; set; }
        public String Currency { get; set; }
        public String Status { get; set; }
        public String OrderId { get; set; }
        public String InvoiceId { get; set; }
        public String International { get; set; }
        public String Method { get; set; }
        public String RefundedAmt { get; set; }
        public String RefundStatus { get; set; }
        public String Captured { get; set; }
        public String Discription { get; set; }
        public String CardId { get; set; }
        public String Bank { get; set; }
        public String Wallet { get; set; }
        public String Vpa { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsMoved { get; set; }
        public String CompanyName { get; set; }
    }
}