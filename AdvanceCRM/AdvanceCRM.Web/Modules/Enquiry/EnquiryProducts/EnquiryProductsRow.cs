
namespace AdvanceCRM.Enquiry
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

    [ConnectionKey("Default"), Module("Enquiry"), TableName("[dbo].[EnquiryProducts]")]
    [DisplayName("Enquiry Products"), InstanceName("Enquiry Products")]
    [ReadPermission("Enquiry:Read")]
    [ModifyPermission("Enquiry:Read")]
    public sealed class EnquiryProductsRow : Row<EnquiryProductsRow.RowFields>, IIdRow, INameRow
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

        [DisplayName("Price"), DisplayFormat("#,##0.####"), NotNull, DefaultValue(0),DecimalEditor(Decimals = 4)]
        public Double? Price
        {
            get { return Fields.Price[this]; }
            set { Fields.Price[this] = value; }
        }

        [DisplayName("Discount"), NotNull, DefaultValue(0)]
        public Double? Discount
        {
            get { return Fields.Discount[this]; }
            set { Fields.Discount[this] = value; }
        }

        [DisplayName("Enquiry"), NotNull, ForeignKey("[dbo].[Enquiry]", "Id"), LeftJoin("jEnquiry"), TextualField("EnquiryAdditionalInfo")]
        [LookupEditor(typeof(EnquiryRow), InplaceAdd = true)]
        public Int32? EnquiryId
        {
            get { return Fields.EnquiryId[this]; }
            set { Fields.EnquiryId[this] = value; }
        }

        [DisplayName("Description"), Size(2000), TextAreaEditor(Rows = 4),NameProperty]
        public String Description
        {
            get { return Fields.Description[this]; }
            set { Fields.Description[this] = value; }
        }

        [DisplayName("Products Name"), Expression("jProducts.[Name]"), QuickSearch, MinSelectLevel(SelectLevel.List)]
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
        [ForeignKey("ProductsUnit", "Id"), LeftJoin("jProductsUnit"),] //added
       
        public Int32? ProductsUnitId
        {
            get { return Fields.ProductsUnitId[this]; }
            set { Fields.ProductsUnitId[this] = value; }
        }

        [DisplayName("Enquiry Contacts Id"), Expression("jEnquiry.[ContactsId]")]
        public Int32? EnquiryContactsId
        {
            get { return Fields.EnquiryContactsId[this]; }
            set { Fields.EnquiryContactsId[this] = value; }
        }

        [DisplayName("Enquiry Date"), Expression("jEnquiry.[Date]")]
        public DateTime? EnquiryDate
        {
            get { return Fields.EnquiryDate[this]; }
            set { Fields.EnquiryDate[this] = value; }
        }

        [DisplayName("Enquiry Status"), Expression("jEnquiry.[Status]")]
        public Int32? EnquiryStatus
        {
            get { return Fields.EnquiryStatus[this]; }
            set { Fields.EnquiryStatus[this] = value; }
        }

        [DisplayName("Enquiry Type"), Expression("jEnquiry.[Type]")]
        public Int32? EnquiryType
        {
            get { return Fields.EnquiryType[this]; }
            set { Fields.EnquiryType[this] = value; }
        }

        [DisplayName("Enquiry Additional Info"), Expression("jEnquiry.[AdditionalInfo]")]
        public String EnquiryAdditionalInfo
        {
            get { return Fields.EnquiryAdditionalInfo[this]; }
            set { Fields.EnquiryAdditionalInfo[this] = value; }
        }

        [DisplayName("Enquiry Source Id"), Expression("jEnquiry.[SourceId]")]
        public Int32? EnquirySourceId
        {
            get { return Fields.EnquirySourceId[this]; }
            set { Fields.EnquirySourceId[this] = value; }
        }

        [DisplayName("Enquiry Stage Id"), Expression("jEnquiry.[StageId]")]
        public Int32? EnquiryStageId
        {
            get { return Fields.EnquiryStageId[this]; }
            set { Fields.EnquiryStageId[this] = value; }
        }

        [DisplayName("Enquiry Branch Id"), Expression("jEnquiry.[BranchId]")]
        public Int32? EnquiryBranchId
        {
            get { return Fields.EnquiryBranchId[this]; }
            set { Fields.EnquiryBranchId[this] = value; }
        }

        [DisplayName("Enquiry Owner Id"), Expression("jEnquiry.[OwnerId]")]
        public Int32? EnquiryOwnerId
        {
            get { return Fields.EnquiryOwnerId[this]; }
            set { Fields.EnquiryOwnerId[this] = value; }
        }

        [DisplayName("Enquiry Assigned Id"), Expression("jEnquiry.[AssignedId]")]
        public Int32? EnquiryAssignedId
        {
            get { return Fields.EnquiryAssignedId[this]; }
            set { Fields.EnquiryAssignedId[this] = value; }
        }

        [DisplayName("Enquiry Reference Name"), Expression("jEnquiry.[ReferenceName]")]
        public String EnquiryReferenceName
        {
            get { return Fields.EnquiryReferenceName[this]; }
            set { Fields.EnquiryReferenceName[this] = value; }
        }

        [DisplayName("Enquiry Reference Phone"), Expression("jEnquiry.[ReferencePhone]")]
        public String EnquiryReferencePhone
        {
            get { return Fields.EnquiryReferencePhone[this]; }
            set { Fields.EnquiryReferencePhone[this] = value; }
        }

        [DisplayName("Enquiry Closing Type"), Expression("jEnquiry.[ClosingType]")]
        public Int32? EnquiryClosingType
        {
            get { return Fields.EnquiryClosingType[this]; }
            set { Fields.EnquiryClosingType[this] = value; }
        }

        [DisplayName("Enquiry Lost Reason"), Expression("jEnquiry.[LostReason]")]
        public String EnquiryLostReason
        {
            get { return Fields.EnquiryLostReason[this]; }
            set { Fields.EnquiryLostReason[this] = value; }
        }

        [DisplayName("Enquiry Closing Date"), Expression("jEnquiry.[ClosingDate]")]
        public DateTime? EnquiryClosingDate
        {
            get { return Fields.EnquiryClosingDate[this]; }
            set { Fields.EnquiryClosingDate[this] = value; }
        }

        [DisplayName("Enquiry Contact Person Id"), Expression("jEnquiry.[ContactPersonId]")]
        public Int32? EnquiryContactPersonId
        {
            get { return Fields.EnquiryContactPersonId[this]; }
            set { Fields.EnquiryContactPersonId[this] = value; }
        }

        [DisplayName("Enquiry Attachments"), Expression("jEnquiry.[Attachments]")]
        public String EnquiryAttachments
        {
            get { return Fields.EnquiryAttachments[this]; }
            set { Fields.EnquiryAttachments[this] = value; }
        }

        
        [DisplayName("Unit"), Expression("jProductsUnit.[ProductsUnit]"), MinSelectLevel(SelectLevel.List), QuickSearch]
        public String Unit
        {
            get { return Fields.Unit[this]; }
            set { Fields.Unit[this] = value; }
        }
        
        [DisplayName("Line Total"), Expression("(t0.[Price] * t0.[Quantity] - t0.[Discount])"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
        }

        [DisplayName("Code"), Size(100), NotMapped]
        public String Code 
        { 
            get { return Fields.Code[this]; } 
            set { Fields.Code[this] = value; } 
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

        //[DisplayName("File Attachments"), Size(1000)]
        //[MultipleImageUploadEditor(FilenameFormat = "EnquiryProduct/~", CopyToHistory = true, AllowNonImage = true)]
        //public String FileAttachment
        //{
        //    get { return Fields.FileAttachment[this]; }
        //    set { Fields.FileAttachment[this] = value; }
        //}

        

        public EnquiryProductsRow()
            : base(Fields)
        {
        }
        
        public EnquiryProductsRow(RowFields fields)
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
            public Int32Field EnquiryId;
            public StringField Description;

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

            public Int32Field EnquiryContactsId;
            public DateTimeField EnquiryDate;
            public Int32Field EnquiryStatus;
            public Int32Field EnquiryType;
            public StringField EnquiryAdditionalInfo;
            public Int32Field EnquirySourceId;
            public Int32Field EnquiryStageId;
            public Int32Field EnquiryBranchId;
            public Int32Field EnquiryOwnerId;
            public Int32Field EnquiryAssignedId;
            public StringField EnquiryReferenceName;
            public StringField EnquiryReferencePhone;
            public Int32Field EnquiryClosingType;
            public StringField EnquiryLostReason;
            public DateTimeField EnquiryClosingDate;
            
            public Int32Field EnquiryContactPersonId;
            public StringField EnquiryAttachments;
            
            public StringField Unit;
            public DecimalField LineTotal; 
            public StringField Code;

           // public StringField FileAttachment;

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
