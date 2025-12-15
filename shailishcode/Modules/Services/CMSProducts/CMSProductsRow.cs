
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

    [ConnectionKey("Default"), Module("Services"), TableName("[dbo].[CMSProductsNew]")]
    [DisplayName("CMS Products"), InstanceName("CMS Products")]
    [ReadPermission("CMS:Read")]
    [InsertPermission("CMS:Insert")]
    [UpdatePermission("CMS:Update")]
    [DeletePermission("CMS:Delete")]

    public sealed class CMSProductsRow : Row<CMSProductsRow.RowFields>, IIdRow
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

        [DisplayName("Quantity"), NotNull, DefaultValue("1")]
        public Double? Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("CMS"), Column("CMSId"), NotNull, ForeignKey("[dbo].[CMS]", "Id"), LeftJoin("jCMS"), TextualField("CMSSerialNo")]
        [LookupEditor(typeof(CMSRow), InplaceAdd = true)]
        public Int32? CMSId
        {
            get { return Fields.CMSId[this]; }
            set { Fields.CMSId[this] = value; }
        }

        [DisplayName("Price"), DisplayFormat("#,##0.####"), NotNull, DefaultValue(0)]
        public Double? Price
        {
            get { return Fields.Price[this]; }
            set { Fields.Price[this] = value; }
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
        public Int32? ProductsUnitId
        {
            get { return Fields.ProductsUnitId[this]; }
            set { Fields.ProductsUnitId[this] = value; }
        }

        [DisplayName("CMS Contacts Id"), Expression("jCMS.[ContactsId]")]
        public Int32? CMSContactsId
        {
            get { return Fields.CMSContactsId[this]; }
            set { Fields.CMSContactsId[this] = value; }
        }

        [DisplayName("CMS Date"), Expression("jCMS.[Date]")]
        public DateTime? CMSDate
        {
            get { return Fields.CMSDate[this]; }
            set { Fields.CMSDate[this] = value; }
        }

        [DisplayName("CMS Products Id"), Expression("jCMS.[ProductsId]")]
        public Int32? CMSProductsId
        {
            get { return Fields.CMSProductsId[this]; }
            set { Fields.CMSProductsId[this] = value; }
        }

        [DisplayName("CMS Serial No"), Expression("jCMS.[SerialNo]")]
        public String CMSSerialNo
        {
            get { return Fields.CMSSerialNo[this]; }
            set { Fields.CMSSerialNo[this] = value; }
        }

        [DisplayName("CMS Complaint Id"), Expression("jCMS.[ComplaintId]")]
        public Int32? CMSComplaintId
        {
            get { return Fields.CMSComplaintId[this]; }
            set { Fields.CMSComplaintId[this] = value; }
        }

        [DisplayName("CMS Category"), Expression("jCMS.[Category]")]
        public Int32? CMSCategory
        {
            get { return Fields.CMSCategory[this]; }
            set { Fields.CMSCategory[this] = value; }
        }

        [DisplayName("CMS Amount"), Expression("jCMS.[Amount]")]
        public Double? CMSAmount
        {
            get { return Fields.CMSAmount[this]; }
            set { Fields.CMSAmount[this] = value; }
        }

        [DisplayName("CMS Expected Completion"), Expression("jCMS.[ExpectedCompletion]")]
        public DateTime? CMSExpectedCompletion
        {
            get { return Fields.CMSExpectedCompletion[this]; }
            set { Fields.CMSExpectedCompletion[this] = value; }
        }

        [DisplayName("CMS Created By"), Expression("jCMS.[AssignedBy]")]
        public Int32? CMSAssignedBy
        {
            get { return Fields.CMSAssignedBy[this]; }
            set { Fields.CMSAssignedBy[this] = value; }
        }

        [DisplayName("CMS Assigned To"), Expression("jCMS.[AssignedTo]")]
        public Int32? CMSAssignedTo
        {
            get { return Fields.CMSAssignedTo[this]; }
            set { Fields.CMSAssignedTo[this] = value; }
        }

        [DisplayName("CMS Instructions"), Expression("jCMS.[Instructions]")]
        public String CMSInstructions
        {
            get { return Fields.CMSInstructions[this]; }
            set { Fields.CMSInstructions[this] = value; }
        }

        [DisplayName("CMS Branch Id"), Expression("jCMS.[BranchId]")]
        public Int32? CMSBranchId
        {
            get { return Fields.CMSBranchId[this]; }
            set { Fields.CMSBranchId[this] = value; }
        }

        [DisplayName("CMS Status"), Expression("jCMS.[Status]")]
        public Int32? CMSStatus
        {
            get { return Fields.CMSStatus[this]; }
            set { Fields.CMSStatus[this] = value; }
        }

        [DisplayName("CMS Completion Date"), Expression("jCMS.[CompletionDate]")]
        public DateTime? CMSCompletionDate
        {
            get { return Fields.CMSCompletionDate[this]; }
            set { Fields.CMSCompletionDate[this] = value; }
        }

        [DisplayName("CMS Feedback"), Expression("jCMS.[Feedback]")]
        public String CMSFeedback
        {
            get { return Fields.CMSFeedback[this]; }
            set { Fields.CMSFeedback[this] = value; }
        }

        [DisplayName("CMS Additional Info"), Expression("jCMS.[AdditionalInfo]")]
        public String CMSAdditionalInfo
        {
            get { return Fields.CMSAdditionalInfo[this]; }
            set { Fields.CMSAdditionalInfo[this] = value; }
        }

        [DisplayName("CMS Image"), Expression("jCMS.[Image]")]
        public String CMSImage
        {
            get { return Fields.CMSImage[this]; }
            set { Fields.CMSImage[this] = value; }
        }

        [DisplayName("CMS Phone"), Expression("jCMS.[Phone]")]
        public String CMSPhone
        {
            get { return Fields.CMSPhone[this]; }
            set { Fields.CMSPhone[this] = value; }
        }

        [DisplayName("CMS Address"), Expression("jCMS.[Address]")]
        public String CMSAddress
        {
            get { return Fields.CMSAddress[this]; }
            set { Fields.CMSAddress[this] = value; }
        }

        [DisplayName("CMS Stage Id"), Expression("jCMS.[StageId]")]
        public Int32? CMSStageId
        {
            get { return Fields.CMSStageId[this]; }
            set { Fields.CMSStageId[this] = value; }
        }

        [DisplayName("CMS Series"), Expression("jCMS.[Series]")]
        public String CMSSeries
        {
            get { return Fields.CMSSeries[this]; }
            set { Fields.CMSSeries[this] = value; }
        }

        [DisplayName("CMS Air Filter"), Expression("jCMS.[AirFilter]")]
        public Double? CMSAirFilter
        {
            get { return Fields.CMSAirFilter[this]; }
            set { Fields.CMSAirFilter[this] = value; }
        }

        [DisplayName("CMS Oil Filter"), Expression("jCMS.[OilFilter]")]
        public Double? CMSOilFilter
        {
            get { return Fields.CMSOilFilter[this]; }
            set { Fields.CMSOilFilter[this] = value; }
        }

        [DisplayName("CMS Oil Seperator"), Expression("jCMS.[OilSeperator]")]
        public Double? CMSOilSeperator
        {
            get { return Fields.CMSOilSeperator[this]; }
            set { Fields.CMSOilSeperator[this] = value; }
        }

        [DisplayName("CMS Oil Change"), Expression("jCMS.[OilChange]")]
        public Double? CMSOilChange
        {
            get { return Fields.CMSOilChange[this]; }
            set { Fields.CMSOilChange[this] = value; }
        }

        [DisplayName("CMS Date Of Reading"), Expression("jCMS.[DateOfReading]")]
        public DateTime? CMSDateOfReading
        {
            get { return Fields.CMSDateOfReading[this]; }
            set { Fields.CMSDateOfReading[this] = value; }
        }

        [DisplayName("CMS Hmr"), Expression("jCMS.[HMR]")]
        public Double? CMSHmr
        {
            get { return Fields.CMSHmr[this]; }
            set { Fields.CMSHmr[this] = value; }
        }

        [DisplayName("CMS Afct"), Expression("jCMS.[AFCT]")]
        public Double? CMSAfct
        {
            get { return Fields.CMSAfct[this]; }
            set { Fields.CMSAfct[this] = value; }
        }

        [DisplayName("CMS Ofct"), Expression("jCMS.[OFCT]")]
        public Double? CMSOfct
        {
            get { return Fields.CMSOfct[this]; }
            set { Fields.CMSOfct[this] = value; }
        }

        [DisplayName("CMS Osct"), Expression("jCMS.[OSCT]")]
        public Double? CMSOsct
        {
            get { return Fields.CMSOsct[this]; }
            set { Fields.CMSOsct[this] = value; }
        }

        [DisplayName("CMS Oct"), Expression("jCMS.[OCT]")]
        public Double? CMSOct
        {
            get { return Fields.CMSOct[this]; }
            set { Fields.CMSOct[this] = value; }
        }

        [DisplayName("CMS Daily Working Hours"), Expression("jCMS.[DailyWorkingHours]")]
        public Double? CMSDailyWorkingHours
        {
            get { return Fields.CMSDailyWorkingHours[this]; }
            set { Fields.CMSDailyWorkingHours[this] = value; }
        }

        [DisplayName("CMS Priority"), Expression("jCMS.[Priority]")]
        public Int32? CMSPriority
        {
            get { return Fields.CMSPriority[this]; }
            set { Fields.CMSPriority[this] = value; }
        }

        [DisplayName("CMS Pmr Closed"), Expression("jCMS.[PMRClosed]")]
        public Boolean? CMSPmrClosed
        {
            get { return Fields.CMSPmrClosed[this]; }
            set { Fields.CMSPmrClosed[this] = value; }
        }

        [DisplayName("CMS Investigation By"), Expression("jCMS.[InvestigationBy]")]
        public Int32? CMSInvestigationBy
        {
            get { return Fields.CMSInvestigationBy[this]; }
            set { Fields.CMSInvestigationBy[this] = value; }
        }

        [DisplayName("CMS Action By"), Expression("jCMS.[ActionBy]")]
        public Int32? CMSActionBy
        {
            get { return Fields.CMSActionBy[this]; }
            set { Fields.CMSActionBy[this] = value; }
        }

        [DisplayName("CMS Supervised By"), Expression("jCMS.[SupervisedBy]")]
        public Int32? CMSSupervisedBy
        {
            get { return Fields.CMSSupervisedBy[this]; }
            set { Fields.CMSSupervisedBy[this] = value; }
        }

        [DisplayName("CMS Observation"), Expression("jCMS.[Observation]")]
        public String CMSObservation
        {
            get { return Fields.CMSObservation[this]; }
            set { Fields.CMSObservation[this] = value; }
        }

        [DisplayName("CMS Action"), Expression("jCMS.[Action]")]
        public String CMSAction
        {
            get { return Fields.CMSAction[this]; }
            set { Fields.CMSAction[this] = value; }
        }

        [DisplayName("CMS Comments"), Expression("jCMS.[Comments]")]
        public String CMSComments
        {
            get { return Fields.CMSComments[this]; }
            set { Fields.CMSComments[this] = value; }
        }

		[DisplayName("Line Total"), Expression("(t0.[Price] * t0.[Quantity])"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
        }
       
        public CMSProductsRow()
            : base(Fields)
        {
        }
        
        public CMSProductsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ProductsId;
            public DoubleField Quantity;
            public Int32Field CMSId;
            public DoubleField Price;

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

            public Int32Field CMSContactsId;
            public DateTimeField CMSDate;
            public Int32Field CMSProductsId;
            public StringField CMSSerialNo;
            public Int32Field CMSComplaintId;
            public Int32Field CMSCategory;
            public DoubleField CMSAmount;
            public DateTimeField CMSExpectedCompletion;
            public Int32Field CMSAssignedBy;
            public Int32Field CMSAssignedTo;
            public StringField CMSInstructions;
            public Int32Field CMSBranchId;
            public Int32Field CMSStatus;
            public DateTimeField CMSCompletionDate;
            public StringField CMSFeedback;
            public StringField CMSAdditionalInfo;
            public StringField CMSImage;
            public StringField CMSPhone;
            public StringField CMSAddress;
            public Int32Field CMSStageId;
            public StringField CMSSeries;
            public DoubleField CMSAirFilter;
            public DoubleField CMSOilFilter;
            public DoubleField CMSOilSeperator;
            public DoubleField CMSOilChange;
            public DateTimeField CMSDateOfReading;
            public DoubleField CMSHmr;
            public DoubleField CMSAfct;
            public DoubleField CMSOfct;
            public DoubleField CMSOsct;
            public DoubleField CMSOct;
            public DoubleField CMSDailyWorkingHours;
            public Int32Field CMSPriority;
            public BooleanField CMSPmrClosed;
            public Int32Field CMSInvestigationBy;
            public Int32Field CMSActionBy;
            public Int32Field CMSSupervisedBy;
            public StringField CMSObservation;
            public StringField CMSAction;
            public StringField CMSComments;
			
			public DecimalField LineTotal;
        }
    }
}
