using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using Serenity.Data.Mapping;

namespace AdvanceCRM.Products.Columns
{
    [ColumnsScript("Products.Bom")]
    [BasedOnRow(typeof(BomRow), CheckNames = true)]
    public class BomColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, QuickFilter, QuickSearch]
        //public String ItemName { get; set; }
        public String ProductsName { get; set; }

        [Hidden]
        public Double Quantity { get; set; }
        [QuickSearch]
        public String Code { get; set; }
        [QuickSearch]
        public String Hsn { get; set; }
        [Hidden]
        public Int32 OperationCost { get; set; }
        public Int32 RawMaterialCost { get; set; }
        public Int32 ScrapMaterialCost { get; set; }
        public Int32 TotalMaterialCost { get; set; }
        [EditLink, QuickFilter, QuickSearch]
        public String OperationName { get; set; }
        [QuickFilter]
        public String WorkStationName { get; set; }
        public String OperatinngTime { get; set; }
        public Int32 OperatingCost { get; set; }
        public Int32 ProcessLoss { get; set; }
        public Int32 ProcessLossQty { get; set; }
        [Hidden]
        public Double Mrp { get; set; }
        [Hidden]
        public Double SellingPrice { get; set; }
        [Hidden]
        public Double Price { get; set; }
        [Hidden]
        public Double Discount { get; set; }
        [Hidden]
        public Double Percentage1 { get; set; }
        [Hidden]
        public Double Percentage2 { get; set; }
        [Hidden]
        public Double DiscountAmount { get; set; }
        [Hidden]
        public String TaxType1 { get; set; }
        [Hidden]
        public String TaxType2 { get; set; }
        [Hidden]
        public String Description { get; set; }
        [Hidden]
        public String Unit { get; set; }
        [Hidden]
        public DateTime WarrantyStart { get; set; }
        [Hidden]
        public DateTime WarrantyEnd { get; set; }
        [Hidden]
        public Int32 ContactsId { get; set; }
        [Hidden]
        public DateTime Date { get; set; }
        [Hidden]
        public Int32 Status { get; set; }
        [Hidden]
        public Int32 Type { get; set; }
        [EditLink]
        public String AdditionalInfo { get; set; }
        [Hidden]
        public Int32 BranchId { get; set; }
        [Hidden]
        public Int32 OwnerId { get; set; }
        [Hidden]
        public Int32 AssignedId { get; set; }
        [Hidden]
        public Boolean OtherAddress { get; set; }
        [Hidden]
        public String ShippingAddress { get; set; }
        [Hidden]
        public Double PackagingCharges { get; set; }
        [Hidden]
        public Double FreightCharges { get; set; }
        [Hidden]
        public Double Advacne { get; set; }
        [Hidden]
        public DateTime DueDate { get; set; }
        [Hidden]
        public String DispatchDetails { get; set; }
        [Hidden]
        public Double Roundup { get; set; }
        public String Subject { get; set; }
        public String Reference { get; set; }
        [Hidden]
        public String ContactPersonName { get; set; }
        [Hidden]
        public Int32 Lines { get; set; }
        [Hidden]
        public Int32 QuotationNo { get; set; }
        [Hidden]
        public DateTime QuotationDate { get; set; }
        [Hidden]
        public Double Conversion { get; set; }
        [Hidden]
        public String PurchaseOrderNo { get; set; }
        [Hidden]
        public String Attachments { get; set; }
        [Hidden]
        public Int32 CompanyId { get; set; }
        [Hidden]
        public Int32 Taxable { get; set; }
        [Hidden]
        public String Image { get; set; }
        public String TechSpecs { get; set; }
        //[Hidden]
        //public String ProductsName { get; set; }
    }
}