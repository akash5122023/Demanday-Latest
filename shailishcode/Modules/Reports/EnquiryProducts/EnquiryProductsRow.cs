
namespace AdvanceCRM.Reports
{
    using Serenity;
    using Serenity.ComponentModel;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Products;
    using AdvanceCRM.Administration;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Reports"), TableName("[dbo].[EnquiryProducts]")]
    [DisplayName("Enquiry Products"), InstanceName("Enquiry Products")]
    [ReadPermission("Administration:General")]
    [ModifyPermission("Administration:General")]
    public sealed class EnquiryProductsRow : Row<EnquiryProductsRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
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

        [DisplayName("Quantity"), NotNull]
        public Double? Quantity
        {
            get { return Fields.Quantity[this]; }
            set { Fields.Quantity[this] = value; }
        }

        [DisplayName("Mrp"), Column("MRP"), NotNull]
        public Double? Mrp
        {
            get { return Fields.Mrp[this]; }
            set { Fields.Mrp[this] = value; }
        }

        [DisplayName("Selling Price"), NotNull]
        public Double? SellingPrice
        {
            get { return Fields.SellingPrice[this]; }
            set { Fields.SellingPrice[this] = value; }
        }

        [DisplayName("Price"), NotNull]
        public Double? Price
        {
            get { return Fields.Price[this]; }
            set { Fields.Price[this] = value; }
        }

        [DisplayName("Discount"), NotNull]
        public Double? Discount
        {
            get { return Fields.Discount[this]; }
            set { Fields.Discount[this] = value; }
        }

        [DisplayName("Enquiry"), NotNull, ForeignKey("[dbo].[Enquiry]", "Id"), LeftJoin("jEnquiry"), TextualField("EnquiryAdditionalInfo")]
        public Int32? EnquiryId
        {
            get { return Fields.EnquiryId[this]; }
            set { Fields.EnquiryId[this] = value; }
        }

        [DisplayName("Description"), Size(2000), QuickSearch,NameProperty]
        public String Description
        {
            get { return Fields.Description[this]; }
            set { Fields.Description[this] = value; }
        }

        [DisplayName("Capacity"), Size(255)]
        public String Capacity
        {
            get { return Fields.Capacity[this]; }
            set { Fields.Capacity[this] = value; }
        }

        [DisplayName("Line Total"), Expression("(t0.[Price] * t0.[Quantity] - t0.[Discount])"), AlignRight, DisplayFormat("#,##0.00"), MinSelectLevel(SelectLevel.List)]
        public Decimal? LineTotal
        {
            get { return Fields.LineTotal[this]; }
            set { Fields.LineTotal[this] = value; }
        }

        [DisplayName("Products Name"), Expression("jProducts.[Name]")]
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

        [DisplayName("Products Mrp"), Expression("jProducts.[MRP]")]
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

        [DisplayName("Products Company Id"), Expression("jProducts.[CompanyId]")]
        public Int32? ProductsCompanyId
        {
            get { return Fields.ProductsCompanyId[this]; }
            set { Fields.ProductsCompanyId[this] = value; }
        }

        [DisplayName("Enquiry Contacts Id"), Expression("jEnquiry.[ContactsId]"),LookupInclude]
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

        public Masters.StatusMaster? EnquiryStatus
        {
            get { return (Masters.StatusMaster?)Fields.EnquiryStatus[this]; }
            set { Fields.EnquiryStatus[this] = (Int32?)value; }
        }
        
       

        [DisplayName("Enquiry Type"), Expression("jEnquiry.[Type]")]

        public Masters.EnquiryTypeMaster? EnquiryType
        {
            get { return (Masters.EnquiryTypeMaster?)Fields.EnquiryType[this]; }
            set { Fields.EnquiryType[this] = (Int32?)value; }
        }
        //public Int32? EnquiryType
        //{
        //    get { return Fields.EnquiryType[this]; }
        //    set { Fields.EnquiryType[this] = value; }
        //}

        [DisplayName("Enquiry Additional Info"), Expression("jEnquiry.[AdditionalInfo]")]
        public String EnquiryAdditionalInfo
        {
            get { return Fields.EnquiryAdditionalInfo[this]; }
            set { Fields.EnquiryAdditionalInfo[this] = value; }
        }

        [DisplayName("Enquiry Source Id"), Expression("jEnquiry.[SourceId]"),LookupInclude]
      
        public Int32? EnquirySourceId
        {
            get { return Fields.EnquirySourceId[this]; }
            set { Fields.EnquirySourceId[this] = value; }
        }

        [DisplayName("Enquiry Stage Id"), Expression("jEnquiry.[StageId]"),LookupInclude]
        public Int32? EnquiryStageId
        {
            get { return Fields.EnquiryStageId[this]; }
            set { Fields.EnquiryStageId[this] = value; }
        }

        [DisplayName("Enquiry Branch Id"), Expression("jEnquiry.[BranchId]"),LookupInclude]
        public Int32? EnquiryBranchId
        {
            get { return Fields.EnquiryBranchId[this]; }
            set { Fields.EnquiryBranchId[this] = value; }
        }

        //[DisplayName("Enquiry Person Id"), Expression("jEnquiry.[ContactPersonId]"), LookupInclude]
        //public Int32? EnquiryContactPersonId
        //{
        //    get { return Fields.EnquiryContactPersonId[this]; }
        //    set { Fields.EnquiryContactPersonId[this] = value; }
        //}

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

        [DisplayName("Enquiry Enquiry N"), Expression("jEnquiry.[EnquiryN]")]
        public String EnquiryEnquiryN
        {
            get { return Fields.EnquiryEnquiryN[this]; }
            set { Fields.EnquiryEnquiryN[this] = value; }
        }

        [DisplayName("Enquiry Enquiry No"), Expression("jEnquiry.[EnquiryNo]")]
        public Int32? EnquiryEnquiryNo
        {
            get { return Fields.EnquiryEnquiryNo[this]; }
            set { Fields.EnquiryEnquiryNo[this] = value; }
        }

        [DisplayName("Enquiry Company Id"), Expression("jEnquiry.[CompanyId]")]
        public Int32? EnquiryCompanyId
        {
            get { return Fields.EnquiryCompanyId[this]; }
            set { Fields.EnquiryCompanyId[this] = value; }
        }

        [DisplayName("Enquiry Source"), Expression("(SELECT Source FROM Source WHERE Id=(SELECT Top (1) SourceId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        public String EnquirySource
        {
            get { return Fields.EnquirySource[this]; }
            set { Fields.EnquirySource[this] = value; }
        }

        [DisplayName("Enquiry Stage"), Expression("(SELECT Stage FROM Stage WHERE Id=(SELECT Top (1) StageId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        public String EnquiryStage
        {
            get { return Fields.EnquiryStage[this]; }
            set { Fields.EnquiryStage[this] = value; }
        }
                
        [DisplayName("Enquiry Contacts"), Expression("(SELECT Name FROM Contacts WHERE Id=(SELECT Top (1) ContactsId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        //[LookupEditor(typeof(ContactsRow),InplaceAdd =false)]
        public String EnquiryContacts
        {
            get { return Fields.EnquiryContacts[this]; }
            set { Fields.EnquiryContacts[this] = value; }
        }

        [DisplayName("Contacts MobileNo"), Expression("(SELECT Phone FROM Contacts WHERE Id=(SELECT Top (1) ContactsId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        public String EnquiryContactsMobile
        {
            get { return Fields.EnquiryContactsMobile[this]; }
            set { Fields.EnquiryContactsMobile[this] = value; }
        }

        [DisplayName("Contacts MailId"), Expression("(SELECT Email FROM Contacts WHERE Id=(SELECT Top (1) ContactsId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        public String EnquiryContactMail
        {
            get { return Fields.EnquiryContactMail[this]; }
            set { Fields.EnquiryContactMail[this] = value; }
        }

        /// <summary>
        [DisplayName("SubContacts"), Expression("(SELECT Name FROM SubContacts WHERE Id=(SELECT Top (1) ContactPersonId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
      
        public String EnquirySubContact
        {
            get { return Fields.EnquiryContacts[this]; }
            set { Fields.EnquiryContacts[this] = value; }
        }

        [DisplayName("SubContacts MobileNo"), Expression("(SELECT Phone FROM SubContacts WHERE Id=(SELECT Top (1) ContactPersonId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        public String EnquirySubContactMobile
        {
            get { return Fields.EnquirySubContactMobile[this]; }
            set { Fields.EnquirySubContactMobile[this] = value; }
        }

        [DisplayName("SubContacts MailId"), Expression("(SELECT Email FROM SubContacts WHERE Id=(SELECT Top (1) ContactPersonId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        public String EnquirySubContactMail
        {
            get { return Fields.EnquirySubContactMail[this]; }
            set { Fields.EnquirySubContactMail[this] = value; }
        }
        /// </summary>

        [DisplayName("Contacts Address"), Expression("(SELECT Address FROM Contacts WHERE Id=(SELECT Top (1) ContactsId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        public String EnquiryContactAddress
        {
            get { return Fields.EnquiryContactAddress[this]; }
            set { Fields.EnquiryContactAddress[this] = value; }
        }

        [DisplayName("Enquiry Branch"), Expression("(SELECT Branch FROM Branch WHERE Id=(SELECT Top (1) BranchId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        public String EnquiryBranch
        {
            get { return Fields.EnquiryBranch[this]; }
            set { Fields.EnquiryBranch[this] = value; }
        }
        [DisplayName("Enquiry CreatedBy"), Expression("(SELECT DisplayName FROM Users WHERE UserId=(SELECT Top (1) OwnerId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        //[LookupEditor(typeof(UserRow), InplaceAdd = true)]
        public String EnquiryOwner
        {
            get { return Fields.EnquiryOwner[this]; }
            set { Fields.EnquiryOwner[this] = value; }
        }
        [DisplayName("Enquiry AssignedTo"), Expression("(SELECT DisplayName FROM Users WHERE UserId=(SELECT Top (1) AssignedId FROM Enquiry WHERE Id = t0.[EnquiryId]))")]
        //[LookupEditor(typeof(UserRow), InplaceAdd = true)]
        public String EnquiryAssigned
        {
            get { return Fields.EnquiryAssigned[this]; }
            set { Fields.EnquiryAssigned[this] = value; }
        }

       // [DisplayName("Multi Assign")]
       // [LookupEditor(typeof(UserRow), Multiple = true), NotMapped]
       //[LinkingSetRelation(typeof(MultiRepEnquiryRow), "EnquiryId", "AssignedId")]
       // // [LinkingSetRelation(]
       // public List<Int32> MultiAssignList
       // {
       //     get { return Fields.MultiAssignList[this]; }
       //     set { Fields.MultiAssignList[this] = value; }
       // }
       

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
            public DoubleField Quantity;
            public DoubleField Mrp;
            public DoubleField SellingPrice;
            public DoubleField Price;
            public DoubleField Discount;
            public Int32Field EnquiryId;
            public StringField Description;
            public StringField Capacity;
            public DecimalField LineTotal;

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
            public Int32Field ProductsCompanyId;

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
            //public Int32Field EnquiryContactPersonId;
            public StringField EnquiryReferenceName;
            public StringField EnquiryReferencePhone;
            public Int32Field EnquiryClosingType;
            public StringField EnquiryLostReason;
            public DateTimeField EnquiryClosingDate;
            public Int32Field EnquiryContactPersonId;
            public StringField EnquiryAttachments;
            public StringField EnquiryEnquiryN;
            public Int32Field EnquiryEnquiryNo;
            public Int32Field EnquiryCompanyId;
            // public readonly ListField<Int32> MultiAssignList;

            public StringField EnquiryContacts;
            public StringField EnquiryContactsMobile;
            public StringField EnquiryContactMail;
            public StringField EnquirySubContact;
            public StringField EnquirySubContactMobile;
            public StringField EnquirySubContactMail;
            public StringField EnquiryContactAddress;
            public StringField EnquirySource;
            public StringField EnquiryStage;
            public StringField EnquiryBranch;
            public StringField EnquiryOwner;
            public StringField EnquiryAssigned;
        }
    }
}
