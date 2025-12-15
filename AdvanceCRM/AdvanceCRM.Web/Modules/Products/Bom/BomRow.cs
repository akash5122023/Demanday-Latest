using AdvanceCRM.Purchase;
using AdvanceCRM.Scripts;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Products
{
    [ConnectionKey("Default"), Module("Products"), TableName("[dbo].[Bom]")]
    [DisplayName("Bom"), InstanceName("Bom")]
    [ReadPermission("Administration:Read")]
    [ModifyPermission("Administration:Modify")]
    //[LookupScript("Products.StockTransfer", Permission = "?", LookupType = typeof(MultiCompanyRowLookupScript<>))]
    [LookupScript("Products.Bom", Permission = "?")]


    public sealed class BomRow : Row<BomRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Contacts Id")]
        public Int32? ContactsId
        {
            get => fields.ContactsId[this];
            set => fields.ContactsId[this] = value;
        }

        [DisplayName("Date")]
        public DateTime? Date
        {
            get => fields.Date[this];
            set => fields.Date[this] = value;
        }

        [DisplayName("Status")]
        public Int32? Status
        {
            get => fields.Status[this];
            set => fields.Status[this] = value;
        }

        [DisplayName("Type")]
        public Int32? Type
        {
            get => fields.Type[this];
            set => fields.Type[this] = value;
        }

        [DisplayName("Additional Info"), Size(200), QuickSearch, NameProperty, TextAreaEditor(Rows = 4)]
        public String AdditionalInfo
        {
            get => fields.AdditionalInfo[this];
            set => fields.AdditionalInfo[this] = value;
        }

        [DisplayName("Branch Id")]
        public Int32? BranchId
        {
            get => fields.BranchId[this];
            set => fields.BranchId[this] = value;
        }

        [DisplayName("Created By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername")]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get => fields.OwnerId[this];
            set => fields.OwnerId[this] = value;
        }

        [DisplayName("Assigned To"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssigned"), TextualField("AssignedUsername")]
        [Administration.UserEditor]
        public Int32? AssignedId
        {
            get => fields.AssignedId[this];
            set => fields.AssignedId[this] = value;
        }

        [DisplayName("Other Address")]
        public Boolean? OtherAddress
        {
            get => fields.OtherAddress[this];
            set => fields.OtherAddress[this] = value;
        }

        [DisplayName("Shipping Address"), Size(1000)]
        public String ShippingAddress
        {
            get => fields.ShippingAddress[this];
            set => fields.ShippingAddress[this] = value;
        }

        [DisplayName("Packaging Charges")]
        public Double? PackagingCharges
        {
            get => fields.PackagingCharges[this];
            set => fields.PackagingCharges[this] = value;
        }

        [DisplayName("Freight Charges")]
        public Double? FreightCharges
        {
            get => fields.FreightCharges[this];
            set => fields.FreightCharges[this] = value;
        }

        [DisplayName("Advacne")]
        public Double? Advacne
        {
            get => fields.Advacne[this];
            set => fields.Advacne[this] = value;
        }

        [DisplayName("Due Date")]
        public DateTime? DueDate
        {
            get => fields.DueDate[this];
            set => fields.DueDate[this] = value;
        }

        [DisplayName("Dispatch Details"), Size(1000)]
        public String DispatchDetails
        {
            get => fields.DispatchDetails[this];
            set => fields.DispatchDetails[this] = value;
        }

        [DisplayName("Roundup")]
        public Double? Roundup
        {
            get => fields.Roundup[this];
            set => fields.Roundup[this] = value;
        }

        [DisplayName("Subject"), Size(1000)]
        public String Subject
        {
            get => fields.Subject[this];
            set => fields.Subject[this] = value;
        }

        [DisplayName("Reference"), Size(1000)]
        public String Reference
        {
            get => fields.Reference[this];
            set => fields.Reference[this] = value;
        }

        [DisplayName("Contact Person"), ForeignKey("[dbo].[SubContacts]", "Id"), LeftJoin("jContactPerson"), TextualField("ContactPersonName")]
        public Int32? ContactPersonId
        {
            get => fields.ContactPersonId[this];
            set => fields.ContactPersonId[this] = value;
        }

        [DisplayName("Lines")]
        public Int32? Lines
        {
            get => fields.Lines[this];
            set => fields.Lines[this] = value;
        }

        [DisplayName("Quotation No")]
        public Int32? QuotationNo
        {
            get => fields.QuotationNo[this];
            set => fields.QuotationNo[this] = value;
        }

        [DisplayName("Quotation Date")]
        public DateTime? QuotationDate
        {
            get => fields.QuotationDate[this];
            set => fields.QuotationDate[this] = value;
        }

        [DisplayName("Conversion")]
        public Double? Conversion
        {
            get => fields.Conversion[this];
            set => fields.Conversion[this] = value;
        }

        [DisplayName("Purchase Order No"), Size(1024)]
        public String PurchaseOrderNo
        {
            get => fields.PurchaseOrderNo[this];
            set => fields.PurchaseOrderNo[this] = value;
        }

        [DisplayName("Finish Good Product Name"), QuickSearch, LookupInclude]
        public String ItemName
        {
            get => fields.ItemName[this];
            set => fields.ItemName[this] = value;
        }

        [DisplayName("Operation Cost")]
        public Int32? OperationCost
        {
            get => fields.OperationCost[this];
            set => fields.OperationCost[this] = value;
        }

        [DisplayName("Raw Material Cost")]
        public Int32? RawMaterialCost
        {
            get => fields.RawMaterialCost[this];
            set => fields.RawMaterialCost[this] = value;
        }

        [DisplayName("Scrap Material Cost")]
        public Int32? ScrapMaterialCost
        {
            get => fields.ScrapMaterialCost[this];
            set => fields.ScrapMaterialCost[this] = value;
        }

        [DisplayName("Total Material Cost")]
        public Int32? TotalMaterialCost
        {
            get => fields.TotalMaterialCost[this];
            set => fields.TotalMaterialCost[this] = value;
        }

        [DisplayName("Operation Name"), Size(1024)]
        public String OperationName
        {
            get => fields.OperationName[this];
            set => fields.OperationName[this] = value;
        }

        [DisplayName("Work Station Name"), Size(1024)]
        public String WorkStationName
        {
            get => fields.WorkStationName[this];
            set => fields.WorkStationName[this] = value;
        }

        [DisplayName("Operatinng Time"), Size(100)]
        public String OperatinngTime
        {
            get => fields.OperatinngTime[this];
            set => fields.OperatinngTime[this] = value;
        }

        [DisplayName("Operating Cost")]
        public Int32? OperatingCost
        {
            get => fields.OperatingCost[this];
            set => fields.OperatingCost[this] = value;
        }

        [DisplayName("Process Loss")]
        public Int32? ProcessLoss
        {
            get => fields.ProcessLoss[this];
            set => fields.ProcessLoss[this] = value;
        }

        [DisplayName("Process Loss Qty")]
        public Int32? ProcessLossQty
        {
            get => fields.ProcessLossQty[this];
            set => fields.ProcessLossQty[this] = value;
        }

        [DisplayName("Attachments"), Size(1024)]
        [MultipleImageUploadEditor(FilenameFormat = "Bom/~", CopyToHistory = true, AllowNonImage = true)]

        public String Attachments
        {
            get => fields.Attachments[this];
            set => fields.Attachments[this] = value;
        }

        [DisplayName("Company Id")]
        public Int32? CompanyId
        {
            get => fields.CompanyId[this];
            set => fields.CompanyId[this] = value;
        }

        [DisplayName("Taxable")]
        public Int32? Taxable
        {
            get => fields.Taxable[this];
            set => fields.Taxable[this] = value;
        }

        [DisplayName("Quantity")]
        public Double? Quantity
        {
            get => fields.Quantity[this];
            set => fields.Quantity[this] = value;
        }

        [DisplayName("Mrp"), Column("MRP"), NotNull]
        public Double? Mrp
        {
            get => fields.Mrp[this];
            set => fields.Mrp[this] = value;
        }

        [DisplayName("Selling Price")]
        public Double? SellingPrice
        {
            get => fields.SellingPrice[this];
            set => fields.SellingPrice[this] = value;
        }

        [DisplayName("Price"), NotNull]
        public Double? Price
        {
            get => fields.Price[this];
            set => fields.Price[this] = value;
        }

        [DisplayName("Discount")]
        public Double? Discount
        {
            get => fields.Discount[this];
            set => fields.Discount[this] = value;
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

        [DisplayName("Warranty Start")]
        public DateTime? WarrantyStart
        {
            get => fields.WarrantyStart[this];
            set => fields.WarrantyStart[this] = value;
        }

        [DisplayName("Warranty End")]
        public DateTime? WarrantyEnd
        {
            get => fields.WarrantyEnd[this];
            set => fields.WarrantyEnd[this] = value;
        }

        [DisplayName("Discount Amount")]
        public Double? DiscountAmount
        {
            get => fields.DiscountAmount[this];
            set => fields.DiscountAmount[this] = value;
        }

        [DisplayName("Description"), Size(2000)]
        public String Description
        {
            get => fields.Description[this];
            set => fields.Description[this] = value;
        }

        [DisplayName("Unit"), Size(128)]
        public String Unit
        {
            get => fields.Unit[this];
            set => fields.Unit[this] = value;
        }

        [DisplayName("Image"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Product/~", CopyToHistory = true)]
        public String Image
        {
            get => fields.Image[this];
            set => fields.Image[this] = value;
        }

        [DisplayName("Tech Specs"), Size(2000), TextAreaEditor(Rows = 8)]
        public String TechSpecs
        {
            get => fields.TechSpecs[this];
            set => fields.TechSpecs[this] = value;
        }

        [DisplayName("Hsn"), Column("HSN"), Size(100)]
        public String Hsn
        {
            get => fields.Hsn[this];
            set => fields.Hsn[this] = value;
        }

        [DisplayName("Code"), Size(100)]
        public String Code
        {
            get => fields.Code[this];
            set => fields.Code[this] = value;
        }

        [DisplayName("Products"), ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName")]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]

        public Int32? ProductsId
        {
            get => fields.ProductsId[this];
            set => fields.ProductsId[this] = value;
        }

        [DisplayName("Contact Person Name"), Expression("jContactPerson.[Name]")]
        public String ContactPersonName
        {
            get => fields.ContactPersonName[this];
            set => fields.ContactPersonName[this] = value;
        }

        [DisplayName("Contact Person Phone"), Expression("jContactPerson.[Phone]")]
        public String ContactPersonPhone
        {
            get => fields.ContactPersonPhone[this];
            set => fields.ContactPersonPhone[this] = value;
        }

        [DisplayName("Contact Person Residential Phone"), Expression("jContactPerson.[ResidentialPhone]")]
        public String ContactPersonResidentialPhone
        {
            get => fields.ContactPersonResidentialPhone[this];
            set => fields.ContactPersonResidentialPhone[this] = value;
        }

        [DisplayName("Contact Person Email"), Expression("jContactPerson.[Email]")]
        public String ContactPersonEmail
        {
            get => fields.ContactPersonEmail[this];
            set => fields.ContactPersonEmail[this] = value;
        }

        [DisplayName("Contact Person Designation"), Expression("jContactPerson.[Designation]")]
        public String ContactPersonDesignation
        {
            get => fields.ContactPersonDesignation[this];
            set => fields.ContactPersonDesignation[this] = value;
        }

        [DisplayName("Contact Person Address"), Expression("jContactPerson.[Address]")]
        public String ContactPersonAddress
        {
            get => fields.ContactPersonAddress[this];
            set => fields.ContactPersonAddress[this] = value;
        }

        [DisplayName("Contact Person Gender"), Expression("jContactPerson.[Gender]")]
        public Int32? ContactPersonGender
        {
            get => fields.ContactPersonGender[this];
            set => fields.ContactPersonGender[this] = value;
        }

        [DisplayName("Contact Person Religion"), Expression("jContactPerson.[Religion]")]
        public Int32? ContactPersonReligion
        {
            get => fields.ContactPersonReligion[this];
            set => fields.ContactPersonReligion[this] = value;
        }

        [DisplayName("Contact Person Marital Status"), Expression("jContactPerson.[MaritalStatus]")]
        public Int32? ContactPersonMaritalStatus
        {
            get => fields.ContactPersonMaritalStatus[this];
            set => fields.ContactPersonMaritalStatus[this] = value;
        }

        [DisplayName("Contact Person Marriage Anniversary"), Expression("jContactPerson.[MarriageAnniversary]")]
        public DateTime? ContactPersonMarriageAnniversary
        {
            get => fields.ContactPersonMarriageAnniversary[this];
            set => fields.ContactPersonMarriageAnniversary[this] = value;
        }

        [DisplayName("Contact Person Birthdate"), Expression("jContactPerson.[Birthdate]")]
        public DateTime? ContactPersonBirthdate
        {
            get => fields.ContactPersonBirthdate[this];
            set => fields.ContactPersonBirthdate[this] = value;
        }

        [DisplayName("Contact Person Contacts Id"), Expression("jContactPerson.[ContactsId]")]
        public Int32? ContactPersonContactsId
        {
            get => fields.ContactPersonContactsId[this];
            set => fields.ContactPersonContactsId[this] = value;
        }

        [DisplayName("Contact Person Project"), Expression("jContactPerson.[Project]")]
        public String ContactPersonProject
        {
            get => fields.ContactPersonProject[this];
            set => fields.ContactPersonProject[this] = value;
        }

        [DisplayName("Contact Person Whatsapp"), Expression("jContactPerson.[Whatsapp]")]
        public String ContactPersonWhatsapp
        {
            get => fields.ContactPersonWhatsapp[this];
            set => fields.ContactPersonWhatsapp[this] = value;
        }

        [DisplayName("Contact Person Passport Number"), Expression("jContactPerson.[PassportNumber]")]
        public String ContactPersonPassportNumber
        {
            get => fields.ContactPersonPassportNumber[this];
            set => fields.ContactPersonPassportNumber[this] = value;
        }

        [DisplayName("Contact Person First Name"), Expression("jContactPerson.[FirstName]")]
        public String ContactPersonFirstName
        {
            get => fields.ContactPersonFirstName[this];
            set => fields.ContactPersonFirstName[this] = value;
        }

        [DisplayName("Contact Person Last Name"), Expression("jContactPerson.[LastName]")]
        public String ContactPersonLastName
        {
            get => fields.ContactPersonLastName[this];
            set => fields.ContactPersonLastName[this] = value;
        }

        [DisplayName("Contact Person Expiry Date"), Expression("jContactPerson.[ExpiryDate]")]
        public DateTime? ContactPersonExpiryDate
        {
            get => fields.ContactPersonExpiryDate[this];
            set => fields.ContactPersonExpiryDate[this] = value;
        }

        [DisplayName("Contact Person Aadhar No"), Expression("jContactPerson.[AadharNo]")]
        public String ContactPersonAadharNo
        {
            get => fields.ContactPersonAadharNo[this];
            set => fields.ContactPersonAadharNo[this] = value;
        }

        [DisplayName("Contact Person Pan No"), Expression("jContactPerson.[PANNo]")]
        public String ContactPersonPanNo
        {
            get => fields.ContactPersonPanNo[this];
            set => fields.ContactPersonPanNo[this] = value;
        }

        [DisplayName("Contact Person File Attachments"), Expression("jContactPerson.[FileAttachments]")]
        public String ContactPersonFileAttachments
        {
            get => fields.ContactPersonFileAttachments[this];
            set => fields.ContactPersonFileAttachments[this] = value;
        }

        [DisplayName("Finish Good Product Name"), Expression("jProducts.[Name]")]
        public String ProductsName
        {
            get => fields.ProductsName[this];
            set => fields.ProductsName[this] = value;
        }

        [DisplayName("Products Code"), Expression("jProducts.[Code]")]
        public String ProductsCode
        {
            get => fields.ProductsCode[this];
            set => fields.ProductsCode[this] = value;
        }

        [DisplayName("Products Division Id"), Expression("jProducts.[DivisionId]")]
        public Int32? ProductsDivisionId
        {
            get => fields.ProductsDivisionId[this];
            set => fields.ProductsDivisionId[this] = value;
        }

        [DisplayName("Products Group Id"), Expression("jProducts.[GroupId]")]
        public Int32? ProductsGroupId
        {
            get => fields.ProductsGroupId[this];
            set => fields.ProductsGroupId[this] = value;
        }

        [DisplayName("Products Selling Price"), Expression("jProducts.[SellingPrice]")]
        public Double? ProductsSellingPrice
        {
            get => fields.ProductsSellingPrice[this];
            set => fields.ProductsSellingPrice[this] = value;
        }

        [DisplayName("Products Mrp"), Expression("jProducts.[MRP]")]
        public Double? ProductsMrp
        {
            get => fields.ProductsMrp[this];
            set => fields.ProductsMrp[this] = value;
        }

        [DisplayName("Products Description"), Expression("jProducts.[Description]")]
        public String ProductsDescription
        {
            get => fields.ProductsDescription[this];
            set => fields.ProductsDescription[this] = value;
        }

        [DisplayName("Products Tax Id1"), Expression("jProducts.[TaxId1]")]
        public Int32? ProductsTaxId1
        {
            get => fields.ProductsTaxId1[this];
            set => fields.ProductsTaxId1[this] = value;
        }

        [DisplayName("Products Tax Id2"), Expression("jProducts.[TaxId2]")]
        public Int32? ProductsTaxId2
        {
            get => fields.ProductsTaxId2[this];
            set => fields.ProductsTaxId2[this] = value;
        }

        [DisplayName("Products Image"), Expression("jProducts.[Image]")]
        public String ProductsImage
        {
            get => fields.ProductsImage[this];
            set => fields.ProductsImage[this] = value;
        }

        [DisplayName("Products Tech Specs"), Expression("jProducts.[TechSpecs]")]
        public String ProductsTechSpecs
        {
            get => fields.ProductsTechSpecs[this];
            set => fields.ProductsTechSpecs[this] = value;
        }

        [DisplayName("Products Hsn"), Expression("jProducts.[HSN]")]
        public String ProductsHsn
        {
            get => fields.ProductsHsn[this];
            set => fields.ProductsHsn[this] = value;
        }

        [DisplayName("Products Channel Customer Price"), Expression("jProducts.[ChannelCustomerPrice]")]
        public Double? ProductsChannelCustomerPrice
        {
            get => fields.ProductsChannelCustomerPrice[this];
            set => fields.ProductsChannelCustomerPrice[this] = value;
        }

        [DisplayName("Products Reseller Price"), Expression("jProducts.[ResellerPrice]")]
        public Double? ProductsResellerPrice
        {
            get => fields.ProductsResellerPrice[this];
            set => fields.ProductsResellerPrice[this] = value;
        }

        [DisplayName("Products Wholesaler Price"), Expression("jProducts.[WholesalerPrice]")]
        public Double? ProductsWholesalerPrice
        {
            get => fields.ProductsWholesalerPrice[this];
            set => fields.ProductsWholesalerPrice[this] = value;
        }

        [DisplayName("Products Dealer Price"), Expression("jProducts.[DealerPrice]")]
        public Double? ProductsDealerPrice
        {
            get => fields.ProductsDealerPrice[this];
            set => fields.ProductsDealerPrice[this] = value;
        }

        [DisplayName("Products Distributor Price"), Expression("jProducts.[DistributorPrice]")]
        public Double? ProductsDistributorPrice
        {
            get => fields.ProductsDistributorPrice[this];
            set => fields.ProductsDistributorPrice[this] = value;
        }

        [DisplayName("Products Stockiest Price"), Expression("jProducts.[StockiestPrice]")]
        public Double? ProductsStockiestPrice
        {
            get => fields.ProductsStockiestPrice[this];
            set => fields.ProductsStockiestPrice[this] = value;
        }

        [DisplayName("Products National Distributor Price"), Expression("jProducts.[NationalDistributorPrice]")]
        public Double? ProductsNationalDistributorPrice
        {
            get => fields.ProductsNationalDistributorPrice[this];
            set => fields.ProductsNationalDistributorPrice[this] = value;
        }

        [DisplayName("Products Minimum Stock"), Expression("jProducts.[MinimumStock]")]
        public Double? ProductsMinimumStock
        {
            get => fields.ProductsMinimumStock[this];
            set => fields.ProductsMinimumStock[this] = value;
        }

        [DisplayName("Products Maximum Stock"), Expression("jProducts.[MaximumStock]")]
        public Double? ProductsMaximumStock
        {
            get => fields.ProductsMaximumStock[this];
            set => fields.ProductsMaximumStock[this] = value;
        }

        [DisplayName("Products Raw Material"), Expression("jProducts.[RawMaterial]")]
        public Boolean? ProductsRawMaterial
        {
            get => fields.ProductsRawMaterial[this];
            set => fields.ProductsRawMaterial[this] = value;
        }

        [DisplayName("Products Purchase Price"), Expression("jProducts.[PurchasePrice]")]
        public Double? ProductsPurchasePrice
        {
            get => fields.ProductsPurchasePrice[this];
            set => fields.ProductsPurchasePrice[this] = value;
        }

        [DisplayName("Products Opening Stock"), Expression("jProducts.[OpeningStock]")]
        public Double? ProductsOpeningStock
        {
            get => fields.ProductsOpeningStock[this];
            set => fields.ProductsOpeningStock[this] = value;
        }

        [DisplayName("Products Unit Id"), Expression("jProducts.[UnitId]")]
        public Int32? ProductsUnitId
        {
            get => fields.ProductsUnitId[this];
            set => fields.ProductsUnitId[this] = value;
        }

        [DisplayName("Products Company Id"), Expression("jProducts.[CompanyId]")]
        public Int32? ProductsCompanyId
        {
            get => fields.ProductsCompanyId[this];
            set => fields.ProductsCompanyId[this] = value;
        }

        [DisplayName("Products Product Type Id"), Expression("jProducts.[ProductTypeId]")]
        public Int32? ProductsProductTypeId
        {
            get => fields.ProductsProductTypeId[this];
            set => fields.ProductsProductTypeId[this] = value;
        }

        [DisplayName("Products Model Segment Id"), Expression("jProducts.[ModelSegmentId]")]
        public Int32? ProductsModelSegmentId
        {
            get => fields.ProductsModelSegmentId[this];
            set => fields.ProductsModelSegmentId[this] = value;
        }

        [DisplayName("Products Model Name Id"), Expression("jProducts.[ModelNameID]")]
        public Int32? ProductsModelNameId
        {
            get => fields.ProductsModelNameId[this];
            set => fields.ProductsModelNameId[this] = value;
        }

        [DisplayName("Products Model Code Id"), Expression("jProducts.[ModelCodeId]")]
        public Int32? ProductsModelCodeId
        {
            get => fields.ProductsModelCodeId[this];
            set => fields.ProductsModelCodeId[this] = value;
        }

        [DisplayName("Products Model Varient Id"), Expression("jProducts.[ModelVarientId]")]
        public Int32? ProductsModelVarientId
        {
            get => fields.ProductsModelVarientId[this];
            set => fields.ProductsModelVarientId[this] = value;
        }

        [DisplayName("Products Model Color Id"), Expression("jProducts.[ModelColorId]")]
        public Int32? ProductsModelColorId
        {
            get => fields.ProductsModelColorId[this];
            set => fields.ProductsModelColorId[this] = value;
        }

        [DisplayName("Products Serial No"), Expression("jProducts.[SerialNo]")]
        public String ProductsSerialNo
        {
            get => fields.ProductsSerialNo[this];
            set => fields.ProductsSerialNo[this] = value;
        }

        [DisplayName("Products Ex Showroom Price"), Expression("jProducts.[ExShowroomPrice]")]
        public Double? ProductsExShowroomPrice
        {
            get => fields.ProductsExShowroomPrice[this];
            set => fields.ProductsExShowroomPrice[this] = value;
        }

        [DisplayName("Products Insurance Amount"), Expression("jProducts.[InsuranceAmount]")]
        public Double? ProductsInsuranceAmount
        {
            get => fields.ProductsInsuranceAmount[this];
            set => fields.ProductsInsuranceAmount[this] = value;
        }

        [DisplayName("Products Registration Amount"), Expression("jProducts.[RegistrationAmount]")]
        public Double? ProductsRegistrationAmount
        {
            get => fields.ProductsRegistrationAmount[this];
            set => fields.ProductsRegistrationAmount[this] = value;
        }

        [DisplayName("Products Road Tax"), Expression("jProducts.[RoadTax]")]
        public Double? ProductsRoadTax
        {
            get => fields.ProductsRoadTax[this];
            set => fields.ProductsRoadTax[this] = value;
        }

        [DisplayName("Products On Road Price"), Expression("jProducts.[OnRoadPrice]")]
        public Double? ProductsOnRoadPrice
        {
            get => fields.ProductsOnRoadPrice[this];
            set => fields.ProductsOnRoadPrice[this] = value;
        }

        [DisplayName("Products Other Taxes"), Expression("jProducts.[OtherTaxes]")]
        public Double? ProductsOtherTaxes
        {
            get => fields.ProductsOtherTaxes[this];
            set => fields.ProductsOtherTaxes[this] = value;
        }

        [DisplayName("Products Extended Warranty"), Expression("jProducts.[ExtendedWarranty]")]
        public Double? ProductsExtendedWarranty
        {
            get => fields.ProductsExtendedWarranty[this];
            set => fields.ProductsExtendedWarranty[this] = value;
        }

        [DisplayName("Products Rsa"), Expression("jProducts.[RSA]")]
        public Double? ProductsRsa
        {
            get => fields.ProductsRsa[this];
            set => fields.ProductsRsa[this] = value;
        }

        [DisplayName("Products Image Attachment"), Expression("jProducts.[ImageAttachment]")]
        public String ProductsImageAttachment
        {
            get => fields.ProductsImageAttachment[this];
            set => fields.ProductsImageAttachment[this] = value;
        }

        [DisplayName("Products File Attachment"), Expression("jProducts.[FileAttachment]")]
        public String ProductsFileAttachment
        {
            get => fields.ProductsFileAttachment[this];
            set => fields.ProductsFileAttachment[this] = value;
        }

        [DisplayName("Products From"), Expression("jProducts.[From]")]
        public String ProductsFrom
        {
            get => fields.ProductsFrom[this];
            set => fields.ProductsFrom[this] = value;
        }

        [DisplayName("Products To"), Expression("jProducts.[To]")]
        public String ProductsTo
        {
            get => fields.ProductsTo[this];
            set => fields.ProductsTo[this] = value;
        }

        [DisplayName("Products Date"), Expression("jProducts.[Date]")]
        public DateTime? ProductsDate
        {
            get => fields.ProductsDate[this];
            set => fields.ProductsDate[this] = value;
        }

        [DisplayName("Products Adults"), Expression("jProducts.[Adults]")]
        public String ProductsAdults
        {
            get => fields.ProductsAdults[this];
            set => fields.ProductsAdults[this] = value;
        }

        [DisplayName("Products Childrens"), Expression("jProducts.[Childrens]")]
        public String ProductsChildrens
        {
            get => fields.ProductsChildrens[this];
            set => fields.ProductsChildrens[this] = value;
        }

        [DisplayName("Products Destination"), Expression("jProducts.[Destination]")]
        public String ProductsDestination
        {
            get => fields.ProductsDestination[this];
            set => fields.ProductsDestination[this] = value;
        }

        [DisplayName("Products Nights"), Expression("jProducts.[Nights]")]
        public String ProductsNights
        {
            get => fields.ProductsNights[this];
            set => fields.ProductsNights[this] = value;
        }

        [DisplayName("Products Hotel Name"), Expression("jProducts.[HotelName]")]
        public String ProductsHotelName
        {
            get => fields.ProductsHotelName[this];
            set => fields.ProductsHotelName[this] = value;
        }

        [DisplayName("Products Meal Plan"), Expression("jProducts.[MealPlan]")]
        public String ProductsMealPlan
        {
            get => fields.ProductsMealPlan[this];
            set => fields.ProductsMealPlan[this] = value;
        }

        [DisplayName("Products"), MasterDetailRelation(foreignKey: "BomId", IncludeColumns = "ProductsId,ProductsName"), NotMapped]
        public List<BomProductsRow> Products { get { return Fields.Products[this]; } set { Fields.Products[this] = value; } }

        public BomRow()
            : base()
        {
        }

        public BomRow(RowFields fields)
            : base(fields)
        {
        }
        //StringField INameRow.NameField
        //{
        //    get { return Fields.ItemName; }
        //    //get { return Fields.Name; }
        //}

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ContactsId;
            public DateTimeField Date;
            public Int32Field Status;
            public Int32Field Type;
            public StringField AdditionalInfo;
            public Int32Field BranchId;
            public Int32Field OwnerId;
            public Int32Field AssignedId;
            public BooleanField OtherAddress;
            public StringField ShippingAddress;
            public DoubleField PackagingCharges;
            public DoubleField FreightCharges;
            public DoubleField Advacne;
            public DateTimeField DueDate;
            public StringField DispatchDetails;
            public DoubleField Roundup;
            public StringField Subject;
            public StringField Reference;
            public Int32Field ContactPersonId;
            public Int32Field Lines;
            public Int32Field QuotationNo;
            public DateTimeField QuotationDate;
            public DoubleField Conversion;
            public StringField PurchaseOrderNo;
            public StringField ItemName;
            public Int32Field OperationCost;
            public Int32Field RawMaterialCost;
            public Int32Field ScrapMaterialCost;
            public Int32Field TotalMaterialCost;
            public StringField OperationName;
            public StringField WorkStationName;
            public StringField OperatinngTime;
            public Int32Field OperatingCost;
            public Int32Field ProcessLoss;
            public Int32Field ProcessLossQty;
            public StringField Attachments;
            public Int32Field CompanyId;
            public Int32Field Taxable;
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
            public DoubleField DiscountAmount;
            public StringField Description;
            public StringField Unit;
            public StringField Image;
            public StringField TechSpecs;
            public StringField Hsn;
            public StringField Code;
            public Int32Field ProductsId;

            public StringField ContactPersonName;
            public StringField ContactPersonPhone;
            public StringField ContactPersonResidentialPhone;
            public StringField ContactPersonEmail;
            public StringField ContactPersonDesignation;
            public StringField ContactPersonAddress;
            public Int32Field ContactPersonGender;
            public Int32Field ContactPersonReligion;
            public Int32Field ContactPersonMaritalStatus;
            public DateTimeField ContactPersonMarriageAnniversary;
            public DateTimeField ContactPersonBirthdate;
            public Int32Field ContactPersonContactsId;
            public StringField ContactPersonProject;
            public StringField ContactPersonWhatsapp;
            public StringField ContactPersonPassportNumber;
            public StringField ContactPersonFirstName;
            public StringField ContactPersonLastName;
            public DateTimeField ContactPersonExpiryDate;
            public StringField ContactPersonAadharNo;
            public StringField ContactPersonPanNo;
            public StringField ContactPersonFileAttachments;

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
            public Int32Field ProductsProductTypeId;
            public Int32Field ProductsModelSegmentId;
            public Int32Field ProductsModelNameId;
            public Int32Field ProductsModelCodeId;
            public Int32Field ProductsModelVarientId;
            public Int32Field ProductsModelColorId;
            public StringField ProductsSerialNo;
            public DoubleField ProductsExShowroomPrice;
            public DoubleField ProductsInsuranceAmount;
            public DoubleField ProductsRegistrationAmount;
            public DoubleField ProductsRoadTax;
            public DoubleField ProductsOnRoadPrice;
            public DoubleField ProductsOtherTaxes;
            public DoubleField ProductsExtendedWarranty;
            public DoubleField ProductsRsa;
            public StringField ProductsImageAttachment;
            public StringField ProductsFileAttachment;
            public StringField ProductsFrom;
            public StringField ProductsTo;
            public DateTimeField ProductsDate;
            public StringField ProductsAdults;
            public StringField ProductsChildrens;
            public StringField ProductsDestination;
            public StringField ProductsNights;
            public StringField ProductsHotelName;
            public StringField ProductsMealPlan;

            public readonly RowListField<BomProductsRow> Products;


        }
    }
}
