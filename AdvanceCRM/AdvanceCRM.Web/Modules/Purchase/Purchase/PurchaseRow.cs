
namespace AdvanceCRM.Purchase
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Masters;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Purchase"), TableName("[dbo].[Purchase]")]
    [DisplayName("Purchase"), InstanceName("Purchase")]
    [ReadPermission("Purchase:Read")]
    [InsertPermission("Purchase:Insert")]
    [UpdatePermission("Purchase:Update")]
    [DeletePermission("Purchase:Delete")]
    [LookupScript("Purchase.PurchaseRow", Permission = "?")]
    public sealed class PurchaseRow : Row<PurchaseRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Invoice No"), Column("Invoice No"), Size(50), NotNull, QuickSearch,NameProperty]
        public String InvoiceNo
        {
            get { return Fields.InvoiceNo[this]; }
            set { Fields.InvoiceNo[this] = value; }
        }

        [DisplayName("Purchase From"), NotNull, ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jPurchaseFrom"), TextualField("PurchaseFromName")]
        [LookupEditor(typeof(ContactsRow),InplaceAdd =true)]
        public Int32? PurchaseFromId
        {
            get { return Fields.PurchaseFromId[this]; }
            set { Fields.PurchaseFromId[this] = value; }
        }

        [DisplayName("Invoice Date"), NotNull]
        public DateTime? InvoiceDate
        {
            get { return Fields.InvoiceDate[this]; }
            set { Fields.InvoiceDate[this] = value; }
        }


        [DisplayName("Status"), NotNull]
        public Masters.StatusMaster? Status
        {
            get { return (Masters.StatusMaster?)Fields.Status[this]; }
            set { Fields.Status[this] = (Int32?)value; }
        }

        [DisplayName("Type")]
        public Masters.InvoiceTypeMaster? Type
        {
            get { return (Masters.InvoiceTypeMaster?)Fields.Type[this]; }
            set { Fields.Type[this] = (Int32?)value; }
        }

        [DisplayName("Additional Info"), Size(200), TextAreaEditor(Rows = 4)]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }

        [DisplayName("Branch"), ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jBranch"), TextualField("Branch")]
        [LookupEditor("Administration.BranchLookup")]
        public Int32? BranchId
        {
            get { return Fields.BranchId[this]; }
            set { Fields.BranchId[this] = value; }
        }

        [DisplayName("Created By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername"), ReadOnly(true)]
        [LookupEditor(typeof(UserRow))]
        public Int32? OwnerId
        {
            get { return Fields.OwnerId[this]; }
            set { Fields.OwnerId[this] = value; }
        }

        [DisplayName("Assigned To"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssigned"), TextualField("AssignedUsername")]
        [LookupEditor(typeof(UserRow))]
        public Int32? AssignedId
        {
            get { return Fields.AssignedId[this]; }
            set { Fields.AssignedId[this] = value; }
        }

        [DisplayName("Reverse Charge")]
        public Boolean? ReverseCharge
        {
            get { return Fields.ReverseCharge[this]; }
            set { Fields.ReverseCharge[this] = value; }
        }

        [DisplayName("Invoice Type"), DefaultValue("1")]
        public Masters.GSTInvoiceTypeMaster? InvoiceType
        {
            get { return (Masters.GSTInvoiceTypeMaster ? )Fields.InvoiceType[this]; }
            set { Fields.InvoiceType[this] = (Int32?)value; }
        }

        [DisplayName("ITC Eligibility"), Column("ITCEligibility")]
        public Masters.GSTITCEligibilityTypeMaster? ITCEligibility
        {
            get { return (Masters.GSTITCEligibilityTypeMaster ? )Fields.ITCEligibility[this]; }
            set { Fields.ITCEligibility[this] = (Int32?)value; }
        }

        [DisplayName("Attachments"), Size(1000)]
        [MultipleImageUploadEditor(FilenameFormat = "Purchase/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachments
        {
            get { return Fields.Attachments[this]; }
            set { Fields.Attachments[this] = value; }
        }

        [DisplayName("Roundup"), DefaultValue("0")]
        public Double? Roundup
        {
            get { return Fields.Roundup[this]; }
            set { Fields.Roundup[this] = value; }
        }


       

        [DisplayName("Purchase From Contact Type"), Expression("jPurchaseFrom.[ContactType]")]
        public Int32? PurchaseFromContactType
        {
            get { return Fields.PurchaseFromContactType[this]; }
            set { Fields.PurchaseFromContactType[this] = value; }
        }

        [DisplayName("Purchase From"), Expression("jPurchaseFrom.[Name]")]
        public String PurchaseFromName
        {
            get { return Fields.PurchaseFromName[this]; }
            set { Fields.PurchaseFromName[this] = value; }
        }

        [DisplayName("Purchase From Phone"), Expression("jPurchaseFrom.[Phone]")]
        public String PurchaseFromPhone
        {
            get { return Fields.PurchaseFromPhone[this]; }
            set { Fields.PurchaseFromPhone[this] = value; }
        }

        [DisplayName("Purchase From Email"), Expression("jPurchaseFrom.[Email]")]
        public String PurchaseFromEmail
        {
            get { return Fields.PurchaseFromEmail[this]; }
            set { Fields.PurchaseFromEmail[this] = value; }
        }

        [DisplayName("Purchase From Address"), Expression("jPurchaseFrom.[Address]")]
        public String PurchaseFromAddress
        {
            get { return Fields.PurchaseFromAddress[this]; }
            set { Fields.PurchaseFromAddress[this] = value; }
        }

        [DisplayName("Purchase From City Id"), Expression("jPurchaseFrom.[CityId]")]
        public Int32? PurchaseFromCityId
        {
            get { return Fields.PurchaseFromCityId[this]; }
            set { Fields.PurchaseFromCityId[this] = value; }
        }

        [DisplayName("Purchase From State Id"), Expression("jPurchaseFrom.[StateId]")]
        public Int32? PurchaseFromStateId
        {
            get { return Fields.PurchaseFromStateId[this]; }
            set { Fields.PurchaseFromStateId[this] = value; }
        }

        [DisplayName("Purchase From Pin"), Expression("jPurchaseFrom.[Pin]")]
        public String PurchaseFromPin
        {
            get { return Fields.PurchaseFromPin[this]; }
            set { Fields.PurchaseFromPin[this] = value; }
        }

        [DisplayName("Purchase From Country"), Expression("jPurchaseFrom.[Country]")]
        public Int32? PurchaseFromCountry
        {
            get { return Fields.PurchaseFromCountry[this]; }
            set { Fields.PurchaseFromCountry[this] = value; }
        }

        [DisplayName("Purchase From Website"), Expression("jPurchaseFrom.[Website]")]
        public String PurchaseFromWebsite
        {
            get { return Fields.PurchaseFromWebsite[this]; }
            set { Fields.PurchaseFromWebsite[this] = value; }
        }

        [DisplayName("Purchase From Additional Info"), Expression("jPurchaseFrom.[AdditionalInfo]")]
        public String PurchaseFromAdditionalInfo
        {
            get { return Fields.PurchaseFromAdditionalInfo[this]; }
            set { Fields.PurchaseFromAdditionalInfo[this] = value; }
        }

        [DisplayName("Purchase From Residential Phone"), Expression("jPurchaseFrom.[ResidentialPhone]")]
        public String PurchaseFromResidentialPhone
        {
            get { return Fields.PurchaseFromResidentialPhone[this]; }
            set { Fields.PurchaseFromResidentialPhone[this] = value; }
        }

        [DisplayName("Purchase From Office Phone"), Expression("jPurchaseFrom.[OfficePhone]")]
        public String PurchaseFromOfficePhone
        {
            get { return Fields.PurchaseFromOfficePhone[this]; }
            set { Fields.PurchaseFromOfficePhone[this] = value; }
        }

        [DisplayName("Purchase From Gender"), Expression("jPurchaseFrom.[Gender]")]
        public Int32? PurchaseFromGender
        {
            get { return Fields.PurchaseFromGender[this]; }
            set { Fields.PurchaseFromGender[this] = value; }
        }

        [DisplayName("Purchase From Religion"), Expression("jPurchaseFrom.[Religion]")]
        public Int32? PurchaseFromReligion
        {
            get { return Fields.PurchaseFromReligion[this]; }
            set { Fields.PurchaseFromReligion[this] = value; }
        }

        [DisplayName("Purchase From Area Id"), Expression("jPurchaseFrom.[AreaId]")]
        public Int32? PurchaseFromAreaId
        {
            get { return Fields.PurchaseFromAreaId[this]; }
            set { Fields.PurchaseFromAreaId[this] = value; }
        }

        [DisplayName("Purchase From Marital Status"), Expression("jPurchaseFrom.[MaritalStatus]")]
        public Int32? PurchaseFromMaritalStatus
        {
            get { return Fields.PurchaseFromMaritalStatus[this]; }
            set { Fields.PurchaseFromMaritalStatus[this] = value; }
        }

        [DisplayName("Purchase From Marriage Anniversary"), Expression("jPurchaseFrom.[MarriageAnniversary]")]
        public DateTime? PurchaseFromMarriageAnniversary
        {
            get { return Fields.PurchaseFromMarriageAnniversary[this]; }
            set { Fields.PurchaseFromMarriageAnniversary[this] = value; }
        }

        [DisplayName("Purchase From Birthdate"), Expression("jPurchaseFrom.[Birthdate]")]
        public DateTime? PurchaseFromBirthdate
        {
            get { return Fields.PurchaseFromBirthdate[this]; }
            set { Fields.PurchaseFromBirthdate[this] = value; }
        }

        [DisplayName("Purchase From Date Of Incorporation"), Expression("jPurchaseFrom.[DateOfIncorporation]")]
        public DateTime? PurchaseFromDateOfIncorporation
        {
            get { return Fields.PurchaseFromDateOfIncorporation[this]; }
            set { Fields.PurchaseFromDateOfIncorporation[this] = value; }
        }

        [DisplayName("Purchase From Category Id"), Expression("jPurchaseFrom.[CategoryId]")]
        public Int32? PurchaseFromCategoryId
        {
            get { return Fields.PurchaseFromCategoryId[this]; }
            set { Fields.PurchaseFromCategoryId[this] = value; }
        }

        [DisplayName("Purchase From Grade Id"), Expression("jPurchaseFrom.[GradeId]")]
        public Int32? PurchaseFromGradeId
        {
            get { return Fields.PurchaseFromGradeId[this]; }
            set { Fields.PurchaseFromGradeId[this] = value; }
        }

        [DisplayName("Purchase From Type"), Expression("jPurchaseFrom.[Type]")]
        public Int32? PurchaseFromType
        {
            get { return Fields.PurchaseFromType[this]; }
            set { Fields.PurchaseFromType[this] = value; }
        }

        [DisplayName("Purchase From Owner Id"), Expression("jPurchaseFrom.[OwnerId]")]
        public Int32? PurchaseFromOwnerId
        {
            get { return Fields.PurchaseFromOwnerId[this]; }
            set { Fields.PurchaseFromOwnerId[this] = value; }
        }

        [DisplayName("Purchase From Assigned Id"), Expression("jPurchaseFrom.[AssignedId]")]
        public Int32? PurchaseFromAssignedId
        {
            get { return Fields.PurchaseFromAssignedId[this]; }
            set { Fields.PurchaseFromAssignedId[this] = value; }
        }

        [DisplayName("Purchase From Channel Category"), Expression("jPurchaseFrom.[ChannelCategory]")]
        public Int32? PurchaseFromChannelCategory
        {
            get { return Fields.PurchaseFromChannelCategory[this]; }
            set { Fields.PurchaseFromChannelCategory[this] = value; }
        }

        [DisplayName("Purchase From National Distributor"), Expression("jPurchaseFrom.[NationalDistributor]")]
        public Int32? PurchaseFromNationalDistributor
        {
            get { return Fields.PurchaseFromNationalDistributor[this]; }
            set { Fields.PurchaseFromNationalDistributor[this] = value; }
        }

        [DisplayName("Purchase From Stockist"), Expression("jPurchaseFrom.[Stockist]")]
        public Int32? PurchaseFromStockist
        {
            get { return Fields.PurchaseFromStockist[this]; }
            set { Fields.PurchaseFromStockist[this] = value; }
        }

        [DisplayName("Purchase From Distributor"), Expression("jPurchaseFrom.[Distributor]")]
        public Int32? PurchaseFromDistributor
        {
            get { return Fields.PurchaseFromDistributor[this]; }
            set { Fields.PurchaseFromDistributor[this] = value; }
        }

        [DisplayName("Purchase From Dealer"), Expression("jPurchaseFrom.[Dealer]")]
        public Int32? PurchaseFromDealer
        {
            get { return Fields.PurchaseFromDealer[this]; }
            set { Fields.PurchaseFromDealer[this] = value; }
        }

        [DisplayName("Purchase From Wholesaler"), Expression("jPurchaseFrom.[Wholesaler]")]
        public Int32? PurchaseFromWholesaler
        {
            get { return Fields.PurchaseFromWholesaler[this]; }
            set { Fields.PurchaseFromWholesaler[this] = value; }
        }

        [DisplayName("Purchase From Reseller"), Expression("jPurchaseFrom.[Reseller]")]
        public Int32? PurchaseFromReseller
        {
            get { return Fields.PurchaseFromReseller[this]; }
            set { Fields.PurchaseFromReseller[this] = value; }
        }

        [DisplayName("GSTIN"), Expression("jPurchaseFrom.[GSTIN]")]
        public String PurchaseFromGSTIN
        {
            get { return Fields.PurchaseFromGSTIN[this]; }
            set { Fields.PurchaseFromGSTIN[this] = value; }
        }

        [DisplayName("Purchase From Pan No"), Expression("jPurchaseFrom.[PANNo]")]
        public String PurchaseFromPanNo
        {
            get { return Fields.PurchaseFromPanNo[this]; }
            set { Fields.PurchaseFromPanNo[this] = value; }
        }

        [DisplayName("Purchase From Cc Emails"), Expression("jPurchaseFrom.[CCEmails]")]
        public String PurchaseFromCcEmails
        {
            get { return Fields.PurchaseFromCcEmails[this]; }
            set { Fields.PurchaseFromCcEmails[this] = value; }
        }

        [DisplayName("Purchase From Bcc Emails"), Expression("jPurchaseFrom.[BCCEmails]")]
        public String PurchaseFromBccEmails
        {
            get { return Fields.PurchaseFromBccEmails[this]; }
            set { Fields.PurchaseFromBccEmails[this] = value; }
        }

        //[DisplayName("Purchase From Contact Person"), Expression("jPurchaseFrom.[ContactPerson]")]
        //public String PurchaseFromContactPerson
        //{
        //    get { return Fields.PurchaseFromContactPerson[this]; }
        //    set { Fields.PurchaseFromContactPerson[this] = value; }
        //}

        //[DisplayName("Purchase From Contact Person Phone"), Expression("jPurchaseFrom.[ContactPersonPhone]")]
        //public String PurchaseFromContactPersonPhone
        //{
        //    get { return Fields.PurchaseFromContactPersonPhone[this]; }
        //    set { Fields.PurchaseFromContactPersonPhone[this] = value; }
        //}

        [DisplayName("Purchase From Attachment"), Expression("jPurchaseFrom.[Attachment]")]
        public String PurchaseFromAttachment
        {
            get { return Fields.PurchaseFromAttachment[this]; }
            set { Fields.PurchaseFromAttachment[this] = value; }
        }

        [DisplayName("Purchase From E Com GSTIN"), Expression("jPurchaseFrom.[EComGSTIN]")]
        public String PurchaseFromEComGSTIN
        {
            get { return Fields.PurchaseFromEComGSTIN[this]; }
            set { Fields.PurchaseFromEComGSTIN[this] = value; }
        }

        [DisplayName("Purchase From Creditors Opening"), Expression("jPurchaseFrom.[CreditorsOpening]")]
        public Double? PurchaseFromCreditorsOpening
        {
            get { return Fields.PurchaseFromCreditorsOpening[this]; }
            set { Fields.PurchaseFromCreditorsOpening[this] = value; }
        }

        [DisplayName("Purchase From Debtors Opening"), Expression("jPurchaseFrom.[DebtorsOpening]")]
        public Double? PurchaseFromDebtorsOpening
        {
            get { return Fields.PurchaseFromDebtorsOpening[this]; }
            set { Fields.PurchaseFromDebtorsOpening[this] = value; }
        }

        [DisplayName("Purchase From Bank Name"), Expression("jPurchaseFrom.[BankName]")]
        public String PurchaseFromBankName
        {
            get { return Fields.PurchaseFromBankName[this]; }
            set { Fields.PurchaseFromBankName[this] = value; }
        }

        [DisplayName("Purchase From Account Number"), Expression("jPurchaseFrom.[AccountNumber]")]
        public String PurchaseFromAccountNumber
        {
            get { return Fields.PurchaseFromAccountNumber[this]; }
            set { Fields.PurchaseFromAccountNumber[this] = value; }
        }

        [DisplayName("Purchase From Ifsc"), Expression("jPurchaseFrom.[IFSC]")]
        public String PurchaseFromIfsc
        {
            get { return Fields.PurchaseFromIfsc[this]; }
            set { Fields.PurchaseFromIfsc[this] = value; }
        }

        [DisplayName("Purchase From Bank Type"), Expression("jPurchaseFrom.[BankType]")]
        public String PurchaseFromBankType
        {
            get { return Fields.PurchaseFromBankType[this]; }
            set { Fields.PurchaseFromBankType[this] = value; }
        }

        [DisplayName("Purchase From Branch"), Expression("jPurchaseFrom.[Branch]")]
        public String PurchaseFromBranch
        {
            get { return Fields.PurchaseFromBranch[this]; }
            set { Fields.PurchaseFromBranch[this] = value; }
        }

        [DisplayName("Purchase From Accounts Email"), Expression("jPurchaseFrom.[AccountsEmail]")]
        public String PurchaseFromAccountsEmail
        {
            get { return Fields.PurchaseFromAccountsEmail[this]; }
            set { Fields.PurchaseFromAccountsEmail[this] = value; }
        }

        [DisplayName("Purchase From Purchase Email"), Expression("jPurchaseFrom.[PurchaseEmail]")]
        public String PurchaseFromPurchaseEmail
        {
            get { return Fields.PurchaseFromPurchaseEmail[this]; }
            set { Fields.PurchaseFromPurchaseEmail[this] = value; }
        }

        [DisplayName("Purchase From Service Email"), Expression("jPurchaseFrom.[ServiceEmail]")]
        public String PurchaseFromServiceEmail
        {
            get { return Fields.PurchaseFromServiceEmail[this]; }
            set { Fields.PurchaseFromServiceEmail[this] = value; }
        }

        [DisplayName("Purchase From Sales Email"), Expression("jPurchaseFrom.[SalesEmail]")]
        public String PurchaseFromSalesEmail
        {
            get { return Fields.PurchaseFromSalesEmail[this]; }
            set { Fields.PurchaseFromSalesEmail[this] = value; }
        }

        [DisplayName("Purchase From Credit Days"), Expression("jPurchaseFrom.[CreditDays]")]
        public Int32? PurchaseFromCreditDays
        {
            get { return Fields.PurchaseFromCreditDays[this]; }
            set { Fields.PurchaseFromCreditDays[this] = value; }
        }

        [DisplayName("Purchase From Customer Type"), Expression("jPurchaseFrom.[CustomerType]")]
        public Int32? PurchaseFromCustomerType
        {
            get { return Fields.PurchaseFromCustomerType[this]; }
            set { Fields.PurchaseFromCustomerType[this] = value; }
        }

        [DisplayName("Purchase From Trasportation Id"), Expression("jPurchaseFrom.[TrasportationId]")]
        public Int32? PurchaseFromTrasportationId
        {
            get { return Fields.PurchaseFromTrasportationId[this]; }
            set { Fields.PurchaseFromTrasportationId[this] = value; }
        }

        [DisplayName("Purchase From Tehsil Id"), Expression("jPurchaseFrom.[TehsilId]")]
        public Int32? PurchaseFromTehsilId
        {
            get { return Fields.PurchaseFromTehsilId[this]; }
            set { Fields.PurchaseFromTehsilId[this] = value; }
        }

        [DisplayName("Purchase From Village Id"), Expression("jPurchaseFrom.[VillageId]")]
        public Int32? PurchaseFromVillageId
        {
            get { return Fields.PurchaseFromVillageId[this]; }
            set { Fields.PurchaseFromVillageId[this] = value; }
        }

        [DisplayName("Purchase From Whatsapp"), Expression("jPurchaseFrom.[Whatsapp]")]
        public String PurchaseFromWhatsapp
        {
            get { return Fields.PurchaseFromWhatsapp[this]; }
            set { Fields.PurchaseFromWhatsapp[this] = value; }
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

        [DisplayName("Created By"), Expression("jOwner.[Username]")]
        public String OwnerUsername
        {
            get { return Fields.OwnerUsername[this]; }
            set { Fields.OwnerUsername[this] = value; }
        }

        [DisplayName("Owner Display Name"), Expression("jOwner.[DisplayName]")]
        public String OwnerDisplayName
        {
            get { return Fields.OwnerDisplayName[this]; }
            set { Fields.OwnerDisplayName[this] = value; }
        }

        [DisplayName("Owner Email"), Expression("jOwner.[Email]")]
        public String OwnerEmail
        {
            get { return Fields.OwnerEmail[this]; }
            set { Fields.OwnerEmail[this] = value; }
        }

        [DisplayName("Owner Source"), Expression("jOwner.[Source]")]
        public String OwnerSource
        {
            get { return Fields.OwnerSource[this]; }
            set { Fields.OwnerSource[this] = value; }
        }

        [DisplayName("Owner Password Hash"), Expression("jOwner.[PasswordHash]")]
        public String OwnerPasswordHash
        {
            get { return Fields.OwnerPasswordHash[this]; }
            set { Fields.OwnerPasswordHash[this] = value; }
        }

        [DisplayName("Owner Password Salt"), Expression("jOwner.[PasswordSalt]")]
        public String OwnerPasswordSalt
        {
            get { return Fields.OwnerPasswordSalt[this]; }
            set { Fields.OwnerPasswordSalt[this] = value; }
        }

        [DisplayName("Owner Last Directory Update"), Expression("jOwner.[LastDirectoryUpdate]")]
        public DateTime? OwnerLastDirectoryUpdate
        {
            get { return Fields.OwnerLastDirectoryUpdate[this]; }
            set { Fields.OwnerLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Owner User Image"), Expression("jOwner.[UserImage]")]
        public String OwnerUserImage
        {
            get { return Fields.OwnerUserImage[this]; }
            set { Fields.OwnerUserImage[this] = value; }
        }

        [DisplayName("Owner Insert Date"), Expression("jOwner.[InsertDate]")]
        public DateTime? OwnerInsertDate
        {
            get { return Fields.OwnerInsertDate[this]; }
            set { Fields.OwnerInsertDate[this] = value; }
        }

        [DisplayName("Owner Insert User Id"), Expression("jOwner.[InsertUserId]")]
        public Int32? OwnerInsertUserId
        {
            get { return Fields.OwnerInsertUserId[this]; }
            set { Fields.OwnerInsertUserId[this] = value; }
        }

        [DisplayName("Owner Update Date"), Expression("jOwner.[UpdateDate]")]
        public DateTime? OwnerUpdateDate
        {
            get { return Fields.OwnerUpdateDate[this]; }
            set { Fields.OwnerUpdateDate[this] = value; }
        }

        [DisplayName("Owner Update User Id"), Expression("jOwner.[UpdateUserId]")]
        public Int32? OwnerUpdateUserId
        {
            get { return Fields.OwnerUpdateUserId[this]; }
            set { Fields.OwnerUpdateUserId[this] = value; }
        }

        [DisplayName("Owner Is Active"), Expression("jOwner.[IsActive]")]
        public Int16? OwnerIsActive
        {
            get { return Fields.OwnerIsActive[this]; }
            set { Fields.OwnerIsActive[this] = value; }
        }

        [DisplayName("Owner Upper Level"), Expression("jOwner.[UpperLevel]")]
        public Int32? OwnerUpperLevel
        {
            get { return Fields.OwnerUpperLevel[this]; }
            set { Fields.OwnerUpperLevel[this] = value; }
        }

        [DisplayName("Owner Upper Level2"), Expression("jOwner.[UpperLevel2]")]
        public Int32? OwnerUpperLevel2
        {
            get { return Fields.OwnerUpperLevel2[this]; }
            set { Fields.OwnerUpperLevel2[this] = value; }
        }

        [DisplayName("Owner Upper Level3"), Expression("jOwner.[UpperLevel3]")]
        public Int32? OwnerUpperLevel3
        {
            get { return Fields.OwnerUpperLevel3[this]; }
            set { Fields.OwnerUpperLevel3[this] = value; }
        }

        [DisplayName("Owner Upper Level4"), Expression("jOwner.[UpperLevel4]")]
        public Int32? OwnerUpperLevel4
        {
            get { return Fields.OwnerUpperLevel4[this]; }
            set { Fields.OwnerUpperLevel4[this] = value; }
        }

        [DisplayName("Owner Upper Level5"), Expression("jOwner.[UpperLevel5]")]
        public Int32? OwnerUpperLevel5
        {
            get { return Fields.OwnerUpperLevel5[this]; }
            set { Fields.OwnerUpperLevel5[this] = value; }
        }

        [DisplayName("Owner Host"), Expression("jOwner.[Host]")]
        public String OwnerHost
        {
            get { return Fields.OwnerHost[this]; }
            set { Fields.OwnerHost[this] = value; }
        }

        [DisplayName("Owner Port"), Expression("jOwner.[Port]")]
        public Int32? OwnerPort
        {
            get { return Fields.OwnerPort[this]; }
            set { Fields.OwnerPort[this] = value; }
        }

        [DisplayName("Owner SSL"), Expression("jOwner.[SSL]")]
        public Boolean? OwnerSsl
        {
            get { return Fields.OwnerSsl[this]; }
            set { Fields.OwnerSsl[this] = value; }
        }

        [DisplayName("Owner Email Id"), Expression("jOwner.[EmailId]")]
        public String OwnerEmailId
        {
            get { return Fields.OwnerEmailId[this]; }
            set { Fields.OwnerEmailId[this] = value; }
        }

        [DisplayName("Owner Email Password"), Expression("jOwner.[EmailPassword]")]
        public String OwnerEmailPassword
        {
            get { return Fields.OwnerEmailPassword[this]; }
            set { Fields.OwnerEmailPassword[this] = value; }
        }

        [DisplayName("Owner Phone"), Expression("jOwner.[Phone]")]
        public String OwnerPhone
        {
            get { return Fields.OwnerPhone[this]; }
            set { Fields.OwnerPhone[this] = value; }
        }

        [DisplayName("Owner Mcsmtp Server"), Expression("jOwner.[MCSMTPServer]")]
        public String OwnerMcsmtpServer
        {
            get { return Fields.OwnerMcsmtpServer[this]; }
            set { Fields.OwnerMcsmtpServer[this] = value; }
        }

        [DisplayName("Owner Mcsmtp Port"), Expression("jOwner.[MCSMTPPort]")]
        public Int32? OwnerMcsmtpPort
        {
            get { return Fields.OwnerMcsmtpPort[this]; }
            set { Fields.OwnerMcsmtpPort[this] = value; }
        }

        [DisplayName("Owner Mcimap Server"), Expression("jOwner.[MCIMAPServer]")]
        public String OwnerMcimapServer
        {
            get { return Fields.OwnerMcimapServer[this]; }
            set { Fields.OwnerMcimapServer[this] = value; }
        }

        [DisplayName("Owner Mcimap Port"), Expression("jOwner.[MCIMAPPort]")]
        public Int32? OwnerMcimapPort
        {
            get { return Fields.OwnerMcimapPort[this]; }
            set { Fields.OwnerMcimapPort[this] = value; }
        }

        [DisplayName("Owner Mc Username"), Expression("jOwner.[MCUsername]")]
        public String OwnerMcUsername
        {
            get { return Fields.OwnerMcUsername[this]; }
            set { Fields.OwnerMcUsername[this] = value; }
        }

        [DisplayName("Owner Mc Password"), Expression("jOwner.[MCPassword]")]
        public String OwnerMcPassword
        {
            get { return Fields.OwnerMcPassword[this]; }
            set { Fields.OwnerMcPassword[this] = value; }
        }

        [DisplayName("Owner Start Time"), Expression("jOwner.[StartTime]")]
        public String OwnerStartTime
        {
            get { return Fields.OwnerStartTime[this]; }
            set { Fields.OwnerStartTime[this] = value; }
        }

        [DisplayName("Owner End Time"), Expression("jOwner.[EndTime]")]
        public String OwnerEndTime
        {
            get { return Fields.OwnerEndTime[this]; }
            set { Fields.OwnerEndTime[this] = value; }
        }

        [DisplayName("Owner Branch Id"), Expression("jOwner.[BranchId]")]
        public Int32? OwnerBranchId
        {
            get { return Fields.OwnerBranchId[this]; }
            set { Fields.OwnerBranchId[this] = value; }
        }

        [DisplayName("Owner Uid"), Expression("jOwner.[UID]")]
        public String OwnerUid
        {
            get { return Fields.OwnerUid[this]; }
            set { Fields.OwnerUid[this] = value; }
        }

        [DisplayName("Owner Non Operational"), Expression("jOwner.[NonOperational]")]
        public Boolean? OwnerNonOperational
        {
            get { return Fields.OwnerNonOperational[this]; }
            set { Fields.OwnerNonOperational[this] = value; }
        }

        [DisplayName("Assigned To"), Expression("jAssigned.[Username]")]
        public String AssignedUsername
        {
            get { return Fields.AssignedUsername[this]; }
            set { Fields.AssignedUsername[this] = value; }
        }

        [DisplayName("Assigned Display Name"), Expression("jAssigned.[DisplayName]")]
        public String AssignedDisplayName
        {
            get { return Fields.AssignedDisplayName[this]; }
            set { Fields.AssignedDisplayName[this] = value; }
        }

        [DisplayName("Assigned Email"), Expression("jAssigned.[Email]")]
        public String AssignedEmail
        {
            get { return Fields.AssignedEmail[this]; }
            set { Fields.AssignedEmail[this] = value; }
        }

        [DisplayName("Assigned Teams Id"), Expression("jAssigned.[TeamsId]")]
        [LookupEditor(typeof(TeamsRow), InplaceAdd = false)]
        public Int32? AssignedTeamsId
        {
            get { return Fields.AssignedTeamsId[this]; }
            set { Fields.AssignedTeamsId[this] = value; }
        }

        [DisplayName("Created-By Teams"), Expression("jOwner.[TeamsId]")]
        [LookupEditor(typeof(TeamsRow),InplaceAdd =false)]
        public Int32? OwnerTeamsId
        {
            get { return Fields.OwnerTeamsId[this]; }
            set { Fields.OwnerTeamsId[this] = value; }
        }

        [DisplayName("Assigned Source"), Expression("jAssigned.[Source]")]
        public String AssignedSource
        {
            get { return Fields.AssignedSource[this]; }
            set { Fields.AssignedSource[this] = value; }
        }

        [DisplayName("Assigned Password Hash"), Expression("jAssigned.[PasswordHash]")]
        public String AssignedPasswordHash
        {
            get { return Fields.AssignedPasswordHash[this]; }
            set { Fields.AssignedPasswordHash[this] = value; }
        }

        [DisplayName("Assigned Password Salt"), Expression("jAssigned.[PasswordSalt]")]
        public String AssignedPasswordSalt
        {
            get { return Fields.AssignedPasswordSalt[this]; }
            set { Fields.AssignedPasswordSalt[this] = value; }
        }

        [DisplayName("Assigned Last Directory Update"), Expression("jAssigned.[LastDirectoryUpdate]")]
        public DateTime? AssignedLastDirectoryUpdate
        {
            get { return Fields.AssignedLastDirectoryUpdate[this]; }
            set { Fields.AssignedLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Assigned User Image"), Expression("jAssigned.[UserImage]")]
        public String AssignedUserImage
        {
            get { return Fields.AssignedUserImage[this]; }
            set { Fields.AssignedUserImage[this] = value; }
        }

        [DisplayName("Assigned Insert Date"), Expression("jAssigned.[InsertDate]")]
        public DateTime? AssignedInsertDate
        {
            get { return Fields.AssignedInsertDate[this]; }
            set { Fields.AssignedInsertDate[this] = value; }
        }

        [DisplayName("Assigned Insert User Id"), Expression("jAssigned.[InsertUserId]")]
        public Int32? AssignedInsertUserId
        {
            get { return Fields.AssignedInsertUserId[this]; }
            set { Fields.AssignedInsertUserId[this] = value; }
        }

        [DisplayName("Assigned Update Date"), Expression("jAssigned.[UpdateDate]")]
        public DateTime? AssignedUpdateDate
        {
            get { return Fields.AssignedUpdateDate[this]; }
            set { Fields.AssignedUpdateDate[this] = value; }
        }

        [DisplayName("Assigned Update User Id"), Expression("jAssigned.[UpdateUserId]")]
        public Int32? AssignedUpdateUserId
        {
            get { return Fields.AssignedUpdateUserId[this]; }
            set { Fields.AssignedUpdateUserId[this] = value; }
        }

        [DisplayName("Assigned Is Active"), Expression("jAssigned.[IsActive]")]
        public Int16? AssignedIsActive
        {
            get { return Fields.AssignedIsActive[this]; }
            set { Fields.AssignedIsActive[this] = value; }
        }

        [DisplayName("Assigned Upper Level"), Expression("jAssigned.[UpperLevel]")]
        public Int32? AssignedUpperLevel
        {
            get { return Fields.AssignedUpperLevel[this]; }
            set { Fields.AssignedUpperLevel[this] = value; }
        }

        [DisplayName("Assigned Upper Level2"), Expression("jAssigned.[UpperLevel2]")]
        public Int32? AssignedUpperLevel2
        {
            get { return Fields.AssignedUpperLevel2[this]; }
            set { Fields.AssignedUpperLevel2[this] = value; }
        }

        [DisplayName("Assigned Upper Level3"), Expression("jAssigned.[UpperLevel3]")]
        public Int32? AssignedUpperLevel3
        {
            get { return Fields.AssignedUpperLevel3[this]; }
            set { Fields.AssignedUpperLevel3[this] = value; }
        }

        [DisplayName("Assigned Upper Level4"), Expression("jAssigned.[UpperLevel4]")]
        public Int32? AssignedUpperLevel4
        {
            get { return Fields.AssignedUpperLevel4[this]; }
            set { Fields.AssignedUpperLevel4[this] = value; }
        }

        [DisplayName("Assigned Upper Level5"), Expression("jAssigned.[UpperLevel5]")]
        public Int32? AssignedUpperLevel5
        {
            get { return Fields.AssignedUpperLevel5[this]; }
            set { Fields.AssignedUpperLevel5[this] = value; }
        }

        [DisplayName("Assigned Host"), Expression("jAssigned.[Host]")]
        public String AssignedHost
        {
            get { return Fields.AssignedHost[this]; }
            set { Fields.AssignedHost[this] = value; }
        }

        [DisplayName("Assigned Port"), Expression("jAssigned.[Port]")]
        public Int32? AssignedPort
        {
            get { return Fields.AssignedPort[this]; }
            set { Fields.AssignedPort[this] = value; }
        }

        [DisplayName("Assigned SSL"), Expression("jAssigned.[SSL]")]
        public Boolean? AssignedSsl
        {
            get { return Fields.AssignedSsl[this]; }
            set { Fields.AssignedSsl[this] = value; }
        }

        [DisplayName("Assigned Email Id"), Expression("jAssigned.[EmailId]")]
        public String AssignedEmailId
        {
            get { return Fields.AssignedEmailId[this]; }
            set { Fields.AssignedEmailId[this] = value; }
        }

        [DisplayName("Assigned Email Password"), Expression("jAssigned.[EmailPassword]")]
        public String AssignedEmailPassword
        {
            get { return Fields.AssignedEmailPassword[this]; }
            set { Fields.AssignedEmailPassword[this] = value; }
        }

        [DisplayName("Assigned Phone"), Expression("jAssigned.[Phone]")]
        public String AssignedPhone
        {
            get { return Fields.AssignedPhone[this]; }
            set { Fields.AssignedPhone[this] = value; }
        }

        [DisplayName("Assigned Mcsmtp Server"), Expression("jAssigned.[MCSMTPServer]")]
        public String AssignedMcsmtpServer
        {
            get { return Fields.AssignedMcsmtpServer[this]; }
            set { Fields.AssignedMcsmtpServer[this] = value; }
        }

        [DisplayName("Assigned Mcsmtp Port"), Expression("jAssigned.[MCSMTPPort]")]
        public Int32? AssignedMcsmtpPort
        {
            get { return Fields.AssignedMcsmtpPort[this]; }
            set { Fields.AssignedMcsmtpPort[this] = value; }
        }

        [DisplayName("Assigned Mcimap Server"), Expression("jAssigned.[MCIMAPServer]")]
        public String AssignedMcimapServer
        {
            get { return Fields.AssignedMcimapServer[this]; }
            set { Fields.AssignedMcimapServer[this] = value; }
        }

        [DisplayName("Assigned Mcimap Port"), Expression("jAssigned.[MCIMAPPort]")]
        public Int32? AssignedMcimapPort
        {
            get { return Fields.AssignedMcimapPort[this]; }
            set { Fields.AssignedMcimapPort[this] = value; }
        }

        [DisplayName("Assigned Mc Username"), Expression("jAssigned.[MCUsername]")]
        public String AssignedMcUsername
        {
            get { return Fields.AssignedMcUsername[this]; }
            set { Fields.AssignedMcUsername[this] = value; }
        }

        [DisplayName("Assigned Mc Password"), Expression("jAssigned.[MCPassword]")]
        public String AssignedMcPassword
        {
            get { return Fields.AssignedMcPassword[this]; }
            set { Fields.AssignedMcPassword[this] = value; }
        }

        [DisplayName("Assigned Start Time"), Expression("jAssigned.[StartTime]")]
        public String AssignedStartTime
        {
            get { return Fields.AssignedStartTime[this]; }
            set { Fields.AssignedStartTime[this] = value; }
        }

        [DisplayName("Assigned End Time"), Expression("jAssigned.[EndTime]")]
        public String AssignedEndTime
        {
            get { return Fields.AssignedEndTime[this]; }
            set { Fields.AssignedEndTime[this] = value; }
        }

        [DisplayName("Assigned Branch Id"), Expression("jAssigned.[BranchId]")]
        public Int32? AssignedBranchId
        {
            get { return Fields.AssignedBranchId[this]; }
            set { Fields.AssignedBranchId[this] = value; }
        }

        [DisplayName("Assigned Uid"), Expression("jAssigned.[UID]")]
        public String AssignedUid
        {
            get { return Fields.AssignedUid[this]; }
            set { Fields.AssignedUid[this] = value; }
        }

        [DisplayName("Assigned Non Operational"), Expression("jAssigned.[NonOperational]")]
        public Boolean? AssignedNonOperational
        {
            get { return Fields.AssignedNonOperational[this]; }
            set { Fields.AssignedNonOperational[this] = value; }
        }


        [DisplayName("Total"), Expression("(SELECT SUM((((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) + (((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) * (Percentage1 / 100)) + (((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) * (Percentage2 / 100)))) FROM PurchaseProducts WHERE PurchaseId=t0.[Id]) + t0.[Roundup]")]
        public Double? Total
        {
            get { return Fields.Total[this]; }
            set { Fields.Total[this] = value; }
        }

        [MasterDetailRelation(foreignKey: "PurchaseId", IncludeColumns = "ProductsName")]
        [DisplayName("Products"), NotMapped]
        public List<PurchaseProductsRow> Products { get { return Fields.Products[this]; } set { Fields.Products[this] = value; } }

        [DisplayName("Approved By"), Expression("jApprovedBy.[Username]")]
        public String ApprovedByUsername
        {
            get { return Fields.ApprovedByUsername[this]; }
            set { Fields.ApprovedByUsername[this] = value; }
        }

        [DisplayName("Approved By Display Name"), Expression("jApprovedBy.[DisplayName]")]
        public String ApprovedByDisplayName
        {
            get { return Fields.ApprovedByDisplayName[this]; }
            set { Fields.ApprovedByDisplayName[this] = value; }
        }

        [DisplayName("Approved By Email"), Expression("jApprovedBy.[Email]")]
        public String ApprovedByEmail
        {
            get { return Fields.ApprovedByEmail[this]; }
            set { Fields.ApprovedByEmail[this] = value; }
        }

        [DisplayName("Approved By Source"), Expression("jApprovedBy.[Source]")]
        public String ApprovedBySource
        {
            get { return Fields.ApprovedBySource[this]; }
            set { Fields.ApprovedBySource[this] = value; }
        }

        [DisplayName("Approved By Password Hash"), Expression("jApprovedBy.[PasswordHash]")]
        public String ApprovedByPasswordHash
        {
            get { return Fields.ApprovedByPasswordHash[this]; }
            set { Fields.ApprovedByPasswordHash[this] = value; }
        }

        [DisplayName("Approved By Password Salt"), Expression("jApprovedBy.[PasswordSalt]")]
        public String ApprovedByPasswordSalt
        {
            get { return Fields.ApprovedByPasswordSalt[this]; }
            set { Fields.ApprovedByPasswordSalt[this] = value; }
        }

        [DisplayName("Approved By Last Directory Update"), Expression("jApprovedBy.[LastDirectoryUpdate]")]
        public DateTime? ApprovedByLastDirectoryUpdate
        {
            get { return Fields.ApprovedByLastDirectoryUpdate[this]; }
            set { Fields.ApprovedByLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Approved By User Image"), Expression("jApprovedBy.[UserImage]")]
        public String ApprovedByUserImage
        {
            get { return Fields.ApprovedByUserImage[this]; }
            set { Fields.ApprovedByUserImage[this] = value; }
        }

        [DisplayName("Approved By Insert Date"), Expression("jApprovedBy.[InsertDate]")]
        public DateTime? ApprovedByInsertDate
        {
            get { return Fields.ApprovedByInsertDate[this]; }
            set { Fields.ApprovedByInsertDate[this] = value; }
        }

        [DisplayName("Approved By Insert User Id"), Expression("jApprovedBy.[InsertUserId]")]
        public Int32? ApprovedByInsertUserId
        {
            get { return Fields.ApprovedByInsertUserId[this]; }
            set { Fields.ApprovedByInsertUserId[this] = value; }
        }

        [DisplayName("Approved By Update Date"), Expression("jApprovedBy.[UpdateDate]")]
        public DateTime? ApprovedByUpdateDate
        {
            get { return Fields.ApprovedByUpdateDate[this]; }
            set { Fields.ApprovedByUpdateDate[this] = value; }
        }

        [DisplayName("Approved By Update User Id"), Expression("jApprovedBy.[UpdateUserId]")]
        public Int32? ApprovedByUpdateUserId
        {
            get { return Fields.ApprovedByUpdateUserId[this]; }
            set { Fields.ApprovedByUpdateUserId[this] = value; }
        }

        [DisplayName("Approved By Is Active"), Expression("jApprovedBy.[IsActive]")]
        public Int16? ApprovedByIsActive
        {
            get { return Fields.ApprovedByIsActive[this]; }
            set { Fields.ApprovedByIsActive[this] = value; }
        }

        [DisplayName("Approved By Upper Level"), Expression("jApprovedBy.[UpperLevel]")]
        public Int32? ApprovedByUpperLevel
        {
            get { return Fields.ApprovedByUpperLevel[this]; }
            set { Fields.ApprovedByUpperLevel[this] = value; }
        }

        [DisplayName("Approved By Upper Level2"), Expression("jApprovedBy.[UpperLevel2]")]
        public Int32? ApprovedByUpperLevel2
        {
            get { return Fields.ApprovedByUpperLevel2[this]; }
            set { Fields.ApprovedByUpperLevel2[this] = value; }
        }

        [DisplayName("Approved By Upper Level3"), Expression("jApprovedBy.[UpperLevel3]")]
        public Int32? ApprovedByUpperLevel3
        {
            get { return Fields.ApprovedByUpperLevel3[this]; }
            set { Fields.ApprovedByUpperLevel3[this] = value; }
        }

        [DisplayName("Approved By Upper Level4"), Expression("jApprovedBy.[UpperLevel4]")]
        public Int32? ApprovedByUpperLevel4
        {
            get { return Fields.ApprovedByUpperLevel4[this]; }
            set { Fields.ApprovedByUpperLevel4[this] = value; }
        }

        [DisplayName("Approved By Upper Level5"), Expression("jApprovedBy.[UpperLevel5]")]
        public Int32? ApprovedByUpperLevel5
        {
            get { return Fields.ApprovedByUpperLevel5[this]; }
            set { Fields.ApprovedByUpperLevel5[this] = value; }
        }

        [DisplayName("Approved By Host"), Expression("jApprovedBy.[Host]")]
        public String ApprovedByHost
        {
            get { return Fields.ApprovedByHost[this]; }
            set { Fields.ApprovedByHost[this] = value; }
        }

        [DisplayName("Approved By Port"), Expression("jApprovedBy.[Port]")]
        public Int32? ApprovedByPort
        {
            get { return Fields.ApprovedByPort[this]; }
            set { Fields.ApprovedByPort[this] = value; }
        }

        [DisplayName("Approved By SSL"), Expression("jApprovedBy.[SSL]")]
        public Boolean? ApprovedBySsl
        {
            get { return Fields.ApprovedBySsl[this]; }
            set { Fields.ApprovedBySsl[this] = value; }
        }

        [DisplayName("Approved By Email Id"), Expression("jApprovedBy.[EmailId]")]
        public String ApprovedByEmailId
        {
            get { return Fields.ApprovedByEmailId[this]; }
            set { Fields.ApprovedByEmailId[this] = value; }
        }

        [DisplayName("Approved By Email Password"), Expression("jApprovedBy.[EmailPassword]")]
        public String ApprovedByEmailPassword
        {
            get { return Fields.ApprovedByEmailPassword[this]; }
            set { Fields.ApprovedByEmailPassword[this] = value; }
        }

        [DisplayName("Approved By Phone"), Expression("jApprovedBy.[Phone]")]
        public String ApprovedByPhone
        {
            get { return Fields.ApprovedByPhone[this]; }
            set { Fields.ApprovedByPhone[this] = value; }
        }

        [DisplayName("Approved By Mcsmtp Server"), Expression("jApprovedBy.[MCSMTPServer]")]
        public String ApprovedByMcsmtpServer
        {
            get { return Fields.ApprovedByMcsmtpServer[this]; }
            set { Fields.ApprovedByMcsmtpServer[this] = value; }
        }

        [DisplayName("Approved By Mcsmtp Port"), Expression("jApprovedBy.[MCSMTPPort]")]
        public Int32? ApprovedByMcsmtpPort
        {
            get { return Fields.ApprovedByMcsmtpPort[this]; }
            set { Fields.ApprovedByMcsmtpPort[this] = value; }
        }

        [DisplayName("Approved By Mcimap Server"), Expression("jApprovedBy.[MCIMAPServer]")]
        public String ApprovedByMcimapServer
        {
            get { return Fields.ApprovedByMcimapServer[this]; }
            set { Fields.ApprovedByMcimapServer[this] = value; }
        }

        [DisplayName("Approved By Mcimap Port"), Expression("jApprovedBy.[MCIMAPPort]")]
        public Int32? ApprovedByMcimapPort
        {
            get { return Fields.ApprovedByMcimapPort[this]; }
            set { Fields.ApprovedByMcimapPort[this] = value; }
        }

        [DisplayName("Approved By Mc Username"), Expression("jApprovedBy.[MCUsername]")]
        public String ApprovedByMcUsername
        {
            get { return Fields.ApprovedByMcUsername[this]; }
            set { Fields.ApprovedByMcUsername[this] = value; }
        }

        [DisplayName("Approved By Mc Password"), Expression("jApprovedBy.[MCPassword]")]
        public String ApprovedByMcPassword
        {
            get { return Fields.ApprovedByMcPassword[this]; }
            set { Fields.ApprovedByMcPassword[this] = value; }
        }

        [DisplayName("Approved By Start Time"), Expression("jApprovedBy.[StartTime]")]
        public String ApprovedByStartTime
        {
            get { return Fields.ApprovedByStartTime[this]; }
            set { Fields.ApprovedByStartTime[this] = value; }
        }

        [DisplayName("Approved By End Time"), Expression("jApprovedBy.[EndTime]")]
        public String ApprovedByEndTime
        {
            get { return Fields.ApprovedByEndTime[this]; }
            set { Fields.ApprovedByEndTime[this] = value; }
        }

        [DisplayName("Approved By Branch Id"), Expression("jApprovedBy.[BranchId]")]
        public Int32? ApprovedByBranchId
        {
            get { return Fields.ApprovedByBranchId[this]; }
            set { Fields.ApprovedByBranchId[this] = value; }
        }

        [DisplayName("Approved By Uid"), Expression("jApprovedBy.[UID]")]
        public String ApprovedByUid
        {
            get { return Fields.ApprovedByUid[this]; }
            set { Fields.ApprovedByUid[this] = value; }
        }

        [DisplayName("Approved By Non Operational"), Expression("jApprovedBy.[NonOperational]")]
        public Boolean? ApprovedByNonOperational
        {
            get { return Fields.ApprovedByNonOperational[this]; }
            set { Fields.ApprovedByNonOperational[this] = value; }
        }


        [DisplayName("Approved By"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jApprovedBy"), TextualField("ApprovedByUsername")]
        [UserEditor, QuickFilter]
        public Int32? ApprovedBy
        {
            get { return Fields.ApprovedBy[this]; }
            set { Fields.ApprovedBy[this] = value; }
        }



        [NotesEditor, NotMapped]
        [DisplayName("")]
        public List<NoteRow> NoteList
        {
            get { return Fields.NoteList[this]; }
            set { Fields.NoteList[this] = value; }
        }

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), LookupInclude]
        [LookupEditor(typeof(CompanyDetailsRow), InplaceAdd = false)]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }
        //public Int32Field CompanyIdField
        //{
        //    get { return Fields.CompanyId; }
        //}
        [DisplayName("Quotation No")]
        public Int32? QuotationNo
        {
            get { return Fields.QuotationNo[this]; }
            set { Fields.QuotationNo[this] = value; }
        }

      

        public PurchaseRow()
            : base(Fields)
        {
        }
        
        public PurchaseRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField InvoiceNo;
            public Int32Field PurchaseFromId;
            public DateTimeField InvoiceDate;
            public DoubleField Total;
            public Int32Field Status;
            public Int32Field Type;
            public StringField AdditionalInfo;
            public Int32Field BranchId;
            public Int32Field OwnerId;
            public Int32Field AssignedId;
            public BooleanField ReverseCharge;
            public Int32Field InvoiceType;
            public Int32Field ITCEligibility;
            public StringField Attachments;
            public DoubleField Roundup;

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
            public StringField PurchaseFromGSTIN;
            public StringField PurchaseFromPanNo;
            public StringField PurchaseFromCcEmails;
            public StringField PurchaseFromBccEmails;
            //public StringField PurchaseFromContactPerson;
            //public StringField PurchaseFromContactPersonPhone;
            public StringField PurchaseFromAttachment;
            public StringField PurchaseFromEComGSTIN;
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

            public StringField Branch;
            public StringField BranchPhone;
            public StringField BranchEmail;
            public StringField BranchAddress;
            public Int32Field BranchCityId;
            public Int32Field BranchStateId;
            public StringField BranchPin;
            public Int32Field BranchCountry;

            public StringField OwnerUsername;
            public StringField OwnerDisplayName;
            public StringField OwnerEmail;
            public StringField OwnerSource;
            public StringField OwnerPasswordHash;
            public StringField OwnerPasswordSalt;
            public DateTimeField OwnerLastDirectoryUpdate;
            public StringField OwnerUserImage;
            public DateTimeField OwnerInsertDate;
            public Int32Field OwnerInsertUserId;
            public DateTimeField OwnerUpdateDate;
            public Int32Field OwnerUpdateUserId;
            public Int16Field OwnerIsActive;
            public Int32Field OwnerUpperLevel;
            public Int32Field OwnerUpperLevel2;
            public Int32Field OwnerUpperLevel3;
            public Int32Field OwnerUpperLevel4;
            public Int32Field OwnerUpperLevel5;
            public StringField OwnerHost;
            public Int32Field OwnerPort;
            public BooleanField OwnerSsl;
            public StringField OwnerEmailId;
            public StringField OwnerEmailPassword;
            public StringField OwnerPhone;
            public StringField OwnerMcsmtpServer;
            public Int32Field OwnerMcsmtpPort;
            public StringField OwnerMcimapServer;
            public Int32Field OwnerMcimapPort;
            public StringField OwnerMcUsername;
            public StringField OwnerMcPassword;
            public StringField OwnerStartTime;
            public StringField OwnerEndTime;
            public Int32Field OwnerBranchId;
            public StringField OwnerUid;
            public BooleanField OwnerNonOperational;

            public StringField AssignedUsername;
            public StringField AssignedDisplayName;
            public StringField AssignedEmail;
            public StringField AssignedSource;
            public StringField AssignedPasswordHash;
            public StringField AssignedPasswordSalt;
            public DateTimeField AssignedLastDirectoryUpdate;
            public StringField AssignedUserImage;
            public DateTimeField AssignedInsertDate;
            public Int32Field AssignedInsertUserId;
            public DateTimeField AssignedUpdateDate;
            public Int32Field AssignedUpdateUserId;
            public Int16Field AssignedIsActive;
            public Int32Field AssignedUpperLevel;
            public Int32Field AssignedUpperLevel2;
            public Int32Field AssignedUpperLevel3;
            public Int32Field AssignedUpperLevel4;
            public Int32Field AssignedUpperLevel5;
            public StringField AssignedHost;
            public Int32Field AssignedPort;
            public BooleanField AssignedSsl;
            public StringField AssignedEmailId;
            public StringField AssignedEmailPassword;
            public StringField AssignedPhone;
            public StringField AssignedMcsmtpServer;
            public Int32Field AssignedMcsmtpPort;
            public StringField AssignedMcimapServer;
            public Int32Field AssignedMcimapPort;
            public StringField AssignedMcUsername;
            public StringField AssignedMcPassword;
            public StringField AssignedStartTime;
            public StringField AssignedEndTime;
            public Int32Field AssignedBranchId;
            public StringField AssignedUid;
            public BooleanField AssignedNonOperational;

            public StringField ApprovedByUsername;
            public StringField ApprovedByDisplayName;
            public StringField ApprovedByEmail;
            public StringField ApprovedBySource;
            public StringField ApprovedByPasswordHash;
            public StringField ApprovedByPasswordSalt;
            public DateTimeField ApprovedByLastDirectoryUpdate;
            public StringField ApprovedByUserImage;
            public DateTimeField ApprovedByInsertDate;
            public Int32Field ApprovedByInsertUserId;
            public DateTimeField ApprovedByUpdateDate;
            public Int32Field ApprovedByUpdateUserId;
            public Int16Field ApprovedByIsActive;
            public Int32Field ApprovedByUpperLevel;
            public Int32Field ApprovedByUpperLevel2;
            public Int32Field ApprovedByUpperLevel3;
            public Int32Field ApprovedByUpperLevel4;
            public Int32Field ApprovedByUpperLevel5;
            public StringField ApprovedByHost;
            public Int32Field ApprovedByPort;
            public BooleanField ApprovedBySsl;
            public StringField ApprovedByEmailId;
            public StringField ApprovedByEmailPassword;
            public StringField ApprovedByPhone;
            public StringField ApprovedByMcsmtpServer;
            public Int32Field ApprovedByMcsmtpPort;
            public StringField ApprovedByMcimapServer;
            public Int32Field ApprovedByMcimapPort;
            public StringField ApprovedByMcUsername;
            public StringField ApprovedByMcPassword;
            public StringField ApprovedByStartTime;
            public StringField ApprovedByEndTime;
            public Int32Field ApprovedByBranchId;
            public StringField ApprovedByUid;
            public BooleanField ApprovedByNonOperational;

            public Int32Field ApprovedBy;

            public readonly RowListField<PurchaseProductsRow> Products;
            public RowListField<NoteRow> NoteList;

            public Int32Field OwnerTeamsId; 
            public Int32Field AssignedTeamsId;

            public Int32Field CompanyId;
            public Int32Field QuotationNo;
        }
    }
}
