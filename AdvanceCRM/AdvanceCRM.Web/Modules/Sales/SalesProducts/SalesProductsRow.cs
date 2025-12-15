
namespace AdvanceCRM.Sales
{
    using AdvanceCRM.Masters;
    using AdvanceCRM.Products;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[SalesProducts]")]
    [DisplayName("Sales Products"), InstanceName("Sales Products")]
    [ReadPermission("Sales:Read")]
    [ModifyPermission("Sales:Read")]
    [LookupScript("Sales.SalesProducts", Permission = "?")]

    public sealed class SalesProductsRow : Row<SalesProductsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, SortOrder(1),IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Products"), NotNull, ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName"), LookupInclude]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductsId
        {
            get { return Fields.ProductsId[this]; }
            set { Fields.ProductsId[this] = value; }
        }

        [DisplayName("Serial"), Size(100), QuickSearch, LookupInclude,NameProperty]
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

        [DisplayName("Quantity"), NotNull]
        public Double? Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("Price"), DisplayFormat("#,##0.####"), NotNull, DecimalEditor(Decimals = 4)]
        public Double? Price
        {
            get { return Fields.Price[this]; }
            set { Fields.Price[this] = value; }
        }

        [DisplayName("Base Price"), DisplayFormat("#,##0.####"), NotNull, DecimalEditor(Decimals = 4)]
        public Double? SellingPrice
        {
            get { return Fields.SellingPrice[this]; }
            set { Fields.SellingPrice[this] = value; }
        }

        [DisplayName("MRP"), DisplayFormat("#,##0.####"), Column("MRP"), NotNull, DecimalEditor(Decimals = 4)]
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

        [DisplayName("Percentage 1")]
        public Double? Percentage1
        {
            get { return Fields.Percentage1[this]; }
            set { Fields.Percentage1[this] = value; }
        }

        [DisplayName("Tax 2"), Size(100)]
        public String TaxType2
        {
            get { return Fields.TaxType2[this]; }
            set { Fields.TaxType2[this] = value; }
        }

        [DisplayName("Percentage 2")]
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

        [DisplayName("Sales"), NotNull, ForeignKey("[dbo].[Sales]", "Id"), LeftJoin("jSales"), TextualField("SalesAdditionalInfo")]
        [LookupEditor(typeof(SalesRow), InplaceAdd = true)]
        public Int32? SalesId
        {
            get { return Fields.SalesId[this]; }
            set { Fields.SalesId[this] = value; }
        }

        [DisplayName("Description"), Size(2000), TextAreaEditor(Rows = 4)]
        public String Description
        {
            get { return Fields.Description[this]; }
            set { Fields.Description[this] = value; }
        }

        [DisplayName("Unit"), Expression("jProductsUnit.[ProductsUnit]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String Unit
        {
            get { return Fields.Unit[this]; }
            set { Fields.Unit[this] = value; }
        }

        [DisplayName("Product"), Expression("jProducts.[Name]"), QuickSearch]
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

        [DisplayName("Sales Contacts Id"), Expression("jSales.[ContactsId]"), LookupInclude]
        public Int32? SalesContactsId
        {
            get { return Fields.SalesContactsId[this]; }
            set { Fields.SalesContactsId[this] = value; }
        }

        [DisplayName("Sales Date"), Expression("jSales.[Date]")]
        public DateTime? SalesDate
        {
            get { return Fields.SalesDate[this]; }
            set { Fields.SalesDate[this] = value; }
        }

        [DisplayName("Sales Status"), Expression("jSales.[Status]")]
        public Int32? SalesStatus
        {
            get { return Fields.SalesStatus[this]; }
            set { Fields.SalesStatus[this] = value; }
        }

        [DisplayName("Sales Type"), Expression("jSales.[Type]")]
        public Int32? SalesType
        {
            get { return Fields.SalesType[this]; }
            set { Fields.SalesType[this] = value; }
        }

        [DisplayName("Sales Additional Info"), Expression("jSales.[AdditionalInfo]")]
        public String SalesAdditionalInfo
        {
            get { return Fields.SalesAdditionalInfo[this]; }
            set { Fields.SalesAdditionalInfo[this] = value; }
        }

        [DisplayName("Sales Source Id"), Expression("jSales.[SourceId]")]
        public Int32? SalesSourceId
        {
            get { return Fields.SalesSourceId[this]; }
            set { Fields.SalesSourceId[this] = value; }
        }

        [DisplayName("Sales Stage Id"), Expression("jSales.[StageId]")]
        public Int32? SalesStageId
        {
            get { return Fields.SalesStageId[this]; }
            set { Fields.SalesStageId[this] = value; }
        }

        [DisplayName("Sales Branch Id"), Expression("jSales.[BranchId]")]
        public Int32? SalesBranchId
        {
            get { return Fields.SalesBranchId[this]; }
            set { Fields.SalesBranchId[this] = value; }
        }

        [DisplayName("Sales Owner Id"), Expression("jSales.[OwnerId]")]
        public Int32? SalesOwnerId
        {
            get { return Fields.SalesOwnerId[this]; }
            set { Fields.SalesOwnerId[this] = value; }
        }

        [DisplayName("Sales Assigned Id"), Expression("jSales.[AssignedId]")]
        public Int32? SalesAssignedId
        {
            get { return Fields.SalesAssignedId[this]; }
            set { Fields.SalesAssignedId[this] = value; }
        }

        [DisplayName("Sales Other Address"), Expression("jSales.[OtherAddress]")]
        public Boolean? SalesOtherAddress
        {
            get { return Fields.SalesOtherAddress[this]; }
            set { Fields.SalesOtherAddress[this] = value; }
        }

        [DisplayName("Sales Shipping Address"), Expression("jSales.[ShippingAddress]")]
        public String SalesShippingAddress
        {
            get { return Fields.SalesShippingAddress[this]; }
            set { Fields.SalesShippingAddress[this] = value; }
        }

        [DisplayName("Sales Packaging Charges"), Expression("jSales.[PackagingCharges]")]
        public Double? SalesPackagingCharges
        {
            get { return Fields.SalesPackagingCharges[this]; }
            set { Fields.SalesPackagingCharges[this] = value; }
        }

        [DisplayName("Sales Freight Charges"), Expression("jSales.[FreightCharges]")]
        public Double? SalesFreightCharges
        {
            get { return Fields.SalesFreightCharges[this]; }
            set { Fields.SalesFreightCharges[this] = value; }
        }

        [DisplayName("Sales Advacne"), Expression("jSales.[Advacne]")]
        public Double? SalesAdvacne
        {
            get { return Fields.SalesAdvacne[this]; }
            set { Fields.SalesAdvacne[this] = value; }
        }

        [DisplayName("Sales Due Date"), Expression("jSales.[DueDate]")]
        public DateTime? SalesDueDate
        {
            get { return Fields.SalesDueDate[this]; }
            set { Fields.SalesDueDate[this] = value; }
        }

        [DisplayName("Sales Dispatch Details"), Expression("jSales.[DispatchDetails]")]
        public String SalesDispatchDetails
        {
            get { return Fields.SalesDispatchDetails[this]; }
            set { Fields.SalesDispatchDetails[this] = value; }
        }

        [DisplayName("Sales Roundup"), Expression("jSales.[Roundup]")]
        public Double? SalesRoundup
        {
            get { return Fields.SalesRoundup[this]; }
            set { Fields.SalesRoundup[this] = value; }
        }

        [DisplayName("Sales Contact Person Id"), Expression("jSales.[ContactPersonId]")]
        public Int32? SalesContactPersonId
        {
            get { return Fields.SalesContactPersonId[this]; }
            set { Fields.SalesContactPersonId[this] = value; }
        }

        [DisplayName("Sales Lines"), Expression("jSales.[Lines]")]
        public Int32? SalesLines
        {
            get { return Fields.SalesLines[this]; }
            set { Fields.SalesLines[this] = value; }
        }

        [DisplayName("Sales Invoice No"), Expression("jSales.[InvoiceNo]")]
        public Int32? SalesInvoiceNo
        {
            get { return Fields.SalesInvoiceNo[this]; }
            set { Fields.SalesInvoiceNo[this] = value; }
        }

        [DisplayName("Sales Reverse Charge"), Expression("jSales.[ReverseCharge]")]
        public Boolean? SalesReverseCharge
        {
            get { return Fields.SalesReverseCharge[this]; }
            set { Fields.SalesReverseCharge[this] = value; }
        }

        [DisplayName("Sales Ecom Type"), Expression("jSales.[EcomType]")]
        public Int32? SalesEcomType
        {
            get { return Fields.SalesEcomType[this]; }
            set { Fields.SalesEcomType[this] = value; }
        }

        [DisplayName("Sales Invoice Type"), Expression("jSales.[InvoiceType]")]
        public Int32? SalesInvoiceType
        {
            get { return Fields.SalesInvoiceType[this]; }
            set { Fields.SalesInvoiceType[this] = value; }
        }

        [DisplayName("Sales Trasportation Id"), Expression("jSales.[TrasportationId]")]
        public Int32? SalesTrasportationId
        {
            get { return Fields.SalesTrasportationId[this]; }
            set { Fields.SalesTrasportationId[this] = value; }
        }

        [DisplayName("Sales Quotation No"), Expression("jSales.[QuotationNo]")]
        public Int32? SalesQuotationNo
        {
            get { return Fields.SalesQuotationNo[this]; }
            set { Fields.SalesQuotationNo[this] = value; }
        }

        [DisplayName("Sales Quotation Date"), Expression("jSales.[QuotationDate]")]
        public DateTime? SalesQuotationDate
        {
            get { return Fields.SalesQuotationDate[this]; }
            set { Fields.SalesQuotationDate[this] = value; }
        }

        [DisplayName("Sales Conversion"), Expression("jSales.[Conversion]")]
        public Double? SalesConversion
        {
            get { return Fields.SalesConversion[this]; }
            set { Fields.SalesConversion[this] = value; }
        }

        [DisplayName("Sales Purchase Order No"), Expression("jSales.[PurchaseOrderNo]")]
        public String SalesPurchaseOrderNo
        {
            get { return Fields.SalesPurchaseOrderNo[this]; }
            set { Fields.SalesPurchaseOrderNo[this] = value; }
        }

        [DisplayName("Sales Closing Date"), Expression("jSales.[ClosingDate]")]
        public DateTime? SalesClosingDate
        {
            get { return Fields.SalesClosingDate[this]; }
            set { Fields.SalesClosingDate[this] = value; }
        }

        [DisplayName("Sales Attachments"), Expression("jSales.[Attachments]")]
        public String SalesAttachments
        {
            get { return Fields.SalesAttachments[this]; }
            set { Fields.SalesAttachments[this] = value; }
        }

        [DisplayName("Sales Currency Conversion"), Expression("jSales.[CurrencyConversion]")]
        public Boolean? SalesCurrencyConversion
        {
            get { return Fields.SalesCurrencyConversion[this]; }
            set { Fields.SalesCurrencyConversion[this] = value; }
        }

        [DisplayName("Sales From Currency"), Expression("jSales.[FromCurrency]")]
        public Int32? SalesFromCurrency
        {
            get { return Fields.SalesFromCurrency[this]; }
            set { Fields.SalesFromCurrency[this] = value; }
        }

        [DisplayName("Sales To Currency"), Expression("jSales.[ToCurrency]")]
        public Int32? SalesToCurrency
        {
            get { return Fields.SalesToCurrency[this]; }
            set { Fields.SalesToCurrency[this] = value; }
        }

        [DisplayName("Sales Taxable"), Expression("jSales.[Taxable]")]
        public Boolean? SalesTaxable
        {
            get { return Fields.SalesTaxable[this]; }
            set { Fields.SalesTaxable[this] = value; }
        }

        [DisplayName("Code"), Size(100), NotMapped]
        public String Code { get { return Fields.Code[this]; } set { Fields.Code[this] = value; } }

        [DisplayName("TAX Inclusive"), NotMapped]
        [BooleanSwitchEditor]
        public Boolean? Inclusive { get { return Fields.Inclusive[this]; } set { Fields.Inclusive[this] = value; } }

        [DisplayName("Discounted Price"), Expression("(t0.[Price] - (t0.[DiscountAmount] / t0.[Quantity]) - (t0.[Price]* t0.[Discount] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? DiscPrice
        {
            get { return Fields.DiscPrice[this]; }
            set { Fields.DiscPrice[this] = value; }
        }

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

        [DisplayName("Grand Total"), Expression("(((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) + (IIF((SELECT Taxable FROM Sales WHERE Id=t0.[SalesId]) = 0, 0,  (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage1] / 100)) + (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage2] / 100)))))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
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

      
        public SalesProductsRow()
            : base(Fields)
        {
        }
        public SalesProductsRow(RowFields fields)
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
            public Int32Field SalesId;
            public StringField Description;
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

            public Int32Field SalesContactsId;
            public DateTimeField SalesDate;
            public Int32Field SalesStatus;
            public Int32Field SalesType;
            public StringField SalesAdditionalInfo;
            public Int32Field SalesSourceId;
            public Int32Field SalesStageId;
            public Int32Field SalesBranchId;
            public Int32Field SalesOwnerId;
            public Int32Field SalesAssignedId;
            public BooleanField SalesOtherAddress;
            public StringField SalesShippingAddress;
            public DoubleField SalesPackagingCharges;
            public DoubleField SalesFreightCharges;
            public DoubleField SalesAdvacne;
            public DateTimeField SalesDueDate;
            public StringField SalesDispatchDetails;
            public DoubleField SalesRoundup;
            public Int32Field SalesContactPersonId;
            public Int32Field SalesLines;
            public Int32Field SalesInvoiceNo;
            public BooleanField SalesReverseCharge;
            public Int32Field SalesEcomType;
            public Int32Field SalesInvoiceType;
            
            public Int32Field SalesTrasportationId;
            public Int32Field SalesQuotationNo;
            public DateTimeField SalesQuotationDate;
            public DoubleField SalesConversion;
            public StringField SalesPurchaseOrderNo;
            public DateTimeField SalesClosingDate;
            public StringField SalesAttachments;
            public BooleanField SalesCurrencyConversion;
            public Int32Field SalesFromCurrency;
            public Int32Field SalesToCurrency;
            public BooleanField SalesTaxable;

            public StringField Code;
            public BooleanField Inclusive;
            public DecimalField DiscPrice;
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
