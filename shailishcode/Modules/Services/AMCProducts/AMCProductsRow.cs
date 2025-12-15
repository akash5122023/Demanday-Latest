
namespace AdvanceCRM.Services
{
    using AdvanceCRM.Products;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Services"), TableName("[dbo].[AMCProducts]")]
    [DisplayName("AMC Products"), InstanceName("AMC Products")]
    [ReadPermission("AMC:Read")]
    [InsertPermission("AMC:Insert")]
    [UpdatePermission("AMC:Update")]
    [DeletePermission("AMC:Delete")]


    public sealed class AMCProductsRow : Row<AMCProductsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
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

        [DisplayName("Serial No"), Size(350), QuickSearch,NameProperty]
        public String SerialNo
        {
            get { return Fields.SerialNo[this]; }
            set { Fields.SerialNo[this] = value; }
        }

        [DisplayName("Rate"), NotNull]
        public Double? Rate
        {
            get { return Fields.Rate[this]; }
            set { Fields.Rate[this] = value; }
        }

        [DisplayName("Rate Type"), NotNull]
        public Masters.AMCTypeMaster? Type
        {
            get { return (Masters.AMCTypeMaster?)Fields.Type[this]; }
            set { Fields.Type[this] = (Int32?)value; }
        }

        [DisplayName("Quantity")]
        public Int32? Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("Visits"), NotNull]
        public Int32? Visits
        {
            get { return Fields.Visits[this]; }
            set { Fields.Visits[this] = value; }
        }

        [DisplayName("Disc(%)"), NotNull]
        public Double? Discount
        {
            get { return Fields.Discount[this]; }
            set { Fields.Discount[this] = value; }
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

        [DisplayName("AMC"), Column("AMCId"), NotNull, ForeignKey("[dbo].[AMC]", "Id"), LeftJoin("jAMC"), TextualField("AMCAdditionalInfo")]
        [LookupEditor(typeof(AMCRow), InplaceAdd = true)]
        public Int32? AMCId
        {
            get { return Fields.AMCId[this]; }
            set { Fields.AMCId[this] = value; }
        }

        [DisplayName("Disc(Amt)"), NotNull]
        public Double? DiscountAmount
        {
            get { return Fields.DiscountAmount[this]; }
            set { Fields.DiscountAmount[this] = value; }
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

        [DisplayName("AMC Date"), Expression("jAMC.[Date]")]
        public DateTime? AMCDate
        {
            get { return Fields.AMCDate[this]; }
            set { Fields.AMCDate[this] = value; }
        }

        [DisplayName("AMC Contacts Id"), Expression("jAMC.[ContactsId]")]
        public Int32? AMCContactsId
        {
            get { return Fields.AMCContactsId[this]; }
            set { Fields.AMCContactsId[this] = value; }
        }

        [DisplayName("AMC Status"), Expression("jAMC.[Status]")]
        public Int32? AMCStatus
        {
            get { return Fields.AMCStatus[this]; }
            set { Fields.AMCStatus[this] = value; }
        }

        [DisplayName("AMC Start Date"), Expression("jAMC.[StartDate]")]
        public DateTime? AMCStartDate
        {
            get { return Fields.AMCStartDate[this]; }
            set { Fields.AMCStartDate[this] = value; }
        }

        [DisplayName("AMC End Date"), Expression("jAMC.[EndDate]")]
        public DateTime? AMCEndDate
        {
            get { return Fields.AMCEndDate[this]; }
            set { Fields.AMCEndDate[this] = value; }
        }

        [DisplayName("AMC Additional Info"), Expression("jAMC.[AdditionalInfo]")]
        public String AMCAdditionalInfo
        {
            get { return Fields.AMCAdditionalInfo[this]; }
            set { Fields.AMCAdditionalInfo[this] = value; }
        }

        [DisplayName("AMC Owner Id"), Expression("jAMC.[OwnerId]")]
        public Int32? AMCOwnerId
        {
            get { return Fields.AMCOwnerId[this]; }
            set { Fields.AMCOwnerId[this] = value; }
        }

        [DisplayName("AMC Assigned Id"), Expression("jAMC.[AssignedId]")]
        public Int32? AMCAssignedId
        {
            get { return Fields.AMCAssignedId[this]; }
            set { Fields.AMCAssignedId[this] = value; }
        }

        [DisplayName("AMC Attachment"), Expression("jAMC.[Attachment]")]
        public String AMCAttachment
        {
            get { return Fields.AMCAttachment[this]; }
            set { Fields.AMCAttachment[this] = value; }
        }

        [DisplayName("AMC Lines"), Expression("jAMC.[Lines]")]
        public Int32? AMCLines
        {
            get { return Fields.AMCLines[this]; }
            set { Fields.AMCLines[this] = value; }
        }

        [DisplayName("AMC Terms"), Expression("jAMC.[Terms]")]
        public String AMCTerms
        {
            get { return Fields.AMCTerms[this]; }
            set { Fields.AMCTerms[this] = value; }
        }

        [DisplayName("Code"), Size(100), NotMapped]
        public String Code { get { return Fields.Code[this]; } set { Fields.Code[this] = value; } }
        
        [DisplayName("Inclusive"), NotMapped]
        [BooleanSwitchEditor]
        public Boolean? Inclusive { get { return Fields.Inclusive[this]; } set { Fields.Inclusive[this] = value; } }
        
        [DisplayName("TAX1 Amount"), Expression("(((t0.[Rate] - (t0.[DiscountAmount] + (t0.[Rate] * (t0.[Discount] / 100))) )* (t0.[Percentage1] / 100)) * (t0.[Quantity] + t0.[Visits]))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax1Amount
        {
            get { return Fields.Tax1Amount[this]; }
            set { Fields.Tax1Amount[this] = value; }
        }

        
        [DisplayName("TAX2 Amount"), Expression("(((t0.[Rate] - (t0.[DiscountAmount] + (t0.[Rate] * (t0.[Discount] / 100))) )* (t0.[Percentage2] / 100)) * (t0.[Quantity] + t0.[Visits]))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? Tax2Amount
        {
            get { return Fields.Tax2Amount[this]; }
            set { Fields.Tax2Amount[this] = value; }
        }

        
        [DisplayName("Line Total"), Expression("(t0.[Rate] * ((t0.[Quantity] + t0.[Visits])) - t0.[DiscountAmount] - ((t0.[Rate] * ((t0.[Quantity] + t0.[Visits]))) * t0.[Discount] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
        }

        [DisplayName("Grand Total"), Expression("(((t0.[Rate] * (t0.[Quantity] + t0.[Visits])) - ((t0.[DiscountAmount] * (t0.[Quantity] + t0.[Visits])) + ((t0.[Rate] * (t0.[Quantity] + t0.[Visits])) * (t0.[Discount] / 100)))) + (((t0.[Rate] * (t0.[Quantity] + t0.[Visits])) - ((t0.[DiscountAmount] * (t0.[Quantity] + t0.[Visits])) + ((t0.[Rate] * (t0.[Quantity] + t0.[Visits])) * (t0.[Discount] / 100)))) * (t0.[Percentage1] / 100)) + (((t0.[Rate] * (t0.[Quantity] + t0.[Visits])) - ((t0.[DiscountAmount] * (t0.[Quantity] + t0.[Visits])) + ((t0.[Rate] * (t0.[Quantity] + t0.[Visits])) * (t0.[Discount] / 100)))) * (t0.[Percentage2] / 100)))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? GrandTotal
        {
            get { return Fields.GrandTotal[this]; }
            set { Fields.GrandTotal[this] = value; }
        }

       
        public AMCProductsRow()
            : base(Fields)
        {
        }
        
       
        public AMCProductsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProductsId;
            public StringField SerialNo;
            public DoubleField Rate;
            public Int32Field Type;
            public Int32Field Quantity;
            public Int32Field Visits;
            public DoubleField Discount;
            public StringField TaxType1;
            public DoubleField Percentage1;
            public StringField TaxType2;
            public DoubleField Percentage2;
            public Int32Field AMCId;
            public DoubleField DiscountAmount;

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

            public DateTimeField AMCDate;
            public Int32Field AMCContactsId;
            public Int32Field AMCStatus;
            public DateTimeField AMCStartDate;
            public DateTimeField AMCEndDate;
            public StringField AMCAdditionalInfo;
            public Int32Field AMCOwnerId;
            public Int32Field AMCAssignedId;
            public StringField AMCAttachment;
            
            public Int32Field AMCLines;
            public StringField AMCTerms;

            public StringField Code;
            public BooleanField Inclusive;
            public DecimalField Tax1Amount;
            public DecimalField Tax2Amount;
            public DecimalField LineTotal;
            public DecimalField GrandTotal;
        }
    }
}
