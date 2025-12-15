using AdvanceCRM.Contacts;
using AdvanceCRM.Products;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Purchase
{
    [ConnectionKey("Default"), Module("Purchase"), TableName("[dbo].[QualityCheck]")]
    [DisplayName("Quality Check"), InstanceName("Quality Check")]
    [ReadPermission("QualityCheck:Read")]
    [InsertPermission("QualityCheck:Insert")]
    [UpdatePermission("QualityCheck:Update")]
    [DeletePermission("QualityCheck:Delete")]
    [LookupScript("Purchase.QualityCheckRow", Permission = "?")]
    public sealed class QualityCheckRow : Row<QualityCheckRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Qc Number"), Column("QCNumber")]
        public Int32? QcNumber
        {
            get => fields.QcNumber[this];
            set => fields.QcNumber[this] = value;
        }

        [DisplayName("Purchase Date")]
        public DateTime? PurchaseDate
        {
            get => fields.PurchaseDate[this];
            set => fields.PurchaseDate[this] = value;
        }

        [DisplayName("Product Name"), Size(255), QuickSearch, NameProperty]
        public String ProductName
        {
            get => fields.ProductName[this];
            set => fields.ProductName[this] = value;
        }

        [DisplayName("Qc Date"), Column("QCDate")]
        public DateTime? QcDate
        {
            get => fields.QcDate[this];
            set => fields.QcDate[this] = value;
        }

        [DisplayName("Inspection Criteria"), Size(200)]
        public String InspectionCriteria
        {
            get => fields.InspectionCriteria[this];
            set => fields.InspectionCriteria[this] = value;
        }

        [DisplayName("Qty Inspected")]
        public Int32? QtyInspected
        {
            get => fields.QtyInspected[this];
            set => fields.QtyInspected[this] = value;
        }

        [DisplayName("Qty Passed")]
        public Int32? QtyPassed
        {
            get => fields.QtyPassed[this];
            set => fields.QtyPassed[this] = value;
        }

        [DisplayName("Qty Rejected")]
        public Int32? QtyRejected
        {
            get => fields.QtyRejected[this];
            set => fields.QtyRejected[this] = value;
        }

        [DisplayName("Deposition Action"), Size(200)]
        public String DepositionAction
        {
            get => fields.DepositionAction[this];
            set => fields.DepositionAction[this] = value;
        }

        [DisplayName("Additional Info"), Size(200), TextAreaEditor(Rows = 4)]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }

        [DisplayName("Attachments"), Size(1024)]
        [MultipleImageUploadEditor(FilenameFormat = "QualityCheck/~", CopyToHistory = true, AllowNonImage = true)]

        public String Attachments
        {
            get { return Fields.Attachments[this]; }
            set { Fields.Attachments[this] = value; }
        }

        [DisplayName("Product"), ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProduct"), TextualField("ProductName1")]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductId
        {
            get => fields.ProductId[this];
            set => fields.ProductId[this] = value;
        }

        [DisplayName("Purchase From"), ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jPurchaseFrom"), TextualField("PurchaseFromName")]
        [LookupEditor(typeof(ContactsRow), InplaceAdd = true)]
        public Int32? PurchaseFromId
        {
            get => fields.PurchaseFromId[this];
            set => fields.PurchaseFromId[this] = value;
        }

        [DisplayName("Product Name"), Expression("jProduct.[Name]"), QuickSearch]
        public String ProductName1
        {
            get => fields.ProductName1[this];
            set => fields.ProductName1[this] = value;
        }

        [DisplayName("Product Code"), Expression("jProduct.[Code]")]
        public String ProductCode
        {
            get => fields.ProductCode[this];
            set => fields.ProductCode[this] = value;
        }

        [DisplayName("Product Division Id"), Expression("jProduct.[DivisionId]")]
        public Int32? ProductDivisionId
        {
            get => fields.ProductDivisionId[this];
            set => fields.ProductDivisionId[this] = value;
        }

        [DisplayName("Product Group Id"), Expression("jProduct.[GroupId]")]
        public Int32? ProductGroupId
        {
            get => fields.ProductGroupId[this];
            set => fields.ProductGroupId[this] = value;
        }

        [DisplayName("Product Selling Price"), Expression("jProduct.[SellingPrice]")]
        public Double? ProductSellingPrice
        {
            get => fields.ProductSellingPrice[this];
            set => fields.ProductSellingPrice[this] = value;
        }

        [DisplayName("Product Mrp"), Expression("jProduct.[MRP]")]
        public Double? ProductMrp
        {
            get => fields.ProductMrp[this];
            set => fields.ProductMrp[this] = value;
        }

        [DisplayName("Product Description"), Expression("jProduct.[Description]")]
        public String ProductDescription
        {
            get => fields.ProductDescription[this];
            set => fields.ProductDescription[this] = value;
        }

        [DisplayName("Product Tax Id1"), Expression("jProduct.[TaxId1]")]
        public Int32? ProductTaxId1
        {
            get => fields.ProductTaxId1[this];
            set => fields.ProductTaxId1[this] = value;
        }

        [DisplayName("Product Tax Id2"), Expression("jProduct.[TaxId2]")]
        public Int32? ProductTaxId2
        {
            get => fields.ProductTaxId2[this];
            set => fields.ProductTaxId2[this] = value;
        }

        [DisplayName("Product Image"), Expression("jProduct.[Image]")]
        public String ProductImage
        {
            get => fields.ProductImage[this];
            set => fields.ProductImage[this] = value;
        }

        [DisplayName("Product Tech Specs"), Expression("jProduct.[TechSpecs]")]
        public String ProductTechSpecs
        {
            get => fields.ProductTechSpecs[this];
            set => fields.ProductTechSpecs[this] = value;
        }

        [DisplayName("Product Hsn"), Expression("jProduct.[HSN]")]
        public String ProductHsn
        {
            get => fields.ProductHsn[this];
            set => fields.ProductHsn[this] = value;
        }

        [DisplayName("Product Channel Customer Price"), Expression("jProduct.[ChannelCustomerPrice]")]
        public Double? ProductChannelCustomerPrice
        {
            get => fields.ProductChannelCustomerPrice[this];
            set => fields.ProductChannelCustomerPrice[this] = value;
        }

        [DisplayName("Product Reseller Price"), Expression("jProduct.[ResellerPrice]")]
        public Double? ProductResellerPrice
        {
            get => fields.ProductResellerPrice[this];
            set => fields.ProductResellerPrice[this] = value;
        }

        [DisplayName("Product Wholesaler Price"), Expression("jProduct.[WholesalerPrice]")]
        public Double? ProductWholesalerPrice
        {
            get => fields.ProductWholesalerPrice[this];
            set => fields.ProductWholesalerPrice[this] = value;
        }

        [DisplayName("Product Dealer Price"), Expression("jProduct.[DealerPrice]")]
        public Double? ProductDealerPrice
        {
            get => fields.ProductDealerPrice[this];
            set => fields.ProductDealerPrice[this] = value;
        }

        [DisplayName("Product Distributor Price"), Expression("jProduct.[DistributorPrice]")]
        public Double? ProductDistributorPrice
        {
            get => fields.ProductDistributorPrice[this];
            set => fields.ProductDistributorPrice[this] = value;
        }

        [DisplayName("Product Stockiest Price"), Expression("jProduct.[StockiestPrice]")]
        public Double? ProductStockiestPrice
        {
            get => fields.ProductStockiestPrice[this];
            set => fields.ProductStockiestPrice[this] = value;
        }

        [DisplayName("Product National Distributor Price"), Expression("jProduct.[NationalDistributorPrice]")]
        public Double? ProductNationalDistributorPrice
        {
            get => fields.ProductNationalDistributorPrice[this];
            set => fields.ProductNationalDistributorPrice[this] = value;
        }

        [DisplayName("Product Minimum Stock"), Expression("jProduct.[MinimumStock]")]
        public Double? ProductMinimumStock
        {
            get => fields.ProductMinimumStock[this];
            set => fields.ProductMinimumStock[this] = value;
        }

        [DisplayName("Product Maximum Stock"), Expression("jProduct.[MaximumStock]")]
        public Double? ProductMaximumStock
        {
            get => fields.ProductMaximumStock[this];
            set => fields.ProductMaximumStock[this] = value;
        }

        [DisplayName("Product Raw Material"), Expression("jProduct.[RawMaterial]")]
        public Boolean? ProductRawMaterial
        {
            get => fields.ProductRawMaterial[this];
            set => fields.ProductRawMaterial[this] = value;
        }

        [DisplayName("Product Purchase Price"), Expression("jProduct.[PurchasePrice]")]
        public Double? ProductPurchasePrice
        {
            get => fields.ProductPurchasePrice[this];
            set => fields.ProductPurchasePrice[this] = value;
        }

        [DisplayName("Product Opening Stock"), Expression("jProduct.[OpeningStock]")]
        public Double? ProductOpeningStock
        {
            get => fields.ProductOpeningStock[this];
            set => fields.ProductOpeningStock[this] = value;
        }

        [DisplayName("Product Unit Id"), Expression("jProduct.[UnitId]")]
        public Int32? ProductUnitId
        {
            get => fields.ProductUnitId[this];
            set => fields.ProductUnitId[this] = value;
        }

        [DisplayName("Product Company Id"), Expression("jProduct.[CompanyId]")]
        public Int32? ProductCompanyId
        {
            get => fields.ProductCompanyId[this];
            set => fields.ProductCompanyId[this] = value;
        }

        [DisplayName("Product Product Type Id"), Expression("jProduct.[ProductTypeId]")]
        public Int32? ProductProductTypeId
        {
            get => fields.ProductProductTypeId[this];
            set => fields.ProductProductTypeId[this] = value;
        }

        [DisplayName("Product Model Segment Id"), Expression("jProduct.[ModelSegmentId]")]
        public Int32? ProductModelSegmentId
        {
            get => fields.ProductModelSegmentId[this];
            set => fields.ProductModelSegmentId[this] = value;
        }

        [DisplayName("Product Model Name Id"), Expression("jProduct.[ModelNameID]")]
        public Int32? ProductModelNameId
        {
            get => fields.ProductModelNameId[this];
            set => fields.ProductModelNameId[this] = value;
        }

        [DisplayName("Product Model Code Id"), Expression("jProduct.[ModelCodeId]")]
        public Int32? ProductModelCodeId
        {
            get => fields.ProductModelCodeId[this];
            set => fields.ProductModelCodeId[this] = value;
        }

        [DisplayName("Product Model Varient Id"), Expression("jProduct.[ModelVarientId]")]
        public Int32? ProductModelVarientId
        {
            get => fields.ProductModelVarientId[this];
            set => fields.ProductModelVarientId[this] = value;
        }

        [DisplayName("Product Model Color Id"), Expression("jProduct.[ModelColorId]")]
        public Int32? ProductModelColorId
        {
            get => fields.ProductModelColorId[this];
            set => fields.ProductModelColorId[this] = value;
        }

        [DisplayName("Product Serial No"), Expression("jProduct.[SerialNo]")]
        public String ProductSerialNo
        {
            get => fields.ProductSerialNo[this];
            set => fields.ProductSerialNo[this] = value;
        }

        [DisplayName("Product Ex Showroom Price"), Expression("jProduct.[ExShowroomPrice]")]
        public Double? ProductExShowroomPrice
        {
            get => fields.ProductExShowroomPrice[this];
            set => fields.ProductExShowroomPrice[this] = value;
        }

        [DisplayName("Product Insurance Amount"), Expression("jProduct.[InsuranceAmount]")]
        public Double? ProductInsuranceAmount
        {
            get => fields.ProductInsuranceAmount[this];
            set => fields.ProductInsuranceAmount[this] = value;
        }

        [DisplayName("Product Registration Amount"), Expression("jProduct.[RegistrationAmount]")]
        public Double? ProductRegistrationAmount
        {
            get => fields.ProductRegistrationAmount[this];
            set => fields.ProductRegistrationAmount[this] = value;
        }

        [DisplayName("Product Road Tax"), Expression("jProduct.[RoadTax]")]
        public Double? ProductRoadTax
        {
            get => fields.ProductRoadTax[this];
            set => fields.ProductRoadTax[this] = value;
        }

        [DisplayName("Product On Road Price"), Expression("jProduct.[OnRoadPrice]")]
        public Double? ProductOnRoadPrice
        {
            get => fields.ProductOnRoadPrice[this];
            set => fields.ProductOnRoadPrice[this] = value;
        }

        [DisplayName("Product Other Taxes"), Expression("jProduct.[OtherTaxes]")]
        public Double? ProductOtherTaxes
        {
            get => fields.ProductOtherTaxes[this];
            set => fields.ProductOtherTaxes[this] = value;
        }

        [DisplayName("Product Extended Warranty"), Expression("jProduct.[ExtendedWarranty]")]
        public Double? ProductExtendedWarranty
        {
            get => fields.ProductExtendedWarranty[this];
            set => fields.ProductExtendedWarranty[this] = value;
        }

        [DisplayName("Product Rsa"), Expression("jProduct.[RSA]")]
        public Double? ProductRsa
        {
            get => fields.ProductRsa[this];
            set => fields.ProductRsa[this] = value;
        }

        [DisplayName("Product Image Attachment"), Expression("jProduct.[ImageAttachment]")]
        public String ProductImageAttachment
        {
            get => fields.ProductImageAttachment[this];
            set => fields.ProductImageAttachment[this] = value;
        }

        [DisplayName("Product File Attachment"), Expression("jProduct.[FileAttachment]")]
        public String ProductFileAttachment
        {
            get => fields.ProductFileAttachment[this];
            set => fields.ProductFileAttachment[this] = value;
        }

        [DisplayName("Product From"), Expression("jProduct.[From]")]
        public String ProductFrom
        {
            get => fields.ProductFrom[this];
            set => fields.ProductFrom[this] = value;
        }

        [DisplayName("Product To"), Expression("jProduct.[To]")]
        public String ProductTo
        {
            get => fields.ProductTo[this];
            set => fields.ProductTo[this] = value;
        }

        [DisplayName("Product Date"), Expression("jProduct.[Date]")]
        public DateTime? ProductDate
        {
            get => fields.ProductDate[this];
            set => fields.ProductDate[this] = value;
        }

        [DisplayName("Product Adults"), Expression("jProduct.[Adults]")]
        public String ProductAdults
        {
            get => fields.ProductAdults[this];
            set => fields.ProductAdults[this] = value;
        }

        [DisplayName("Product Childrens"), Expression("jProduct.[Childrens]")]
        public String ProductChildrens
        {
            get => fields.ProductChildrens[this];
            set => fields.ProductChildrens[this] = value;
        }

        [DisplayName("Product Destination"), Expression("jProduct.[Destination]")]
        public String ProductDestination
        {
            get => fields.ProductDestination[this];
            set => fields.ProductDestination[this] = value;
        }

        [DisplayName("Product Nights"), Expression("jProduct.[Nights]")]
        public String ProductNights
        {
            get => fields.ProductNights[this];
            set => fields.ProductNights[this] = value;
        }

        [DisplayName("Product Hotel Name"), Expression("jProduct.[HotelName]")]
        public String ProductHotelName
        {
            get => fields.ProductHotelName[this];
            set => fields.ProductHotelName[this] = value;
        }

        [DisplayName("Product Meal Plan"), Expression("jProduct.[MealPlan]")]
        public String ProductMealPlan
        {
            get => fields.ProductMealPlan[this];
            set => fields.ProductMealPlan[this] = value;
        }

        [DisplayName("Purchase From Contact Type"), Expression("jPurchaseFrom.[ContactType]")]
        public Int32? PurchaseFromContactType
        {
            get => fields.PurchaseFromContactType[this];
            set => fields.PurchaseFromContactType[this] = value;
        }

        [DisplayName("Purchase From Name"), Expression("jPurchaseFrom.[Name]")]
        public String PurchaseFromName
        {
            get => fields.PurchaseFromName[this];
            set => fields.PurchaseFromName[this] = value;
        }

        [DisplayName("Purchase From Phone"), Expression("jPurchaseFrom.[Phone]")]
        public String PurchaseFromPhone
        {
            get => fields.PurchaseFromPhone[this];
            set => fields.PurchaseFromPhone[this] = value;
        }

        [DisplayName("Purchase From Email"), Expression("jPurchaseFrom.[Email]")]
        public String PurchaseFromEmail
        {
            get => fields.PurchaseFromEmail[this];
            set => fields.PurchaseFromEmail[this] = value;
        }

        [DisplayName("Purchase From Address"), Expression("jPurchaseFrom.[Address]")]
        public String PurchaseFromAddress
        {
            get => fields.PurchaseFromAddress[this];
            set => fields.PurchaseFromAddress[this] = value;
        }

        [DisplayName("Purchase From City Id"), Expression("jPurchaseFrom.[CityId]")]
        public Int32? PurchaseFromCityId
        {
            get => fields.PurchaseFromCityId[this];
            set => fields.PurchaseFromCityId[this] = value;
        }

        [DisplayName("Purchase From State Id"), Expression("jPurchaseFrom.[StateId]")]
        public Int32? PurchaseFromStateId
        {
            get => fields.PurchaseFromStateId[this];
            set => fields.PurchaseFromStateId[this] = value;
        }

        [DisplayName("Purchase From Pin"), Expression("jPurchaseFrom.[Pin]")]
        public String PurchaseFromPin
        {
            get => fields.PurchaseFromPin[this];
            set => fields.PurchaseFromPin[this] = value;
        }

        [DisplayName("Purchase From Country"), Expression("jPurchaseFrom.[Country]")]
        public Int32? PurchaseFromCountry
        {
            get => fields.PurchaseFromCountry[this];
            set => fields.PurchaseFromCountry[this] = value;
        }

        [DisplayName("Purchase From Website"), Expression("jPurchaseFrom.[Website]")]
        public String PurchaseFromWebsite
        {
            get => fields.PurchaseFromWebsite[this];
            set => fields.PurchaseFromWebsite[this] = value;
        }

        [DisplayName("Purchase From Additional Info"), Expression("jPurchaseFrom.[AdditionalInfo]")]
        public String PurchaseFromAdditionalInfo
        {
            get => fields.PurchaseFromAdditionalInfo[this];
            set => fields.PurchaseFromAdditionalInfo[this] = value;
        }

        [DisplayName("Purchase From Residential Phone"), Expression("jPurchaseFrom.[ResidentialPhone]")]
        public String PurchaseFromResidentialPhone
        {
            get => fields.PurchaseFromResidentialPhone[this];
            set => fields.PurchaseFromResidentialPhone[this] = value;
        }

        [DisplayName("Purchase From Office Phone"), Expression("jPurchaseFrom.[OfficePhone]")]
        public String PurchaseFromOfficePhone
        {
            get => fields.PurchaseFromOfficePhone[this];
            set => fields.PurchaseFromOfficePhone[this] = value;
        }

        [DisplayName("Purchase From Gender"), Expression("jPurchaseFrom.[Gender]")]
        public Int32? PurchaseFromGender
        {
            get => fields.PurchaseFromGender[this];
            set => fields.PurchaseFromGender[this] = value;
        }

        [DisplayName("Purchase From Religion"), Expression("jPurchaseFrom.[Religion]")]
        public Int32? PurchaseFromReligion
        {
            get => fields.PurchaseFromReligion[this];
            set => fields.PurchaseFromReligion[this] = value;
        }

        [DisplayName("Purchase From Area Id"), Expression("jPurchaseFrom.[AreaId]")]
        public Int32? PurchaseFromAreaId
        {
            get => fields.PurchaseFromAreaId[this];
            set => fields.PurchaseFromAreaId[this] = value;
        }

        [DisplayName("Purchase From Marital Status"), Expression("jPurchaseFrom.[MaritalStatus]")]
        public Int32? PurchaseFromMaritalStatus
        {
            get => fields.PurchaseFromMaritalStatus[this];
            set => fields.PurchaseFromMaritalStatus[this] = value;
        }

        [DisplayName("Purchase From Marriage Anniversary"), Expression("jPurchaseFrom.[MarriageAnniversary]")]
        public DateTime? PurchaseFromMarriageAnniversary
        {
            get => fields.PurchaseFromMarriageAnniversary[this];
            set => fields.PurchaseFromMarriageAnniversary[this] = value;
        }

        [DisplayName("Purchase From Birthdate"), Expression("jPurchaseFrom.[Birthdate]")]
        public DateTime? PurchaseFromBirthdate
        {
            get => fields.PurchaseFromBirthdate[this];
            set => fields.PurchaseFromBirthdate[this] = value;
        }

        [DisplayName("Purchase From Date Of Incorporation"), Expression("jPurchaseFrom.[DateOfIncorporation]")]
        public DateTime? PurchaseFromDateOfIncorporation
        {
            get => fields.PurchaseFromDateOfIncorporation[this];
            set => fields.PurchaseFromDateOfIncorporation[this] = value;
        }

        [DisplayName("Purchase From Category Id"), Expression("jPurchaseFrom.[CategoryId]")]
        public Int32? PurchaseFromCategoryId
        {
            get => fields.PurchaseFromCategoryId[this];
            set => fields.PurchaseFromCategoryId[this] = value;
        }

        [DisplayName("Purchase From Grade Id"), Expression("jPurchaseFrom.[GradeId]")]
        public Int32? PurchaseFromGradeId
        {
            get => fields.PurchaseFromGradeId[this];
            set => fields.PurchaseFromGradeId[this] = value;
        }

        [DisplayName("Purchase From Type"), Expression("jPurchaseFrom.[Type]")]
        public Int32? PurchaseFromType
        {
            get => fields.PurchaseFromType[this];
            set => fields.PurchaseFromType[this] = value;
        }

        [DisplayName("Purchase From Owner Id"), Expression("jPurchaseFrom.[OwnerId]")]
        public Int32? PurchaseFromOwnerId
        {
            get => fields.PurchaseFromOwnerId[this];
            set => fields.PurchaseFromOwnerId[this] = value;
        }

        [DisplayName("Purchase From Assigned Id"), Expression("jPurchaseFrom.[AssignedId]")]
        public Int32? PurchaseFromAssignedId
        {
            get => fields.PurchaseFromAssignedId[this];
            set => fields.PurchaseFromAssignedId[this] = value;
        }

        [DisplayName("Purchase From Channel Category"), Expression("jPurchaseFrom.[ChannelCategory]")]
        public Int32? PurchaseFromChannelCategory
        {
            get => fields.PurchaseFromChannelCategory[this];
            set => fields.PurchaseFromChannelCategory[this] = value;
        }

        [DisplayName("Purchase From National Distributor"), Expression("jPurchaseFrom.[NationalDistributor]")]
        public Int32? PurchaseFromNationalDistributor
        {
            get => fields.PurchaseFromNationalDistributor[this];
            set => fields.PurchaseFromNationalDistributor[this] = value;
        }

        [DisplayName("Purchase From Stockist"), Expression("jPurchaseFrom.[Stockist]")]
        public Int32? PurchaseFromStockist
        {
            get => fields.PurchaseFromStockist[this];
            set => fields.PurchaseFromStockist[this] = value;
        }

        [DisplayName("Purchase From Distributor"), Expression("jPurchaseFrom.[Distributor]")]
        public Int32? PurchaseFromDistributor
        {
            get => fields.PurchaseFromDistributor[this];
            set => fields.PurchaseFromDistributor[this] = value;
        }

        [DisplayName("Purchase From Dealer"), Expression("jPurchaseFrom.[Dealer]")]
        public Int32? PurchaseFromDealer
        {
            get => fields.PurchaseFromDealer[this];
            set => fields.PurchaseFromDealer[this] = value;
        }

        [DisplayName("Purchase From Wholesaler"), Expression("jPurchaseFrom.[Wholesaler]")]
        public Int32? PurchaseFromWholesaler
        {
            get => fields.PurchaseFromWholesaler[this];
            set => fields.PurchaseFromWholesaler[this] = value;
        }

        [DisplayName("Purchase From Reseller"), Expression("jPurchaseFrom.[Reseller]")]
        public Int32? PurchaseFromReseller
        {
            get => fields.PurchaseFromReseller[this];
            set => fields.PurchaseFromReseller[this] = value;
        }

        [DisplayName("Purchase From Gstin"), Expression("jPurchaseFrom.[GSTIN]")]
        public String PurchaseFromGstin
        {
            get => fields.PurchaseFromGstin[this];
            set => fields.PurchaseFromGstin[this] = value;
        }

        [DisplayName("Purchase From Pan No"), Expression("jPurchaseFrom.[PANNo]")]
        public String PurchaseFromPanNo
        {
            get => fields.PurchaseFromPanNo[this];
            set => fields.PurchaseFromPanNo[this] = value;
        }

        [DisplayName("Purchase From Cc Emails"), Expression("jPurchaseFrom.[CCEmails]")]
        public String PurchaseFromCcEmails
        {
            get => fields.PurchaseFromCcEmails[this];
            set => fields.PurchaseFromCcEmails[this] = value;
        }

        [DisplayName("Purchase From Bcc Emails"), Expression("jPurchaseFrom.[BCCEmails]")]
        public String PurchaseFromBccEmails
        {
            get => fields.PurchaseFromBccEmails[this];
            set => fields.PurchaseFromBccEmails[this] = value;
        }

        [DisplayName("Purchase From Attachment"), Expression("jPurchaseFrom.[Attachment]")]
        public String PurchaseFromAttachment
        {
            get => fields.PurchaseFromAttachment[this];
            set => fields.PurchaseFromAttachment[this] = value;
        }

        [DisplayName("Purchase From E Com Gstin"), Expression("jPurchaseFrom.[EComGSTIN]")]
        public String PurchaseFromEComGstin
        {
            get => fields.PurchaseFromEComGstin[this];
            set => fields.PurchaseFromEComGstin[this] = value;
        }

        [DisplayName("Purchase From Creditors Opening"), Expression("jPurchaseFrom.[CreditorsOpening]")]
        public Double? PurchaseFromCreditorsOpening
        {
            get => fields.PurchaseFromCreditorsOpening[this];
            set => fields.PurchaseFromCreditorsOpening[this] = value;
        }

        [DisplayName("Purchase From Debtors Opening"), Expression("jPurchaseFrom.[DebtorsOpening]")]
        public Double? PurchaseFromDebtorsOpening
        {
            get => fields.PurchaseFromDebtorsOpening[this];
            set => fields.PurchaseFromDebtorsOpening[this] = value;
        }

        [DisplayName("Purchase From Bank Name"), Expression("jPurchaseFrom.[BankName]")]
        public String PurchaseFromBankName
        {
            get => fields.PurchaseFromBankName[this];
            set => fields.PurchaseFromBankName[this] = value;
        }

        [DisplayName("Purchase From Account Number"), Expression("jPurchaseFrom.[AccountNumber]")]
        public String PurchaseFromAccountNumber
        {
            get => fields.PurchaseFromAccountNumber[this];
            set => fields.PurchaseFromAccountNumber[this] = value;
        }

        [DisplayName("Purchase From Ifsc"), Expression("jPurchaseFrom.[IFSC]")]
        public String PurchaseFromIfsc
        {
            get => fields.PurchaseFromIfsc[this];
            set => fields.PurchaseFromIfsc[this] = value;
        }

        [DisplayName("Purchase From Bank Type"), Expression("jPurchaseFrom.[BankType]")]
        public String PurchaseFromBankType
        {
            get => fields.PurchaseFromBankType[this];
            set => fields.PurchaseFromBankType[this] = value;
        }

        [DisplayName("Purchase From Branch"), Expression("jPurchaseFrom.[Branch]")]
        public String PurchaseFromBranch
        {
            get => fields.PurchaseFromBranch[this];
            set => fields.PurchaseFromBranch[this] = value;
        }

        [DisplayName("Purchase From Accounts Email"), Expression("jPurchaseFrom.[AccountsEmail]")]
        public String PurchaseFromAccountsEmail
        {
            get => fields.PurchaseFromAccountsEmail[this];
            set => fields.PurchaseFromAccountsEmail[this] = value;
        }

        [DisplayName("Purchase From Purchase Email"), Expression("jPurchaseFrom.[PurchaseEmail]")]
        public String PurchaseFromPurchaseEmail
        {
            get => fields.PurchaseFromPurchaseEmail[this];
            set => fields.PurchaseFromPurchaseEmail[this] = value;
        }

        [DisplayName("Purchase From Service Email"), Expression("jPurchaseFrom.[ServiceEmail]")]
        public String PurchaseFromServiceEmail
        {
            get => fields.PurchaseFromServiceEmail[this];
            set => fields.PurchaseFromServiceEmail[this] = value;
        }

        [DisplayName("Purchase From Sales Email"), Expression("jPurchaseFrom.[SalesEmail]")]
        public String PurchaseFromSalesEmail
        {
            get => fields.PurchaseFromSalesEmail[this];
            set => fields.PurchaseFromSalesEmail[this] = value;
        }

        [DisplayName("Purchase From Credit Days"), Expression("jPurchaseFrom.[CreditDays]")]
        public Int32? PurchaseFromCreditDays
        {
            get => fields.PurchaseFromCreditDays[this];
            set => fields.PurchaseFromCreditDays[this] = value;
        }

        [DisplayName("Purchase From Customer Type"), Expression("jPurchaseFrom.[CustomerType]")]
        public Int32? PurchaseFromCustomerType
        {
            get => fields.PurchaseFromCustomerType[this];
            set => fields.PurchaseFromCustomerType[this] = value;
        }

        [DisplayName("Purchase From Trasportation Id"), Expression("jPurchaseFrom.[TrasportationId]")]
        public Int32? PurchaseFromTrasportationId
        {
            get => fields.PurchaseFromTrasportationId[this];
            set => fields.PurchaseFromTrasportationId[this] = value;
        }

        [DisplayName("Purchase From Tehsil Id"), Expression("jPurchaseFrom.[TehsilId]")]
        public Int32? PurchaseFromTehsilId
        {
            get => fields.PurchaseFromTehsilId[this];
            set => fields.PurchaseFromTehsilId[this] = value;
        }

        [DisplayName("Purchase From Village Id"), Expression("jPurchaseFrom.[VillageId]")]
        public Int32? PurchaseFromVillageId
        {
            get => fields.PurchaseFromVillageId[this];
            set => fields.PurchaseFromVillageId[this] = value;
        }

        [DisplayName("Purchase From Whatsapp"), Expression("jPurchaseFrom.[Whatsapp]")]
        public String PurchaseFromWhatsapp
        {
            get => fields.PurchaseFromWhatsapp[this];
            set => fields.PurchaseFromWhatsapp[this] = value;
        }

        [DisplayName("Purchase From Consent For Calling"), Expression("jPurchaseFrom.[ConsentForCalling]")]
        public Boolean? PurchaseFromConsentForCalling
        {
            get => fields.PurchaseFromConsentForCalling[this];
            set => fields.PurchaseFromConsentForCalling[this] = value;
        }

        [DisplayName("Purchase From Additional Info2"), Expression("jPurchaseFrom.[AdditionalInfo2]")]
        public String PurchaseFromAdditionalInfo2
        {
            get => fields.PurchaseFromAdditionalInfo2[this];
            set => fields.PurchaseFromAdditionalInfo2[this] = value;
        }

        [DisplayName("Purchase From Date Created"), Expression("jPurchaseFrom.[DateCreated]")]
        public DateTime? PurchaseFromDateCreated
        {
            get => fields.PurchaseFromDateCreated[this];
            set => fields.PurchaseFromDateCreated[this] = value;
        }

        [DisplayName("Purchase From Passport Number"), Expression("jPurchaseFrom.[PassportNumber]")]
        public String PurchaseFromPassportNumber
        {
            get => fields.PurchaseFromPassportNumber[this];
            set => fields.PurchaseFromPassportNumber[this] = value;
        }

        [DisplayName("Purchase From First Name"), Expression("jPurchaseFrom.[FirstName]")]
        public String PurchaseFromFirstName
        {
            get => fields.PurchaseFromFirstName[this];
            set => fields.PurchaseFromFirstName[this] = value;
        }

        [DisplayName("Purchase From Last Name"), Expression("jPurchaseFrom.[LastName]")]
        public String PurchaseFromLastName
        {
            get => fields.PurchaseFromLastName[this];
            set => fields.PurchaseFromLastName[this] = value;
        }

        [DisplayName("Purchase From Expiry Date"), Expression("jPurchaseFrom.[ExpiryDate]")]
        public DateTime? PurchaseFromExpiryDate
        {
            get => fields.PurchaseFromExpiryDate[this];
            set => fields.PurchaseFromExpiryDate[this] = value;
        }

        [DisplayName("Purchase From Aadhar No"), Expression("jPurchaseFrom.[AadharNo]")]
        public String PurchaseFromAadharNo
        {
            get => fields.PurchaseFromAadharNo[this];
            set => fields.PurchaseFromAadharNo[this] = value;
        }

        public QualityCheckRow()
            : base()
        {
        }

        public QualityCheckRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field QcNumber;
            public DateTimeField PurchaseDate;
            public StringField ProductName;
            public DateTimeField QcDate;
            public StringField InspectionCriteria;
            public Int32Field QtyInspected;
            public Int32Field QtyPassed;
            public Int32Field QtyRejected;
            public StringField DepositionAction;
            public StringField AdditionalInfo;
            public StringField Attachments;
            public Int32Field ProductId;
            public Int32Field PurchaseFromId;

            public StringField ProductName1;
            public StringField ProductCode;
            public Int32Field ProductDivisionId;
            public Int32Field ProductGroupId;
            public DoubleField ProductSellingPrice;
            public DoubleField ProductMrp;
            public StringField ProductDescription;
            public Int32Field ProductTaxId1;
            public Int32Field ProductTaxId2;
            public StringField ProductImage;
            public StringField ProductTechSpecs;
            public StringField ProductHsn;
            public DoubleField ProductChannelCustomerPrice;
            public DoubleField ProductResellerPrice;
            public DoubleField ProductWholesalerPrice;
            public DoubleField ProductDealerPrice;
            public DoubleField ProductDistributorPrice;
            public DoubleField ProductStockiestPrice;
            public DoubleField ProductNationalDistributorPrice;
            public DoubleField ProductMinimumStock;
            public DoubleField ProductMaximumStock;
            public BooleanField ProductRawMaterial;
            public DoubleField ProductPurchasePrice;
            public DoubleField ProductOpeningStock;
            public Int32Field ProductUnitId;
            public Int32Field ProductCompanyId;
            public Int32Field ProductProductTypeId;
            public Int32Field ProductModelSegmentId;
            public Int32Field ProductModelNameId;
            public Int32Field ProductModelCodeId;
            public Int32Field ProductModelVarientId;
            public Int32Field ProductModelColorId;
            public StringField ProductSerialNo;
            public DoubleField ProductExShowroomPrice;
            public DoubleField ProductInsuranceAmount;
            public DoubleField ProductRegistrationAmount;
            public DoubleField ProductRoadTax;
            public DoubleField ProductOnRoadPrice;
            public DoubleField ProductOtherTaxes;
            public DoubleField ProductExtendedWarranty;
            public DoubleField ProductRsa;
            public StringField ProductImageAttachment;
            public StringField ProductFileAttachment;
            public StringField ProductFrom;
            public StringField ProductTo;
            public DateTimeField ProductDate;
            public StringField ProductAdults;
            public StringField ProductChildrens;
            public StringField ProductDestination;
            public StringField ProductNights;
            public StringField ProductHotelName;
            public StringField ProductMealPlan;

            public Int32Field PurchaseFromContactType;
            public StringField PurchaseFromName;
            public StringField PurchaseFromPhone;
            public StringField PurchaseFromEmail;
            public StringField PurchaseFromAddress;
            public Int32Field PurchaseFromCityId;
            public Int32Field PurchaseFromStateId;
            public StringField PurchaseFromPin;
            public Int32Field PurchaseFromCountry;
            public StringField PurchaseFromWebsite;
            public StringField PurchaseFromAdditionalInfo;
            public StringField PurchaseFromResidentialPhone;
            public StringField PurchaseFromOfficePhone;
            public Int32Field PurchaseFromGender;
            public Int32Field PurchaseFromReligion;
            public Int32Field PurchaseFromAreaId;
            public Int32Field PurchaseFromMaritalStatus;
            public DateTimeField PurchaseFromMarriageAnniversary;
            public DateTimeField PurchaseFromBirthdate;
            public DateTimeField PurchaseFromDateOfIncorporation;
            public Int32Field PurchaseFromCategoryId;
            public Int32Field PurchaseFromGradeId;
            public Int32Field PurchaseFromType;
            public Int32Field PurchaseFromOwnerId;
            public Int32Field PurchaseFromAssignedId;
            public Int32Field PurchaseFromChannelCategory;
            public Int32Field PurchaseFromNationalDistributor;
            public Int32Field PurchaseFromStockist;
            public Int32Field PurchaseFromDistributor;
            public Int32Field PurchaseFromDealer;
            public Int32Field PurchaseFromWholesaler;
            public Int32Field PurchaseFromReseller;
            public StringField PurchaseFromGstin;
            public StringField PurchaseFromPanNo;
            public StringField PurchaseFromCcEmails;
            public StringField PurchaseFromBccEmails;
            public StringField PurchaseFromAttachment;
            public StringField PurchaseFromEComGstin;
            public DoubleField PurchaseFromCreditorsOpening;
            public DoubleField PurchaseFromDebtorsOpening;
            public StringField PurchaseFromBankName;
            public StringField PurchaseFromAccountNumber;
            public StringField PurchaseFromIfsc;
            public StringField PurchaseFromBankType;
            public StringField PurchaseFromBranch;
            public StringField PurchaseFromAccountsEmail;
            public StringField PurchaseFromPurchaseEmail;
            public StringField PurchaseFromServiceEmail;
            public StringField PurchaseFromSalesEmail;
            public Int32Field PurchaseFromCreditDays;
            public Int32Field PurchaseFromCustomerType;
            public Int32Field PurchaseFromTrasportationId;
            public Int32Field PurchaseFromTehsilId;
            public Int32Field PurchaseFromVillageId;
            public StringField PurchaseFromWhatsapp;
            public BooleanField PurchaseFromConsentForCalling;
            public StringField PurchaseFromAdditionalInfo2;
            public DateTimeField PurchaseFromDateCreated;
            public StringField PurchaseFromPassportNumber;
            public StringField PurchaseFromFirstName;
            public StringField PurchaseFromLastName;
            public DateTimeField PurchaseFromExpiryDate;
            public StringField PurchaseFromAadharNo;
        }
    }
}
