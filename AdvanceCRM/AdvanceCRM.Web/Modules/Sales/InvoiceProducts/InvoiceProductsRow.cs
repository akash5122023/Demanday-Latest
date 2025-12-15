
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

    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[InvoiceProducts]")]
    [DisplayName("Invoice Products"), InstanceName("Invoice Products")]
    [ReadPermission("Proforma:Read")]
    [ModifyPermission("Proforma:Read")]

    public sealed class InvoiceProductsRow : Row<InvoiceProductsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, SortOrder(1),IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Products"), NotNull, ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName")]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductsId
        {
            get { return Fields.ProductsId[this]; }
            set { Fields.ProductsId[this] = value; }
        }

        [DisplayName("Capacity"), Size(200)]
        public String Capacity
        {
            get { return Fields.Capacity[this]; }
            set { Fields.Capacity[this] = value; }
        }

        [DisplayName("Quantity"), NotNull]
        public Double? Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("MRP"), DisplayFormat("#,##0.####"), Column("MRP"), NotNull, DecimalEditor(Decimals = 4)]
        public Double? Mrp
        {
            get { return Fields.Mrp[this]; }
            set { Fields.Mrp[this] = value; }
        }

        [DisplayName("Base Price"), DisplayFormat("#,##0.####"), NotNull, DecimalEditor(Decimals = 4)]
        public Double? SellingPrice
        {
            get { return Fields.SellingPrice[this]; }
            set { Fields.SellingPrice[this] = value; }
        }

        [DisplayName("Price"), DisplayFormat("#,##0.####"), NotNull, DecimalEditor(Decimals = 4)]
        public Double? Price
        {
            get { return Fields.Price[this]; }
            set { Fields.Price[this] = value; }
        }

        [DisplayName("Disc(%)"), NotNull]
        public Double? Discount
        {
            get { return Fields.Discount[this]; }
            set { Fields.Discount[this] = value; }
        }

        [DisplayName("Tax 1"), Size(100)]
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

        [DisplayName("Invoice"), NotNull, ForeignKey("[dbo].[Invoice]", "Id"), LeftJoin("jInvoice"), TextualField("InvoiceAdditionalInfo")]
        [LookupEditor(typeof(InvoiceRow), InplaceAdd = true)]
        public Int32? InvoiceId
        {
            get { return Fields.InvoiceId[this]; }
            set { Fields.InvoiceId[this] = value; }
        }

        [DisplayName("Disc(Amt)"), NotNull]
        public Double? DiscountAmount
        {
            get { return Fields.DiscountAmount[this]; }
            set { Fields.DiscountAmount[this] = value; }
        }

        [DisplayName("Description"), Size(2000), QuickSearch, TextAreaEditor(Rows = 4), NameProperty]
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

        [DisplayName("Invoice Contacts Id"), Expression("jInvoice.[ContactsId]")]
        public Int32? InvoiceContactsId
        {
            get { return Fields.InvoiceContactsId[this]; }
            set { Fields.InvoiceContactsId[this] = value; }
        }

        [DisplayName("Invoice Date"), Expression("jInvoice.[Date]")]
        public DateTime? InvoiceDate
        {
            get { return Fields.InvoiceDate[this]; }
            set { Fields.InvoiceDate[this] = value; }
        }

        [DisplayName("Invoice Status"), Expression("jInvoice.[Status]")]
        public Int32? InvoiceStatus
        {
            get { return Fields.InvoiceStatus[this]; }
            set { Fields.InvoiceStatus[this] = value; }
        }

        [DisplayName("Invoice Type"), Expression("jInvoice.[Type]")]
        public Int32? InvoiceType
        {
            get { return Fields.InvoiceType[this]; }
            set { Fields.InvoiceType[this] = value; }
        }

        [DisplayName("Invoice Additional Info"), Expression("jInvoice.[AdditionalInfo]")]
        public String InvoiceAdditionalInfo
        {
            get { return Fields.InvoiceAdditionalInfo[this]; }
            set { Fields.InvoiceAdditionalInfo[this] = value; }
        }

        [DisplayName("Invoice Source Id"), Expression("jInvoice.[SourceId]")]
        public Int32? InvoiceSourceId
        {
            get { return Fields.InvoiceSourceId[this]; }
            set { Fields.InvoiceSourceId[this] = value; }
        }

        [DisplayName("Invoice Stage Id"), Expression("jInvoice.[StageId]")]
        public Int32? InvoiceStageId
        {
            get { return Fields.InvoiceStageId[this]; }
            set { Fields.InvoiceStageId[this] = value; }
        }

        [DisplayName("Invoice Branch Id"), Expression("jInvoice.[BranchId]")]
        public Int32? InvoiceBranchId
        {
            get { return Fields.InvoiceBranchId[this]; }
            set { Fields.InvoiceBranchId[this] = value; }
        }

        [DisplayName("Invoice Owner Id"), Expression("jInvoice.[OwnerId]")]
        public Int32? InvoiceOwnerId
        {
            get { return Fields.InvoiceOwnerId[this]; }
            set { Fields.InvoiceOwnerId[this] = value; }
        }

        [DisplayName("Invoice Assigned Id"), Expression("jInvoice.[AssignedId]")]
        public Int32? InvoiceAssignedId
        {
            get { return Fields.InvoiceAssignedId[this]; }
            set { Fields.InvoiceAssignedId[this] = value; }
        }

        [DisplayName("Invoice Other Address"), Expression("jInvoice.[OtherAddress]")]
        public Boolean? InvoiceOtherAddress
        {
            get { return Fields.InvoiceOtherAddress[this]; }
            set { Fields.InvoiceOtherAddress[this] = value; }
        }

        [DisplayName("Invoice Shipping Address"), Expression("jInvoice.[ShippingAddress]")]
        public String InvoiceShippingAddress
        {
            get { return Fields.InvoiceShippingAddress[this]; }
            set { Fields.InvoiceShippingAddress[this] = value; }
        }

        [DisplayName("Invoice Packaging Charges"), Expression("jInvoice.[PackagingCharges]")]
        public Double? InvoicePackagingCharges
        {
            get { return Fields.InvoicePackagingCharges[this]; }
            set { Fields.InvoicePackagingCharges[this] = value; }
        }

        [DisplayName("Invoice Freight Charges"), Expression("jInvoice.[FreightCharges]")]
        public Double? InvoiceFreightCharges
        {
            get { return Fields.InvoiceFreightCharges[this]; }
            set { Fields.InvoiceFreightCharges[this] = value; }
        }

        [DisplayName("Invoice Advacne"), Expression("jInvoice.[Advacne]")]
        public Double? InvoiceAdvacne
        {
            get { return Fields.InvoiceAdvacne[this]; }
            set { Fields.InvoiceAdvacne[this] = value; }
        }

        [DisplayName("Invoice Due Date"), Expression("jInvoice.[DueDate]")]
        public DateTime? InvoiceDueDate
        {
            get { return Fields.InvoiceDueDate[this]; }
            set { Fields.InvoiceDueDate[this] = value; }
        }

        [DisplayName("Invoice Dispatch Details"), Expression("jInvoice.[DispatchDetails]")]
        public String InvoiceDispatchDetails
        {
            get { return Fields.InvoiceDispatchDetails[this]; }
            set { Fields.InvoiceDispatchDetails[this] = value; }
        }

        [DisplayName("Invoice Roundup"), Expression("jInvoice.[Roundup]")]
        public Double? InvoiceRoundup
        {
            get { return Fields.InvoiceRoundup[this]; }
            set { Fields.InvoiceRoundup[this] = value; }
        }

        [DisplayName("Invoice Subject"), Expression("jInvoice.[Subject]")]
        public String InvoiceSubject
        {
            get { return Fields.InvoiceSubject[this]; }
            set { Fields.InvoiceSubject[this] = value; }
        }

        [DisplayName("Invoice Reference"), Expression("jInvoice.[Reference]")]
        public String InvoiceReference
        {
            get { return Fields.InvoiceReference[this]; }
            set { Fields.InvoiceReference[this] = value; }
        }

        [DisplayName("Invoice Contact Person Id"), Expression("jInvoice.[ContactPersonId]")]
        public Int32? InvoiceContactPersonId
        {
            get { return Fields.InvoiceContactPersonId[this]; }
            set { Fields.InvoiceContactPersonId[this] = value; }
        }

        [DisplayName("Invoice Lines"), Expression("jInvoice.[Lines]")]
        public Int32? InvoiceLines
        {
            get { return Fields.InvoiceLines[this]; }
            set { Fields.InvoiceLines[this] = value; }
        }

        [DisplayName("Invoice Quotation No"), Expression("jInvoice.[QuotationNo]")]
        public Int32? InvoiceQuotationNo
        {
            get { return Fields.InvoiceQuotationNo[this]; }
            set { Fields.InvoiceQuotationNo[this] = value; }
        }

        [DisplayName("Invoice Quotation Date"), Expression("jInvoice.[QuotationDate]")]
        public DateTime? InvoiceQuotationDate
        {
            get { return Fields.InvoiceQuotationDate[this]; }
            set { Fields.InvoiceQuotationDate[this] = value; }
        }

        [DisplayName("Invoice Conversion"), Expression("jInvoice.[Conversion]")]
        public Double? InvoiceConversion
        {
            get { return Fields.InvoiceConversion[this]; }
            set { Fields.InvoiceConversion[this] = value; }
        }

        [DisplayName("Invoice Purchase Order No"), Expression("jInvoice.[PurchaseOrderNo]")]
        public String InvoicePurchaseOrderNo
        {
            get { return Fields.InvoicePurchaseOrderNo[this]; }
            set { Fields.InvoicePurchaseOrderNo[this] = value; }
        }

        [DisplayName("Invoice Closing Date"), Expression("jInvoice.[ClosingDate]")]
        public DateTime? InvoiceClosingDate
        {
            get { return Fields.InvoiceClosingDate[this]; }
            set { Fields.InvoiceClosingDate[this] = value; }
        }

        [DisplayName("Invoice Attachments"), Expression("jInvoice.[Attachments]")]
        public String InvoiceAttachments
        {
            get { return Fields.InvoiceAttachments[this]; }
            set { Fields.InvoiceAttachments[this] = value; }
        }

        [DisplayName("Invoice Currency Conversion"), Expression("jInvoice.[CurrencyConversion]")]
        public Boolean? InvoiceCurrencyConversion
        {
            get { return Fields.InvoiceCurrencyConversion[this]; }
            set { Fields.InvoiceCurrencyConversion[this] = value; }
        }

        [DisplayName("Invoice From Currency"), Expression("jInvoice.[FromCurrency]")]
        public Int32? InvoiceFromCurrency
        {
            get { return Fields.InvoiceFromCurrency[this]; }
            set { Fields.InvoiceFromCurrency[this] = value; }
        }

        [DisplayName("Invoice To Currency"), Expression("jInvoice.[ToCurrency]")]
        public Int32? InvoiceToCurrency
        {
            get { return Fields.InvoiceToCurrency[this]; }
            set { Fields.InvoiceToCurrency[this] = value; }
        }

        [DisplayName("Invoice Taxable"), Expression("jInvoice.[Taxable]")]
        public Boolean? InvoiceTaxable
        {
            get { return Fields.InvoiceTaxable[this]; }
            set { Fields.InvoiceTaxable[this] = value; }
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

        [DisplayName("Grand Total"), Expression("(((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) + (IIF((SELECT Taxable FROM Invoice WHERE Id=t0.[InvoiceId]) = 0, 0,  (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage1] / 100)) + (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage2] / 100)))))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
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

       
        public InvoiceProductsRow()
            : base(Fields)
        {
        }
        public InvoiceProductsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProductsId;
            public StringField Capacity;
            public DoubleField Quantity;
            public DoubleField Mrp;
            public DoubleField SellingPrice;
            public DoubleField Price;
            public DoubleField Discount;
            public StringField TaxType1;
            public DoubleField Percentage1;
            public StringField TaxType2;
            public DoubleField Percentage2;
            public DateTimeField WarrantyStart;
            public DateTimeField WarrantyEnd;
            public Int32Field InvoiceId;
            public DoubleField DiscountAmount;
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

            public Int32Field InvoiceContactsId;
            public DateTimeField InvoiceDate;
            public Int32Field InvoiceStatus;
            public Int32Field InvoiceType;
            public StringField InvoiceAdditionalInfo;
            public Int32Field InvoiceSourceId;
            public Int32Field InvoiceStageId;
            public Int32Field InvoiceBranchId;
            public Int32Field InvoiceOwnerId;
            public Int32Field InvoiceAssignedId;
            public BooleanField InvoiceOtherAddress;
            public StringField InvoiceShippingAddress;
            public DoubleField InvoicePackagingCharges;
            public DoubleField InvoiceFreightCharges;
            public DoubleField InvoiceAdvacne;
            public DateTimeField InvoiceDueDate;
            public StringField InvoiceDispatchDetails;
            public DoubleField InvoiceRoundup;
            public StringField InvoiceSubject;
            public StringField InvoiceReference;
            public Int32Field InvoiceContactPersonId;
            public Int32Field InvoiceLines;
            
            public Int32Field InvoiceQuotationNo;
            public DateTimeField InvoiceQuotationDate;
            public DoubleField InvoiceConversion;
            public StringField InvoicePurchaseOrderNo;
            public DateTimeField InvoiceClosingDate;
            public StringField InvoiceAttachments;
            public BooleanField InvoiceCurrencyConversion;
            public Int32Field InvoiceFromCurrency;
            public Int32Field InvoiceToCurrency;
            public BooleanField InvoiceTaxable;
            
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
