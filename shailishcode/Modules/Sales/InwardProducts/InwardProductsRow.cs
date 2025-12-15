using AdvanceCRM.Administration;
using AdvanceCRM.Products;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Sales
{
    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[InwardProducts]")]
    [DisplayName("Inward Products"), InstanceName("Inward Products")]
    [ReadPermission("Inward:Read")]
    [ModifyPermission("Inward:Read")]
    public sealed class InwardProductsRow : Row<InwardProductsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty, SortOrder(1)]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Products"), NotNull, ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName")]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductsId
        {
            get => fields.ProductsId[this];
            set => fields.ProductsId[this] = value;
        }

        [DisplayName("Serial"), Size(100), QuickSearch, NameProperty]
        public String Serial
        {
            get => fields.Serial[this];
            set => fields.Serial[this] = value;
        }

        [DisplayName("Batch"), Size(100)]
        public String Batch
        {
            get => fields.Batch[this];
            set => fields.Batch[this] = value;
        }

        [DisplayName("Quantity"), NotNull, DefaultValue("1")]
        public Double? Quantity
        {
            get => fields.Quantity[this];
            set => fields.Quantity[this] = value;
        }

        [DisplayName("Price"), DisplayFormat("#,##0.####"), NotNull]
        public Double? Price
        {
            get => fields.Price[this];
            set => fields.Price[this] = value;
        }

        [DisplayName("Selling Price"), DisplayFormat("#,##0.####"), NotNull]
        public Double? SellingPrice
        {
            get => fields.SellingPrice[this];
            set => fields.SellingPrice[this] = value;
        }

        [DisplayName("MRP"), Column("MRP"), DisplayFormat("#,##0.####"), NotNull]
        public Double? Mrp
        {
            get => fields.Mrp[this];
            set => fields.Mrp[this] = value;
        }

        [DisplayName("Disc(%)"), NotNull, DefaultValue("0")]
        public Double? Discount
        {
            get => fields.Discount[this];
            set => fields.Discount[this] = value;
        }

        [DisplayName("Disc(Amt)"), NotNull, DefaultValue("0")]
        public Double? DiscountAmount
        {
            get => fields.DiscountAmount[this];
            set => fields.DiscountAmount[this] = value;
        }

        [DisplayName("Inward"), NotNull, ForeignKey("[dbo].[Inward]", "Id"), LeftJoin("jInward"), TextualField("InwardShippingAddress")]
        [LookupEditor(typeof(InwardRow), InplaceAdd = true)]
        public Int32? InwardId
        {
            get => fields.InwardId[this];
            set => fields.InwardId[this] = value;
        }

        [DisplayName("Description"), Size(2000), TextAreaEditor(Rows = 4)]
        public String Description
        {
            get => fields.Description[this];
            set => fields.Description[this] = value;
        }

        [DisplayName("Unit"), Expression("jProductsUnit.[ProductsUnit]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String Unit
        {
            get => fields.Unit[this];
            set => fields.Unit[this] = value;
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

        [DisplayName("Inward Contacts Id"), Expression("jInward.[ContactsId]")]
        public Int32? InwardContactsId
        {
            get { return Fields.InwardContactsId[this]; }
            set { Fields.InwardContactsId[this] = value; }
        }

        [DisplayName("Inward Date"), Expression("jInward.[Date]")]
        public DateTime? InwardDate
        {
            get { return Fields.InwardDate[this]; }
            set { Fields.InwardDate[this] = value; }
        }

        [DisplayName("Inward Other Address"), Expression("jInward.[OtherAddress]")]
        public Boolean? InwardOtherAddress
        {
            get { return Fields.InwardOtherAddress[this]; }
            set { Fields.InwardOtherAddress[this] = value; }
        }

        [DisplayName("Inward Shipping Address"), Expression("jInward.[ShippingAddress]")]
        public String InwardShippingAddress
        {
            get { return Fields.InwardShippingAddress[this]; }
            set { Fields.InwardShippingAddress[this] = value; }
        }

        [DisplayName("Inward Packaging Charges"), Expression("jInward.[PackagingCharges]")]
        public Double? InwardPackagingCharges
        {
            get { return Fields.InwardPackagingCharges[this]; }
            set { Fields.InwardPackagingCharges[this] = value; }
        }

        [DisplayName("Inward Freight Charges"), Expression("jInward.[FreightCharges]")]
        public Double? InwardFreightCharges
        {
            get { return Fields.InwardFreightCharges[this]; }
            set { Fields.InwardFreightCharges[this] = value; }
        }

        [DisplayName("Inward Advacne"), Expression("jInward.[Advacne]")]
        public Double? InwardAdvacne
        {
            get { return Fields.InwardAdvacne[this]; }
            set { Fields.InwardAdvacne[this] = value; }
        }

        [DisplayName("Inward Due Date"), Expression("jInward.[DueDate]")]
        public DateTime? InwardDueDate
        {
            get { return Fields.InwardDueDate[this]; }
            set { Fields.InwardDueDate[this] = value; }
        }

        [DisplayName("Inward Dispatch Details"), Expression("jInward.[DispatchDetails]")]
        public String InwardDispatchDetails
        {
            get { return Fields.InwardDispatchDetails[this]; }
            set { Fields.InwardDispatchDetails[this] = value; }
        }

        [DisplayName("Inward Status"), Expression("jInward.[Status]")]
        public Int32? InwardStatus
        {
            get { return Fields.InwardStatus[this]; }
            set { Fields.InwardStatus[this] = value; }
        }

        [DisplayName("Inward Type"), Expression("jInward.[Type]")]
        public Int32? InwardType
        {
            get { return Fields.InwardType[this]; }
            set { Fields.InwardType[this] = value; }
        }

        [DisplayName("Inward Additional Info"), Expression("jInward.[AdditionalInfo]")]
        public String InwardAdditionalInfo
        {
            get { return Fields.InwardAdditionalInfo[this]; }
            set { Fields.InwardAdditionalInfo[this] = value; }
        }

        [DisplayName("Inward Source Id"), Expression("jInward.[SourceId]")]
        public Int32? InwardSourceId
        {
            get { return Fields.InwardSourceId[this]; }
            set { Fields.InwardSourceId[this] = value; }
        }

        [DisplayName("Inward Stage Id"), Expression("jInward.[StageId]")]
        public Int32? InwardStageId
        {
            get { return Fields.InwardStageId[this]; }
            set { Fields.InwardStageId[this] = value; }
        }

        [DisplayName("Inward Branch Id"), Expression("jInward.[BranchId]")]
        public Int32? InwardBranchId
        {
            get { return Fields.InwardBranchId[this]; }
            set { Fields.InwardBranchId[this] = value; }
        }

        [DisplayName("Inward Owner Id"), Expression("jInward.[OwnerId]")]
        public Int32? InwardOwnerId
        {
            get { return Fields.InwardOwnerId[this]; }
            set { Fields.InwardOwnerId[this] = value; }
        }

        [DisplayName("Inward Assigned Id"), Expression("jInward.[AssignedId]")]
        public Int32? InwardAssignedId
        {
            get { return Fields.InwardAssignedId[this]; }
            set { Fields.InwardAssignedId[this] = value; }
        }

        [DisplayName("Inward Total"), Expression("jInward.[Total]")]
        public Double? InwardTotal
        {
            get { return Fields.InwardTotal[this]; }
            set { Fields.InwardTotal[this] = value; }
        }

        [DisplayName("Inward Invoice Made"), Expression("jInward.[InvoiceMade]")]
        public Boolean? InwardInvoiceMade
        {
            get { return Fields.InwardInvoiceMade[this]; }
            set { Fields.InwardInvoiceMade[this] = value; }
        }

        [DisplayName("Inward Contact Person Id"), Expression("jInward.[ContactPersonId]")]
        public Int32? InwardContactPersonId
        {
            get { return Fields.InwardContactPersonId[this]; }
            set { Fields.InwardContactPersonId[this] = value; }
        }

        [DisplayName("Inward Contact Person Phone"), Expression("jInward.[ContactPersonPhone]")]
        public String InwardContactPersonPhone
        {
            get { return Fields.InwardContactPersonPhone[this]; }
            set { Fields.InwardContactPersonPhone[this] = value; }
        }

        [DisplayName("Inward Quotation No"), Expression("jInward.[QuotationNo]")]
        public Int32? InwardQuotationNo
        {
            get { return Fields.InwardQuotationNo[this]; }
            set { Fields.InwardQuotationNo[this] = value; }
        }

        [DisplayName("Inward Quotation Date"), Expression("jInward.[QuotationDate]")]
        public DateTime? InwardQuotationDate
        {
            get { return Fields.InwardQuotationDate[this]; }
            set { Fields.InwardQuotationDate[this] = value; }
        }

        [DisplayName("Code"), Size(100), NotMapped]
        public String Code { get { return Fields.Code[this]; } set { Fields.Code[this] = value; } }

        [DisplayName("Line Total"), Expression("(t0.[Price] * (t0.[Quantity]) - t0.[DiscountAmount] - ((t0.[Price] * (t0.[Quantity])) * t0.[Discount] / 100))"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
        }        
        [DisplayName("Tax Type1"), Size(100)]
        public String TaxType1
        {
            get => fields.TaxType1[this];
            set => fields.TaxType1[this] = value;
        }

        [DisplayName("Percentage1")]
        public Double? Percentage1
        {
            get => fields.Percentage1[this];
            set => fields.Percentage1[this] = value;
        }

        [DisplayName("Tax Type2"), Size(100)]
        public String TaxType2
        {
            get => fields.TaxType2[this];
            set => fields.TaxType2[this] = value;
        }

        [DisplayName("Percentage2")]
        public Double? Percentage2
        {
            get => fields.Percentage2[this];
            set => fields.Percentage2[this] = value;
        }

        [DisplayName("From"), Size(100)]
        public String From
        {
            get => fields.From[this];
            set => fields.From[this] = value;
        }

        [DisplayName("To"), Size(100)]
        public String To
        {
            get => fields.To[this];
            set => fields.To[this] = value;
        }

        [DisplayName("Date")]
        public DateTime? Date
        {
            get => fields.Date[this];
            set => fields.Date[this] = value;
        }

        [DisplayName("Adults"), Size(50)]
        public String Adults
        {
            get => fields.Adults[this];
            set => fields.Adults[this] = value;
        }

        [DisplayName("Childrens"), Size(50)]
        public String Childrens
        {
            get => fields.Childrens[this];
            set => fields.Childrens[this] = value;
        }

        [DisplayName("Destination"), Size(100)]
        public String Destination
        {
            get => fields.Destination[this];
            set => fields.Destination[this] = value;
        }

        [DisplayName("Nights"), Size(50)]
        public String Nights
        {
            get => fields.Nights[this];
            set => fields.Nights[this] = value;
        }

        [DisplayName("Hotel Name"), Size(100)]
        public String HotelName
        {
            get => fields.HotelName[this];
            set => fields.HotelName[this] = value;
        }

        [DisplayName("Meal Plan"), Size(100)]
        public String MealPlan
        {
            get => fields.MealPlan[this];
            set => fields.MealPlan[this] = value;
        }        
        [DisplayName("Branch"), NotNull, ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jBranch"), TextualField("BranchName")]
        [LookupEditor(typeof(BranchRow), InplaceAdd = true)]
        public Int32? BranchId
        {
            get => fields.BranchId[this];
            set => fields.BranchId[this] = value;
        }

        [DisplayName("Branch"), Expression("jBranch.[Branch]")]
        public String Branch
        {
            get => fields.Branch[this];
            set => fields.Branch[this] = value;
        }

        [DisplayName("Branch Phone"), Expression("jBranch.[Phone]")]
        public String BranchPhone
        {
            get => fields.BranchPhone[this];
            set => fields.BranchPhone[this] = value;
        }

        [DisplayName("Branch Email"), Expression("jBranch.[Email]")]
        public String BranchEmail
        {
            get => fields.BranchEmail[this];
            set => fields.BranchEmail[this] = value;
        }

        [DisplayName("Branch Address"), Expression("jBranch.[Address]")]
        public String BranchAddress
        {
            get => fields.BranchAddress[this];
            set => fields.BranchAddress[this] = value;
        }

        [DisplayName("Branch City Id"), Expression("jBranch.[CityId]")]
        public Int32? BranchCityId
        {
            get => fields.BranchCityId[this];
            set => fields.BranchCityId[this] = value;
        }

        [DisplayName("Branch State Id"), Expression("jBranch.[StateId]")]
        public Int32? BranchStateId
        {
            get => fields.BranchStateId[this];
            set => fields.BranchStateId[this] = value;
        }

        [DisplayName("Branch Pin"), Expression("jBranch.[Pin]")]
        public String BranchPin
        {
            get => fields.BranchPin[this];
            set => fields.BranchPin[this] = value;
        }

        [DisplayName("Branch Country"), Expression("jBranch.[Country]")]
        public Int32? BranchCountry
        {
            get => fields.BranchCountry[this];
            set => fields.BranchCountry[this] = value;
        }

        [DisplayName("Branch Company Id"), Expression("jBranch.[CompanyId]")]
        public Int32? BranchCompanyId
        {
            get => fields.BranchCompanyId[this];
            set => fields.BranchCompanyId[this] = value;
        }

        public InwardProductsRow()
            : base()
        {
        }

        public InwardProductsRow(RowFields fields)
            : base(fields)
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
            public Int32Field InwardId;
            public StringField Description;
            public StringField Unit;
            public StringField TaxType1;
            public DoubleField Percentage1;
            public StringField TaxType2;
            public DoubleField Percentage2;
            public StringField From;
            public StringField To;
            public DateTimeField Date;
            public StringField Adults;
            public StringField Childrens;
            public StringField Destination;
            public StringField Nights;
            public StringField HotelName;
            public StringField MealPlan;
            public Int32Field BranchId;

            public StringField Branch;
            public StringField BranchPhone;
            public StringField BranchEmail;
            public StringField BranchAddress;
            public Int32Field BranchCityId;
            public Int32Field BranchStateId;
            public StringField BranchPin;
            public Int32Field BranchCountry;
            public Int32Field BranchCompanyId;

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

            public Int32Field InwardContactsId;
            public DateTimeField InwardDate;
            public BooleanField InwardOtherAddress;
            public StringField InwardShippingAddress;
            public DoubleField InwardPackagingCharges;
            public DoubleField InwardFreightCharges;
            public DoubleField InwardAdvacne;
            public DateTimeField InwardDueDate;
            public StringField InwardDispatchDetails;
            public Int32Field InwardStatus;
            public Int32Field InwardType;
            public StringField InwardAdditionalInfo;
            public Int32Field InwardSourceId;
            public Int32Field InwardStageId;
            public Int32Field InwardBranchId;
            public Int32Field InwardOwnerId;
            public Int32Field InwardAssignedId;
            public DoubleField InwardTotal;
            public BooleanField InwardInvoiceMade;
            public Int32Field InwardContactPersonId;
            public StringField InwardContactPersonPhone;

            public Int32Field InwardQuotationNo;
            public DateTimeField InwardQuotationDate;

            public StringField Code;
            public DecimalField LineTotal;
            
        }
    }
}
