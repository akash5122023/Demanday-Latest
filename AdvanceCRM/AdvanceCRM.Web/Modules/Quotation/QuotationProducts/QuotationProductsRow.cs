
namespace AdvanceCRM.Quotation
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

    [ConnectionKey("Default"), Module("Quotation"), TableName("[dbo].[QuotationProducts]")]
    [DisplayName("Quotation Products"), InstanceName("Quotation Products")]
    [ReadPermission("Quotation:Read")]
    [ModifyPermission("Quotation:Read")]

    public sealed class QuotationProductsRow : Row<QuotationProductsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, SortOrder(1),IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Product"), NotNull, ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName")]
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

        [DisplayName("Quantity"), NotNull, DefaultValue(1)]

        public Double? Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("MRP"), DisplayFormat("#,##0.####"), Column("MRP"), NotNull, DefaultValue(0), DecimalEditor(Decimals = 4)]
        public Double? Mrp
        {
            get { return Fields.Mrp[this]; }
            set { Fields.Mrp[this] = value; }
        }

        [DisplayName("Base Price"), DisplayFormat("#,##0.####"), NotNull, DefaultValue(0), DecimalEditor(Decimals = 4)]
        public Double? SellingPrice
        {
            get { return Fields.SellingPrice[this]; }
            set { Fields.SellingPrice[this] = value; }
        }

        [DisplayName("Price"), DisplayFormat("#,##0.####"), NotNull, DefaultValue(0), DecimalEditor(Decimals = 4)]
        public Double? Price
        {
            get { return Fields.Price[this]; }
            set { Fields.Price[this] = value; }
        }

        [DisplayName("Disc(%)"), NotNull, DefaultValue(0)]
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

        [DisplayName("Percentage"), DefaultValue("0")]
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

        [DisplayName("Percentage"), DefaultValue("0")]
        public Double? Percentage2
        {
            get { return Fields.Percentage2[this]; }
            set { Fields.Percentage2[this] = value; }
        }

        [DisplayName("Quotation"), NotNull, ForeignKey("[dbo].[Quotation]", "Id"), LeftJoin("jQuotation"), TextualField("QuotationAdditionalInfo")]
        [LookupEditor(typeof(QuotationRow), InplaceAdd = true)]
        public Int32? QuotationId
        {
            get { return Fields.QuotationId[this]; }
            set { Fields.QuotationId[this] = value; }
        }

        [DisplayName("Disc(Amt)"), NotNull]
        public Double? DiscountAmount
        {
            get { return Fields.DiscountAmount[this]; }
            set { Fields.DiscountAmount[this] = value; }
        }
        //[DisplayName("Image Attachment"), Size(500)]
        //[ImageUploadEditor(FilenameFormat = "QuotationProduct/~", CopyToHistory = true, DisableDefaultBehavior = true)]
        //public String ImageAttachment
        //{
        //    get { return Fields.ImageAttachment[this]; }
        //    set { Fields.ImageAttachment[this] = value; }
        //}

        [DisplayName("File Attachments"), Size(1000)]
        [ImageUploadEditor(FilenameFormat = "QuotationProduct/~", CopyToHistory = true, DisableDefaultBehavior = true)]
        public String FileAttachment
        {
            get { return Fields.FileAttachment[this]; }
            set { Fields.FileAttachment[this] = value; }
        }

        [DisplayName("Description"), Size(2000), TextAreaEditor(Rows = 4),NameProperty]
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
        [DisplayName("Products From"), Expression("jProducts.[From]")]
        public String ProductsFrom
        {
            get { return Fields.ProductsFrom[this]; }
            set { Fields.ProductsFrom[this] = value; }
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

        [DisplayName("Quotation Contacts Id"), Expression("jQuotation.[ContactsId]")]
        public Int32? QuotationContactsId
        {
            get { return Fields.QuotationContactsId[this]; }
            set { Fields.QuotationContactsId[this] = value; }
        }

        [DisplayName("Quotation Date"), Expression("jQuotation.[Date]")]
        public DateTime? QuotationDate
        {
            get { return Fields.QuotationDate[this]; }
            set { Fields.QuotationDate[this] = value; }
        }

        [DisplayName("Quotation Status"), Expression("jQuotation.[Status]")]
        public Int32? QuotationStatus
        {
            get { return Fields.QuotationStatus[this]; }
            set { Fields.QuotationStatus[this] = value; }
        }

        [DisplayName("Quotation Type"), Expression("jQuotation.[Type]")]
        public Int32? QuotationType
        {
            get { return Fields.QuotationType[this]; }
            set { Fields.QuotationType[this] = value; }
        }

        [DisplayName("Quotation Additional Info"), Expression("jQuotation.[AdditionalInfo]")]
        public String QuotationAdditionalInfo
        {
            get { return Fields.QuotationAdditionalInfo[this]; }
            set { Fields.QuotationAdditionalInfo[this] = value; }
        }

        [DisplayName("Quotation Source Id"), Expression("jQuotation.[SourceId]")]
        public Int32? QuotationSourceId
        {
            get { return Fields.QuotationSourceId[this]; }
            set { Fields.QuotationSourceId[this] = value; }
        }

        [DisplayName("Quotation Stage Id"), Expression("jQuotation.[StageId]")]
        public Int32? QuotationStageId
        {
            get { return Fields.QuotationStageId[this]; }
            set { Fields.QuotationStageId[this] = value; }
        }

        [DisplayName("Quotation Branch Id"), Expression("jQuotation.[BranchId]")]
        public Int32? QuotationBranchId
        {
            get { return Fields.QuotationBranchId[this]; }
            set { Fields.QuotationBranchId[this] = value; }
        }

        [DisplayName("Quotation Owner Id"), Expression("jQuotation.[OwnerId]")]
        public Int32? QuotationOwnerId
        {
            get { return Fields.QuotationOwnerId[this]; }
            set { Fields.QuotationOwnerId[this] = value; }
        }

        [DisplayName("Quotation Assigned Id"), Expression("jQuotation.[AssignedId]")]
        public Int32? QuotationAssignedId
        {
            get { return Fields.QuotationAssignedId[this]; }
            set { Fields.QuotationAssignedId[this] = value; }
        }

        [DisplayName("Quotation Reference Name"), Expression("jQuotation.[ReferenceName]")]
        public String QuotationReferenceName
        {
            get { return Fields.QuotationReferenceName[this]; }
            set { Fields.QuotationReferenceName[this] = value; }
        }

        [DisplayName("Quotation Reference Phone"), Expression("jQuotation.[ReferencePhone]")]
        public String QuotationReferencePhone
        {
            get { return Fields.QuotationReferencePhone[this]; }
            set { Fields.QuotationReferencePhone[this] = value; }
        }

        [DisplayName("Quotation Closing Type"), Expression("jQuotation.[ClosingType]")]
        public Int32? QuotationClosingType
        {
            get { return Fields.QuotationClosingType[this]; }
            set { Fields.QuotationClosingType[this] = value; }
        }

        [DisplayName("Quotation Lost Reason"), Expression("jQuotation.[LostReason]")]
        public String QuotationLostReason
        {
            get { return Fields.QuotationLostReason[this]; }
            set { Fields.QuotationLostReason[this] = value; }
        }

        [DisplayName("Quotation Subject"), Expression("jQuotation.[Subject]")]
        public String QuotationSubject
        {
            get { return Fields.QuotationSubject[this]; }
            set { Fields.QuotationSubject[this] = value; }
        }

        [DisplayName("Quotation Reference"), Expression("jQuotation.[Reference]")]
        public String QuotationReference
        {
            get { return Fields.QuotationReference[this]; }
            set { Fields.QuotationReference[this] = value; }
        }

        [DisplayName("Quotation Attachment"), Expression("jQuotation.[Attachment]")]
        public String QuotationAttachment
        {
            get { return Fields.QuotationAttachment[this]; }
            set { Fields.QuotationAttachment[this] = value; }
        }

        [DisplayName("Quotation Lines"), Expression("jQuotation.[Lines]")]
        public Int32? QuotationLines
        {
            get { return Fields.QuotationLines[this]; }
            set { Fields.QuotationLines[this] = value; }
        }

        [DisplayName("Quotation Contact Person Id"), Expression("jQuotation.[ContactPersonId]")]
        public Int32? QuotationContactPersonId
        {
            get { return Fields.QuotationContactPersonId[this]; }
            set { Fields.QuotationContactPersonId[this] = value; }
        }

        [DisplayName("Quotation Closing Date"), Expression("jQuotation.[ClosingDate]")]
        public DateTime? QuotationClosingDate
        {
            get { return Fields.QuotationClosingDate[this]; }
            set { Fields.QuotationClosingDate[this] = value; }
        }

        [DisplayName("Quotation Enquiry No"), Expression("jQuotation.[EnquiryNo]")]
        public Int32? QuotationEnquiryNo
        {
            get { return Fields.QuotationEnquiryNo[this]; }
            set { Fields.QuotationEnquiryNo[this] = value; }
        }

        [DisplayName("Quotation Enquiry Date"), Expression("jQuotation.[EnquiryDate]")]
        public DateTime? QuotationEnquiryDate
        {
            get { return Fields.QuotationEnquiryDate[this]; }
            set { Fields.QuotationEnquiryDate[this] = value; }
        }

        [DisplayName("Quotation Conversion"), Expression("jQuotation.[Conversion]")]
        public Double? QuotationConversion
        {
            get { return Fields.QuotationConversion[this]; }
            set { Fields.QuotationConversion[this] = value; }
        }

        [DisplayName("Quotation Currency Conversion"), Expression("jQuotation.[CurrencyConversion]")]
        public Boolean? QuotationCurrencyConversion
        {
            get { return Fields.QuotationCurrencyConversion[this]; }
            set { Fields.QuotationCurrencyConversion[this] = value; }
        }

        [DisplayName("Quotation From Currency"), Expression("jQuotation.[FromCurrency]")]
        public Int32? QuotationFromCurrency
        {
            get { return Fields.QuotationFromCurrency[this]; }
            set { Fields.QuotationFromCurrency[this] = value; }
        }

        [DisplayName("Quotation To Currency"), Expression("jQuotation.[ToCurrency]")]
        public Int32? QuotationToCurrency
        {
            get { return Fields.QuotationToCurrency[this]; }
            set { Fields.QuotationToCurrency[this] = value; }
        }

        [DisplayName("Quotation Taxable"), Expression("jQuotation.[Taxable]")]
        public Boolean? QuotationTaxable
        {
            get { return Fields.QuotationTaxable[this]; }
            set { Fields.QuotationTaxable[this] = value; }
        }

        [LookupEditor("Products.ProductCodeLookup")]
        [DisplayName("Code"), Size(100), NotMapped]
        public String Code 
        { 
        	get { return Fields.Code[this]; } 
        	set { Fields.Code[this] = value; } 
        }
        
        [DisplayName("Inclusive"), NotMapped]
        [BooleanSwitchEditor]
        public Boolean? Inclusive 
        { 
        	get { return Fields.Inclusive[this]; } 
        	set { Fields.Inclusive[this] = value; }
        }

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

        [DisplayName("Products Division"), Size(200)]
        public String ProductsDivision
        {
            get { return Fields.ProductsDivision[this]; }
            set { Fields.ProductsDivision[this] = value; }
        }

        [DisplayName("Grand Total"), Expression("(((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) + (IIF((SELECT Taxable FROM Quotation WHERE Id=t0.[QuotationId]) = 0, 0,  (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage1] / 100)) + (((t0.[Price] * t0.[Quantity]) - ((t0.[DiscountAmount]) + ((t0.[Price] * t0.[Quantity]) * (t0.[Discount] / 100)))) * (t0.[Percentage2] / 100)))))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? GrandTotal
        {
            get { return Fields.GrandTotal[this]; }
            set { Fields.GrandTotal[this] = value; }
        }

        //  Expression("(SELECT Details FROM QuotationFollowups WHERE QuotationId=t0.[Id] AND Id=(SELECT MAX(Id) FROM QuotationFollowups WHERE QuotationId=t0.[Id]))"), MinSelectLevel(SelectLevel.List)]

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



       

        public QuotationProductsRow()
            : base(Fields)
        {
        }
        
        public QuotationProductsRow(RowFields fields)
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
            public Int32Field QuotationId;
            public DoubleField DiscountAmount;
            public StringField Description;
            public StringField Unit;
            public StringField ProductsDivision;

            public StringField ProductsName;
            public StringField ProductsCode;
            public Int32Field ProductsDivisionId;
            public Int32Field ProductsGroupId;
            public DoubleField ProductsSellingPrice;
            public DoubleField ProductsMrp;
            public StringField ProductsDescription;
            public StringField ProductsFrom;

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
         //   public StringField DivisionProductsDivision;

            public Int32Field QuotationContactsId;
            public DateTimeField QuotationDate;
            public Int32Field QuotationStatus;
            public Int32Field QuotationType;
            public StringField QuotationAdditionalInfo;
            public Int32Field QuotationSourceId;
            public Int32Field QuotationStageId;
            public Int32Field QuotationBranchId;
            public Int32Field QuotationOwnerId;
            public Int32Field QuotationAssignedId;
            public StringField QuotationReferenceName;
            public StringField QuotationReferencePhone;
            public Int32Field QuotationClosingType;
            public StringField QuotationLostReason;
            public StringField QuotationSubject;
            public StringField QuotationReference;
            public StringField QuotationAttachment;
            public Int32Field QuotationLines;
            public Int32Field QuotationContactPersonId;
            public DateTimeField QuotationClosingDate;
            
            public Int32Field QuotationEnquiryNo;
            public DateTimeField QuotationEnquiryDate;
            public DoubleField QuotationConversion;
            public BooleanField QuotationCurrencyConversion;
            public Int32Field QuotationFromCurrency;
            public Int32Field QuotationToCurrency;
            public BooleanField QuotationTaxable;
            
            public StringField Code;
            public BooleanField Inclusive;
            public DecimalField DiscPrice;
            public DecimalField Tax1Amount;
            public DecimalField Tax2Amount;
            public DecimalField LineTotal;
            public DecimalField GrandTotal;


            //public StringField ImageAttachment;
            public StringField FileAttachment;

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
