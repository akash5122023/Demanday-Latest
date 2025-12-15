
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("ThirdParty.WcOrderDetails")]
    [BasedOnRow(typeof(WcOrderDetailsRow), CheckNames = true)]
    public class WcOrderDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Wcoid { get; set; }
        public String ParentId { get; set; }
        public String Status { get; set; }
        public String Currency { get; set; }
        public String IncludedTax { get; set; }
        public String DateCreated { get; set; }
        public String DateModified { get; set; }
        public String DiscountTotal { get; set; }
        public String DiscountTax { get; set; }
        public String ShippingTotal { get; set; }
        public String ShipppingTax { get; set; }
        public String CartTax { get; set; }
        public String Total { get; set; }
        public String TotalTax { get; set; }
        public String CustomerId { get; set; }
        public String OrderKey { get; set; }
        public String BFirstName { get; set; }
        public String BLastName { get; set; }
        public String BCompany { get; set; }
        public String BEmail { get; set; }
        public String BPhone { get; set; }
        public String BPostCode { get; set; }
        public String BAddress1 { get; set; }
        public String BAddress2 { get; set; }
        public String BCity { get; set; }
        public String BState { get; set; }
        public String BCountry { get; set; }
        public String SFirstName { get; set; }
        public String SLastName { get; set; }
        public String SCompany { get; set; }
        public String SPhone { get; set; }
        public String SPostCode { get; set; }
        public String SAddress1 { get; set; }
        public String SAddress2 { get; set; }
        public String SCity { get; set; }
        public String SState { get; set; }
        public String SCountry { get; set; }
        public String PaymentMethod { get; set; }
        public String TransactionId { get; set; }
        public String CustomerIp { get; set; }
        public String ItemId { get; set; }
        public String ItemName { get; set; }
        public String ProductId { get; set; }
        public String VariationId { get; set; }
        public String Quantity { get; set; }
        public String TaxClass { get; set; }
        public String SubTotal { get; set; }
        public String SubTotaltax { get; set; }
        public String ItemTotal { get; set; }
        public String ItemTotaltax { get; set; }
        public String TaxRateCode { get; set; }
        public String TaxRateId { get; set; }
        public String TaxLabel { get; set; }
        public String TaxCompound { get; set; }
        public String TaxTotal { get; set; }
        public String TaxShippingTotal { get; set; }
        public String TaxRatePercent { get; set; }
        public Boolean IsMoved { get; set; }
    }
}