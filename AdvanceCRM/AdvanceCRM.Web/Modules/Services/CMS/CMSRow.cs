
namespace AdvanceCRM.Services
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Products;
    using AdvanceCRM.Employee;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Services"), TableName("[dbo].[CMS]")]
    [DisplayName("CMS"), InstanceName("CMS")]
    [ReadPermission("CMS:Read")]
    [InsertPermission("CMS:Insert")]
    [UpdatePermission("CMS:Update")]
    [DeletePermission("CMS:Delete")]
    [LookupScript("Services.CMS", Permission = "?")]
    public sealed class CMSRow : Row<CMSRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }
        [DisplayName("Project"), ForeignKey("[dbo].[Project]", "Id"), LeftJoin("jProject"), TextualField("Project"), LookupInclude]
        [LookupEditor(typeof(ProjectRow), InplaceAdd = true)]
        public Int32? ProjectId
        {
            get { return Fields.ProjectId[this]; }
            set { Fields.ProjectId[this] = value; }
        }
        [DisplayName("Contacts"), NotNull, ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jContacts"), TextualField("ContactsName")]
        [LookupEditor(typeof(ContactsRow), InplaceAdd = true)]
        public Int32? ContactsId
        {
            get { return Fields.ContactsId[this]; }
            set { Fields.ContactsId[this] = value; }
        }

        [DisplayName("Date"), NotNull, DefaultValue("now"), DateTimeEditor]
        public DateTime? Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
        }

        [DisplayName("Products"), ForeignKey("[dbo].[Products]", "Id"), LeftJoin("jProducts"), TextualField("ProductsName")]
        [LookupEditor(typeof(ProductsRow), InplaceAdd = true)]
        public Int32? ProductsId
        {
            get { return Fields.ProductsId[this]; }
            set { Fields.ProductsId[this] = value; }
        }

        [DisplayName("Serial No"), Size(70), QuickSearch,NameProperty]
        public String SerialNo
        {
            get { return Fields.SerialNo[this]; }
            set { Fields.SerialNo[this] = value; }
        }

        [DisplayName("Complaint"), NotNull, ForeignKey("[dbo].[ComplaintType]", "Id"), LeftJoin("jComplaint"), TextualField("ComplaintComplaintType")]
        [LookupEditor(typeof(ComplaintTypeRow), InplaceAdd = true)]
        public Int32? ComplaintId
        {
            get { return Fields.ComplaintId[this]; }
            set { Fields.ComplaintId[this] = value; }
        }

        [DisplayName("Category"), NotNull]
        public Masters.CMSCategoryMaster? Category
        {
            get { return (Masters.CMSCategoryMaster?)Fields.Category[this]; }
            set { Fields.Category[this] = (Int32?)value; }
        }

        [DisplayName("Amount")]
        public Double? Amount
        {
            get { return Fields.Amount[this]; }
            set { Fields.Amount[this] = value; }
        }

        [DisplayName("Total"), Expression("t0.[Amount]")]
        public Double? Total
        {
            get { return Fields.Total[this]; }
            set { Fields.Total[this] = value; }
        }

        [DisplayName("Expected Completion"), NotNull]
        public DateTime? ExpectedCompletion
        {
            get { return Fields.ExpectedCompletion[this]; }
            set { Fields.ExpectedCompletion[this] = value; }
        }

        [DisplayName("Created By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssignedBy"), TextualField("AssignedByUsername")]
        [Administration.UserEditor]
        public Int32? AssignedBy
        {
            get { return Fields.AssignedBy[this]; }
            set { Fields.AssignedBy[this] = value; }
        }

        [DisplayName("Assigned To"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssignedTo"), TextualField("AssignedToUsername")]
        [Administration.UserEditor]
        public Int32? AssignedTo
        {
            get { return Fields.AssignedTo[this]; }
            set { Fields.AssignedTo[this] = value; }
        }

        [DisplayName("Instructions"), Size(450), TextAreaEditor(Rows = 4)]
        public String Instructions
        {
            get { return Fields.Instructions[this]; }
            set { Fields.Instructions[this] = value; }
        }

        [DisplayName("Branch"), ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jBranch"), TextualField("Branch")]
        [LookupEditor("Administration.BranchLookup")]
        public Int32? BranchId
        {
            get { return Fields.BranchId[this]; }
            set { Fields.BranchId[this] = value; }
        }

        [DisplayName("Status"), NotNull]
        public Masters.CMSStatusMaster? Status
        {
            get { return (Masters.CMSStatusMaster?)Fields.Status[this]; }
            set { Fields.Status[this] = (Int32?)value; }
        }

        [DisplayName("Completion Date")]
        public DateTime? CompletionDate
        {
            get { return Fields.CompletionDate[this]; }
            set { Fields.CompletionDate[this] = value; }
        }

        [DisplayName("Feedback"), Size(450), TextAreaEditor(Rows = 4)]
        public String Feedback
        {
            get { return Fields.Feedback[this]; }
            set { Fields.Feedback[this] = value; }
        }

        [DisplayName("Additional Info"), Size(2000), TextAreaEditor(Rows = 4)]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }

        [DisplayName("Attachments"), Size(500)]
        [MultipleImageUploadEditor(FilenameFormat = "Complaints/~", CopyToHistory = true, AllowNonImage = true)]
        public String Image
        {
            get { return Fields.Image[this]; }
            set { Fields.Image[this] = value; }
        }

        [DisplayName("Phone"), Size(50)]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Address"), Size(100)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("Dealer/Partner"), ForeignKey("[dbo].[Dealer]", "Id"), LeftJoin("jDealer"), TextualField("DealerDealerName"), LookupInclude]
        [LookupEditor(typeof(DealerRow), InplaceAdd = true)]
        public Int32? DealerId
        {
            get { return Fields.DealerId[this]; }
            set { Fields.DealerId[this] = value; }
        }

        [DisplayName("Stage"), ForeignKey("[dbo].[Stage]", "Id"), LeftJoin("jStage"), TextualField("Stage")]
        [LookupEditor(typeof(StageRow), InplaceAdd = true, FilterField = "Type", FilterValue = Masters.StageTypeMaster.Service)]
        public Int32? StageId
        {
            get { return Fields.StageId[this]; }
            set { Fields.StageId[this] = value; }
        }

        [DisplayName("Priority"), NotNull, DefaultValue("1")]
        public Masters.PriorityMaster? Priority
        {
            get { return (Masters.PriorityMaster?)Fields.Priority[this]; }
            set { Fields.Priority[this] = (Int32?)value; }
        }

        [DisplayName("Pmr Closed"), Column("PMRClosed")]
        public Boolean? PMRClosed
        {
            get { return Fields.PMRClosed[this]; }
            set { Fields.PMRClosed[this] = value; }
        }

        [DisplayName("Investigation By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jInvestigationBy"), TextualField("InvestigationByUsername")]
        [Administration.UserEditor]
        public Int32? InvestigationBy
        {
            get { return Fields.InvestigationBy[this]; }
            set { Fields.InvestigationBy[this] = value; }
        }

        [DisplayName("Action By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jActionBy"), TextualField("ActionByUsername")]
        [Administration.UserEditor]
        public Int32? ActionBy
        {
            get { return Fields.ActionBy[this]; }
            set { Fields.ActionBy[this] = value; }
        }

        [DisplayName("Supervised By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jSupervisedBy"), TextualField("SupervisedByUsername")]
        [Administration.UserEditor]
        public Int32? SupervisedBy
        {
            get { return Fields.SupervisedBy[this]; }
            set { Fields.SupervisedBy[this] = value; }
        }

        [DisplayName("Investigation Observation"), Size(250), TextAreaEditor(Rows = 4)]
        public String Observation
        {
            get { return Fields.Observation[this]; }
            set { Fields.Observation[this] = value; }
        }

        [DisplayName("Corrective Action"), Size(250), TextAreaEditor(Rows = 4)]
        public String Action
        {
            get { return Fields.Action[this]; }
            set { Fields.Action[this] = value; }
        }

        [DisplayName("Supervisor Comments"), Size(250), TextAreaEditor(Rows = 4)]
        public String Comments
        {
            get { return Fields.Comments[this]; }
            set { Fields.Comments[this] = value; }
        }

        [DisplayName("No.")]
        public Int32? CMSNo
        {
            get { return Fields.CMSNo[this]; }
            set { Fields.CMSNo[this] = value; }
        }

        [DisplayName("Contacts Contact Type"), Expression("jContacts.[ContactType]")]
        public Int32? ContactsContactType
        {
            get { return Fields.ContactsContactType[this]; }
            set { Fields.ContactsContactType[this] = value; }
        }

        [DisplayName("Contact"), Expression("jContacts.[Name]"), QuickSearch]
        public String ContactsName
        {
            get { return Fields.ContactsName[this]; }
            set { Fields.ContactsName[this] = value; }
        }

        [DisplayName("Phone"), Expression("jContacts.[Phone]"), QuickSearch]
        public String ContactsPhone
        {
            get { return Fields.ContactsPhone[this]; }
            set { Fields.ContactsPhone[this] = value; }
        }

        [DisplayName("Contacts Email"), Expression("jContacts.[Email]")]
        public String ContactsEmail
        {
            get { return Fields.ContactsEmail[this]; }
            set { Fields.ContactsEmail[this] = value; }
        }

        [DisplayName("Address"), Expression("jContacts.[Address]"), TextAreaEditor(Rows = 4)]
        public String ContactsAddress
        {
            get { return Fields.ContactsAddress[this]; }
            set { Fields.ContactsAddress[this] = value; }
        }

        [DisplayName("Contacts City Id"), Expression("jContacts.[CityId]")]
        public Int32? ContactsCityId
        {
            get { return Fields.ContactsCityId[this]; }
            set { Fields.ContactsCityId[this] = value; }
        }

        [DisplayName("Contacts State Id"), Expression("jContacts.[StateId]")]
        public Int32? ContactsStateId
        {
            get { return Fields.ContactsStateId[this]; }
            set { Fields.ContactsStateId[this] = value; }
        }

        [DisplayName("Contacts Pin"), Expression("jContacts.[Pin]")]
        public String ContactsPin
        {
            get { return Fields.ContactsPin[this]; }
            set { Fields.ContactsPin[this] = value; }
        }

        [DisplayName("Contacts Country"), Expression("jContacts.[Country]")]
        public Int32? ContactsCountry
        {
            get { return Fields.ContactsCountry[this]; }
            set { Fields.ContactsCountry[this] = value; }
        }

        [DisplayName("Contacts Website"), Expression("jContacts.[Website]")]
        public String ContactsWebsite
        {
            get { return Fields.ContactsWebsite[this]; }
            set { Fields.ContactsWebsite[this] = value; }
        }

        [DisplayName("Contacts Additional Info"), Expression("jContacts.[AdditionalInfo]")]
        public String ContactsAdditionalInfo
        {
            get { return Fields.ContactsAdditionalInfo[this]; }
            set { Fields.ContactsAdditionalInfo[this] = value; }
        }

        [DisplayName("Contacts Residential Phone"), Expression("jContacts.[ResidentialPhone]")]
        public String ContactsResidentialPhone
        {
            get { return Fields.ContactsResidentialPhone[this]; }
            set { Fields.ContactsResidentialPhone[this] = value; }
        }

        [DisplayName("Contacts Office Phone"), Expression("jContacts.[OfficePhone]")]
        public String ContactsOfficePhone
        {
            get { return Fields.ContactsOfficePhone[this]; }
            set { Fields.ContactsOfficePhone[this] = value; }
        }

        [DisplayName("Contacts Gender"), Expression("jContacts.[Gender]")]
        public Int32? ContactsGender
        {
            get { return Fields.ContactsGender[this]; }
            set { Fields.ContactsGender[this] = value; }
        }

        [DisplayName("Contacts Religion"), Expression("jContacts.[Religion]")]
        public Int32? ContactsReligion
        {
            get { return Fields.ContactsReligion[this]; }
            set { Fields.ContactsReligion[this] = value; }
        }

        [DisplayName("Contacts Area Id"), Expression("jContacts.[AreaId]")]
        public Int32? ContactsAreaId
        {
            get { return Fields.ContactsAreaId[this]; }
            set { Fields.ContactsAreaId[this] = value; }
        }

        [DisplayName("Contacts Marital Status"), Expression("jContacts.[MaritalStatus]")]
        public Int32? ContactsMaritalStatus
        {
            get { return Fields.ContactsMaritalStatus[this]; }
            set { Fields.ContactsMaritalStatus[this] = value; }
        }

        [DisplayName("Purchase Date")]
        public DateTime? PurchaseDate
        {
            get { return Fields.PurchaseDate[this]; }
            set { Fields.PurchaseDate[this] = value; }
        }

        [DisplayName("Invoice No"), Size(20)]
        public String InvoiceNo
        {
            get { return Fields.InvoiceNo[this]; }
            set { Fields.InvoiceNo[this] = value; }
        }


        [DisplayName("Employee/Sales_PerSon"), ForeignKey("[dbo].[Employee]", "Id"), LeftJoin("jEmployee"), TextualField("EmployeeName")]
        [LookupEditor(typeof(EmployeeRow), InplaceAdd = true)]
        public Int32? EmployeeId
        {
            get { return Fields.EmployeeId[this]; }
            set { Fields.EmployeeId[this] = value; }
        }

        [DisplayName("Contacts Marriage Anniversary"), Expression("jContacts.[MarriageAnniversary]")]
        public DateTime? ContactsMarriageAnniversary
        {
            get { return Fields.ContactsMarriageAnniversary[this]; }
            set { Fields.ContactsMarriageAnniversary[this] = value; }
        }

        [DisplayName("Contacts Birthdate"), Expression("jContacts.[Birthdate]")]
        public DateTime? ContactsBirthdate
        {
            get { return Fields.ContactsBirthdate[this]; }
            set { Fields.ContactsBirthdate[this] = value; }
        }

        [DisplayName("Contacts Date Of Incorporation"), Expression("jContacts.[DateOfIncorporation]")]
        public DateTime? ContactsDateOfIncorporation
        {
            get { return Fields.ContactsDateOfIncorporation[this]; }
            set { Fields.ContactsDateOfIncorporation[this] = value; }
        }

        [DisplayName("Contacts Category Id"), Expression("jContacts.[CategoryId]")]
        public Int32? ContactsCategoryId
        {
            get { return Fields.ContactsCategoryId[this]; }
            set { Fields.ContactsCategoryId[this] = value; }
        }

        [DisplayName("Contacts Grade Id"), Expression("jContacts.[GradeId]")]
        public Int32? ContactsGradeId
        {
            get { return Fields.ContactsGradeId[this]; }
            set { Fields.ContactsGradeId[this] = value; }
        }

        [DisplayName("Contacts Type"), Expression("jContacts.[Type]")]
        public Int32? ContactsType
        {
            get { return Fields.ContactsType[this]; }
            set { Fields.ContactsType[this] = value; }
        }

        [DisplayName("Contacts Owner Id"), Expression("jContacts.[OwnerId]")]
        public Int32? ContactsOwnerId
        {
            get { return Fields.ContactsOwnerId[this]; }
            set { Fields.ContactsOwnerId[this] = value; }
        }

        [DisplayName("Contacts Assigned Id"), Expression("jContacts.[AssignedId]")]
        public Int32? ContactsAssignedId
        {
            get { return Fields.ContactsAssignedId[this]; }
            set { Fields.ContactsAssignedId[this] = value; }
        }

        [DisplayName("Contacts Channel Category"), Expression("jContacts.[ChannelCategory]")]
        public Int32? ContactsChannelCategory
        {
            get { return Fields.ContactsChannelCategory[this]; }
            set { Fields.ContactsChannelCategory[this] = value; }
        }

        [DisplayName("Contacts National Distributor"), Expression("jContacts.[NationalDistributor]")]
        public Int32? ContactsNationalDistributor
        {
            get { return Fields.ContactsNationalDistributor[this]; }
            set { Fields.ContactsNationalDistributor[this] = value; }
        }

        [DisplayName("Contacts Stockist"), Expression("jContacts.[Stockist]")]
        public Int32? ContactsStockist
        {
            get { return Fields.ContactsStockist[this]; }
            set { Fields.ContactsStockist[this] = value; }
        }

        [DisplayName("Contacts Distributor"), Expression("jContacts.[Distributor]")]
        public Int32? ContactsDistributor
        {
            get { return Fields.ContactsDistributor[this]; }
            set { Fields.ContactsDistributor[this] = value; }
        }

        [DisplayName("Contacts Dealer"), Expression("jContacts.[Dealer]")]
        public Int32? ContactsDealer
        {
            get { return Fields.ContactsDealer[this]; }
            set { Fields.ContactsDealer[this] = value; }
        }

        [DisplayName("Contacts Wholesaler"), Expression("jContacts.[Wholesaler]")]
        public Int32? ContactsWholesaler
        {
            get { return Fields.ContactsWholesaler[this]; }
            set { Fields.ContactsWholesaler[this] = value; }
        }

        [DisplayName("Contacts Reseller"), Expression("jContacts.[Reseller]")]
        public Int32? ContactsReseller
        {
            get { return Fields.ContactsReseller[this]; }
            set { Fields.ContactsReseller[this] = value; }
        }

        [DisplayName("Contacts GSTIN"), Expression("jContacts.[GSTIN]")]
        public String ContactsGstin
        {
            get { return Fields.ContactsGstin[this]; }
            set { Fields.ContactsGstin[this] = value; }
        }

        [DisplayName("Contacts Pan No"), Expression("jContacts.[PANNo]")]
        public String ContactsPanNo
        {
            get { return Fields.ContactsPanNo[this]; }
            set { Fields.ContactsPanNo[this] = value; }
        }

        [DisplayName("Contacts Cc Emails"), Expression("jContacts.[CCEmails]")]
        public String ContactsCCEmails
        {
            get { return Fields.ContactsCCEmails[this]; }
            set { Fields.ContactsCCEmails[this] = value; }
        }

        [DisplayName("Contacts Bcc Emails"), Expression("jContacts.[BCCEmails]")]
        public String ContactsBCCEmails
        {
            get { return Fields.ContactsBCCEmails[this]; }
            set { Fields.ContactsBCCEmails[this] = value; }
        }

        [DisplayName("Contacts Attachment"), Expression("jContacts.[Attachment]")]
        public String ContactsAttachment
        {
            get { return Fields.ContactsAttachment[this]; }
            set { Fields.ContactsAttachment[this] = value; }
        }

        [DisplayName("Contacts E Com GSTIN"), Expression("jContacts.[EComGSTIN]")]
        public String ContactsEComGstin
        {
            get { return Fields.ContactsEComGstin[this]; }
            set { Fields.ContactsEComGstin[this] = value; }
        }

        [DisplayName("Contacts Creditors Opening"), Expression("jContacts.[CreditorsOpening]")]
        public Double? ContactsCreditorsOpening
        {
            get { return Fields.ContactsCreditorsOpening[this]; }
            set { Fields.ContactsCreditorsOpening[this] = value; }
        }

        [DisplayName("Contacts Debtors Opening"), Expression("jContacts.[DebtorsOpening]")]
        public Double? ContactsDebtorsOpening
        {
            get { return Fields.ContactsDebtorsOpening[this]; }
            set { Fields.ContactsDebtorsOpening[this] = value; }
        }

        [DisplayName("Contacts Bank Name"), Expression("jContacts.[BankName]")]
        public String ContactsBankName
        {
            get { return Fields.ContactsBankName[this]; }
            set { Fields.ContactsBankName[this] = value; }
        }

        [DisplayName("Contacts Account Number"), Expression("jContacts.[AccountNumber]")]
        public String ContactsAccountNumber
        {
            get { return Fields.ContactsAccountNumber[this]; }
            set { Fields.ContactsAccountNumber[this] = value; }
        }

        [DisplayName("Contacts Ifsc"), Expression("jContacts.[IFSC]")]
        public String ContactsIfsc
        {
            get { return Fields.ContactsIfsc[this]; }
            set { Fields.ContactsIfsc[this] = value; }
        }

        [DisplayName("Contacts Bank Type"), Expression("jContacts.[BankType]")]
        public String ContactsBankType
        {
            get { return Fields.ContactsBankType[this]; }
            set { Fields.ContactsBankType[this] = value; }
        }

        [DisplayName("Contacts Branch"), Expression("jContacts.[Branch]")]
        public String ContactsBranch
        {
            get { return Fields.ContactsBranch[this]; }
            set { Fields.ContactsBranch[this] = value; }
        }

        [DisplayName("Contacts Accounts Email"), Expression("jContacts.[AccountsEmail]")]
        public String ContactsAccountsEmail
        {
            get { return Fields.ContactsAccountsEmail[this]; }
            set { Fields.ContactsAccountsEmail[this] = value; }
        }

        [DisplayName("Contacts Purchase Email"), Expression("jContacts.[PurchaseEmail]")]
        public String ContactsPurchaseEmail
        {
            get { return Fields.ContactsPurchaseEmail[this]; }
            set { Fields.ContactsPurchaseEmail[this] = value; }
        }

        [DisplayName("Contacts Service Email"), Expression("jContacts.[ServiceEmail]")]
        public String ContactsServiceEmail
        {
            get { return Fields.ContactsServiceEmail[this]; }
            set { Fields.ContactsServiceEmail[this] = value; }
        }

        [DisplayName("Contacts Sales Email"), Expression("jContacts.[SalesEmail]")]
        public String ContactsSalesEmail
        {
            get { return Fields.ContactsSalesEmail[this]; }
            set { Fields.ContactsSalesEmail[this] = value; }
        }

        [DisplayName("Contacts Credit Days"), Expression("jContacts.[CreditDays]")]
        public Int32? ContactsCreditDays
        {
            get { return Fields.ContactsCreditDays[this]; }
            set { Fields.ContactsCreditDays[this] = value; }
        }

        [DisplayName("Contacts Customer Type"), Expression("jContacts.[CustomerType]")]
        public Int32? ContactsCustomerType
        {
            get { return Fields.ContactsCustomerType[this]; }
            set { Fields.ContactsCustomerType[this] = value; }
        }

        [DisplayName("Contacts Trasportation Id"), Expression("jContacts.[TrasportationId]")]
        public Int32? ContactsTrasportationId
        {
            get { return Fields.ContactsTrasportationId[this]; }
            set { Fields.ContactsTrasportationId[this] = value; }
        }

        [DisplayName("Contacts Tehsil Id"), Expression("jContacts.[TehsilId]")]
        public Int32? ContactsTehsilId
        {
            get { return Fields.ContactsTehsilId[this]; }
            set { Fields.ContactsTehsilId[this] = value; }
        }

        [DisplayName("Contacts Village Id"), Expression("jContacts.[VillageId]")]
        public Int32? ContactsVillageId
        {
            get { return Fields.ContactsVillageId[this]; }
            set { Fields.ContactsVillageId[this] = value; }
        }

        [DisplayName("Contacts Whatsapp"), Expression("jContacts.[Whatsapp]")]
        public String ContactsWhatsapp
        {
            get { return Fields.ContactsWhatsapp[this]; }
            set { Fields.ContactsWhatsapp[this] = value; }
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

        [DisplayName("Complaint Type"), Expression("jComplaint.[ComplaintType]")]
        public String ComplaintComplaintType
        {
            get { return Fields.ComplaintComplaintType[this]; }
            set { Fields.ComplaintComplaintType[this] = value; }
        }

        [DisplayName("Created By"), Expression("jAssignedBy.[Username]")]
        public String AssignedByUsername
        {
            get { return Fields.AssignedByUsername[this]; }
            set { Fields.AssignedByUsername[this] = value; }
        }

        [DisplayName("Created By Display Name"), Expression("jAssignedBy.[DisplayName]")]
        public String AssignedByDisplayName
        {
            get { return Fields.AssignedByDisplayName[this]; }
            set { Fields.AssignedByDisplayName[this] = value; }
        }

        [DisplayName("Created By Email"), Expression("jAssignedBy.[Email]")]
        public String AssignedByEmail
        {
            get { return Fields.AssignedByEmail[this]; }
            set { Fields.AssignedByEmail[this] = value; }
        }

        [DisplayName("Created By Source"), Expression("jAssignedBy.[Source]")]
        public String AssignedBySource
        {
            get { return Fields.AssignedBySource[this]; }
            set { Fields.AssignedBySource[this] = value; }
        }

        [DisplayName("Created By Password Hash"), Expression("jAssignedBy.[PasswordHash]")]
        public String AssignedByPasswordHash
        {
            get { return Fields.AssignedByPasswordHash[this]; }
            set { Fields.AssignedByPasswordHash[this] = value; }
        }

        [DisplayName("Created By Password Salt"), Expression("jAssignedBy.[PasswordSalt]")]
        public String AssignedByPasswordSalt
        {
            get { return Fields.AssignedByPasswordSalt[this]; }
            set { Fields.AssignedByPasswordSalt[this] = value; }
        }

        [DisplayName("Created By Last Directory Update"), Expression("jAssignedBy.[LastDirectoryUpdate]")]
        public DateTime? AssignedByLastDirectoryUpdate
        {
            get { return Fields.AssignedByLastDirectoryUpdate[this]; }
            set { Fields.AssignedByLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Created By User Image"), Expression("jAssignedBy.[UserImage]")]
        public String AssignedByUserImage
        {
            get { return Fields.AssignedByUserImage[this]; }
            set { Fields.AssignedByUserImage[this] = value; }
        }

        [DisplayName("Created By Insert Date"), Expression("jAssignedBy.[InsertDate]")]
        public DateTime? AssignedByInsertDate
        {
            get { return Fields.AssignedByInsertDate[this]; }
            set { Fields.AssignedByInsertDate[this] = value; }
        }

        [DisplayName("Created By Insert User Id"), Expression("jAssignedBy.[InsertUserId]")]
        public Int32? AssignedByInsertUserId
        {
            get { return Fields.AssignedByInsertUserId[this]; }
            set { Fields.AssignedByInsertUserId[this] = value; }
        }

        [DisplayName("Created By Update Date"), Expression("jAssignedBy.[UpdateDate]")]
        public DateTime? AssignedByUpdateDate
        {
            get { return Fields.AssignedByUpdateDate[this]; }
            set { Fields.AssignedByUpdateDate[this] = value; }
        }

        [DisplayName("Created By Update User Id"), Expression("jAssignedBy.[UpdateUserId]")]
        public Int32? AssignedByUpdateUserId
        {
            get { return Fields.AssignedByUpdateUserId[this]; }
            set { Fields.AssignedByUpdateUserId[this] = value; }
        }

        [DisplayName("Created By Is Active"), Expression("jAssignedBy.[IsActive]")]
        public Int16? AssignedByIsActive
        {
            get { return Fields.AssignedByIsActive[this]; }
            set { Fields.AssignedByIsActive[this] = value; }
        }

        [DisplayName("Created By Upper Level"), Expression("jAssignedBy.[UpperLevel]")]
        public Int32? AssignedByUpperLevel
        {
            get { return Fields.AssignedByUpperLevel[this]; }
            set { Fields.AssignedByUpperLevel[this] = value; }
        }

        [DisplayName("Created By Upper Level2"), Expression("jAssignedBy.[UpperLevel2]")]
        public Int32? AssignedByUpperLevel2
        {
            get { return Fields.AssignedByUpperLevel2[this]; }
            set { Fields.AssignedByUpperLevel2[this] = value; }
        }

        [DisplayName("Created By Upper Level3"), Expression("jAssignedBy.[UpperLevel3]")]
        public Int32? AssignedByUpperLevel3
        {
            get { return Fields.AssignedByUpperLevel3[this]; }
            set { Fields.AssignedByUpperLevel3[this] = value; }
        }

        [DisplayName("Created By Upper Level4"), Expression("jAssignedBy.[UpperLevel4]")]
        public Int32? AssignedByUpperLevel4
        {
            get { return Fields.AssignedByUpperLevel4[this]; }
            set { Fields.AssignedByUpperLevel4[this] = value; }
        }

        [DisplayName("Created By Upper Level5"), Expression("jAssignedBy.[UpperLevel5]")]
        public Int32? AssignedByUpperLevel5
        {
            get { return Fields.AssignedByUpperLevel5[this]; }
            set { Fields.AssignedByUpperLevel5[this] = value; }
        }

        [DisplayName("Created By Host"), Expression("jAssignedBy.[Host]")]
        public String AssignedByHost
        {
            get { return Fields.AssignedByHost[this]; }
            set { Fields.AssignedByHost[this] = value; }
        }

        [DisplayName("Created By Port"), Expression("jAssignedBy.[Port]")]
        public Int32? AssignedByPort
        {
            get { return Fields.AssignedByPort[this]; }
            set { Fields.AssignedByPort[this] = value; }
        }

        [DisplayName("Created By SSL"), Expression("jAssignedBy.[SSL]")]
        public Boolean? AssignedBySsl
        {
            get { return Fields.AssignedBySsl[this]; }
            set { Fields.AssignedBySsl[this] = value; }
        }

        [DisplayName("Created By Email Id"), Expression("jAssignedBy.[EmailId]")]
        public String AssignedByEmailId
        {
            get { return Fields.AssignedByEmailId[this]; }
            set { Fields.AssignedByEmailId[this] = value; }
        }

        [DisplayName("Created By Email Password"), Expression("jAssignedBy.[EmailPassword]")]
        public String AssignedByEmailPassword
        {
            get { return Fields.AssignedByEmailPassword[this]; }
            set { Fields.AssignedByEmailPassword[this] = value; }
        }

        [DisplayName("Created By Phone"), Expression("jAssignedBy.[Phone]")]
        public String AssignedByPhone
        {
            get { return Fields.AssignedByPhone[this]; }
            set { Fields.AssignedByPhone[this] = value; }
        }

        [DisplayName("Created By Mcsmtp Server"), Expression("jAssignedBy.[MCSMTPServer]")]
        public String AssignedByMcsmtpServer
        {
            get { return Fields.AssignedByMcsmtpServer[this]; }
            set { Fields.AssignedByMcsmtpServer[this] = value; }
        }

        [DisplayName("Created By Mcsmtp Port"), Expression("jAssignedBy.[MCSMTPPort]")]
        public Int32? AssignedByMcsmtpPort
        {
            get { return Fields.AssignedByMcsmtpPort[this]; }
            set { Fields.AssignedByMcsmtpPort[this] = value; }
        }

        [DisplayName("Created By Mcimap Server"), Expression("jAssignedBy.[MCIMAPServer]")]
        public String AssignedByMcimapServer
        {
            get { return Fields.AssignedByMcimapServer[this]; }
            set { Fields.AssignedByMcimapServer[this] = value; }
        }

        [DisplayName("Created By Mcimap Port"), Expression("jAssignedBy.[MCIMAPPort]")]
        public Int32? AssignedByMcimapPort
        {
            get { return Fields.AssignedByMcimapPort[this]; }
            set { Fields.AssignedByMcimapPort[this] = value; }
        }

        [DisplayName("Created By Mc Username"), Expression("jAssignedBy.[MCUsername]")]
        public String AssignedByMcUsername
        {
            get { return Fields.AssignedByMcUsername[this]; }
            set { Fields.AssignedByMcUsername[this] = value; }
        }

        [DisplayName("Created By Mc Password"), Expression("jAssignedBy.[MCPassword]")]
        public String AssignedByMcPassword
        {
            get { return Fields.AssignedByMcPassword[this]; }
            set { Fields.AssignedByMcPassword[this] = value; }
        }

        [DisplayName("Created By Start Time"), Expression("jAssignedBy.[StartTime]")]
        public String AssignedByStartTime
        {
            get { return Fields.AssignedByStartTime[this]; }
            set { Fields.AssignedByStartTime[this] = value; }
        }

        [DisplayName("Created By End Time"), Expression("jAssignedBy.[EndTime]")]
        public String AssignedByEndTime
        {
            get { return Fields.AssignedByEndTime[this]; }
            set { Fields.AssignedByEndTime[this] = value; }
        }

        [DisplayName("Created By Branch Id"), Expression("jAssignedBy.[BranchId]")]
        public Int32? AssignedByBranchId
        {
            get { return Fields.AssignedByBranchId[this]; }
            set { Fields.AssignedByBranchId[this] = value; }
        }

        [DisplayName("Created By Uid"), Expression("jAssignedBy.[UID]")]
        public String AssignedByUid
        {
            get { return Fields.AssignedByUid[this]; }
            set { Fields.AssignedByUid[this] = value; }
        }

        [DisplayName("Created By Non Operational"), Expression("jAssignedBy.[NonOperational]")]
        public Boolean? AssignedByNonOperational
        {
            get { return Fields.AssignedByNonOperational[this]; }
            set { Fields.AssignedByNonOperational[this] = value; }
        }

        [DisplayName("Assigned To"), Expression("jAssignedTo.[Username]")]
        public String AssignedToUsername
        {
            get { return Fields.AssignedToUsername[this]; }
            set { Fields.AssignedToUsername[this] = value; }
        }

        [DisplayName("Assigned To Display Name"), Expression("jAssignedTo.[DisplayName]")]
        public String AssignedToDisplayName
        {
            get { return Fields.AssignedToDisplayName[this]; }
            set { Fields.AssignedToDisplayName[this] = value; }
        }

        [DisplayName("Assigned Display Name"), Expression("jAssignedTo.[DisplayName]")]
        public String AssignedDisplayName
        {
            get { return Fields.AssignedDisplayName[this]; }
            set { Fields.AssignedDisplayName[this] = value; }
        }

        [DisplayName("Assigned To Email"), Expression("jAssignedTo.[Email]")]
        public String AssignedToEmail
        {
            get { return Fields.AssignedToEmail[this]; }
            set { Fields.AssignedToEmail[this] = value; }
        }

        [DisplayName("Assigned To Source"), Expression("jAssignedTo.[Source]")]
        public String AssignedToSource
        {
            get { return Fields.AssignedToSource[this]; }
            set { Fields.AssignedToSource[this] = value; }
        }

        [DisplayName("Assigned To Password Hash"), Expression("jAssignedTo.[PasswordHash]")]
        public String AssignedToPasswordHash
        {
            get { return Fields.AssignedToPasswordHash[this]; }
            set { Fields.AssignedToPasswordHash[this] = value; }
        }

        [DisplayName("Assigned To Password Salt"), Expression("jAssignedTo.[PasswordSalt]")]
        public String AssignedToPasswordSalt
        {
            get { return Fields.AssignedToPasswordSalt[this]; }
            set { Fields.AssignedToPasswordSalt[this] = value; }
        }

        [DisplayName("Assigned To Last Directory Update"), Expression("jAssignedTo.[LastDirectoryUpdate]")]
        public DateTime? AssignedToLastDirectoryUpdate
        {
            get { return Fields.AssignedToLastDirectoryUpdate[this]; }
            set { Fields.AssignedToLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Assigned To User Image"), Expression("jAssignedTo.[UserImage]")]
        public String AssignedToUserImage
        {
            get { return Fields.AssignedToUserImage[this]; }
            set { Fields.AssignedToUserImage[this] = value; }
        }

        [DisplayName("Assigned To Insert Date"), Expression("jAssignedTo.[InsertDate]")]
        public DateTime? AssignedToInsertDate
        {
            get { return Fields.AssignedToInsertDate[this]; }
            set { Fields.AssignedToInsertDate[this] = value; }
        }

        [DisplayName("Assigned To Insert User Id"), Expression("jAssignedTo.[InsertUserId]")]
        public Int32? AssignedToInsertUserId
        {
            get { return Fields.AssignedToInsertUserId[this]; }
            set { Fields.AssignedToInsertUserId[this] = value; }
        }

        [DisplayName("Assigned To Update Date"), Expression("jAssignedTo.[UpdateDate]")]
        public DateTime? AssignedToUpdateDate
        {
            get { return Fields.AssignedToUpdateDate[this]; }
            set { Fields.AssignedToUpdateDate[this] = value; }
        }

        [DisplayName("Assigned To Update User Id"), Expression("jAssignedTo.[UpdateUserId]")]
        public Int32? AssignedToUpdateUserId
        {
            get { return Fields.AssignedToUpdateUserId[this]; }
            set { Fields.AssignedToUpdateUserId[this] = value; }
        }

        [DisplayName("Assigned To Is Active"), Expression("jAssignedTo.[IsActive]")]
        public Int16? AssignedToIsActive
        {
            get { return Fields.AssignedToIsActive[this]; }
            set { Fields.AssignedToIsActive[this] = value; }
        }

        [DisplayName("Assigned Is Active"), Expression("jAssignedTo.[IsActive]")]
        public Int16? AssignedIsActive
        {
            get { return Fields.AssignedIsActive[this]; }
            set { Fields.AssignedIsActive[this] = value; }
        }

        [DisplayName("Assigned To Upper Level"), Expression("jAssignedTo.[UpperLevel]")]
        public Int32? AssignedToUpperLevel
        {
            get { return Fields.AssignedToUpperLevel[this]; }
            set { Fields.AssignedToUpperLevel[this] = value; }
        }

        [DisplayName("Assigned To Upper Level2"), Expression("jAssignedTo.[UpperLevel2]")]
        public Int32? AssignedToUpperLevel2
        {
            get { return Fields.AssignedToUpperLevel2[this]; }
            set { Fields.AssignedToUpperLevel2[this] = value; }
        }

        [DisplayName("Assigned To Upper Level3"), Expression("jAssignedTo.[UpperLevel3]")]
        public Int32? AssignedToUpperLevel3
        {
            get { return Fields.AssignedToUpperLevel3[this]; }
            set { Fields.AssignedToUpperLevel3[this] = value; }
        }

        [DisplayName("Assigned To Upper Level4"), Expression("jAssignedTo.[UpperLevel4]")]
        public Int32? AssignedToUpperLevel4
        {
            get { return Fields.AssignedToUpperLevel4[this]; }
            set { Fields.AssignedToUpperLevel4[this] = value; }
        }

        [DisplayName("Assigned To Upper Level5"), Expression("jAssignedTo.[UpperLevel5]")]
        public Int32? AssignedToUpperLevel5
        {
            get { return Fields.AssignedToUpperLevel5[this]; }
            set { Fields.AssignedToUpperLevel5[this] = value; }
        }

        [DisplayName("Assigned To Host"), Expression("jAssignedTo.[Host]")]
        public String AssignedToHost
        {
            get { return Fields.AssignedToHost[this]; }
            set { Fields.AssignedToHost[this] = value; }
        }

        [DisplayName("Assigned To Port"), Expression("jAssignedTo.[Port]")]
        public Int32? AssignedToPort
        {
            get { return Fields.AssignedToPort[this]; }
            set { Fields.AssignedToPort[this] = value; }
        }

        [DisplayName("Assigned To SSL"), Expression("jAssignedTo.[SSL]")]
        public Boolean? AssignedToSsl
        {
            get { return Fields.AssignedToSsl[this]; }
            set { Fields.AssignedToSsl[this] = value; }
        }

        [DisplayName("Assigned To Email Id"), Expression("jAssignedTo.[EmailId]")]
        public String AssignedToEmailId
        {
            get { return Fields.AssignedToEmailId[this]; }
            set { Fields.AssignedToEmailId[this] = value; }
        }

        [DisplayName("Assigned To Email Password"), Expression("jAssignedTo.[EmailPassword]")]
        public String AssignedToEmailPassword
        {
            get { return Fields.AssignedToEmailPassword[this]; }
            set { Fields.AssignedToEmailPassword[this] = value; }
        }

        [DisplayName("Assigned To Phone"), Expression("jAssignedTo.[Phone]")]
        public String AssignedToPhone
        {
            get { return Fields.AssignedToPhone[this]; }
            set { Fields.AssignedToPhone[this] = value; }
        }

        [DisplayName("Assigned To Mcsmtp Server"), Expression("jAssignedTo.[MCSMTPServer]")]
        public String AssignedToMcsmtpServer
        {
            get { return Fields.AssignedToMcsmtpServer[this]; }
            set { Fields.AssignedToMcsmtpServer[this] = value; }
        }

        [DisplayName("Assigned To Mcsmtp Port"), Expression("jAssignedTo.[MCSMTPPort]")]
        public Int32? AssignedToMcsmtpPort
        {
            get { return Fields.AssignedToMcsmtpPort[this]; }
            set { Fields.AssignedToMcsmtpPort[this] = value; }
        }

        [DisplayName("Assigned To Mcimap Server"), Expression("jAssignedTo.[MCIMAPServer]")]
        public String AssignedToMcimapServer
        {
            get { return Fields.AssignedToMcimapServer[this]; }
            set { Fields.AssignedToMcimapServer[this] = value; }
        }

        [DisplayName("Assigned To Mcimap Port"), Expression("jAssignedTo.[MCIMAPPort]")]
        public Int32? AssignedToMcimapPort
        {
            get { return Fields.AssignedToMcimapPort[this]; }
            set { Fields.AssignedToMcimapPort[this] = value; }
        }

        [DisplayName("Assigned To Mc Username"), Expression("jAssignedTo.[MCUsername]")]
        public String AssignedToMcUsername
        {
            get { return Fields.AssignedToMcUsername[this]; }
            set { Fields.AssignedToMcUsername[this] = value; }
        }

        [DisplayName("Assigned To Mc Password"), Expression("jAssignedTo.[MCPassword]")]
        public String AssignedToMcPassword
        {
            get { return Fields.AssignedToMcPassword[this]; }
            set { Fields.AssignedToMcPassword[this] = value; }
        }

        [DisplayName("Assigned To Start Time"), Expression("jAssignedTo.[StartTime]")]
        public String AssignedToStartTime
        {
            get { return Fields.AssignedToStartTime[this]; }
            set { Fields.AssignedToStartTime[this] = value; }
        }

        [DisplayName("Assigned To End Time"), Expression("jAssignedTo.[EndTime]")]
        public String AssignedToEndTime
        {
            get { return Fields.AssignedToEndTime[this]; }
            set { Fields.AssignedToEndTime[this] = value; }
        }

        [DisplayName("Assigned To Branch Id"), Expression("jAssignedTo.[BranchId]")]
        public Int32? AssignedToBranchId
        {
            get { return Fields.AssignedToBranchId[this]; }
            set { Fields.AssignedToBranchId[this] = value; }
        }

        [DisplayName("Assigned To Uid"), Expression("jAssignedTo.[UID]")]
        public String AssignedToUid
        {
            get { return Fields.AssignedToUid[this]; }
            set { Fields.AssignedToUid[this] = value; }
        }

        [DisplayName("Assigned To Non Operational"), Expression("jAssignedTo.[NonOperational]")]
        public Boolean? AssignedToNonOperational
        {
            get { return Fields.AssignedToNonOperational[this]; }
            set { Fields.AssignedToNonOperational[this] = value; }
        }

        [DisplayName("Branch"), Expression("jBranch.[Branch]")]
        public String Branch
        {
            get { return Fields.Branch[this]; }
            set { Fields.Branch[this] = value; }
        }

        [DisplayName("Branch Phone"), Expression("jBranch.[Phone]")]
        public String BranchPhone
        {
            get { return Fields.BranchPhone[this]; }
            set { Fields.BranchPhone[this] = value; }
        }

        [DisplayName("Branch Email"), Expression("jBranch.[Email]")]
        public String BranchEmail
        {
            get { return Fields.BranchEmail[this]; }
            set { Fields.BranchEmail[this] = value; }
        }

        [DisplayName("Branch Address"), Expression("jBranch.[Address]")]
        public String BranchAddress
        {
            get { return Fields.BranchAddress[this]; }
            set { Fields.BranchAddress[this] = value; }
        }

        [DisplayName("Branch City Id"), Expression("jBranch.[CityId]")]
        public Int32? BranchCityId
        {
            get { return Fields.BranchCityId[this]; }
            set { Fields.BranchCityId[this] = value; }
        }

        [DisplayName("Branch State Id"), Expression("jBranch.[StateId]")]
        public Int32? BranchStateId
        {
            get { return Fields.BranchStateId[this]; }
            set { Fields.BranchStateId[this] = value; }
        }

        [DisplayName("Branch Pin"), Expression("jBranch.[Pin]")]
        public String BranchPin
        {
            get { return Fields.BranchPin[this]; }
            set { Fields.BranchPin[this] = value; }
        }

        [DisplayName("Branch Country"), Expression("jBranch.[Country]")]
        public Int32? BranchCountry
        {
            get { return Fields.BranchCountry[this]; }
            set { Fields.BranchCountry[this] = value; }
        }

        [DisplayName("Stage"), Expression("jStage.[Stage]")]
        public String Stage
        {
            get { return Fields.Stage[this]; }
            set { Fields.Stage[this] = value; }
        }

        [DisplayName("Stage Type"), Expression("jStage.[Type]")]
        public Int32? StageType
        {
            get { return Fields.StageType[this]; }
            set { Fields.StageType[this] = value; }
        }

        [DisplayName("Investigation By"), Expression("jInvestigationBy.[Username]")]
        public String InvestigationByUsername
        {
            get { return Fields.InvestigationByUsername[this]; }
            set { Fields.InvestigationByUsername[this] = value; }
        }

        [DisplayName("Investigation By Display Name"), Expression("jInvestigationBy.[DisplayName]")]
        public String InvestigationByDisplayName
        {
            get { return Fields.InvestigationByDisplayName[this]; }
            set { Fields.InvestigationByDisplayName[this] = value; }
        }

        [DisplayName("Investigation By Email"), Expression("jInvestigationBy.[Email]")]
        public String InvestigationByEmail
        {
            get { return Fields.InvestigationByEmail[this]; }
            set { Fields.InvestigationByEmail[this] = value; }
        }

        [DisplayName("Investigation By Source"), Expression("jInvestigationBy.[Source]")]
        public String InvestigationBySource
        {
            get { return Fields.InvestigationBySource[this]; }
            set { Fields.InvestigationBySource[this] = value; }
        }

        [DisplayName("Investigation By Password Hash"), Expression("jInvestigationBy.[PasswordHash]")]
        public String InvestigationByPasswordHash
        {
            get { return Fields.InvestigationByPasswordHash[this]; }
            set { Fields.InvestigationByPasswordHash[this] = value; }
        }

        [DisplayName("Investigation By Password Salt"), Expression("jInvestigationBy.[PasswordSalt]")]
        public String InvestigationByPasswordSalt
        {
            get { return Fields.InvestigationByPasswordSalt[this]; }
            set { Fields.InvestigationByPasswordSalt[this] = value; }
        }

        [DisplayName("Investigation By Last Directory Update"), Expression("jInvestigationBy.[LastDirectoryUpdate]")]
        public DateTime? InvestigationByLastDirectoryUpdate
        {
            get { return Fields.InvestigationByLastDirectoryUpdate[this]; }
            set { Fields.InvestigationByLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Investigation By User Image"), Expression("jInvestigationBy.[UserImage]")]
        public String InvestigationByUserImage
        {
            get { return Fields.InvestigationByUserImage[this]; }
            set { Fields.InvestigationByUserImage[this] = value; }
        }

        [DisplayName("Investigation By Insert Date"), Expression("jInvestigationBy.[InsertDate]")]
        public DateTime? InvestigationByInsertDate
        {
            get { return Fields.InvestigationByInsertDate[this]; }
            set { Fields.InvestigationByInsertDate[this] = value; }
        }

        [DisplayName("Investigation By Insert User Id"), Expression("jInvestigationBy.[InsertUserId]")]
        public Int32? InvestigationByInsertUserId
        {
            get { return Fields.InvestigationByInsertUserId[this]; }
            set { Fields.InvestigationByInsertUserId[this] = value; }
        }

        [DisplayName("Investigation By Update Date"), Expression("jInvestigationBy.[UpdateDate]")]
        public DateTime? InvestigationByUpdateDate
        {
            get { return Fields.InvestigationByUpdateDate[this]; }
            set { Fields.InvestigationByUpdateDate[this] = value; }
        }

        [DisplayName("Investigation By Update User Id"), Expression("jInvestigationBy.[UpdateUserId]")]
        public Int32? InvestigationByUpdateUserId
        {
            get { return Fields.InvestigationByUpdateUserId[this]; }
            set { Fields.InvestigationByUpdateUserId[this] = value; }
        }

        [DisplayName("Investigation By Is Active"), Expression("jInvestigationBy.[IsActive]")]
        public Int16? InvestigationByIsActive
        {
            get { return Fields.InvestigationByIsActive[this]; }
            set { Fields.InvestigationByIsActive[this] = value; }
        }

        [DisplayName("Investigation By Upper Level"), Expression("jInvestigationBy.[UpperLevel]")]
        public Int32? InvestigationByUpperLevel
        {
            get { return Fields.InvestigationByUpperLevel[this]; }
            set { Fields.InvestigationByUpperLevel[this] = value; }
        }

        [DisplayName("Investigation By Upper Level2"), Expression("jInvestigationBy.[UpperLevel2]")]
        public Int32? InvestigationByUpperLevel2
        {
            get { return Fields.InvestigationByUpperLevel2[this]; }
            set { Fields.InvestigationByUpperLevel2[this] = value; }
        }

        [DisplayName("Investigation By Upper Level3"), Expression("jInvestigationBy.[UpperLevel3]")]
        public Int32? InvestigationByUpperLevel3
        {
            get { return Fields.InvestigationByUpperLevel3[this]; }
            set { Fields.InvestigationByUpperLevel3[this] = value; }
        }

        [DisplayName("Investigation By Upper Level4"), Expression("jInvestigationBy.[UpperLevel4]")]
        public Int32? InvestigationByUpperLevel4
        {
            get { return Fields.InvestigationByUpperLevel4[this]; }
            set { Fields.InvestigationByUpperLevel4[this] = value; }
        }

        [DisplayName("Investigation By Upper Level5"), Expression("jInvestigationBy.[UpperLevel5]")]
        public Int32? InvestigationByUpperLevel5
        {
            get { return Fields.InvestigationByUpperLevel5[this]; }
            set { Fields.InvestigationByUpperLevel5[this] = value; }
        }

        [DisplayName("Investigation By Host"), Expression("jInvestigationBy.[Host]")]
        public String InvestigationByHost
        {
            get { return Fields.InvestigationByHost[this]; }
            set { Fields.InvestigationByHost[this] = value; }
        }

        [DisplayName("Investigation By Port"), Expression("jInvestigationBy.[Port]")]
        public Int32? InvestigationByPort
        {
            get { return Fields.InvestigationByPort[this]; }
            set { Fields.InvestigationByPort[this] = value; }
        }

        [DisplayName("Investigation By SSL"), Expression("jInvestigationBy.[SSL]")]
        public Boolean? InvestigationBySsl
        {
            get { return Fields.InvestigationBySsl[this]; }
            set { Fields.InvestigationBySsl[this] = value; }
        }

        [DisplayName("Investigation By Email Id"), Expression("jInvestigationBy.[EmailId]")]
        public String InvestigationByEmailId
        {
            get { return Fields.InvestigationByEmailId[this]; }
            set { Fields.InvestigationByEmailId[this] = value; }
        }

        [DisplayName("Investigation By Email Password"), Expression("jInvestigationBy.[EmailPassword]")]
        public String InvestigationByEmailPassword
        {
            get { return Fields.InvestigationByEmailPassword[this]; }
            set { Fields.InvestigationByEmailPassword[this] = value; }
        }

        [DisplayName("Investigation By Phone"), Expression("jInvestigationBy.[Phone]")]
        public String InvestigationByPhone
        {
            get { return Fields.InvestigationByPhone[this]; }
            set { Fields.InvestigationByPhone[this] = value; }
        }

        [DisplayName("Investigation By Mcsmtp Server"), Expression("jInvestigationBy.[MCSMTPServer]")]
        public String InvestigationByMcsmtpServer
        {
            get { return Fields.InvestigationByMcsmtpServer[this]; }
            set { Fields.InvestigationByMcsmtpServer[this] = value; }
        }

        [DisplayName("Investigation By Mcsmtp Port"), Expression("jInvestigationBy.[MCSMTPPort]")]
        public Int32? InvestigationByMcsmtpPort
        {
            get { return Fields.InvestigationByMcsmtpPort[this]; }
            set { Fields.InvestigationByMcsmtpPort[this] = value; }
        }

        [DisplayName("Investigation By Mcimap Server"), Expression("jInvestigationBy.[MCIMAPServer]")]
        public String InvestigationByMcimapServer
        {
            get { return Fields.InvestigationByMcimapServer[this]; }
            set { Fields.InvestigationByMcimapServer[this] = value; }
        }

        [DisplayName("Investigation By Mcimap Port"), Expression("jInvestigationBy.[MCIMAPPort]")]
        public Int32? InvestigationByMcimapPort
        {
            get { return Fields.InvestigationByMcimapPort[this]; }
            set { Fields.InvestigationByMcimapPort[this] = value; }
        }

        [DisplayName("Investigation By Mc Username"), Expression("jInvestigationBy.[MCUsername]")]
        public String InvestigationByMcUsername
        {
            get { return Fields.InvestigationByMcUsername[this]; }
            set { Fields.InvestigationByMcUsername[this] = value; }
        }

        [DisplayName("Investigation By Mc Password"), Expression("jInvestigationBy.[MCPassword]")]
        public String InvestigationByMcPassword
        {
            get { return Fields.InvestigationByMcPassword[this]; }
            set { Fields.InvestigationByMcPassword[this] = value; }
        }

        [DisplayName("Investigation By Start Time"), Expression("jInvestigationBy.[StartTime]")]
        public String InvestigationByStartTime
        {
            get { return Fields.InvestigationByStartTime[this]; }
            set { Fields.InvestigationByStartTime[this] = value; }
        }

        [DisplayName("Investigation By End Time"), Expression("jInvestigationBy.[EndTime]")]
        public String InvestigationByEndTime
        {
            get { return Fields.InvestigationByEndTime[this]; }
            set { Fields.InvestigationByEndTime[this] = value; }
        }

        [DisplayName("Investigation By Branch Id"), Expression("jInvestigationBy.[BranchId]")]
        public Int32? InvestigationByBranchId
        {
            get { return Fields.InvestigationByBranchId[this]; }
            set { Fields.InvestigationByBranchId[this] = value; }
        }

        [DisplayName("Investigation By Uid"), Expression("jInvestigationBy.[UID]")]
        public String InvestigationByUid
        {
            get { return Fields.InvestigationByUid[this]; }
            set { Fields.InvestigationByUid[this] = value; }
        }

        [DisplayName("Investigation By Non Operational"), Expression("jInvestigationBy.[NonOperational]")]
        public Boolean? InvestigationByNonOperational
        {
            get { return Fields.InvestigationByNonOperational[this]; }
            set { Fields.InvestigationByNonOperational[this] = value; }
        }

        [DisplayName("Action By"), Expression("jActionBy.[Username]")]
        public String ActionByUsername
        {
            get { return Fields.ActionByUsername[this]; }
            set { Fields.ActionByUsername[this] = value; }
        }

        [DisplayName("Action By Display Name"), Expression("jActionBy.[DisplayName]")]
        public String ActionByDisplayName
        {
            get { return Fields.ActionByDisplayName[this]; }
            set { Fields.ActionByDisplayName[this] = value; }
        }

        [DisplayName("Action By Email"), Expression("jActionBy.[Email]")]
        public String ActionByEmail
        {
            get { return Fields.ActionByEmail[this]; }
            set { Fields.ActionByEmail[this] = value; }
        }

        [DisplayName("Action By Source"), Expression("jActionBy.[Source]")]
        public String ActionBySource
        {
            get { return Fields.ActionBySource[this]; }
            set { Fields.ActionBySource[this] = value; }
        }

        [DisplayName("Action By Password Hash"), Expression("jActionBy.[PasswordHash]")]
        public String ActionByPasswordHash
        {
            get { return Fields.ActionByPasswordHash[this]; }
            set { Fields.ActionByPasswordHash[this] = value; }
        }

        [DisplayName("Action By Password Salt"), Expression("jActionBy.[PasswordSalt]")]
        public String ActionByPasswordSalt
        {
            get { return Fields.ActionByPasswordSalt[this]; }
            set { Fields.ActionByPasswordSalt[this] = value; }
        }

        [DisplayName("Action By Last Directory Update"), Expression("jActionBy.[LastDirectoryUpdate]")]
        public DateTime? ActionByLastDirectoryUpdate
        {
            get { return Fields.ActionByLastDirectoryUpdate[this]; }
            set { Fields.ActionByLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Action By User Image"), Expression("jActionBy.[UserImage]")]
        public String ActionByUserImage
        {
            get { return Fields.ActionByUserImage[this]; }
            set { Fields.ActionByUserImage[this] = value; }
        }

        [DisplayName("Action By Insert Date"), Expression("jActionBy.[InsertDate]")]
        public DateTime? ActionByInsertDate
        {
            get { return Fields.ActionByInsertDate[this]; }
            set { Fields.ActionByInsertDate[this] = value; }
        }

        [DisplayName("Action By Insert User Id"), Expression("jActionBy.[InsertUserId]")]
        public Int32? ActionByInsertUserId
        {
            get { return Fields.ActionByInsertUserId[this]; }
            set { Fields.ActionByInsertUserId[this] = value; }
        }

        [DisplayName("Action By Update Date"), Expression("jActionBy.[UpdateDate]")]
        public DateTime? ActionByUpdateDate
        {
            get { return Fields.ActionByUpdateDate[this]; }
            set { Fields.ActionByUpdateDate[this] = value; }
        }

        [DisplayName("Action By Update User Id"), Expression("jActionBy.[UpdateUserId]")]
        public Int32? ActionByUpdateUserId
        {
            get { return Fields.ActionByUpdateUserId[this]; }
            set { Fields.ActionByUpdateUserId[this] = value; }
        }

        [DisplayName("Action By Is Active"), Expression("jActionBy.[IsActive]")]
        public Int16? ActionByIsActive
        {
            get { return Fields.ActionByIsActive[this]; }
            set { Fields.ActionByIsActive[this] = value; }
        }

        [DisplayName("Action By Upper Level"), Expression("jActionBy.[UpperLevel]")]
        public Int32? ActionByUpperLevel
        {
            get { return Fields.ActionByUpperLevel[this]; }
            set { Fields.ActionByUpperLevel[this] = value; }
        }

        [DisplayName("Action By Upper Level2"), Expression("jActionBy.[UpperLevel2]")]
        public Int32? ActionByUpperLevel2
        {
            get { return Fields.ActionByUpperLevel2[this]; }
            set { Fields.ActionByUpperLevel2[this] = value; }
        }

        [DisplayName("Action By Upper Level3"), Expression("jActionBy.[UpperLevel3]")]
        public Int32? ActionByUpperLevel3
        {
            get { return Fields.ActionByUpperLevel3[this]; }
            set { Fields.ActionByUpperLevel3[this] = value; }
        }

        [DisplayName("Action By Upper Level4"), Expression("jActionBy.[UpperLevel4]")]
        public Int32? ActionByUpperLevel4
        {
            get { return Fields.ActionByUpperLevel4[this]; }
            set { Fields.ActionByUpperLevel4[this] = value; }
        }

        [DisplayName("Action By Upper Level5"), Expression("jActionBy.[UpperLevel5]")]
        public Int32? ActionByUpperLevel5
        {
            get { return Fields.ActionByUpperLevel5[this]; }
            set { Fields.ActionByUpperLevel5[this] = value; }
        }

        [DisplayName("Action By Host"), Expression("jActionBy.[Host]")]
        public String ActionByHost
        {
            get { return Fields.ActionByHost[this]; }
            set { Fields.ActionByHost[this] = value; }
        }

        [DisplayName("Action By Port"), Expression("jActionBy.[Port]")]
        public Int32? ActionByPort
        {
            get { return Fields.ActionByPort[this]; }
            set { Fields.ActionByPort[this] = value; }
        }

        [DisplayName("Action By SSL"), Expression("jActionBy.[SSL]")]
        public Boolean? ActionBySsl
        {
            get { return Fields.ActionBySsl[this]; }
            set { Fields.ActionBySsl[this] = value; }
        }

        [DisplayName("Action By Email Id"), Expression("jActionBy.[EmailId]")]
        public String ActionByEmailId
        {
            get { return Fields.ActionByEmailId[this]; }
            set { Fields.ActionByEmailId[this] = value; }
        }

        [DisplayName("Action By Email Password"), Expression("jActionBy.[EmailPassword]")]
        public String ActionByEmailPassword
        {
            get { return Fields.ActionByEmailPassword[this]; }
            set { Fields.ActionByEmailPassword[this] = value; }
        }

        [DisplayName("Action By Phone"), Expression("jActionBy.[Phone]")]
        public String ActionByPhone
        {
            get { return Fields.ActionByPhone[this]; }
            set { Fields.ActionByPhone[this] = value; }
        }

        [DisplayName("Action By Mcsmtp Server"), Expression("jActionBy.[MCSMTPServer]")]
        public String ActionByMcsmtpServer
        {
            get { return Fields.ActionByMcsmtpServer[this]; }
            set { Fields.ActionByMcsmtpServer[this] = value; }
        }

        [DisplayName("Action By Mcsmtp Port"), Expression("jActionBy.[MCSMTPPort]")]
        public Int32? ActionByMcsmtpPort
        {
            get { return Fields.ActionByMcsmtpPort[this]; }
            set { Fields.ActionByMcsmtpPort[this] = value; }
        }

        [DisplayName("Action By Mcimap Server"), Expression("jActionBy.[MCIMAPServer]")]
        public String ActionByMcimapServer
        {
            get { return Fields.ActionByMcimapServer[this]; }
            set { Fields.ActionByMcimapServer[this] = value; }
        }

        [DisplayName("Action By Mcimap Port"), Expression("jActionBy.[MCIMAPPort]")]
        public Int32? ActionByMcimapPort
        {
            get { return Fields.ActionByMcimapPort[this]; }
            set { Fields.ActionByMcimapPort[this] = value; }
        }

        [DisplayName("Action By Mc Username"), Expression("jActionBy.[MCUsername]")]
        public String ActionByMcUsername
        {
            get { return Fields.ActionByMcUsername[this]; }
            set { Fields.ActionByMcUsername[this] = value; }
        }

        [DisplayName("Action By Mc Password"), Expression("jActionBy.[MCPassword]")]
        public String ActionByMcPassword
        {
            get { return Fields.ActionByMcPassword[this]; }
            set { Fields.ActionByMcPassword[this] = value; }
        }

        [DisplayName("Action By Start Time"), Expression("jActionBy.[StartTime]")]
        public String ActionByStartTime
        {
            get { return Fields.ActionByStartTime[this]; }
            set { Fields.ActionByStartTime[this] = value; }
        }

        [DisplayName("Action By End Time"), Expression("jActionBy.[EndTime]")]
        public String ActionByEndTime
        {
            get { return Fields.ActionByEndTime[this]; }
            set { Fields.ActionByEndTime[this] = value; }
        }

        [DisplayName("Action By Branch Id"), Expression("jActionBy.[BranchId]")]
        public Int32? ActionByBranchId
        {
            get { return Fields.ActionByBranchId[this]; }
            set { Fields.ActionByBranchId[this] = value; }
        }

        [DisplayName("Action By Uid"), Expression("jActionBy.[UID]")]
        public String ActionByUid
        {
            get { return Fields.ActionByUid[this]; }
            set { Fields.ActionByUid[this] = value; }
        }

        [DisplayName("Action By Non Operational"), Expression("jActionBy.[NonOperational]")]
        public Boolean? ActionByNonOperational
        {
            get { return Fields.ActionByNonOperational[this]; }
            set { Fields.ActionByNonOperational[this] = value; }
        }

        [DisplayName("Supervised By"), Expression("jSupervisedBy.[Username]")]
        public String SupervisedByUsername
        {
            get { return Fields.SupervisedByUsername[this]; }
            set { Fields.SupervisedByUsername[this] = value; }
        }

        [DisplayName("Supervised By Display Name"), Expression("jSupervisedBy.[DisplayName]")]
        public String SupervisedByDisplayName
        {
            get { return Fields.SupervisedByDisplayName[this]; }
            set { Fields.SupervisedByDisplayName[this] = value; }
        }

        [DisplayName("Supervised By Email"), Expression("jSupervisedBy.[Email]")]
        public String SupervisedByEmail
        {
            get { return Fields.SupervisedByEmail[this]; }
            set { Fields.SupervisedByEmail[this] = value; }
        }

        [DisplayName("Supervised By Source"), Expression("jSupervisedBy.[Source]")]
        public String SupervisedBySource
        {
            get { return Fields.SupervisedBySource[this]; }
            set { Fields.SupervisedBySource[this] = value; }
        }

        [DisplayName("Supervised By Password Hash"), Expression("jSupervisedBy.[PasswordHash]")]
        public String SupervisedByPasswordHash
        {
            get { return Fields.SupervisedByPasswordHash[this]; }
            set { Fields.SupervisedByPasswordHash[this] = value; }
        }

        [DisplayName("Supervised By Password Salt"), Expression("jSupervisedBy.[PasswordSalt]")]
        public String SupervisedByPasswordSalt
        {
            get { return Fields.SupervisedByPasswordSalt[this]; }
            set { Fields.SupervisedByPasswordSalt[this] = value; }
        }

        [DisplayName("Supervised By Last Directory Update"), Expression("jSupervisedBy.[LastDirectoryUpdate]")]
        public DateTime? SupervisedByLastDirectoryUpdate
        {
            get { return Fields.SupervisedByLastDirectoryUpdate[this]; }
            set { Fields.SupervisedByLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Supervised By User Image"), Expression("jSupervisedBy.[UserImage]")]
        public String SupervisedByUserImage
        {
            get { return Fields.SupervisedByUserImage[this]; }
            set { Fields.SupervisedByUserImage[this] = value; }
        }

        [DisplayName("Supervised By Insert Date"), Expression("jSupervisedBy.[InsertDate]")]
        public DateTime? SupervisedByInsertDate
        {
            get { return Fields.SupervisedByInsertDate[this]; }
            set { Fields.SupervisedByInsertDate[this] = value; }
        }

        [DisplayName("Supervised By Insert User Id"), Expression("jSupervisedBy.[InsertUserId]")]
        public Int32? SupervisedByInsertUserId
        {
            get { return Fields.SupervisedByInsertUserId[this]; }
            set { Fields.SupervisedByInsertUserId[this] = value; }
        }

        [DisplayName("Supervised By Update Date"), Expression("jSupervisedBy.[UpdateDate]")]
        public DateTime? SupervisedByUpdateDate
        {
            get { return Fields.SupervisedByUpdateDate[this]; }
            set { Fields.SupervisedByUpdateDate[this] = value; }
        }

        [DisplayName("Supervised By Update User Id"), Expression("jSupervisedBy.[UpdateUserId]")]
        public Int32? SupervisedByUpdateUserId
        {
            get { return Fields.SupervisedByUpdateUserId[this]; }
            set { Fields.SupervisedByUpdateUserId[this] = value; }
        }

        [DisplayName("Supervised By Is Active"), Expression("jSupervisedBy.[IsActive]")]
        public Int16? SupervisedByIsActive
        {
            get { return Fields.SupervisedByIsActive[this]; }
            set { Fields.SupervisedByIsActive[this] = value; }
        }

        [DisplayName("Supervised By Upper Level"), Expression("jSupervisedBy.[UpperLevel]")]
        public Int32? SupervisedByUpperLevel
        {
            get { return Fields.SupervisedByUpperLevel[this]; }
            set { Fields.SupervisedByUpperLevel[this] = value; }
        }

        [DisplayName("Supervised By Upper Level2"), Expression("jSupervisedBy.[UpperLevel2]")]
        public Int32? SupervisedByUpperLevel2
        {
            get { return Fields.SupervisedByUpperLevel2[this]; }
            set { Fields.SupervisedByUpperLevel2[this] = value; }
        }

        [DisplayName("Supervised By Upper Level3"), Expression("jSupervisedBy.[UpperLevel3]")]
        public Int32? SupervisedByUpperLevel3
        {
            get { return Fields.SupervisedByUpperLevel3[this]; }
            set { Fields.SupervisedByUpperLevel3[this] = value; }
        }

        [DisplayName("Supervised By Upper Level4"), Expression("jSupervisedBy.[UpperLevel4]")]
        public Int32? SupervisedByUpperLevel4
        {
            get { return Fields.SupervisedByUpperLevel4[this]; }
            set { Fields.SupervisedByUpperLevel4[this] = value; }
        }

        [DisplayName("Supervised By Upper Level5"), Expression("jSupervisedBy.[UpperLevel5]")]
        public Int32? SupervisedByUpperLevel5
        {
            get { return Fields.SupervisedByUpperLevel5[this]; }
            set { Fields.SupervisedByUpperLevel5[this] = value; }
        }

        [DisplayName("Supervised By Host"), Expression("jSupervisedBy.[Host]")]
        public String SupervisedByHost
        {
            get { return Fields.SupervisedByHost[this]; }
            set { Fields.SupervisedByHost[this] = value; }
        }

        [DisplayName("Supervised By Port"), Expression("jSupervisedBy.[Port]")]
        public Int32? SupervisedByPort
        {
            get { return Fields.SupervisedByPort[this]; }
            set { Fields.SupervisedByPort[this] = value; }
        }

        [DisplayName("Supervised By SSL"), Expression("jSupervisedBy.[SSL]")]
        public Boolean? SupervisedBySsl
        {
            get { return Fields.SupervisedBySsl[this]; }
            set { Fields.SupervisedBySsl[this] = value; }
        }

        [DisplayName("Supervised By Email Id"), Expression("jSupervisedBy.[EmailId]")]
        public String SupervisedByEmailId
        {
            get { return Fields.SupervisedByEmailId[this]; }
            set { Fields.SupervisedByEmailId[this] = value; }
        }

        [DisplayName("Supervised By Email Password"), Expression("jSupervisedBy.[EmailPassword]")]
        public String SupervisedByEmailPassword
        {
            get { return Fields.SupervisedByEmailPassword[this]; }
            set { Fields.SupervisedByEmailPassword[this] = value; }
        }

        [DisplayName("Supervised By Phone"), Expression("jSupervisedBy.[Phone]")]
        public String SupervisedByPhone
        {
            get { return Fields.SupervisedByPhone[this]; }
            set { Fields.SupervisedByPhone[this] = value; }
        }

        [DisplayName("Supervised By Mcsmtp Server"), Expression("jSupervisedBy.[MCSMTPServer]")]
        public String SupervisedByMcsmtpServer
        {
            get { return Fields.SupervisedByMcsmtpServer[this]; }
            set { Fields.SupervisedByMcsmtpServer[this] = value; }
        }

        [DisplayName("Supervised By Mcsmtp Port"), Expression("jSupervisedBy.[MCSMTPPort]")]
        public Int32? SupervisedByMcsmtpPort
        {
            get { return Fields.SupervisedByMcsmtpPort[this]; }
            set { Fields.SupervisedByMcsmtpPort[this] = value; }
        }

        [DisplayName("Supervised By Mcimap Server"), Expression("jSupervisedBy.[MCIMAPServer]")]
        public String SupervisedByMcimapServer
        {
            get { return Fields.SupervisedByMcimapServer[this]; }
            set { Fields.SupervisedByMcimapServer[this] = value; }
        }

        [DisplayName("Supervised By Mcimap Port"), Expression("jSupervisedBy.[MCIMAPPort]")]
        public Int32? SupervisedByMcimapPort
        {
            get { return Fields.SupervisedByMcimapPort[this]; }
            set { Fields.SupervisedByMcimapPort[this] = value; }
        }

        [DisplayName("Supervised By Mc Username"), Expression("jSupervisedBy.[MCUsername]")]
        public String SupervisedByMcUsername
        {
            get { return Fields.SupervisedByMcUsername[this]; }
            set { Fields.SupervisedByMcUsername[this] = value; }
        }

        [DisplayName("Supervised By Mc Password"), Expression("jSupervisedBy.[MCPassword]")]
        public String SupervisedByMcPassword
        {
            get { return Fields.SupervisedByMcPassword[this]; }
            set { Fields.SupervisedByMcPassword[this] = value; }
        }

        [DisplayName("Supervised By Start Time"), Expression("jSupervisedBy.[StartTime]")]
        public String SupervisedByStartTime
        {
            get { return Fields.SupervisedByStartTime[this]; }
            set { Fields.SupervisedByStartTime[this] = value; }
        }

        [DisplayName("Supervised By End Time"), Expression("jSupervisedBy.[EndTime]")]
        public String SupervisedByEndTime
        {
            get { return Fields.SupervisedByEndTime[this]; }
            set { Fields.SupervisedByEndTime[this] = value; }
        }

        [DisplayName("Supervised By Branch Id"), Expression("jSupervisedBy.[BranchId]")]
        public Int32? SupervisedByBranchId
        {
            get { return Fields.SupervisedByBranchId[this]; }
            set { Fields.SupervisedByBranchId[this] = value; }
        }

        [DisplayName("Supervised By Uid"), Expression("jSupervisedBy.[UID]")]
        public String SupervisedByUid
        {
            get { return Fields.SupervisedByUid[this]; }
            set { Fields.SupervisedByUid[this] = value; }
        }

        [DisplayName("Supervised By Non Operational"), Expression("jSupervisedBy.[NonOperational]")]
        public Boolean? SupervisedByNonOperational
        {
            get { return Fields.SupervisedByNonOperational[this]; }
            set { Fields.SupervisedByNonOperational[this] = value; }
        }

        [DisplayName("Products"), MasterDetailRelation(foreignKey: "CMSId", IncludeColumns = "ProductsName"), NotMapped]
        [CMSProductsEditor]
        public List<CMSProductsRow> Products
        {
            get { return Fields.Products[this]; }
            set { Fields.Products[this] = value; }
        }

        [DisplayName("Dealer Name"), Expression("jDealer.[DealerName]")]
        public String DealerDealerName
        {
            get { return Fields.DealerDealerName[this]; }
            set { Fields.DealerDealerName[this] = value; }
        }


        [DisplayName("Dealer Phone"), Expression("jDealer.[Phone]"), LookupInclude]
        public String DealerPhone
        {
            get { return Fields.DealerPhone[this]; }
            set { Fields.DealerPhone[this] = value; }
        }

        [DisplayName("Dealer Email"), Expression("jDealer.[Email]"), LookupInclude]
        public String DealerEmail
        {
            get { return Fields.DealerEmail[this]; }
            set { Fields.DealerEmail[this] = value; }
        }

        [DisplayName("Dealer Address"), Expression("jDealer.[Address]"), LookupInclude]
        public String DealerAddress
        {
            get { return Fields.DealerAddress[this]; }
            set { Fields.DealerAddress[this] = value; }
        }

        [DisplayName("Dealer City Id"), Expression("jDealer.[CityId]")]
        public Int32? DealerCityId
        {
            get { return Fields.DealerCityId[this]; }
            set { Fields.DealerCityId[this] = value; }
        }

        [DisplayName("Dealer State Id"), Expression("jDealer.[StateId]")]
        public Int32? DealerStateId
        {
            get { return Fields.DealerStateId[this]; }
            set { Fields.DealerStateId[this] = value; }
        }

        [DisplayName("Dealer Pin"), Expression("jDealer.[Pin]")]
        public String DealerPin
        {
            get { return Fields.DealerPin[this]; }
            set { Fields.DealerPin[this] = value; }
        }

        [DisplayName("Ticket No"), QuickSearch]
        public Int32? TicketNo
        {
            get { return Fields.TicketNo[this]; }
            set { Fields.TicketNo[this] = value; }
        }

        [DisplayName("Dealer Country"), Expression("jDealer.[Country]")]
        public Int32? DealerCountry
        {
            get { return Fields.DealerCountry[this]; }
            set { Fields.DealerCountry[this] = value; }
        }

        [DisplayName("Dealer Additional Info"), Expression("jDealer.[AdditionalInfo]")]
        public String DealerAdditionalInfo
        {
            get { return Fields.DealerAdditionalInfo[this]; }
            set { Fields.DealerAdditionalInfo[this] = value; }
        }



        [DisplayName("Employee Emp Code"), Expression("jEmployee.[EmpCode]")]
        public String EmployeeEmpCode
        {
            get { return Fields.EmployeeEmpCode[this]; }
            set { Fields.EmployeeEmpCode[this] = value; }
        }

        [DisplayName("Employee Department Id"), Expression("jEmployee.[DepartmentId]")]
        public Int32? EmployeeDepartmentId
        {
            get { return Fields.EmployeeDepartmentId[this]; }
            set { Fields.EmployeeDepartmentId[this] = value; }
        }

        [DisplayName("Employee Name"), Expression("jEmployee.[Name]"), LookupInclude]
        public String EmployeeName
        {
            get { return Fields.EmployeeName[this]; }
            set { Fields.EmployeeName[this] = value; }
        }
        [DisplayName("Cmsn"), Column("CMSN"), Size(20), QuickSearch]
        public String Cmsn
        {
            get { return Fields.Cmsn[this]; }
            set { Fields.Cmsn[this] = value; }
        }
        [DisplayName("Employee Phone"), Expression("jEmployee.[Phone]"), LookupInclude]
        public String EmployeePhone
        {
            get { return Fields.EmployeePhone[this]; }
            set { Fields.EmployeePhone[this] = value; }
        }

        [DisplayName("Employee Email"), Expression("jEmployee.[Email]"), LookupInclude]
        public String EmployeeEmail
        {
            get { return Fields.EmployeeEmail[this]; }
            set { Fields.EmployeeEmail[this] = value; }
        }

        [DisplayName("Employee Address"), Expression("jEmployee.[Address]")]
        public String EmployeeAddress
        {
            get { return Fields.EmployeeAddress[this]; }
            set { Fields.EmployeeAddress[this] = value; }
        }

        [DisplayName("Employee Professional Email"), Expression("jEmployee.[ProfessionalEmail]")]
        public String EmployeeProfessionalEmail
        {
            get { return Fields.EmployeeProfessionalEmail[this]; }
            set { Fields.EmployeeProfessionalEmail[this] = value; }
        }

        [DisplayName("Employee City Id"), Expression("jEmployee.[CityId]")]
        public Int32? EmployeeCityId
        {
            get { return Fields.EmployeeCityId[this]; }
            set { Fields.EmployeeCityId[this] = value; }
        }

        [DisplayName("Employee State Id"), Expression("jEmployee.[StateId]")]
        public Int32? EmployeeStateId
        {
            get { return Fields.EmployeeStateId[this]; }
            set { Fields.EmployeeStateId[this] = value; }
        }

        [DisplayName("Employee Pin"), Expression("jEmployee.[Pin]")]
        public String EmployeePin
        {
            get { return Fields.EmployeePin[this]; }
            set { Fields.EmployeePin[this] = value; }
        }

        [DisplayName("Employee Country"), Expression("jEmployee.[Country]")]
        public Int32? EmployeeCountry
        {
            get { return Fields.EmployeeCountry[this]; }
            set { Fields.EmployeeCountry[this] = value; }
        }

        [DisplayName("Employee Additional Info"), Expression("jEmployee.[AdditionalInfo]")]
        public String EmployeeAdditionalInfo
        {
            get { return Fields.EmployeeAdditionalInfo[this]; }
            set { Fields.EmployeeAdditionalInfo[this] = value; }
        }

        [DisplayName("Employee Gender"), Expression("jEmployee.[Gender]")]
        public Int32? EmployeeGender
        {
            get { return Fields.EmployeeGender[this]; }
            set { Fields.EmployeeGender[this] = value; }
        }

        [DisplayName("Employee Religion"), Expression("jEmployee.[Religion]")]
        public Int32? EmployeeReligion
        {
            get { return Fields.EmployeeReligion[this]; }
            set { Fields.EmployeeReligion[this] = value; }
        }

        [DisplayName("Employee Area Id"), Expression("jEmployee.[AreaId]")]
        public Int32? EmployeeAreaId
        {
            get { return Fields.EmployeeAreaId[this]; }
            set { Fields.EmployeeAreaId[this] = value; }
        }

        [DisplayName("Employee Marital Status"), Expression("jEmployee.[MaritalStatus]")]
        public Int32? EmployeeMaritalStatus
        {
            get { return Fields.EmployeeMaritalStatus[this]; }
            set { Fields.EmployeeMaritalStatus[this] = value; }
        }

        [DisplayName("Employee Marriage Anniversary"), Expression("jEmployee.[MarriageAnniversary]")]
        public DateTime? EmployeeMarriageAnniversary
        {
            get { return Fields.EmployeeMarriageAnniversary[this]; }
            set { Fields.EmployeeMarriageAnniversary[this] = value; }
        }

        [DisplayName("Employee Birthdate"), Expression("jEmployee.[Birthdate]")]
        public DateTime? EmployeeBirthdate
        {
            get { return Fields.EmployeeBirthdate[this]; }
            set { Fields.EmployeeBirthdate[this] = value; }
        }

        [DisplayName("Employee Date Of Joining"), Expression("jEmployee.[DateOfJoining]")]
        public DateTime? EmployeeDateOfJoining
        {
            get { return Fields.EmployeeDateOfJoining[this]; }
            set { Fields.EmployeeDateOfJoining[this] = value; }
        }

        [DisplayName("Employee Company Id"), Expression("jEmployee.[CompanyId]")]
        public Int32? EmployeeCompanyId
        {
            get { return Fields.EmployeeCompanyId[this]; }
            set { Fields.EmployeeCompanyId[this] = value; }
        }

        [DisplayName("Employee Roles Id"), Expression("jEmployee.[RolesId]")]
        public Int32? EmployeeRolesId
        {
            get { return Fields.EmployeeRolesId[this]; }
            set { Fields.EmployeeRolesId[this] = value; }
        }

        [DisplayName("Employee Owner Id"), Expression("jEmployee.[OwnerId]")]
        public Int32? EmployeeOwnerId
        {
            get { return Fields.EmployeeOwnerId[this]; }
            set { Fields.EmployeeOwnerId[this] = value; }
        }

        [DisplayName("Employee Adhaar No"), Expression("jEmployee.[AdhaarNo]")]
        public String EmployeeAdhaarNo
        {
            get { return Fields.EmployeeAdhaarNo[this]; }
            set { Fields.EmployeeAdhaarNo[this] = value; }
        }

        [DisplayName("Employee Pan No"), Expression("jEmployee.[PANNo]")]
        public String EmployeePanNo
        {
            get { return Fields.EmployeePanNo[this]; }
            set { Fields.EmployeePanNo[this] = value; }
        }

        [DisplayName("Employee Attachment"), Expression("jEmployee.[Attachment]")]
        public String EmployeeAttachment
        {
            get { return Fields.EmployeeAttachment[this]; }
            set { Fields.EmployeeAttachment[this] = value; }
        }

        [DisplayName("Employee Bank Name"), Expression("jEmployee.[BankName]")]
        public String EmployeeBankName
        {
            get { return Fields.EmployeeBankName[this]; }
            set { Fields.EmployeeBankName[this] = value; }
        }

        [DisplayName("Employee Account Number"), Expression("jEmployee.[AccountNumber]")]
        public String EmployeeAccountNumber
        {
            get { return Fields.EmployeeAccountNumber[this]; }
            set { Fields.EmployeeAccountNumber[this] = value; }
        }

        [DisplayName("Employee Ifsc"), Expression("jEmployee.[IFSC]")]
        public String EmployeeIfsc
        {
            get { return Fields.EmployeeIfsc[this]; }
            set { Fields.EmployeeIfsc[this] = value; }
        }

        [DisplayName("Employee Bank Type"), Expression("jEmployee.[BankType]")]
        public String EmployeeBankType
        {
            get { return Fields.EmployeeBankType[this]; }
            set { Fields.EmployeeBankType[this] = value; }
        }

        [DisplayName("Employee Branch"), Expression("jEmployee.[Branch]")]
        public String EmployeeBranch
        {
            get { return Fields.EmployeeBranch[this]; }
            set { Fields.EmployeeBranch[this] = value; }
        }

        [DisplayName("Employee Tehsil Id"), Expression("jEmployee.[TehsilId]")]
        public Int32? EmployeeTehsilId
        {
            get { return Fields.EmployeeTehsilId[this]; }
            set { Fields.EmployeeTehsilId[this] = value; }
        }

        [DisplayName("Employee Village Id"), Expression("jEmployee.[VillageId]")]
        public Int32? EmployeeVillageId
        {
            get { return Fields.EmployeeVillageId[this]; }
            set { Fields.EmployeeVillageId[this] = value; }
        }
        [DisplayName("Project"), Expression("jProject.[Project]")]
        public String Project
        {
            get { return Fields.Project[this]; }
            set { Fields.Project[this] = value; }
        }
        [DisplayName("Quantity")]
        public Int32? Qty
        {
            get { return Fields.Qty[this]; }
            set { Fields.Qty[this] = value; }
        }
        [DisplayName("Representative")]
        public String Representative
        {
            get { return Fields.Representative[this]; }
            set { Fields.Representative[this] = value; }
        }

        [NotesEditor, NotMapped]
        [DisplayName("")]
        public List<NoteRow> NoteList
        {
            get { return Fields.NoteList[this]; }
            set { Fields.NoteList[this] = value; }
        }

        [TimelineEditor, NotMapped]
        [DisplayName("")]
        public List<TimelineRow> Timeline
        {
            get { return Fields.Timeline[this]; }
            set { Fields.Timeline[this] = value; }
        }

    
        public CMSRow()
            : base(Fields)
        {
        }
        public CMSRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ContactsId;
            public DateTimeField Date;
            public Int32Field ProductsId;
            public StringField SerialNo;
            public Int32Field ComplaintId;
            public Int32Field Category;
            public DoubleField Amount;
            public DoubleField Total;
            public DateTimeField ExpectedCompletion;
            public Int32Field AssignedBy;
            public Int32Field AssignedTo;
            public StringField Instructions;
            public Int32Field BranchId;
            public Int32Field Status;
            public DateTimeField CompletionDate;
            public StringField Feedback;
            public StringField AdditionalInfo;
            public StringField Image;
            public StringField Phone;
            public StringField Address;
            public Int32Field StageId;
            public Int32Field Priority;
            public BooleanField PMRClosed;
            public Int32Field InvestigationBy;
            public Int32Field ActionBy;
            public Int32Field SupervisedBy;
            public StringField Observation;
            public StringField Action;
            public StringField Comments;
            public Int32Field CMSNo;
            public Int32Field DealerId;
            public DateTimeField PurchaseDate;
            public StringField InvoiceNo;
            public Int32Field EmployeeId;
            public Int32Field ProjectId;
            public Int32Field TicketNo;

            public StringField Project;

            public Int32Field ContactsContactType;
            public StringField ContactsName;
            public StringField ContactsPhone;
            public StringField ContactsEmail;
            public StringField ContactsAddress;
            public Int32Field ContactsCityId;
            public Int32Field ContactsStateId;
            public StringField ContactsPin;
            public Int32Field ContactsCountry;
            public StringField ContactsWebsite;
            public StringField ContactsAdditionalInfo;
            public StringField ContactsResidentialPhone;
            public StringField ContactsOfficePhone;
            public Int32Field ContactsGender;
            public Int32Field ContactsReligion;
            public Int32Field ContactsAreaId;
            public Int32Field ContactsMaritalStatus;
            public DateTimeField ContactsMarriageAnniversary;
            public DateTimeField ContactsBirthdate;
            public DateTimeField ContactsDateOfIncorporation;
            public Int32Field ContactsCategoryId;
            public Int32Field ContactsGradeId;
            public Int32Field ContactsType;
            public Int32Field ContactsOwnerId;
            public Int32Field ContactsAssignedId;
            public Int32Field ContactsChannelCategory;
            public Int32Field ContactsNationalDistributor;
            public Int32Field ContactsStockist;
            public Int32Field ContactsDistributor;
            public Int32Field ContactsDealer;
            public Int32Field ContactsWholesaler;
            public Int32Field ContactsReseller;
            public StringField ContactsGstin;
            public StringField ContactsPanNo;
            public StringField ContactsCCEmails;
            public StringField ContactsBCCEmails;


            public StringField ContactsAttachment;
            public StringField ContactsEComGstin;
            public DoubleField ContactsCreditorsOpening;
            public DoubleField ContactsDebtorsOpening;
            public StringField ContactsBankName;
            public StringField ContactsAccountNumber;
            public StringField ContactsIfsc;
            public StringField ContactsBankType;
            public StringField ContactsBranch;
            public StringField ContactsAccountsEmail;
            public StringField ContactsPurchaseEmail;
            public StringField ContactsServiceEmail;
            public StringField ContactsSalesEmail;
            public Int32Field ContactsCreditDays;
            public Int32Field ContactsCustomerType;
            public Int32Field ContactsTrasportationId;
            public Int32Field ContactsTehsilId;
            public Int32Field ContactsVillageId;
            public StringField ContactsWhatsapp;
            public StringField Cmsn;

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

            public StringField ComplaintComplaintType;

            public StringField AssignedByUsername;
            public StringField AssignedByDisplayName;
            public StringField AssignedByEmail;
            public StringField AssignedBySource;
            public StringField AssignedByPasswordHash;
            public StringField AssignedByPasswordSalt;
            public DateTimeField AssignedByLastDirectoryUpdate;
            public StringField AssignedByUserImage;
            public DateTimeField AssignedByInsertDate;
            public Int32Field AssignedByInsertUserId;
            public DateTimeField AssignedByUpdateDate;
            public Int32Field AssignedByUpdateUserId;
            public Int16Field AssignedByIsActive;
            public Int32Field AssignedByUpperLevel;
            public Int32Field AssignedByUpperLevel2;
            public Int32Field AssignedByUpperLevel3;
            public Int32Field AssignedByUpperLevel4;
            public Int32Field AssignedByUpperLevel5;
            public StringField AssignedByHost;
            public Int32Field AssignedByPort;
            public BooleanField AssignedBySsl;
            public StringField AssignedByEmailId;
            public StringField AssignedByEmailPassword;
            public StringField AssignedByPhone;
            public StringField AssignedByMcsmtpServer;
            public Int32Field AssignedByMcsmtpPort;
            public StringField AssignedByMcimapServer;
            public Int32Field AssignedByMcimapPort;
            public StringField AssignedByMcUsername;
            public StringField AssignedByMcPassword;
            public StringField AssignedByStartTime;
            public StringField AssignedByEndTime;
            public Int32Field AssignedByBranchId;
            public StringField AssignedByUid;
            public BooleanField AssignedByNonOperational;

            public StringField AssignedToUsername;
            public StringField AssignedDisplayName;
            public StringField AssignedToDisplayName;
            public StringField AssignedToEmail;
            public StringField AssignedToSource;
            public StringField AssignedToPasswordHash;
            public StringField AssignedToPasswordSalt;
            public DateTimeField AssignedToLastDirectoryUpdate;
            public StringField AssignedToUserImage;
            public DateTimeField AssignedToInsertDate;
            public Int32Field AssignedToInsertUserId;
            public DateTimeField AssignedToUpdateDate;
            public Int32Field AssignedToUpdateUserId;
            public Int16Field AssignedToIsActive;
            public Int16Field AssignedIsActive;
            public Int32Field AssignedToUpperLevel;
            public Int32Field AssignedToUpperLevel2;
            public Int32Field AssignedToUpperLevel3;
            public Int32Field AssignedToUpperLevel4;
            public Int32Field AssignedToUpperLevel5;
            public StringField AssignedToHost;
            public Int32Field AssignedToPort;
            public BooleanField AssignedToSsl;
            public StringField AssignedToEmailId;
            public StringField AssignedToEmailPassword;
            public StringField AssignedToPhone;
            public StringField AssignedToMcsmtpServer;
            public Int32Field AssignedToMcsmtpPort;
            public StringField AssignedToMcimapServer;
            public Int32Field AssignedToMcimapPort;
            public StringField AssignedToMcUsername;
            public StringField AssignedToMcPassword;
            public StringField AssignedToStartTime;
            public StringField AssignedToEndTime;
            public Int32Field AssignedToBranchId;
            public StringField AssignedToUid;
            public BooleanField AssignedToNonOperational;

            public StringField Branch;
            public StringField BranchPhone;
            public StringField BranchEmail;
            public StringField BranchAddress;
            public Int32Field BranchCityId;
            public Int32Field BranchStateId;
            public StringField BranchPin;
            public Int32Field BranchCountry;

            public StringField DealerDealerName;
            public StringField DealerPhone;
            public StringField DealerEmail;
            public StringField DealerAddress;
            public Int32Field DealerCityId;
            public Int32Field DealerStateId;
            public StringField DealerPin;
            public Int32Field DealerCountry;
            public StringField DealerAdditionalInfo;

            public StringField Stage;
            public Int32Field StageType;

            public StringField InvestigationByUsername;
            public StringField InvestigationByDisplayName;
            public StringField InvestigationByEmail;
            public StringField InvestigationBySource;
            public StringField InvestigationByPasswordHash;
            public StringField InvestigationByPasswordSalt;
            public DateTimeField InvestigationByLastDirectoryUpdate;
            public StringField InvestigationByUserImage;
            public DateTimeField InvestigationByInsertDate;
            public Int32Field InvestigationByInsertUserId;
            public DateTimeField InvestigationByUpdateDate;
            public Int32Field InvestigationByUpdateUserId;
            public Int16Field InvestigationByIsActive;
            public Int32Field InvestigationByUpperLevel;
            public Int32Field InvestigationByUpperLevel2;
            public Int32Field InvestigationByUpperLevel3;
            public Int32Field InvestigationByUpperLevel4;
            public Int32Field InvestigationByUpperLevel5;
            public StringField InvestigationByHost;
            public Int32Field InvestigationByPort;
            public BooleanField InvestigationBySsl;
            public StringField InvestigationByEmailId;
            public StringField InvestigationByEmailPassword;
            public StringField InvestigationByPhone;
            public StringField InvestigationByMcsmtpServer;
            public Int32Field InvestigationByMcsmtpPort;
            public StringField InvestigationByMcimapServer;
            public Int32Field InvestigationByMcimapPort;
            public StringField InvestigationByMcUsername;
            public StringField InvestigationByMcPassword;
            public StringField InvestigationByStartTime;
            public StringField InvestigationByEndTime;
            public Int32Field InvestigationByBranchId;
            public StringField InvestigationByUid;
            public BooleanField InvestigationByNonOperational;

            public StringField ActionByUsername;
            public StringField ActionByDisplayName;
            public StringField ActionByEmail;
            public StringField ActionBySource;
            public StringField ActionByPasswordHash;
            public StringField ActionByPasswordSalt;
            public DateTimeField ActionByLastDirectoryUpdate;
            public StringField ActionByUserImage;
            public DateTimeField ActionByInsertDate;
            public Int32Field ActionByInsertUserId;
            public DateTimeField ActionByUpdateDate;
            public Int32Field ActionByUpdateUserId;
            public Int16Field ActionByIsActive;
            public Int32Field ActionByUpperLevel;
            public Int32Field ActionByUpperLevel2;
            public Int32Field ActionByUpperLevel3;
            public Int32Field ActionByUpperLevel4;
            public Int32Field ActionByUpperLevel5;
            public StringField ActionByHost;
            public Int32Field ActionByPort;
            public BooleanField ActionBySsl;
            public StringField ActionByEmailId;
            public StringField ActionByEmailPassword;
            public StringField ActionByPhone;
            public StringField ActionByMcsmtpServer;
            public Int32Field ActionByMcsmtpPort;
            public StringField ActionByMcimapServer;
            public Int32Field ActionByMcimapPort;
            public StringField ActionByMcUsername;
            public StringField ActionByMcPassword;
            public StringField ActionByStartTime;
            public StringField ActionByEndTime;
            public Int32Field ActionByBranchId;
            public StringField ActionByUid;
            public BooleanField ActionByNonOperational;

            public StringField SupervisedByUsername;
            public StringField SupervisedByDisplayName;
            public StringField SupervisedByEmail;
            public StringField SupervisedBySource;
            public StringField SupervisedByPasswordHash;
            public StringField SupervisedByPasswordSalt;
            public DateTimeField SupervisedByLastDirectoryUpdate;
            public StringField SupervisedByUserImage;
            public DateTimeField SupervisedByInsertDate;
            public Int32Field SupervisedByInsertUserId;
            public DateTimeField SupervisedByUpdateDate;
            public Int32Field SupervisedByUpdateUserId;
            public Int16Field SupervisedByIsActive;
            public Int32Field SupervisedByUpperLevel;
            public Int32Field SupervisedByUpperLevel2;
            public Int32Field SupervisedByUpperLevel3;
            public Int32Field SupervisedByUpperLevel4;
            public Int32Field SupervisedByUpperLevel5;
            public StringField SupervisedByHost;
            public Int32Field SupervisedByPort;
            public BooleanField SupervisedBySsl;
            public StringField SupervisedByEmailId;
            public StringField SupervisedByEmailPassword;
            public StringField SupervisedByPhone;
            public StringField SupervisedByMcsmtpServer;
            public Int32Field SupervisedByMcsmtpPort;
            public StringField SupervisedByMcimapServer;
            public Int32Field SupervisedByMcimapPort;
            public StringField SupervisedByMcUsername;
            public StringField SupervisedByMcPassword;
            public StringField SupervisedByStartTime;
            public StringField SupervisedByEndTime;
            public Int32Field SupervisedByBranchId;
            public StringField SupervisedByUid;
            public BooleanField SupervisedByNonOperational;

            public readonly RowListField<CMSProductsRow> Products;
            public RowListField<NoteRow> NoteList;
            public RowListField<TimelineRow> Timeline;

            public StringField EmployeeEmpCode;
            public Int32Field EmployeeDepartmentId;
            public StringField EmployeeName;
            public StringField EmployeePhone;
            public StringField EmployeeEmail;
            public StringField EmployeeAddress;
            public StringField EmployeeProfessionalEmail;
            public Int32Field EmployeeCityId;
            public Int32Field EmployeeStateId;
            public StringField EmployeePin;
            public Int32Field EmployeeCountry;
            public StringField EmployeeAdditionalInfo;
            public Int32Field EmployeeGender;
            public Int32Field EmployeeReligion;
            public Int32Field EmployeeAreaId;
            public Int32Field EmployeeMaritalStatus;
            public DateTimeField EmployeeMarriageAnniversary;
            public DateTimeField EmployeeBirthdate;
            public DateTimeField EmployeeDateOfJoining;
            public Int32Field EmployeeCompanyId;
            public Int32Field EmployeeRolesId;
            public Int32Field EmployeeOwnerId;
            public StringField EmployeeAdhaarNo;
            public StringField EmployeePanNo;
            public StringField EmployeeAttachment;
            public StringField EmployeeBankName;
            public StringField EmployeeAccountNumber;
            public StringField EmployeeIfsc;
            public StringField EmployeeBankType;
            public StringField EmployeeBranch;
            public Int32Field EmployeeTehsilId;
            public Int32Field EmployeeVillageId;
            public Int32Field Qty;
            public StringField Representative;
        }
    }
}
