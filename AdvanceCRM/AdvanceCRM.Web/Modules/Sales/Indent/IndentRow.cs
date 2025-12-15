using AdvanceCRM.Contacts;
using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace AdvanceCRM.Sales
{
    [ConnectionKey("Default"), Module("Sales"), TableName("[dbo].[Indent]")]
    [DisplayName("Indent"), InstanceName("Indent")]
    [ReadPermission("Indent:Read")]
    [InsertPermission("Indent:Insert")]
    [UpdatePermission("Indent:Update")]
    [DeletePermission("Indent:Delete")]
    [LookupScript("Sales.Indent", Permission = "?")]
    public sealed class IndentRow : Row<IndentRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity]
        public Int32? Id
        {
            get => fields.Id[this];
            set => fields.Id[this] = value;
        }

        [DisplayName("Contacts"), NotNull, ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jContacts"), TextualField("Contacts Name")]
        [LookupEditor(typeof(ContactsRow), InplaceAdd = true)]
        public Int32? ContactsId
        {
            get => fields.ContactsId[this];
            set => fields.ContactsId[this] = value;
        }

        [DisplayName("Date"), NotNull]
        public DateTime? Date
        {
            get => fields.Date[this];
            set => fields.Date[this] = value;
        }

        [DisplayName("Status"), NotNull]
        public Masters.StatusMaster? Status
        {
            get { return (Masters.StatusMaster?)Fields.Status[this]; }
            set { Fields.Status[this] = (Int32?)value; }
        }

        [DisplayName("Additional Info"), Size(200), QuickSearch, NameProperty]
        public String AdditionalInfo
        {
            get => fields.AdditionalInfo[this];
            set => fields.AdditionalInfo[this] = value;
        }

        [DisplayName("Invoice No"), Size(100)]
        public String InvoiceNo
        {
            get => fields.InvoiceNo[this];
            set => fields.InvoiceNo[this] = value;
        }

        [DisplayName("Branch"), ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jBranch"), TextualField("Branch")]
        [LookupEditor("Administration.BranchLookup")]
        public Int32? BranchId
        {
            get => fields.BranchId[this];
            set => fields.BranchId[this] = value;
        }

        [DisplayName("Owner"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername")]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get => fields.OwnerId[this];
            set => fields.OwnerId[this] = value;
        }

        [DisplayName("Assigned"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssigned"), TextualField("AssignedUsername")]
        [Administration.UserEditor]
        public Int32? AssignedId
        {
            get => fields.AssignedId[this];
            set => fields.AssignedId[this] = value;
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

        [DisplayName("Contact Type"), Expression("jContacts.[ContactType]")]
        public Int32? ContactsContactType
        {
            get => fields.ContactsContactType[this];
            set => fields.ContactsContactType[this] = value;
        }

        [DisplayName("Contacts Name"), Expression("jContacts.[Name]"), Size(200), QuickSearch]
        public string ContactsName 
        { 
            get => fields.ContactsName[this]; 
            set => fields.ContactsName[this] = value; 
        }


        [DisplayName("Contacts Phone"), Expression("jContacts.[Phone]")]
        public String ContactsPhone
        {
            get => fields.ContactsPhone[this];
            set => fields.ContactsPhone[this] = value;
        }

        [DisplayName("Contacts Email"), Expression("jContacts.[Email]")]
        public String ContactsEmail
        {
            get => fields.ContactsEmail[this];
            set => fields.ContactsEmail[this] = value;
        }

        [DisplayName("Contacts Address"), Expression("jContacts.[Address]")]
        public String ContactsAddress
        {
            get => fields.ContactsAddress[this];
            set => fields.ContactsAddress[this] = value;
        }

        [DisplayName("Contacts City Id"), Expression("jContacts.[CityId]")]
        public Int32? ContactsCityId
        {
            get => fields.ContactsCityId[this];
            set => fields.ContactsCityId[this] = value;
        }

        [DisplayName("Contacts State Id"), Expression("jContacts.[StateId]")]
        public Int32? ContactsStateId
        {
            get => fields.ContactsStateId[this];
            set => fields.ContactsStateId[this] = value;
        }

        [DisplayName("Contacts Pin"), Expression("jContacts.[Pin]")]
        public String ContactsPin
        {
            get => fields.ContactsPin[this];
            set => fields.ContactsPin[this] = value;
        }

        [DisplayName("Contacts Country"), Expression("jContacts.[Country]")]
        public Int32? ContactsCountry
        {
            get => fields.ContactsCountry[this];
            set => fields.ContactsCountry[this] = value;
        }

        [DisplayName("Contacts Website"), Expression("jContacts.[Website]")]
        public String ContactsWebsite
        {
            get => fields.ContactsWebsite[this];
            set => fields.ContactsWebsite[this] = value;
        }

        [DisplayName("Contacts Additional Info"), Expression("jContacts.[AdditionalInfo]")]
        public String ContactsAdditionalInfo
        {
            get => fields.ContactsAdditionalInfo[this];
            set => fields.ContactsAdditionalInfo[this] = value;
        }

        [DisplayName("Contacts Residential Phone"), Expression("jContacts.[ResidentialPhone]")]
        public String ContactsResidentialPhone
        {
            get => fields.ContactsResidentialPhone[this];
            set => fields.ContactsResidentialPhone[this] = value;
        }

        [DisplayName("Contacts Office Phone"), Expression("jContacts.[OfficePhone]")]
        public String ContactsOfficePhone
        {
            get => fields.ContactsOfficePhone[this];
            set => fields.ContactsOfficePhone[this] = value;
        }

        [DisplayName("Contacts Gender"), Expression("jContacts.[Gender]")]
        public Int32? ContactsGender
        {
            get => fields.ContactsGender[this];
            set => fields.ContactsGender[this] = value;
        }

        [DisplayName("Contacts Religion"), Expression("jContacts.[Religion]")]
        public Int32? ContactsReligion
        {
            get => fields.ContactsReligion[this];
            set => fields.ContactsReligion[this] = value;
        }

        [DisplayName("Contacts Area Id"), Expression("jContacts.[AreaId]")]
        public Int32? ContactsAreaId
        {
            get => fields.ContactsAreaId[this];
            set => fields.ContactsAreaId[this] = value;
        }

        [DisplayName("Contacts Marital Status"), Expression("jContacts.[MaritalStatus]")]
        public Int32? ContactsMaritalStatus
        {
            get => fields.ContactsMaritalStatus[this];
            set => fields.ContactsMaritalStatus[this] = value;
        }

        [DisplayName("Contacts Marriage Anniversary"), Expression("jContacts.[MarriageAnniversary]")]
        public DateTime? ContactsMarriageAnniversary
        {
            get => fields.ContactsMarriageAnniversary[this];
            set => fields.ContactsMarriageAnniversary[this] = value;
        }

        [DisplayName("Contacts Birthdate"), Expression("jContacts.[Birthdate]")]
        public DateTime? ContactsBirthdate
        {
            get => fields.ContactsBirthdate[this];
            set => fields.ContactsBirthdate[this] = value;
        }

        [DisplayName("Contacts Date Of Incorporation"), Expression("jContacts.[DateOfIncorporation]")]
        public DateTime? ContactsDateOfIncorporation
        {
            get => fields.ContactsDateOfIncorporation[this];
            set => fields.ContactsDateOfIncorporation[this] = value;
        }

        [DisplayName("Contacts Category Id"), Expression("jContacts.[CategoryId]")]
        public Int32? ContactsCategoryId
        {
            get => fields.ContactsCategoryId[this];
            set => fields.ContactsCategoryId[this] = value;
        }

        [DisplayName("Contacts Grade Id"), Expression("jContacts.[GradeId]")]
        public Int32? ContactsGradeId
        {
            get => fields.ContactsGradeId[this];
            set => fields.ContactsGradeId[this] = value;
        }

        [DisplayName("Contacts Type"), Expression("jContacts.[Type]")]
        public Int32? ContactsType
        {
            get => fields.ContactsType[this];
            set => fields.ContactsType[this] = value;
        }

        [DisplayName("Contacts Owner Id"), Expression("jContacts.[OwnerId]")]
        public Int32? ContactsOwnerId
        {
            get => fields.ContactsOwnerId[this];
            set => fields.ContactsOwnerId[this] = value;
        }

        [DisplayName("Contacts Assigned Id"), Expression("jContacts.[AssignedId]")]
        public Int32? ContactsAssignedId
        {
            get => fields.ContactsAssignedId[this];
            set => fields.ContactsAssignedId[this] = value;
        }

        [DisplayName("Contacts Channel Category"), Expression("jContacts.[ChannelCategory]")]
        public Int32? ContactsChannelCategory
        {
            get => fields.ContactsChannelCategory[this];
            set => fields.ContactsChannelCategory[this] = value;
        }

        [DisplayName("Contacts National Distributor"), Expression("jContacts.[NationalDistributor]")]
        public Int32? ContactsNationalDistributor
        {
            get => fields.ContactsNationalDistributor[this];
            set => fields.ContactsNationalDistributor[this] = value;
        }

        [DisplayName("Contacts Stockist"), Expression("jContacts.[Stockist]")]
        public Int32? ContactsStockist
        {
            get => fields.ContactsStockist[this];
            set => fields.ContactsStockist[this] = value;
        }

        [DisplayName("Contacts Distributor"), Expression("jContacts.[Distributor]")]
        public Int32? ContactsDistributor
        {
            get => fields.ContactsDistributor[this];
            set => fields.ContactsDistributor[this] = value;
        }

        [DisplayName("Contacts Dealer"), Expression("jContacts.[Dealer]")]
        public Int32? ContactsDealer
        {
            get => fields.ContactsDealer[this];
            set => fields.ContactsDealer[this] = value;
        }

        [DisplayName("Contacts Wholesaler"), Expression("jContacts.[Wholesaler]")]
        public Int32? ContactsWholesaler
        {
            get => fields.ContactsWholesaler[this];
            set => fields.ContactsWholesaler[this] = value;
        }

        [DisplayName("Contacts Reseller"), Expression("jContacts.[Reseller]")]
        public Int32? ContactsReseller
        {
            get => fields.ContactsReseller[this];
            set => fields.ContactsReseller[this] = value;
        }

        [DisplayName("Contacts Gstin"), Expression("jContacts.[GSTIN]")]
        public String ContactsGstin
        {
            get => fields.ContactsGstin[this];
            set => fields.ContactsGstin[this] = value;
        }

        [DisplayName("Contacts Pan No"), Expression("jContacts.[PANNo]")]
        public String ContactsPanNo
        {
            get => fields.ContactsPanNo[this];
            set => fields.ContactsPanNo[this] = value;
        }

        [DisplayName("Contacts Cc Emails"), Expression("jContacts.[CCEmails]")]
        public String ContactsCcEmails
        {
            get => fields.ContactsCcEmails[this];
            set => fields.ContactsCcEmails[this] = value;
        }

        [DisplayName("Contacts Bcc Emails"), Expression("jContacts.[BCCEmails]")]
        public String ContactsBccEmails
        {
            get => fields.ContactsBccEmails[this];
            set => fields.ContactsBccEmails[this] = value;
        }

        [DisplayName("Contacts Attachment"), Expression("jContacts.[Attachment]")]
        public String ContactsAttachment
        {
            get => fields.ContactsAttachment[this];
            set => fields.ContactsAttachment[this] = value;
        }

        [DisplayName("Contacts E Com Gstin"), Expression("jContacts.[EComGSTIN]")]
        public String ContactsEComGstin
        {
            get => fields.ContactsEComGstin[this];
            set => fields.ContactsEComGstin[this] = value;
        }

        [DisplayName("Contacts Creditors Opening"), Expression("jContacts.[CreditorsOpening]")]
        public Double? ContactsCreditorsOpening
        {
            get => fields.ContactsCreditorsOpening[this];
            set => fields.ContactsCreditorsOpening[this] = value;
        }

        [DisplayName("Contacts Debtors Opening"), Expression("jContacts.[DebtorsOpening]")]
        public Double? ContactsDebtorsOpening
        {
            get => fields.ContactsDebtorsOpening[this];
            set => fields.ContactsDebtorsOpening[this] = value;
        }

        [DisplayName("Contacts Bank Name"), Expression("jContacts.[BankName]")]
        public String ContactsBankName
        {
            get => fields.ContactsBankName[this];
            set => fields.ContactsBankName[this] = value;
        }

        [DisplayName("Contacts Account Number"), Expression("jContacts.[AccountNumber]")]
        public String ContactsAccountNumber
        {
            get => fields.ContactsAccountNumber[this];
            set => fields.ContactsAccountNumber[this] = value;
        }

        [DisplayName("Contacts Ifsc"), Expression("jContacts.[IFSC]")]
        public String ContactsIfsc
        {
            get => fields.ContactsIfsc[this];
            set => fields.ContactsIfsc[this] = value;
        }

        [DisplayName("Contacts Bank Type"), Expression("jContacts.[BankType]")]
        public String ContactsBankType
        {
            get => fields.ContactsBankType[this];
            set => fields.ContactsBankType[this] = value;
        }

        [DisplayName("Contacts Branch"), Expression("jContacts.[Branch]")]
        public String ContactsBranch
        {
            get => fields.ContactsBranch[this];
            set => fields.ContactsBranch[this] = value;
        }

        [DisplayName("Contacts Accounts Email"), Expression("jContacts.[AccountsEmail]")]
        public String ContactsAccountsEmail
        {
            get => fields.ContactsAccountsEmail[this];
            set => fields.ContactsAccountsEmail[this] = value;
        }

        [DisplayName("Contacts Purchase Email"), Expression("jContacts.[PurchaseEmail]")]
        public String ContactsPurchaseEmail
        {
            get => fields.ContactsPurchaseEmail[this];
            set => fields.ContactsPurchaseEmail[this] = value;
        }

        [DisplayName("Contacts Service Email"), Expression("jContacts.[ServiceEmail]")]
        public String ContactsServiceEmail
        {
            get => fields.ContactsServiceEmail[this];
            set => fields.ContactsServiceEmail[this] = value;
        }

        [DisplayName("Contacts Sales Email"), Expression("jContacts.[SalesEmail]")]
        public String ContactsSalesEmail
        {
            get => fields.ContactsSalesEmail[this];
            set => fields.ContactsSalesEmail[this] = value;
        }

        [DisplayName("Contacts Credit Days"), Expression("jContacts.[CreditDays]")]
        public Int32? ContactsCreditDays
        {
            get => fields.ContactsCreditDays[this];
            set => fields.ContactsCreditDays[this] = value;
        }

        [DisplayName("Contacts Customer Type"), Expression("jContacts.[CustomerType]")]
        public Int32? ContactsCustomerType
        {
            get => fields.ContactsCustomerType[this];
            set => fields.ContactsCustomerType[this] = value;
        }

        [DisplayName("Contacts Trasportation Id"), Expression("jContacts.[TrasportationId]")]
        public Int32? ContactsTrasportationId
        {
            get => fields.ContactsTrasportationId[this];
            set => fields.ContactsTrasportationId[this] = value;
        }

        [DisplayName("Contacts Tehsil Id"), Expression("jContacts.[TehsilId]")]
        public Int32? ContactsTehsilId
        {
            get => fields.ContactsTehsilId[this];
            set => fields.ContactsTehsilId[this] = value;
        }

        [DisplayName("Contacts Village Id"), Expression("jContacts.[VillageId]")]
        public Int32? ContactsVillageId
        {
            get => fields.ContactsVillageId[this];
            set => fields.ContactsVillageId[this] = value;
        }

        [DisplayName("Contacts Whatsapp"), Expression("jContacts.[Whatsapp]")]
        public String ContactsWhatsapp
        {
            get => fields.ContactsWhatsapp[this];
            set => fields.ContactsWhatsapp[this] = value;
        }

        [DisplayName("Contacts Consent For Calling"), Expression("jContacts.[ConsentForCalling]")]
        public Boolean? ContactsConsentForCalling
        {
            get => fields.ContactsConsentForCalling[this];
            set => fields.ContactsConsentForCalling[this] = value;
        }

        [DisplayName("Contacts Additional Info2"), Expression("jContacts.[AdditionalInfo2]")]
        public String ContactsAdditionalInfo2
        {
            get => fields.ContactsAdditionalInfo2[this];
            set => fields.ContactsAdditionalInfo2[this] = value;
        }

        [DisplayName("Contacts Date Created"), Expression("jContacts.[DateCreated]")]
        public DateTime? ContactsDateCreated
        {
            get => fields.ContactsDateCreated[this];
            set => fields.ContactsDateCreated[this] = value;
        }

        [DisplayName("Contacts Passport Number"), Expression("jContacts.[PassportNumber]")]
        public String ContactsPassportNumber
        {
            get => fields.ContactsPassportNumber[this];
            set => fields.ContactsPassportNumber[this] = value;
        }

        [DisplayName("Contacts First Name"), Expression("jContacts.[FirstName]")]
        public String ContactsFirstName
        {
            get => fields.ContactsFirstName[this];
            set => fields.ContactsFirstName[this] = value;
        }

        [DisplayName("Contacts Last Name"), Expression("jContacts.[LastName]")]
        public String ContactsLastName
        {
            get => fields.ContactsLastName[this];
            set => fields.ContactsLastName[this] = value;
        }

        [DisplayName("Contacts Expiry Date"), Expression("jContacts.[ExpiryDate]")]
        public DateTime? ContactsExpiryDate
        {
            get => fields.ContactsExpiryDate[this];
            set => fields.ContactsExpiryDate[this] = value;
        }

        [DisplayName("Contacts Aadhar No"), Expression("jContacts.[AadharNo]")]
        public String ContactsAadharNo
        {
            get => fields.ContactsAadharNo[this];
            set => fields.ContactsAadharNo[this] = value;
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

        [DisplayName("Owner Username"), Expression("jOwner.[Username]")]
        public String OwnerUsername
        {
            get => fields.OwnerUsername[this];
            set => fields.OwnerUsername[this] = value;
        }

        [DisplayName("Owner Display Name"), Expression("jOwner.[DisplayName]")]
        public String OwnerDisplayName
        {
            get => fields.OwnerDisplayName[this];
            set => fields.OwnerDisplayName[this] = value;
        }

        [DisplayName("Owner Email"), Expression("jOwner.[Email]")]
        public String OwnerEmail
        {
            get => fields.OwnerEmail[this];
            set => fields.OwnerEmail[this] = value;
        }

        [DisplayName("Owner Upper Level"), Expression("jOwner.[UpperLevel]")]
        public Int32? OwnerUpperLevel
        {
            get => fields.OwnerUpperLevel[this];
            set => fields.OwnerUpperLevel[this] = value;
        }

        [DisplayName("Owner Upper Level2"), Expression("jOwner.[UpperLevel2]")]
        public Int32? OwnerUpperLevel2
        {
            get => fields.OwnerUpperLevel2[this];
            set => fields.OwnerUpperLevel2[this] = value;
        }

        [DisplayName("Owner Upper Level3"), Expression("jOwner.[UpperLevel3]")]
        public Int32? OwnerUpperLevel3
        {
            get => fields.OwnerUpperLevel3[this];
            set => fields.OwnerUpperLevel3[this] = value;
        }

        [DisplayName("Owner Upper Level4"), Expression("jOwner.[UpperLevel4]")]
        public Int32? OwnerUpperLevel4
        {
            get => fields.OwnerUpperLevel4[this];
            set => fields.OwnerUpperLevel4[this] = value;
        }

        [DisplayName("Owner Upper Level5"), Expression("jOwner.[UpperLevel5]")]
        public Int32? OwnerUpperLevel5
        {
            get => fields.OwnerUpperLevel5[this];
            set => fields.OwnerUpperLevel5[this] = value;
        }

        [DisplayName("Owner Host"), Expression("jOwner.[Host]")]
        public String OwnerHost
        {
            get => fields.OwnerHost[this];
            set => fields.OwnerHost[this] = value;
        }

        [DisplayName("Owner Port"), Expression("jOwner.[Port]")]
        public Int32? OwnerPort
        {
            get => fields.OwnerPort[this];
            set => fields.OwnerPort[this] = value;
        }

        [DisplayName("Owner Ssl"), Expression("jOwner.[SSL]")]
        public Boolean? OwnerSsl
        {
            get => fields.OwnerSsl[this];
            set => fields.OwnerSsl[this] = value;
        }

        [DisplayName("Owner Email Id"), Expression("jOwner.[EmailId]")]
        public String OwnerEmailId
        {
            get => fields.OwnerEmailId[this];
            set => fields.OwnerEmailId[this] = value;
        }

        [DisplayName("Owner Email Password"), Expression("jOwner.[EmailPassword]")]
        public String OwnerEmailPassword
        {
            get => fields.OwnerEmailPassword[this];
            set => fields.OwnerEmailPassword[this] = value;
        }

        [DisplayName("Owner Phone"), Expression("jOwner.[Phone]")]
        public String OwnerPhone
        {
            get => fields.OwnerPhone[this];
            set => fields.OwnerPhone[this] = value;
        }

        [DisplayName("Owner Mcsmtp Server"), Expression("jOwner.[MCSMTPServer]")]
        public String OwnerMcsmtpServer
        {
            get => fields.OwnerMcsmtpServer[this];
            set => fields.OwnerMcsmtpServer[this] = value;
        }

        [DisplayName("Owner Mcsmtp Port"), Expression("jOwner.[MCSMTPPort]")]
        public Int32? OwnerMcsmtpPort
        {
            get => fields.OwnerMcsmtpPort[this];
            set => fields.OwnerMcsmtpPort[this] = value;
        }

        [DisplayName("Owner Mcimap Server"), Expression("jOwner.[MCIMAPServer]")]
        public String OwnerMcimapServer
        {
            get => fields.OwnerMcimapServer[this];
            set => fields.OwnerMcimapServer[this] = value;
        }

        [DisplayName("Owner Mcimap Port"), Expression("jOwner.[MCIMAPPort]")]
        public Int32? OwnerMcimapPort
        {
            get => fields.OwnerMcimapPort[this];
            set => fields.OwnerMcimapPort[this] = value;
        }

        [DisplayName("Owner Mc Username"), Expression("jOwner.[MCUsername]")]
        public String OwnerMcUsername
        {
            get => fields.OwnerMcUsername[this];
            set => fields.OwnerMcUsername[this] = value;
        }

        [DisplayName("Owner Mc Password"), Expression("jOwner.[MCPassword]")]
        public String OwnerMcPassword
        {
            get => fields.OwnerMcPassword[this];
            set => fields.OwnerMcPassword[this] = value;
        }

        [DisplayName("Owner Start Time"), Expression("jOwner.[StartTime]")]
        public String OwnerStartTime
        {
            get => fields.OwnerStartTime[this];
            set => fields.OwnerStartTime[this] = value;
        }

        [DisplayName("Owner End Time"), Expression("jOwner.[EndTime]")]
        public String OwnerEndTime
        {
            get => fields.OwnerEndTime[this];
            set => fields.OwnerEndTime[this] = value;
        }

        [DisplayName("Owner Uid"), Expression("jOwner.[UID]")]
        public String OwnerUid
        {
            get => fields.OwnerUid[this];
            set => fields.OwnerUid[this] = value;
        }

        [DisplayName("Owner Non Operational"), Expression("jOwner.[NonOperational]")]
        public Boolean? OwnerNonOperational
        {
            get => fields.OwnerNonOperational[this];
            set => fields.OwnerNonOperational[this] = value;
        }

        [DisplayName("Owner Branch Id"), Expression("jOwner.[BranchId]")]
        public Int32? OwnerBranchId
        {
            get => fields.OwnerBranchId[this];
            set => fields.OwnerBranchId[this] = value;
        }

        [DisplayName("Owner Company Id"), Expression("jOwner.[CompanyId]")]
        public Int32? OwnerCompanyId
        {
            get => fields.OwnerCompanyId[this];
            set => fields.OwnerCompanyId[this] = value;
        }

        [DisplayName("Owner Enquiry"), Expression("jOwner.[Enquiry]")]
        public Boolean? OwnerEnquiry
        {
            get => fields.OwnerEnquiry[this];
            set => fields.OwnerEnquiry[this] = value;
        }

        [DisplayName("Owner Quotation"), Expression("jOwner.[Quotation]")]
        public Boolean? OwnerQuotation
        {
            get => fields.OwnerQuotation[this];
            set => fields.OwnerQuotation[this] = value;
        }

        [DisplayName("Owner Tasks"), Expression("jOwner.[Tasks]")]
        public Boolean? OwnerTasks
        {
            get => fields.OwnerTasks[this];
            set => fields.OwnerTasks[this] = value;
        }

        [DisplayName("Owner Contacts"), Expression("jOwner.[Contacts]")]
        public Boolean? OwnerContacts
        {
            get => fields.OwnerContacts[this];
            set => fields.OwnerContacts[this] = value;
        }

        [DisplayName("Owner Purchase"), Expression("jOwner.[Purchase]")]
        public Boolean? OwnerPurchase
        {
            get => fields.OwnerPurchase[this];
            set => fields.OwnerPurchase[this] = value;
        }

        [DisplayName("Owner Sales"), Expression("jOwner.[Sales]")]
        public Boolean? OwnerSales
        {
            get => fields.OwnerSales[this];
            set => fields.OwnerSales[this] = value;
        }

        [DisplayName("Owner Cms"), Expression("jOwner.[CMS]")]
        public Boolean? OwnerCms
        {
            get => fields.OwnerCms[this];
            set => fields.OwnerCms[this] = value;
        }

        [DisplayName("Owner Location"), Expression("jOwner.[Location]")]
        public String OwnerLocation
        {
            get => fields.OwnerLocation[this];
            set => fields.OwnerLocation[this] = value;
        }

        [DisplayName("Owner Coordinates"), Expression("jOwner.[Coordinates]")]
        public String OwnerCoordinates
        {
            get => fields.OwnerCoordinates[this];
            set => fields.OwnerCoordinates[this] = value;
        }

        [DisplayName("Owner Teams Id"), Expression("jOwner.[TeamsId]")]
        public Int32? OwnerTeamsId
        {
            get => fields.OwnerTeamsId[this];
            set => fields.OwnerTeamsId[this] = value;
        }

        [DisplayName("Owner Tenant Id"), Expression("jOwner.[TenantId]")]
        public Int32? OwnerTenantId
        {
            get => fields.OwnerTenantId[this];
            set => fields.OwnerTenantId[this] = value;
        }

        [DisplayName("Owner Url"), Expression("jOwner.[Url]")]
        public String OwnerUrl
        {
            get => fields.OwnerUrl[this];
            set => fields.OwnerUrl[this] = value;
        }

        [DisplayName("Owner Plan"), Expression("jOwner.[Plan]")]
        public String OwnerPlan
        {
            get => fields.OwnerPlan[this];
            set => fields.OwnerPlan[this] = value;
        }

        [DisplayName("Assigned Username"), Expression("jAssigned.[Username]")]
        public String AssignedUsername
        {
            get => fields.AssignedUsername[this];
            set => fields.AssignedUsername[this] = value;
        }

        [DisplayName("Assigned Display Name"), Expression("jAssigned.[DisplayName]")]
        public String AssignedDisplayName
        {
            get => fields.AssignedDisplayName[this];
            set => fields.AssignedDisplayName[this] = value;
        }

        [DisplayName("Assigned Email"), Expression("jAssigned.[Email]")]
        public String AssignedEmail
        {
            get => fields.AssignedEmail[this];
            set => fields.AssignedEmail[this] = value;
        }

        [DisplayName("Assigned Upper Level"), Expression("jAssigned.[UpperLevel]")]
        public Int32? AssignedUpperLevel
        {
            get => fields.AssignedUpperLevel[this];
            set => fields.AssignedUpperLevel[this] = value;
        }

        [DisplayName("Assigned Upper Level2"), Expression("jAssigned.[UpperLevel2]")]
        public Int32? AssignedUpperLevel2
        {
            get => fields.AssignedUpperLevel2[this];
            set => fields.AssignedUpperLevel2[this] = value;
        }

        [DisplayName("Assigned Upper Level3"), Expression("jAssigned.[UpperLevel3]")]
        public Int32? AssignedUpperLevel3
        {
            get => fields.AssignedUpperLevel3[this];
            set => fields.AssignedUpperLevel3[this] = value;
        }

        [DisplayName("Assigned Upper Level4"), Expression("jAssigned.[UpperLevel4]")]
        public Int32? AssignedUpperLevel4
        {
            get => fields.AssignedUpperLevel4[this];
            set => fields.AssignedUpperLevel4[this] = value;
        }

        [DisplayName("Assigned Upper Level5"), Expression("jAssigned.[UpperLevel5]")]
        public Int32? AssignedUpperLevel5
        {
            get => fields.AssignedUpperLevel5[this];
            set => fields.AssignedUpperLevel5[this] = value;
        }

        [DisplayName("Assigned Host"), Expression("jAssigned.[Host]")]
        public String AssignedHost
        {
            get => fields.AssignedHost[this];
            set => fields.AssignedHost[this] = value;
        }

        [DisplayName("Assigned Port"), Expression("jAssigned.[Port]")]
        public Int32? AssignedPort
        {
            get => fields.AssignedPort[this];
            set => fields.AssignedPort[this] = value;
        }

        [DisplayName("Assigned Ssl"), Expression("jAssigned.[SSL]")]
        public Boolean? AssignedSsl
        {
            get => fields.AssignedSsl[this];
            set => fields.AssignedSsl[this] = value;
        }

        [DisplayName("Assigned Email Id"), Expression("jAssigned.[EmailId]")]
        public String AssignedEmailId
        {
            get => fields.AssignedEmailId[this];
            set => fields.AssignedEmailId[this] = value;
        }

        [DisplayName("Assigned Email Password"), Expression("jAssigned.[EmailPassword]")]
        public String AssignedEmailPassword
        {
            get => fields.AssignedEmailPassword[this];
            set => fields.AssignedEmailPassword[this] = value;
        }

        [DisplayName("Assigned Phone"), Expression("jAssigned.[Phone]")]
        public String AssignedPhone
        {
            get => fields.AssignedPhone[this];
            set => fields.AssignedPhone[this] = value;
        }

        [DisplayName("Assigned Mcsmtp Server"), Expression("jAssigned.[MCSMTPServer]")]
        public String AssignedMcsmtpServer
        {
            get => fields.AssignedMcsmtpServer[this];
            set => fields.AssignedMcsmtpServer[this] = value;
        }

        [DisplayName("Assigned Mcsmtp Port"), Expression("jAssigned.[MCSMTPPort]")]
        public Int32? AssignedMcsmtpPort
        {
            get => fields.AssignedMcsmtpPort[this];
            set => fields.AssignedMcsmtpPort[this] = value;
        }

        [DisplayName("Assigned Mcimap Server"), Expression("jAssigned.[MCIMAPServer]")]
        public String AssignedMcimapServer
        {
            get => fields.AssignedMcimapServer[this];
            set => fields.AssignedMcimapServer[this] = value;
        }

        [DisplayName("Assigned Mcimap Port"), Expression("jAssigned.[MCIMAPPort]")]
        public Int32? AssignedMcimapPort
        {
            get => fields.AssignedMcimapPort[this];
            set => fields.AssignedMcimapPort[this] = value;
        }

        [DisplayName("Assigned Mc Username"), Expression("jAssigned.[MCUsername]")]
        public String AssignedMcUsername
        {
            get => fields.AssignedMcUsername[this];
            set => fields.AssignedMcUsername[this] = value;
        }

        [DisplayName("Assigned Mc Password"), Expression("jAssigned.[MCPassword]")]
        public String AssignedMcPassword
        {
            get => fields.AssignedMcPassword[this];
            set => fields.AssignedMcPassword[this] = value;
        }

        [DisplayName("Assigned Start Time"), Expression("jAssigned.[StartTime]")]
        public String AssignedStartTime
        {
            get => fields.AssignedStartTime[this];
            set => fields.AssignedStartTime[this] = value;
        }

        [DisplayName("Assigned End Time"), Expression("jAssigned.[EndTime]")]
        public String AssignedEndTime
        {
            get => fields.AssignedEndTime[this];
            set => fields.AssignedEndTime[this] = value;
        }

        [DisplayName("Assigned Uid"), Expression("jAssigned.[UID]")]
        public String AssignedUid
        {
            get => fields.AssignedUid[this];
            set => fields.AssignedUid[this] = value;
        }

        [DisplayName("Assigned Non Operational"), Expression("jAssigned.[NonOperational]")]
        public Boolean? AssignedNonOperational
        {
            get => fields.AssignedNonOperational[this];
            set => fields.AssignedNonOperational[this] = value;
        }

        [DisplayName("Assigned Branch Id"), Expression("jAssigned.[BranchId]")]
        public Int32? AssignedBranchId
        {
            get => fields.AssignedBranchId[this];
            set => fields.AssignedBranchId[this] = value;
        }

        [DisplayName("Assigned Company Id"), Expression("jAssigned.[CompanyId]")]
        public Int32? AssignedCompanyId
        {
            get => fields.AssignedCompanyId[this];
            set => fields.AssignedCompanyId[this] = value;
        }

        [DisplayName("Assigned Enquiry"), Expression("jAssigned.[Enquiry]")]
        public Boolean? AssignedEnquiry
        {
            get => fields.AssignedEnquiry[this];
            set => fields.AssignedEnquiry[this] = value;
        }

        [DisplayName("Assigned Quotation"), Expression("jAssigned.[Quotation]")]
        public Boolean? AssignedQuotation
        {
            get => fields.AssignedQuotation[this];
            set => fields.AssignedQuotation[this] = value;
        }

        [DisplayName("Assigned Tasks"), Expression("jAssigned.[Tasks]")]
        public Boolean? AssignedTasks
        {
            get => fields.AssignedTasks[this];
            set => fields.AssignedTasks[this] = value;
        }

        [DisplayName("Assigned Contacts"), Expression("jAssigned.[Contacts]")]
        public Boolean? AssignedContacts
        {
            get => fields.AssignedContacts[this];
            set => fields.AssignedContacts[this] = value;
        }

        [DisplayName("Assigned Purchase"), Expression("jAssigned.[Purchase]")]
        public Boolean? AssignedPurchase
        {
            get => fields.AssignedPurchase[this];
            set => fields.AssignedPurchase[this] = value;
        }

        [DisplayName("Assigned Sales"), Expression("jAssigned.[Sales]")]
        public Boolean? AssignedSales
        {
            get => fields.AssignedSales[this];
            set => fields.AssignedSales[this] = value;
        }

        [DisplayName("Assigned Cms"), Expression("jAssigned.[CMS]")]
        public Boolean? AssignedCms
        {
            get => fields.AssignedCms[this];
            set => fields.AssignedCms[this] = value;
        }

        [DisplayName("Assigned Location"), Expression("jAssigned.[Location]")]
        public String AssignedLocation
        {
            get => fields.AssignedLocation[this];
            set => fields.AssignedLocation[this] = value;
        }

        [DisplayName("Assigned Coordinates"), Expression("jAssigned.[Coordinates]")]
        public String AssignedCoordinates
        {
            get => fields.AssignedCoordinates[this];
            set => fields.AssignedCoordinates[this] = value;
        }

        [DisplayName("Assigned Teams Id"), Expression("jAssigned.[TeamsId]")]
        public Int32? AssignedTeamsId
        {
            get => fields.AssignedTeamsId[this];
            set => fields.AssignedTeamsId[this] = value;
        }

        [DisplayName("Assigned Tenant Id"), Expression("jAssigned.[TenantId]")]
        public Int32? AssignedTenantId
        {
            get => fields.AssignedTenantId[this];
            set => fields.AssignedTenantId[this] = value;
        }

        [DisplayName("Assigned Url"), Expression("jAssigned.[Url]")]
        public String AssignedUrl
        {
            get => fields.AssignedUrl[this];
            set => fields.AssignedUrl[this] = value;
        }

        [DisplayName("Assigned Plan"), Expression("jAssigned.[Plan]")]
        public String AssignedPlan
        {
            get => fields.AssignedPlan[this];
            set => fields.AssignedPlan[this] = value;
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
        [MasterDetailRelation(foreignKey: "IndentId"), NotMapped]
        public List<IndentProductsRow> Products
        {
            get => Fields.Products[this];
            set => Fields.Products[this] = value;
        }


        public IndentRow()
            : base()
        {
        }

        public IndentRow(RowFields fields)
            : base(fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ContactsId;
            public DateTimeField Date;
            public Int32Field Status;
            public StringField AdditionalInfo;
            public StringField InvoiceNo;
            public Int32Field BranchId;
            public Int32Field OwnerId;
            public Int32Field AssignedId;
            public StringField Subject;
            public StringField Reference;
            public Int32Field ContactPersonId;
            public Int32Field Lines;

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
            public StringField ContactsCcEmails;
            public StringField ContactsBccEmails;
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
            public BooleanField ContactsConsentForCalling;
            public StringField ContactsAdditionalInfo2;
            public DateTimeField ContactsDateCreated;
            public StringField ContactsPassportNumber;
            public StringField ContactsFirstName;
            public StringField ContactsLastName;
            public DateTimeField ContactsExpiryDate;
            public StringField ContactsAadharNo;

            public StringField Branch;
            public StringField BranchPhone;
            public StringField BranchEmail;
            public StringField BranchAddress;
            public Int32Field BranchCityId;
            public Int32Field BranchStateId;
            public StringField BranchPin;
            public Int32Field BranchCountry;
            public Int32Field BranchCompanyId;

            public StringField OwnerUsername;
            public StringField OwnerDisplayName;
            public StringField OwnerEmail;
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
            public StringField OwnerUid;
            public BooleanField OwnerNonOperational;
            public Int32Field OwnerBranchId;
            public Int32Field OwnerCompanyId;
            public BooleanField OwnerEnquiry;
            public BooleanField OwnerQuotation;
            public BooleanField OwnerTasks;
            public BooleanField OwnerContacts;
            public BooleanField OwnerPurchase;
            public BooleanField OwnerSales;
            public BooleanField OwnerCms;
            public StringField OwnerLocation;
            public StringField OwnerCoordinates;
            public Int32Field OwnerTeamsId;
            public Int32Field OwnerTenantId;
            public StringField OwnerUrl;
            public StringField OwnerPlan;

            public StringField AssignedUsername;
            public StringField AssignedDisplayName;
            public StringField AssignedEmail;
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
            public StringField AssignedUid;
            public BooleanField AssignedNonOperational;
            public Int32Field AssignedBranchId;
            public Int32Field AssignedCompanyId;
            public BooleanField AssignedEnquiry;
            public BooleanField AssignedQuotation;
            public BooleanField AssignedTasks;
            public BooleanField AssignedContacts;
            public BooleanField AssignedPurchase;
            public BooleanField AssignedSales;
            public BooleanField AssignedCms;
            public StringField AssignedLocation;
            public StringField AssignedCoordinates;
            public Int32Field AssignedTeamsId;
            public Int32Field AssignedTenantId;
            public StringField AssignedUrl;
            public StringField AssignedPlan;

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

            public readonly RowListField<IndentProductsRow> Products;            
        }
    }
}
