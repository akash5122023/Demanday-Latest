
namespace AdvanceCRM.Purchase
{
    using AdvanceCRM.Masters;
    using AdvanceCRM.Products;
    using Serenity;
    using AdvanceCRM.Scripts;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Purchase"), TableName("[dbo].[PurchaseProducts]")]
    [DisplayName("Purchase Products"), InstanceName("Purchase Products")]
    [ReadPermission("Purchase:Read")]
    [ModifyPermission("Purchase:Read")]
    [LookupScript("Purchase.PurchaseProductsRow", Permission = "?")]
    public sealed class PurchaseProductsRow : Row<PurchaseProductsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, SortOrder(1),IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Products"), NotNull, ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName"),LookupInclude]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductsId
        {
            get { return Fields.ProductsId[this]; }
            set { Fields.ProductsId[this] = value; }
        }

        [DisplayName("Serial"), Size(100), LookupInclude, QuickSearch,NameProperty]
        public String Serial
        {
            get { return Fields.Serial[this]; }
            set { Fields.Serial[this] = value; }
        }

        [DisplayName("Batch"), Size(100)]
        public String Batch
        {
            get { return Fields.Batch[this]; }
            set { Fields.Batch[this] = value; }
        }

        [DisplayName("Quantity"), NotNull,LookupInclude]
        public Double? Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("Price"), DisplayFormat("#,##0.####"), NotNull]
        public Double? Price
        {
            get { return Fields.Price[this]; }
            set { Fields.Price[this] = value; }
        }

        [DisplayName("Base Price"), DisplayFormat("#,##0.####"), NotNull, DefaultValue("0")]
        public Double? SellingPrice
        {
            get { return Fields.SellingPrice[this]; }
            set { Fields.SellingPrice[this] = value; }
        }

        [DisplayName("MRP"), DisplayFormat("#,##0.####"), Column("MRP"), NotNull, DefaultValue("0")]
        public Double? Mrp
        {
            get { return Fields.Mrp[this]; }
            set { Fields.Mrp[this] = value; }
        }

        [DisplayName("Disc(%)"), NotNull]
        public Double? Discount
        {
            get { return Fields.Discount[this]; }
            set { Fields.Discount[this] = value; }
        }

        [DisplayName("Disc(Amt)"), NotNull]
        public Double? DiscountAmount
        {
            get { return Fields.DiscountAmount[this]; }
            set { Fields.DiscountAmount[this] = value; }
        }

        [DisplayName("Tax Type1"), Size(100)]
        public String TaxType1
        {
            get { return Fields.TaxType1[this]; }
            set { Fields.TaxType1[this] = value; }
        }

        [DisplayName("Percentage1")]
        public Double? Percentage1
        {
            get { return Fields.Percentage1[this]; }
            set { Fields.Percentage1[this] = value; }
        }

        [DisplayName("Tax Type2"), Size(100)]
        public String TaxType2
        {
            get { return Fields.TaxType2[this]; }
            set { Fields.TaxType2[this] = value; }
        }

        [DisplayName("Percentage2")]
        public Double? Percentage2
        {
            get { return Fields.Percentage2[this]; }
            set { Fields.Percentage2[this] = value; }
        }

        [DisplayName("Warranty Start")]
        public DateTime? WarrantyStart
        {
            get { return Fields.WarrantyStart[this]; }
            set { Fields.WarrantyStart[this] = value; }
        }

        [DisplayName("Warranty End")]
        public DateTime? WarrantyEnd
        {
            get { return Fields.WarrantyEnd[this]; }
            set { Fields.WarrantyEnd[this] = value; }
        }

        [DisplayName("Sold")]
        public Boolean? Sold
        {
            get { return Fields.Sold[this]; }
            set { Fields.Sold[this] = value; }
        }

        [DisplayName("Purchase"), NotNull, ForeignKey("[dbo].[Purchase]", "Id"), LeftJoin("jPurchase"), TextualField("PurchaseInvoiceNo")]
        [LookupEditor(typeof(PurchaseRow), InplaceAdd = true)]
        public Int32? PurchaseId
        {
            get { return Fields.PurchaseId[this]; }
            set { Fields.PurchaseId[this] = value; }
        }

        [DisplayName("Unit"), Expression("jProductsUnit.[ProductsUnit]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String Unit
        {
            get { return Fields.Unit[this]; }
            set { Fields.Unit[this] = value; }
        }

        [DisplayName("Product"), Expression("jProducts.[Name]")]
        public String ProductsName
        {
            get { return Fields.ProductsName[this]; }
            set { Fields.ProductsName[this] = value; }
        }

        [DisplayName("Products Code"), Expression("jProducts.[Code]")]
        public String ProductsCode
        {
            get { return Fields.ProductsCode[this]; }
            set { Fields.ProductsCode[this] = value; }
        }

        [DisplayName("Products Division Id"), Expression("jProducts.[DivisionId]")]
        public Int32? ProductsDivisionId
        {
            get { return Fields.ProductsDivisionId[this]; }
            set { Fields.ProductsDivisionId[this] = value; }
        }

        [DisplayName("Products Group Id"), Expression("jProducts.[GroupId]")]
        public Int32? ProductsGroupId
        {
            get { return Fields.ProductsGroupId[this]; }
            set { Fields.ProductsGroupId[this] = value; }
        }

        [DisplayName("Products Selling Price"), Expression("jProducts.[SellingPrice]")]
        public Double? ProductsSellingPrice
        {
            get { return Fields.ProductsSellingPrice[this]; }
            set { Fields.ProductsSellingPrice[this] = value; }
        }

        [DisplayName("Products MRP"), Expression("jProducts.[MRP]")]
        public Double? ProductsMrp
        {
            get { return Fields.ProductsMrp[this]; }
            set { Fields.ProductsMrp[this] = value; }
        }

        [DisplayName("Products Description"), Expression("jProducts.[Description]")]
        public String ProductsDescription
        {
            get { return Fields.ProductsDescription[this]; }
            set { Fields.ProductsDescription[this] = value; }
        }

        [DisplayName("Products Tax Id1"), Expression("jProducts.[TaxId1]")]
        public Int32? ProductsTaxId1
        {
            get { return Fields.ProductsTaxId1[this]; }
            set { Fields.ProductsTaxId1[this] = value; }
        }

        [DisplayName("Products Tax Id2"), Expression("jProducts.[TaxId2]")]
        public Int32? ProductsTaxId2
        {
            get { return Fields.ProductsTaxId2[this]; }
            set { Fields.ProductsTaxId2[this] = value; }
        }

        [DisplayName("Products Image"), Expression("jProducts.[Image]")]
        public String ProductsImage
        {
            get { return Fields.ProductsImage[this]; }
            set { Fields.ProductsImage[this] = value; }
        }

        [DisplayName("Products Tech Specs"), Expression("jProducts.[TechSpecs]")]
        public String ProductsTechSpecs
        {
            get { return Fields.ProductsTechSpecs[this]; }
            set { Fields.ProductsTechSpecs[this] = value; }
        }

        [DisplayName("Products Hsn"), Expression("jProducts.[HSN]")]
        public String ProductsHsn
        {
            get { return Fields.ProductsHsn[this]; }
            set { Fields.ProductsHsn[this] = value; }
        }

        [DisplayName("Products Channel Customer Price"), Expression("jProducts.[ChannelCustomerPrice]")]
        public Double? ProductsChannelCustomerPrice
        {
            get { return Fields.ProductsChannelCustomerPrice[this]; }
            set { Fields.ProductsChannelCustomerPrice[this] = value; }
        }

        [DisplayName("Products Reseller Price"), Expression("jProducts.[ResellerPrice]")]
        public Double? ProductsResellerPrice
        {
            get { return Fields.ProductsResellerPrice[this]; }
            set { Fields.ProductsResellerPrice[this] = value; }
        }

        [DisplayName("Products Wholesaler Price"), Expression("jProducts.[WholesalerPrice]")]
        public Double? ProductsWholesalerPrice
        {
            get { return Fields.ProductsWholesalerPrice[this]; }
            set { Fields.ProductsWholesalerPrice[this] = value; }
        }

        [DisplayName("Products Dealer Price"), Expression("jProducts.[DealerPrice]")]
        public Double? ProductsDealerPrice
        {
            get { return Fields.ProductsDealerPrice[this]; }
            set { Fields.ProductsDealerPrice[this] = value; }
        }

        [DisplayName("Products Distributor Price"), Expression("jProducts.[DistributorPrice]")]
        public Double? ProductsDistributorPrice
        {
            get { return Fields.ProductsDistributorPrice[this]; }
            set { Fields.ProductsDistributorPrice[this] = value; }
        }

        [DisplayName("Products Stockiest Price"), Expression("jProducts.[StockiestPrice]")]
        public Double? ProductsStockiestPrice
        {
            get { return Fields.ProductsStockiestPrice[this]; }
            set { Fields.ProductsStockiestPrice[this] = value; }
        }

        [DisplayName("Products National Distributor Price"), Expression("jProducts.[NationalDistributorPrice]")]
        public Double? ProductsNationalDistributorPrice
        {
            get { return Fields.ProductsNationalDistributorPrice[this]; }
            set { Fields.ProductsNationalDistributorPrice[this] = value; }
        }

        [DisplayName("Products Minimum Stock"), Expression("jProducts.[MinimumStock]")]
        public Double? ProductsMinimumStock
        {
            get { return Fields.ProductsMinimumStock[this]; }
            set { Fields.ProductsMinimumStock[this] = value; }
        }

        [DisplayName("Products Maximum Stock"), Expression("jProducts.[MaximumStock]")]
        public Double? ProductsMaximumStock
        {
            get { return Fields.ProductsMaximumStock[this]; }
            set { Fields.ProductsMaximumStock[this] = value; }
        }

        [DisplayName("Products Raw Material"), Expression("jProducts.[RawMaterial]")]
        public Boolean? ProductsRawMaterial
        {
            get { return Fields.ProductsRawMaterial[this]; }
            set { Fields.ProductsRawMaterial[this] = value; }
        }

        [DisplayName("Products Purchase Price"), Expression("jProducts.[PurchasePrice]")]
        public Double? ProductsPurchasePrice
        {
            get { return Fields.ProductsPurchasePrice[this]; }
            set { Fields.ProductsPurchasePrice[this] = value; }
        }

        [DisplayName("Products Opening Stock"), Expression("jProducts.[OpeningStock]")]
        public Double? ProductsOpeningStock
        {
            get { return Fields.ProductsOpeningStock[this]; }
            set { Fields.ProductsOpeningStock[this] = value; }
        }

        [DisplayName("Products Unit Id"), Expression("jProducts.[UnitId]")]
        [ForeignKey("ProductsUnit", "Id"), LeftJoin("jProductsUnit")] //added
        public Int32? ProductsUnitId
        {
            get { return Fields.ProductsUnitId[this]; }
            set { Fields.ProductsUnitId[this] = value; }
        }

        [DisplayName("Purchase Invoice No"), Expression("jPurchase.[Invoice No]")]
        public String PurchaseInvoiceNo
        {
            get { return Fields.PurchaseInvoiceNo[this]; }
            set { Fields.PurchaseInvoiceNo[this] = value; }
        }

        [DisplayName("Purchase Purchase From Id"), Expression("jPurchase.[PurchaseFromId]")]
        public Int32? PurchasePurchaseFromId
        {
            get { return Fields.PurchasePurchaseFromId[this]; }
            set { Fields.PurchasePurchaseFromId[this] = value; }
        }

        [DisplayName("Purchase Invoice Date"), Expression("jPurchase.[InvoiceDate]")]
        public DateTime? PurchaseInvoiceDate
        {
            get { return Fields.PurchaseInvoiceDate[this]; }
            set { Fields.PurchaseInvoiceDate[this] = value; }
        }

        [DisplayName("Purchase Total"), Expression("jPurchase.[Total]")]
        public Double? PurchaseTotal
        {
            get { return Fields.PurchaseTotal[this]; }
            set { Fields.PurchaseTotal[this] = value; }
        }

        [DisplayName("Purchase Status"), Expression("jPurchase.[Status]")]
        public Int32? PurchaseStatus
        {
            get { return Fields.PurchaseStatus[this]; }
            set { Fields.PurchaseStatus[this] = value; }
        }

        [DisplayName("Purchase Type"), Expression("jPurchase.[Type]")]
        public Int32? PurchaseType
        {
            get { return Fields.PurchaseType[this]; }
            set { Fields.PurchaseType[this] = value; }
        }

        [DisplayName("Purchase Additional Info"), Expression("jPurchase.[AdditionalInfo]")]
        public String PurchaseAdditionalInfo
        {
            get { return Fields.PurchaseAdditionalInfo[this]; }
            set { Fields.PurchaseAdditionalInfo[this] = value; }
        }

        [DisplayName("Purchase Branch Id"), Expression("jPurchase.[BranchId]")]
        public Int32? PurchaseBranchId
        {
            get { return Fields.PurchaseBranchId[this]; }
            set { Fields.PurchaseBranchId[this] = value; }
        }

        [DisplayName("Purchase Owner Id"), Expression("jPurchase.[OwnerId]")]
        public Int32? PurchaseOwnerId
        {
            get { return Fields.PurchaseOwnerId[this]; }
            set { Fields.PurchaseOwnerId[this] = value; }
        }

        [DisplayName("Purchase Assigned Id"), Expression("jPurchase.[AssignedId]")]
        public Int32? PurchaseAssignedId
        {
            get { return Fields.PurchaseAssignedId[this]; }
            set { Fields.PurchaseAssignedId[this] = value; }
        }

        [DisplayName("Purchase Reverse Charge"), Expression("jPurchase.[ReverseCharge]")]
        public Boolean? PurchaseReverseCharge
        {
            get { return Fields.PurchaseReverseCharge[this]; }
            set { Fields.PurchaseReverseCharge[this] = value; }
        }

        [DisplayName("Purchase Invoice Type"), Expression("jPurchase.[InvoiceType]")]
        public Int32? PurchaseInvoiceType
        {
            get { return Fields.PurchaseInvoiceType[this]; }
            set { Fields.PurchaseInvoiceType[this] = value; }
        }

        [DisplayName("Purchase Itc Eligibility"), Expression("jPurchase.[ITCEligibility]")]
        public Int32? PurchaseItcEligibility
        {
            get { return Fields.PurchaseItcEligibility[this]; }
            set { Fields.PurchaseItcEligibility[this] = value; }
        }

        [DisplayName("Inclusive"), NotMapped]
        public Boolean? Inclusive { get { return Fields.Inclusive[this]; } set { Fields.Inclusive[this] = value; } }


        [DisplayName("TAX1 Amount"), Expression("(((t0.[Price] * t0.[Quantity]) - (t0.[DiscountAmount] + (t0.[Price] * t0.[Quantity] * (t0.[Discount] / 100)))) * (t0.[Percentage1] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax1Amount
        {
            get { return Fields.Tax1Amount[this]; }
            set { Fields.Tax1Amount[this] = value; }
        }

        [DisplayName("TAX2 Amount"), Expression("(((t0.[Price] * t0.[Quantity]) - (t0.[DiscountAmount] + (t0.[Price] * t0.[Quantity] * (t0.[Discount] / 100)))) * (t0.[Percentage2] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax2Amount
        {
            get { return Fields.Tax2Amount[this]; }
            set { Fields.Tax2Amount[this] = value; }
        }

        [DisplayName("Line Total"), Expression("(t0.[Price] * (t0.[Quantity]) - t0.[DiscountAmount] - ((t0.[Price] * (t0.[Quantity])) * t0.[Discount] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
        }

        [DisplayName("Grand Total"), Expression("(((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) + (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage1] / 100)) + (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage2] / 100)))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? GrandTotal
        {
            get { return Fields.GrandTotal[this]; }
            set { Fields.GrandTotal[this] = value; }
        }
        [DisplayName("From")]
        public String From
        {
            get { return Fields.From[this]; }
            set { Fields.From[this] = value; }
        }
        [DisplayName("To")]
        public String To
        {
            get { return Fields.To[this]; }
            set { Fields.To[this] = value; }
        }
        [DisplayName("Date")]
        public DateTime? Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
        }
        [DisplayName("Destination")]
        public String Destination
        {
            get { return Fields.Destination[this]; }
            set { Fields.Destination[this] = value; }
        }
        [DisplayName("Nights")]
        public String Nights
        {
            get { return Fields.Nights[this]; }
            set { Fields.Nights[this] = value; }
        }
        [DisplayName("Adults")]
        public String Adults
        {
            get { return Fields.Adults[this]; }
            set { Fields.Adults[this] = value; }
        }
        [DisplayName("Childrens")]
        public String Childrens
        {
            get { return Fields.Childrens[this]; }
            set { Fields.Childrens[this] = value; }
        }
        [DisplayName("Hotel Name")]
        public String HotelName
        {
            get { return Fields.HotelName[this]; }
            set { Fields.HotelName[this] = value; }
        }
        [DisplayName("Meal Plan")]
        public String MealPlan
        {
            get { return Fields.MealPlan[this]; }
            set { Fields.MealPlan[this] = value; }
        }


        
        public PurchaseProductsRow()
            : base(Fields)
        {
        }
        
        public PurchaseProductsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProductsId;
            public StringField Serial;
            public StringField Batch;
            public DoubleField Quantity;
            public DoubleField Price;
            public DoubleField SellingPrice;
            public DoubleField Mrp;
            public DoubleField Discount;
            public DoubleField DiscountAmount;
            public StringField TaxType1;
            public DoubleField Percentage1;
            public StringField TaxType2;
            public DoubleField Percentage2;
            public DateTimeField WarrantyStart;
            public DateTimeField WarrantyEnd;
            public BooleanField Sold;
            public Int32Field PurchaseId;
            public StringField Unit;

            public StringField ProductsName;
            public StringField ProductsCode;
            public Int32Field ProductsDivisionId;
            public Int32Field ProductsGroupId;
            public DoubleField ProductsSellingPrice;
            public DoubleField ProductsMrp;
            public StringField ProductsDescription;
            public Int32Field ProductsTaxId1;
            public Int32Field ProductsTaxId2;
            public StringField ProductsImage;
            public StringField ProductsTechSpecs;
            public StringField ProductsHsn;
            public DoubleField ProductsChannelCustomerPrice;
            public DoubleField ProductsResellerPrice;
            public DoubleField ProductsWholesalerPrice;
            public DoubleField ProductsDealerPrice;
            public DoubleField ProductsDistributorPrice;
            public DoubleField ProductsStockiestPrice;
            public DoubleField ProductsNationalDistributorPrice;
            public DoubleField ProductsMinimumStock;
            public DoubleField ProductsMaximumStock;
            public BooleanField ProductsRawMaterial;
            public DoubleField ProductsPurchasePrice;
            public DoubleField ProductsOpeningStock;
            public Int32Field ProductsUnitId;

            public StringField PurchaseInvoiceNo;
            public Int32Field PurchasePurchaseFromId;
            public DateTimeField PurchaseInvoiceDate;
            public DoubleField PurchaseTotal;
            public Int32Field PurchaseStatus;
            public Int32Field PurchaseType;
            public StringField PurchaseAdditionalInfo;
            public Int32Field PurchaseBranchId;
            public Int32Field PurchaseOwnerId;
            public Int32Field PurchaseAssignedId;
            public BooleanField PurchaseReverseCharge;
            public Int32Field PurchaseInvoiceType;
            public Int32Field PurchaseItcEligibility;

            public BooleanField Inclusive;
            public DecimalField Tax1Amount;
            public DecimalField Tax2Amount;
            public DecimalField LineTotal;
            public DecimalField GrandTotal;

            public StringField From;
            public StringField To;
            public DateTimeField Date;
            public StringField Destination;
            public StringField Nights;
            public StringField Adults;
            public StringField Childrens;
            public StringField HotelName;
            public StringField MealPlan;
        }
    }
}
