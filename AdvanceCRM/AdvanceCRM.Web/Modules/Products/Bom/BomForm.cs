using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Products.Forms
{
    [FormScript("Products.Bom")]
    [BasedOnRow(typeof(BomRow), CheckNames = true)]
    public class BomForm
    {
        [Category("Finish Good Product Details")]
        [HalfWidth]
        public Int32 ProductsId { get; set; }

        [OneThirdWidth(UntilNext = true)]
        [Hidden]
        public Double Quantity { get; set; }
        public String Code { get; set; }
        [Hidden]
        public String Hsn { get; set; }
        [Hidden]
        public String Unit { get; set; }
        [Hidden]
        public Int32 ContactsId { get; set; }
        [Hidden]
        public DateTime Date { get; set; }
        [Hidden]
        public Int32 Status { get; set; }
        [Hidden]
        public Int32 Type { get; set; }

        [Category("Product Details")]
        [FullWidth]
        [BomProductsEditor]
        public List<BomProductsRow> Products { get; set; }
        [Category("Pricing")]
        [OneThirdWidth(UntilNext = true)]
        [DefaultValue("0")]
        public Double SellingPrice { get; set; }
        [DefaultValue("0")]
        public Double Mrp { get; set; }
        [DefaultValue("0")]
        public Double Price { get; set; }
        [Hidden]
        public Double PackagingCharges { get; set; }
        [Hidden]
        public Double FreightCharges { get; set; }
        [Hidden]
        public Double Advacne { get; set; }
        [Hidden]
        public Double Roundup { get; set; }
        [Hidden]
        public DateTime DueDate { get; set; }
        [Hidden]
        public Int32 ContactPersonId { get; set; }
        [Hidden]
        public Int32 QuotationNo { get; set; }
        [Hidden]
        public DateTime QuotationDate { get; set; }
        [Hidden]
        public Double Conversion { get; set; }
        [Hidden]
        public String PurchaseOrderNo { get; set; }
        [Category("Additional Details")]
        [Hidden]
        public String DispatchDetails { get; set; }
        [FullWidth]
        public String AdditionalInfo { get; set; }
        [FullWidth]
        public String Attachments { get; set; }
        [Hidden]
        public String Subject { get; set; }
        [Hidden]
        public String Reference { get; set; }
        [Hidden]
        public Int32 Lines { get; set; }
        [Category("Tech Specs")]
        public String TechSpecs { get; set; }
        [Category("Images")]
        public String Image { get; set; }
        [Category("Representative")]
        [HalfWidth, ReadOnly(true)]
        public Int32 OwnerId { get; set; }
        [HalfWidth]
        public Int32 AssignedId { get; set; }
        [ReadOnly(true), DefaultValue(1), Hidden]
        public Int32 CompanyId { get; set; }




        //public Int32 BranchId { get; set; }

        //public Boolean OtherAddress { get; set; }
        //public String ShippingAddress { get; set; }



        //public String ItemName { get; set; }
        //public Int32 OperationCost { get; set; }
        //public Int32 RawMaterialCost { get; set; }
        //public Int32 ScrapMaterialCost { get; set; }
        //public Int32 TotalMaterialCost { get; set; }
        //public String OperationName { get; set; }
        //public String WorkStationName { get; set; }
        //public String OperatinngTime { get; set; }
        //public Int32 OperatingCost { get; set; }
        //public Int32 ProcessLoss { get; set; }
        //public Int32 ProcessLossQty { get; set; }
        //public Int32 Taxable { get; set; }

        //public Double Discount { get; set; }
        //public String TaxType1 { get; set; }
        //public Double Percentage1 { get; set; }
        //public String TaxType2 { get; set; }
        //public Double Percentage2 { get; set; }
        //public DateTime WarrantyStart { get; set; }
        //public DateTime WarrantyEnd { get; set; }
        //public Double DiscountAmount { get; set; }
        //public String Description { get; set; }
    }
}