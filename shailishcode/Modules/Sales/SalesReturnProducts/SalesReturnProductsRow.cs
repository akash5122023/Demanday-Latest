
namespace AdvanceCRM.Sales
{
    using AdvanceCRM.Products;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[SalesReturnProducts]")]
    [DisplayName("Sales Return Products"), InstanceName("Sales Return Products")]
    [ReadPermission("SalesReturn:Read")]
    [InsertPermission("SalesReturn:Insert")]
    [UpdatePermission("SalesReturn:Update")]
    [DeletePermission("SalesReturn:Delete")]

    public sealed class SalesReturnProductsRow : Row<SalesReturnProductsRow.RowFields>, IIdRow, INameRow
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

        [DisplayName("Serial"), Size(100), QuickSearch,NameProperty]
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

        [DisplayName("Price"), DisplayFormat("#,##0.####"), NotNull]
        public Double? Price
        {
            get { return Fields.Price[this]; }
            set { Fields.Price[this] = value; }
        }

        //[DisplayName("Disc(%)"), NotNull]
        //public Double? Discount
        //{
        //    get { return Fields.Discount[this]; }
        //    set { Fields.Discount[this] = value; }
        //}

        //[DisplayName("Disc(Amt)"), NotNull]
        //public Double? DiscountAmount
        //{
        //    get { return Fields.DiscountAmount[this]; }
        //    set { Fields.DiscountAmount[this] = value; }
        //}

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

        [DisplayName("Description"), Size(2000), TextAreaEditor(Rows = 4)]
        public String Description
        {
            get { return Fields.Description[this]; }
            set { Fields.Description[this] = value; }
        }

        //[DisplayName("Unit"), Expression("jProductsUnit.[ProductsUnit]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        //public String Unit
        //{
        //    get { return Fields.Unit[this]; }
        //    set { Fields.Unit[this] = value; }
        //}

        [DisplayName("Sales Return"), NotNull, ForeignKey("[dbo].[SalesReturn]", "Id"), LeftJoin("jSalesReturn"), TextualField("SalesReturnAdditionalInfo")]
        [LookupEditor(typeof(SalesReturnRow), InplaceAdd = true)]
        public Int32? SalesReturnId
        {
            get { return Fields.SalesReturnId[this]; }
            set { Fields.SalesReturnId[this] = value; }
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
        public Int32? ProductsUnitId
        {
            get { return Fields.ProductsUnitId[this]; }
            set { Fields.ProductsUnitId[this] = value; }
        }

        [DisplayName("Sales Return Contacts Id"), Expression("jSalesReturn.[ContactsId]")]
        public Int32? SalesReturnContactsId
        {
            get { return Fields.SalesReturnContactsId[this]; }
            set { Fields.SalesReturnContactsId[this] = value; }
        }

        [DisplayName("Sales Return Date"), Expression("jSalesReturn.[Date]")]
        public DateTime? SalesReturnDate
        {
            get { return Fields.SalesReturnDate[this]; }
            set { Fields.SalesReturnDate[this] = value; }
        }

        [DisplayName("Sales Return Status"), Expression("jSalesReturn.[Status]")]
        public Int32? SalesReturnStatus
        {
            get { return Fields.SalesReturnStatus[this]; }
            set { Fields.SalesReturnStatus[this] = value; }
        }

        [DisplayName("Sales Return Type"), Expression("jSalesReturn.[Type]")]
        public Int32? SalesReturnType
        {
            get { return Fields.SalesReturnType[this]; }
            set { Fields.SalesReturnType[this] = value; }
        }

        [DisplayName("Sales Return Additional Info"), Expression("jSalesReturn.[AdditionalInfo]")]
        public String SalesReturnAdditionalInfo
        {
            get { return Fields.SalesReturnAdditionalInfo[this]; }
            set { Fields.SalesReturnAdditionalInfo[this] = value; }
        }

        [DisplayName("Sales Return Source Id"), Expression("jSalesReturn.[SourceId]")]
        public Int32? SalesReturnSourceId
        {
            get { return Fields.SalesReturnSourceId[this]; }
            set { Fields.SalesReturnSourceId[this] = value; }
        }

        [DisplayName("Sales Return Stage Id"), Expression("jSalesReturn.[StageId]")]
        public Int32? SalesReturnStageId
        {
            get { return Fields.SalesReturnStageId[this]; }
            set { Fields.SalesReturnStageId[this] = value; }
        }

        [DisplayName("Sales Return Branch Id"), Expression("jSalesReturn.[BranchId]")]
        public Int32? SalesReturnBranchId
        {
            get { return Fields.SalesReturnBranchId[this]; }
            set { Fields.SalesReturnBranchId[this] = value; }
        }

        [DisplayName("Sales Return Owner Id"), Expression("jSalesReturn.[OwnerId]")]
        public Int32? SalesReturnOwnerId
        {
            get { return Fields.SalesReturnOwnerId[this]; }
            set { Fields.SalesReturnOwnerId[this] = value; }
        }

        [DisplayName("Sales Return Assigned Id"), Expression("jSalesReturn.[AssignedId]")]
        public Int32? SalesReturnAssignedId
        {
            get { return Fields.SalesReturnAssignedId[this]; }
            set { Fields.SalesReturnAssignedId[this] = value; }
        }

        [DisplayName("Sales Return Other Address"), Expression("jSalesReturn.[OtherAddress]")]
        public Boolean? SalesReturnOtherAddress
        {
            get { return Fields.SalesReturnOtherAddress[this]; }
            set { Fields.SalesReturnOtherAddress[this] = value; }
        }

        [DisplayName("Sales Return Shipping Address"), Expression("jSalesReturn.[ShippingAddress]")]
        public String SalesReturnShippingAddress
        {
            get { return Fields.SalesReturnShippingAddress[this]; }
            set { Fields.SalesReturnShippingAddress[this] = value; }
        }

        [DisplayName("Sales Return Packaging Charges"), Expression("jSalesReturn.[PackagingCharges]")]
        public Double? SalesReturnPackagingCharges
        {
            get { return Fields.SalesReturnPackagingCharges[this]; }
            set { Fields.SalesReturnPackagingCharges[this] = value; }
        }

        [DisplayName("Sales Return Freight Charges"), Expression("jSalesReturn.[FreightCharges]")]
        public Double? SalesReturnFreightCharges
        {
            get { return Fields.SalesReturnFreightCharges[this]; }
            set { Fields.SalesReturnFreightCharges[this] = value; }
        }

        [DisplayName("Sales Return Advacne"), Expression("jSalesReturn.[Advacne]")]
        public Double? SalesReturnAdvacne
        {
            get { return Fields.SalesReturnAdvacne[this]; }
            set { Fields.SalesReturnAdvacne[this] = value; }
        }

        [DisplayName("Sales Return Due Date"), Expression("jSalesReturn.[DueDate]")]
        public DateTime? SalesReturnDueDate
        {
            get { return Fields.SalesReturnDueDate[this]; }
            set { Fields.SalesReturnDueDate[this] = value; }
        }

        [DisplayName("Sales Return Dispatch Details"), Expression("jSalesReturn.[DispatchDetails]")]
        public String SalesReturnDispatchDetails
        {
            get { return Fields.SalesReturnDispatchDetails[this]; }
            set { Fields.SalesReturnDispatchDetails[this] = value; }
        }

        [DisplayName("Sales Return Roundup"), Expression("jSalesReturn.[Roundup]")]
        public Double? SalesReturnRoundup
        {
            get { return Fields.SalesReturnRoundup[this]; }
            set { Fields.SalesReturnRoundup[this] = value; }
        }

        [DisplayName("Sales Return Contact Person Id"), Expression("jSalesReturn.[ContactPersonId]")]
        public Int32? SalesReturnContactPersonId
        {
            get { return Fields.SalesReturnContactPersonId[this]; }
            set { Fields.SalesReturnContactPersonId[this] = value; }
        }

        [DisplayName("Sales Return Lines"), Expression("jSalesReturn.[Lines]")]
        public Int32? SalesReturnLines
        {
            get { return Fields.SalesReturnLines[this]; }
            set { Fields.SalesReturnLines[this] = value; }
        }

        [DisplayName("Sales Return Invoice No"), Expression("jSalesReturn.[InvoiceNo]")]
        public Int32? SalesReturnInvoiceNo
        {
            get { return Fields.SalesReturnInvoiceNo[this]; }
            set { Fields.SalesReturnInvoiceNo[this] = value; }
        }

        [DisplayName("Sales Return Reverse Charge"), Expression("jSalesReturn.[ReverseCharge]")]
        public Boolean? SalesReturnReverseCharge
        {
            get { return Fields.SalesReturnReverseCharge[this]; }
            set { Fields.SalesReturnReverseCharge[this] = value; }
        }

        [DisplayName("Sales Return Ecom Type"), Expression("jSalesReturn.[EcomType]")]
        public Int32? SalesReturnEcomType
        {
            get { return Fields.SalesReturnEcomType[this]; }
            set { Fields.SalesReturnEcomType[this] = value; }
        }

        [DisplayName("Sales Return Invoice Type"), Expression("jSalesReturn.[InvoiceType]")]
        public Int32? SalesReturnInvoiceType
        {
            get { return Fields.SalesReturnInvoiceType[this]; }
            set { Fields.SalesReturnInvoiceType[this] = value; }
        }

        [DisplayName("Sales Return Trasportation Id"), Expression("jSalesReturn.[TrasportationId]")]
        public Int32? SalesReturnTrasportationId
        {
            get { return Fields.SalesReturnTrasportationId[this]; }
            set { Fields.SalesReturnTrasportationId[this] = value; }
        }

        [DisplayName("Sales Return Quotation No"), Expression("jSalesReturn.[QuotationNo]")]
        public Int32? SalesReturnQuotationNo
        {
            get { return Fields.SalesReturnQuotationNo[this]; }
            set { Fields.SalesReturnQuotationNo[this] = value; }
        }

        [DisplayName("Sales Return Quotation Date"), Expression("jSalesReturn.[QuotationDate]")]
        public DateTime? SalesReturnQuotationDate
        {
            get { return Fields.SalesReturnQuotationDate[this]; }
            set { Fields.SalesReturnQuotationDate[this] = value; }
        }

        [DisplayName("Sales Return Conversion"), Expression("jSalesReturn.[Conversion]")]
        public Double? SalesReturnConversion
        {
            get { return Fields.SalesReturnConversion[this]; }
            set { Fields.SalesReturnConversion[this] = value; }
        }

        [DisplayName("Sales Return Purchase Order No"), Expression("jSalesReturn.[PurchaseOrderNo]")]
        public String SalesReturnPurchaseOrderNo
        {
            get { return Fields.SalesReturnPurchaseOrderNo[this]; }
            set { Fields.SalesReturnPurchaseOrderNo[this] = value; }
        }

        [DisplayName("Sales Return Closing Date"), Expression("jSalesReturn.[ClosingDate]")]
        public DateTime? SalesReturnClosingDate
        {
            get { return Fields.SalesReturnClosingDate[this]; }
            set { Fields.SalesReturnClosingDate[this] = value; }
        }

        [DisplayName("Sales Return Attachments"), Expression("jSalesReturn.[Attachments]")]
        public String SalesReturnAttachments
        {
            get { return Fields.SalesReturnAttachments[this]; }
            set { Fields.SalesReturnAttachments[this] = value; }
        }

        [DisplayName("Sales Return Currency Conversion"), Expression("jSalesReturn.[CurrencyConversion]")]
        public Boolean? SalesReturnCurrencyConversion
        {
            get { return Fields.SalesReturnCurrencyConversion[this]; }
            set { Fields.SalesReturnCurrencyConversion[this] = value; }
        }

        [DisplayName("Sales Return From Currency"), Expression("jSalesReturn.[FromCurrency]")]
        public Int32? SalesReturnFromCurrency
        {
            get { return Fields.SalesReturnFromCurrency[this]; }
            set { Fields.SalesReturnFromCurrency[this] = value; }
        }

        [DisplayName("Sales Return To Currency"), Expression("jSalesReturn.[ToCurrency]")]
        public Int32? SalesReturnToCurrency
        {
            get { return Fields.SalesReturnToCurrency[this]; }
            set { Fields.SalesReturnToCurrency[this] = value; }
        }

        [DisplayName("Sales Return Taxable"), Expression("jSalesReturn.[Taxable]")]
        public Boolean? SalesReturnTaxable
        {
            get { return Fields.SalesReturnTaxable[this]; }
            set { Fields.SalesReturnTaxable[this] = value; }
        }
        
        [DisplayName("Code"), Size(100), NotMapped]
        public String Code { get { return Fields.Code[this]; } set { Fields.Code[this] = value; } }

        [DisplayName("TAX Inclusive"), NotMapped]
        [BooleanSwitchEditor]
        public Boolean? Inclusive { get { return Fields.Inclusive[this]; } set { Fields.Inclusive[this] = value; } }

        [DisplayName("TAX1 Amount"), Expression("((t0.[Price] * t0.[Quantity]) * (t0.[Percentage1] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax1Amount
        {
            get { return Fields.Tax1Amount[this]; }
            set { Fields.Tax1Amount[this] = value; }
        }

        [DisplayName("TAX2 Amount"), Expression("((t0.[Price] * t0.[Quantity]) * (t0.[Percentage2] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax2Amount
        {
            get { return Fields.Tax2Amount[this]; }
            set { Fields.Tax2Amount[this] = value; }
        }

        [DisplayName("Line Total"), Expression("(t0.[Price] * t0.[Quantity])"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
        }

        [DisplayName("Grand Total"), Expression("((t0.[Price] * t0.[Quantity]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Percentage1] / 100)) + ((t0.[Price] * t0.[Quantity]) * (t0.[Percentage2] / 100)))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
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

       

        public SalesReturnProductsRow()
            : base(Fields)
        {
        }
        public SalesReturnProductsRow(RowFields fields)
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
            public StringField TaxType1;
            public DoubleField Percentage1;
            public StringField TaxType2;
            public DoubleField Percentage2;
            public StringField Description;
            public Int32Field SalesReturnId;

            //public DoubleField Discount;
            //public DoubleField DiscountAmount;
            //public StringField Unit;


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

            public Int32Field SalesReturnContactsId;
            public DateTimeField SalesReturnDate;
            public Int32Field SalesReturnStatus;
            public Int32Field SalesReturnType;
            public StringField SalesReturnAdditionalInfo;
            public Int32Field SalesReturnSourceId;
            public Int32Field SalesReturnStageId;
            public Int32Field SalesReturnBranchId;
            public Int32Field SalesReturnOwnerId;
            public Int32Field SalesReturnAssignedId;
            public BooleanField SalesReturnOtherAddress;
            public StringField SalesReturnShippingAddress;
            public DoubleField SalesReturnPackagingCharges;
            public DoubleField SalesReturnFreightCharges;
            public DoubleField SalesReturnAdvacne;
            public DateTimeField SalesReturnDueDate;
            public StringField SalesReturnDispatchDetails;
            public DoubleField SalesReturnRoundup;
            public Int32Field SalesReturnContactPersonId;
            public Int32Field SalesReturnLines;
            public Int32Field SalesReturnInvoiceNo;
            public BooleanField SalesReturnReverseCharge;
            public Int32Field SalesReturnEcomType;
            public Int32Field SalesReturnInvoiceType;
            public Int32Field SalesReturnTrasportationId;
            public Int32Field SalesReturnQuotationNo;
            public DateTimeField SalesReturnQuotationDate;
            public DoubleField SalesReturnConversion;
            public StringField SalesReturnPurchaseOrderNo;
            public DateTimeField SalesReturnClosingDate;
            public StringField SalesReturnAttachments;
            public BooleanField SalesReturnCurrencyConversion;
            public Int32Field SalesReturnFromCurrency;
            public Int32Field SalesReturnToCurrency;
            public BooleanField SalesReturnTaxable;
            
            public StringField Code;
            public BooleanField Inclusive;
            public DecimalField LineTotal;
            public DecimalField Tax1Amount;
            public DecimalField Tax2Amount;
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
