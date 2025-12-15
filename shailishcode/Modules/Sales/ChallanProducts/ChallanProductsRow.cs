
namespace AdvanceCRM.Sales
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Products;
using Microsoft.AspNetCore.Mvc;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[ChallanProducts]")]
    [DisplayName("Challan Products"), InstanceName("Challan Products")]
    [ReadPermission("Challan:Read")]
    [ModifyPermission("Challan:Read")]

    public sealed class ChallanProductsRow : Row<ChallanProductsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
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

        [DisplayName("Quantity"), NotNull, DefaultValue("1")]
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

        [DisplayName("Base Price"), DisplayFormat("#,##0.####"), NotNull]
        public Double? SellingPrice
        {
            get { return Fields.SellingPrice[this]; }
            set { Fields.SellingPrice[this] = value; }
        }

        [DisplayName("Branch"), Column("BranchId")]
        [ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jBranch")]
        [LookupEditor(typeof(Administration.BranchRow))]
        public Int32? BranchId
        {
            get => fields.BranchId[this];
            set => fields.BranchId[this] = value;
        }

        [DisplayName("MRP"), DisplayFormat("#,##0.####"), Column("MRP"), NotNull]
        public Double? Mrp
        {
            get { return Fields.Mrp[this]; }
            set { Fields.Mrp[this] = value; }
        }

        [DisplayName("Disc(%)"), NotNull, DefaultValue("0")]
        public Double? Discount
        {
            get { return Fields.Discount[this]; }
            set { Fields.Discount[this] = value; }
        }

        [DisplayName("Disc(Amt)"), NotNull, DefaultValue("0")]
        public Double? DiscountAmount
        {
            get { return Fields.DiscountAmount[this]; }
            set { Fields.DiscountAmount[this] = value; }
        }

        [DisplayName("Challan"), NotNull, ForeignKey("[dbo].[Challan]", "Id"), LeftJoin("jChallan"), TextualField("ChallanShippingAddress")]
        [LookupEditor(typeof(ChallanRow), InplaceAdd = true)]
        public Int32? ChallanId
        {
            get { return Fields.ChallanId[this]; }
            set { Fields.ChallanId[this] = value; }
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

        [DisplayName("Challan Contacts Id"), Expression("jChallan.[ContactsId]")]
        public Int32? ChallanContactsId
        {
            get { return Fields.ChallanContactsId[this]; }
            set { Fields.ChallanContactsId[this] = value; }
        }

        [DisplayName("Challan Date"), Expression("jChallan.[Date]")]
        public DateTime? ChallanDate
        {
            get { return Fields.ChallanDate[this]; }
            set { Fields.ChallanDate[this] = value; }
        }

        [DisplayName("Challan Other Address"), Expression("jChallan.[OtherAddress]")]
        public Boolean? ChallanOtherAddress
        {
            get { return Fields.ChallanOtherAddress[this]; }
            set { Fields.ChallanOtherAddress[this] = value; }
        }

        [DisplayName("Challan Shipping Address"), Expression("jChallan.[ShippingAddress]")]
        public String ChallanShippingAddress
        {
            get { return Fields.ChallanShippingAddress[this]; }
            set { Fields.ChallanShippingAddress[this] = value; }
        }

        [DisplayName("Challan Packaging Charges"), Expression("jChallan.[PackagingCharges]")]
        public Double? ChallanPackagingCharges
        {
            get { return Fields.ChallanPackagingCharges[this]; }
            set { Fields.ChallanPackagingCharges[this] = value; }
        }

        [DisplayName("Challan Freight Charges"), Expression("jChallan.[FreightCharges]")]
        public Double? ChallanFreightCharges
        {
            get { return Fields.ChallanFreightCharges[this]; }
            set { Fields.ChallanFreightCharges[this] = value; }
        }

        [DisplayName("Challan Advacne"), Expression("jChallan.[Advacne]")]
        public Double? ChallanAdvacne
        {
            get { return Fields.ChallanAdvacne[this]; }
            set { Fields.ChallanAdvacne[this] = value; }
        }

        [DisplayName("Challan Due Date"), Expression("jChallan.[DueDate]")]
        public DateTime? ChallanDueDate
        {
            get { return Fields.ChallanDueDate[this]; }
            set { Fields.ChallanDueDate[this] = value; }
        }

        [DisplayName("Challan Dispatch Details"), Expression("jChallan.[DispatchDetails]")]
        public String ChallanDispatchDetails
        {
            get { return Fields.ChallanDispatchDetails[this]; }
            set { Fields.ChallanDispatchDetails[this] = value; }
        }

        [DisplayName("Challan Status"), Expression("jChallan.[Status]")]
        public Int32? ChallanStatus
        {
            get { return Fields.ChallanStatus[this]; }
            set { Fields.ChallanStatus[this] = value; }
        }

        [DisplayName("Challan Type"), Expression("jChallan.[Type]")]
        public Int32? ChallanType
        {
            get { return Fields.ChallanType[this]; }
            set { Fields.ChallanType[this] = value; }
        }

        [DisplayName("Challan Additional Info"), Expression("jChallan.[AdditionalInfo]")]
        public String ChallanAdditionalInfo
        {
            get { return Fields.ChallanAdditionalInfo[this]; }
            set { Fields.ChallanAdditionalInfo[this] = value; }
        }

        [DisplayName("Challan Source Id"), Expression("jChallan.[SourceId]")]
        public Int32? ChallanSourceId
        {
            get { return Fields.ChallanSourceId[this]; }
            set { Fields.ChallanSourceId[this] = value; }
        }

        [DisplayName("Challan Stage Id"), Expression("jChallan.[StageId]")]
        public Int32? ChallanStageId
        {
            get { return Fields.ChallanStageId[this]; }
            set { Fields.ChallanStageId[this] = value; }
        }

        [DisplayName("Challan Branch Id"), Expression("jChallan.[BranchId]")]
        public Int32? ChallanBranchId
        {
            get { return Fields.ChallanBranchId[this]; }
            set { Fields.ChallanBranchId[this] = value; }
        }

        [DisplayName("Challan Owner Id"), Expression("jChallan.[OwnerId]")]
        public Int32? ChallanOwnerId
        {
            get { return Fields.ChallanOwnerId[this]; }
            set { Fields.ChallanOwnerId[this] = value; }
        }

        [DisplayName("Challan Assigned Id"), Expression("jChallan.[AssignedId]")]
        public Int32? ChallanAssignedId
        {
            get { return Fields.ChallanAssignedId[this]; }
            set { Fields.ChallanAssignedId[this] = value; }
        }

        [DisplayName("Challan Total"), Expression("jChallan.[Total]")]
        public Double? ChallanTotal
        {
            get { return Fields.ChallanTotal[this]; }
            set { Fields.ChallanTotal[this] = value; }
        }

        [DisplayName("Challan Invoice Made"), Expression("jChallan.[InvoiceMade]")]
        public Boolean? ChallanInvoiceMade
        {
            get { return Fields.ChallanInvoiceMade[this]; }
            set { Fields.ChallanInvoiceMade[this] = value; }
        }

        [DisplayName("Challan Contact Person Id"), Expression("jChallan.[ContactPersonId]")]
        public Int32? ChallanContactPersonId
        {
            get { return Fields.ChallanContactPersonId[this]; }
            set { Fields.ChallanContactPersonId[this] = value; }
        }

        [DisplayName("Challan Contact Person Phone"), Expression("jChallan.[ContactPersonPhone]")]
        public String ChallanContactPersonPhone
        {
            get { return Fields.ChallanContactPersonPhone[this]; }
            set { Fields.ChallanContactPersonPhone[this] = value; }
        }

        [DisplayName("Challan Quotation No"), Expression("jChallan.[QuotationNo]")]
        public Int32? ChallanQuotationNo
        {
            get { return Fields.ChallanQuotationNo[this]; }
            set { Fields.ChallanQuotationNo[this] = value; }
        }

        [DisplayName("Challan Quotation Date"), Expression("jChallan.[QuotationDate]")]
        public DateTime? ChallanQuotationDate
        {
            get { return Fields.ChallanQuotationDate[this]; }
            set { Fields.ChallanQuotationDate[this] = value; }
        }

        [DisplayName("Code"), Size(100), NotMapped]
        public String Code { get { return Fields.Code[this]; } set { Fields.Code[this] = value; } }

        [DisplayName("Line Total"), Expression("(t0.[Price] * (t0.[Quantity]) - t0.[DiscountAmount] - ((t0.[Price] * (t0.[Quantity])) * t0.[Discount] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
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

        

        public ChallanProductsRow()
            : base(Fields)
        {
        }
        public ChallanProductsRow(RowFields fields)
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
            public Int32Field ChallanId;
            public StringField Description;
            public StringField Unit;

            public StringField TaxType1;
            public DoubleField Percentage1;
            public StringField TaxType2;
            public DoubleField Percentage2;

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

            public Int32Field ChallanContactsId;
            public DateTimeField ChallanDate;
            public BooleanField ChallanOtherAddress;
            public StringField ChallanShippingAddress;
            public DoubleField ChallanPackagingCharges;
            public DoubleField ChallanFreightCharges;
            public DoubleField ChallanAdvacne;
            public DateTimeField ChallanDueDate;
            public StringField ChallanDispatchDetails;
            public Int32Field ChallanStatus;
            public Int32Field ChallanType;
            public StringField ChallanAdditionalInfo;
            public Int32Field ChallanSourceId;
            public Int32Field ChallanStageId;
            public Int32Field ChallanBranchId;
            public Int32Field ChallanOwnerId;
            public Int32Field ChallanAssignedId;
            public DoubleField ChallanTotal;
            public BooleanField ChallanInvoiceMade;
            public Int32Field ChallanContactPersonId;
            public StringField ChallanContactPersonPhone;
            
            public Int32Field ChallanQuotationNo;
            public DateTimeField ChallanQuotationDate;

            public DecimalField Tax1Amount;
            public DecimalField Tax2Amount;

            public StringField Code;
        public DecimalField LineTotal;

            public StringField From;
            public StringField To;
            public DateTimeField Date;
            public StringField Destination;
            public StringField Nights;
            public StringField Adults;
            public StringField Childrens;
            public StringField HotelName;
            public StringField MealPlan;
            
            public Int32Field BranchId;
            public StringField BranchName { get; set; }

        }
    }
}
