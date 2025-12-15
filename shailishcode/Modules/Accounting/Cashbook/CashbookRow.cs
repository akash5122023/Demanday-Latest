
namespace AdvanceCRM.Accounting
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Employee;
    using AdvanceCRM.Masters;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Accounting"), TableName("[dbo].[Cashbook]")]
    [DisplayName("Cashbook"), InstanceName("Cashbook")]
    [ReadPermission("Cashbook:Read")]
    [InsertPermission("Cashbook:Insert")]
    [UpdatePermission("Cashbook:Update")]
    [DeletePermission("Cashbook:Delete")]

    public sealed class CashbookRow : Row<CashbookRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog, IMultiCompanyRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Date"), NotNull, DefaultValue("now")]
        [QuickFilter]
        public DateTime? Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
        }

        [DisplayName("Type"), NotNull]
        public Masters.TransactionTypeMaster? Type
        {
            get { return (Masters.TransactionTypeMaster?)Fields.Type[this]; }
            set { Fields.Type[this] = (Int32?)value; }
        }

        [DisplayName("Head"), NotNull, ForeignKey("[dbo].[AccountingHeads]", "Id"), LeftJoin("jHead"), TextualField("Head1")]
        [LookupEditor(typeof(AccountingHeadsRow), InplaceAdd = true, FilterField = "Type", FilterValue = "Expense")]
        public Int32? Head
        {
            get { return Fields.Head[this]; }
            set { Fields.Head[this] = value; }
        }

        [DisplayName("Contacts"), ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jContacts"), TextualField("ContactsName")]
        [LookupEditor(typeof(ContactsRow), InplaceAdd = true), QuickFilter]
        public Int32? ContactsId
        {
            get { return Fields.ContactsId[this]; }
            set { Fields.ContactsId[this] = value; }
        }

        [DisplayName("Invoice No"), Size(50), QuickSearch,NameProperty]
        public String InvoiceNo
        {
            get { return Fields.InvoiceNo[this]; }
            set { Fields.InvoiceNo[this] = value; }
        }

        [DisplayName("Cash In")]
        public Double? CashIn
        {
            get { return Fields.CashIn[this]; }
            set { Fields.CashIn[this] = value; }
        }

        [DisplayName("Cash Out")]
        public Double? CashOut
        {
            get { return Fields.CashOut[this]; }
            set { Fields.CashOut[this] = value; }
        }
        [DisplayName("Project Amount")]
        public Double? ProjectAmtIn
        {
            get { return Fields.ProjectAmtIn[this]; }
            set { Fields.ProjectAmtIn[this] = value; }
        }
        [DisplayName("Project Cost")]
        [BooleanSwitchEditor]
        public Boolean? IsCashIn
        {
            get { return Fields.IsCashIn[this]; }
            set { Fields.IsCashIn[this] = value; }
        }
        [DisplayName("Purpose")]
        public String? Purpose
        {
            get { return Fields.Purpose[this]; }
            set { Fields.Purpose[this] = value; }
        }
        [DisplayName("Narration"), Size(300), TextAreaEditor(Rows = 3)]
        public String Narration
        {
            get { return Fields.Narration[this]; }
            set { Fields.Narration[this] = value; }
        }

        [DisplayName("Bank"), ForeignKey("[dbo].[BankMaster]", "Id"), LeftJoin("jBank"), TextualField("BankBankName")]
        [LookupEditor(typeof(BankMasterRow), InplaceAdd = true)]
        public Int32? BankId
        {
            get { return Fields.BankId[this]; }
            set { Fields.BankId[this] = value; }
        }

        [DisplayName("Head"), Expression("jHead.[Head]")]
        public String Head1
        {
            get { return Fields.Head1[this]; }
            set { Fields.Head1[this] = value; }
        }

        [DisplayName("Head Type"), Expression("jHead.[Type]")]
        public Int32? HeadType
        {
            get { return Fields.HeadType[this]; }
            set { Fields.HeadType[this] = value; }
        }

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull]
        [Insertable(false), Updatable(false)] 
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }
        public Int32Field CompanyIdField
        {
            get { return Fields.CompanyId; }
        }

        [DisplayName("Contacts Contact Type"), Expression("jContacts.[ContactType]")]
        public Int32? ContactsContactType
        {
            get { return Fields.ContactsContactType[this]; }
            set { Fields.ContactsContactType[this] = value; }
        }

        [DisplayName("Contacts Name"), Expression("jContacts.[Name]")]
        public String ContactsName
        {
            get { return Fields.ContactsName[this]; }
            set { Fields.ContactsName[this] = value; }
        }

        [DisplayName("Phone"), Expression("jContacts.[Phone]")]
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

        [DisplayName("Address"), Expression("jContacts.[Address]")]
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
        public String ContactsCcEmails
        {
            get { return Fields.ContactsCcEmails[this]; }
            set { Fields.ContactsCcEmails[this] = value; }
        }

        [DisplayName("Contacts Bcc Emails"), Expression("jContacts.[BCCEmails]")]
        public String ContactsBccEmails
        {
            get { return Fields.ContactsBccEmails[this]; }
            set { Fields.ContactsBccEmails[this] = value; }
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

        [DisplayName("Bank Bank Name"), Expression("jBank.[BankName]")]
        public String BankBankName
        {
            get { return Fields.BankBankName[this]; }
            set { Fields.BankBankName[this] = value; }
        }

        [DisplayName("Bank Account Number"), Expression("jBank.[AccountNumber]")]
        public String BankAccountNumber
        {
            get { return Fields.BankAccountNumber[this]; }
            set { Fields.BankAccountNumber[this] = value; }
        }

        [DisplayName("Bank Ifsc"), Expression("jBank.[IFSC]")]
        public String BankIfsc
        {
            get { return Fields.BankIfsc[this]; }
            set { Fields.BankIfsc[this] = value; }
        }

        [DisplayName("Bank Type"), Expression("jBank.[Type]")]
        public String BankType
        {
            get { return Fields.BankType[this]; }
            set { Fields.BankType[this] = value; }
        }

        [DisplayName("Bank Branch"), Expression("jBank.[Branch]")]
        public String BankBranch
        {
            get { return Fields.BankBranch[this]; }
            set { Fields.BankBranch[this] = value; }
        }

        [DisplayName("Bank Additional Info"), Expression("jBank.[AdditionalInfo]")]
        public String BankAdditionalInfo
        {
            get { return Fields.BankAdditionalInfo[this]; }
            set { Fields.BankAdditionalInfo[this] = value; }
        }
        [DisplayName("Project"), ForeignKey("[dbo].[Project]", "Id"), LeftJoin("jProject"), TextualField("Project")]
        [LookupEditor(typeof(ProjectRow), InplaceAdd = true), QuickFilter]
        public Int32? ProjectId
        {
            get { return Fields.ProjectId[this]; }
            set { Fields.ProjectId[this] = value; }
        }
        [DisplayName("Project Name"),Expression("jProject.Project")]
        public String ProjectName
        {
            get { return Fields.ProjectName[this]; }
            set { Fields.ProjectName[this] = value; }
        }
        [DisplayName("Employee"), ForeignKey("[dbo].[Employee]", "Id"), LeftJoin("jEmployee"), TextualField("EmployeeName")]
        [LookupEditor(typeof(EmployeeRow), InplaceAdd = true), QuickFilter]
        public Int32? EmployeeId
        {
            get { return Fields.EmployeeId[this]; }
            set { Fields.EmployeeId[this] = value; }
        }
        [DisplayName("Employee Name"), Expression("jEmployee.[Name]")]
        public String EmployeeName
        {
            get { return Fields.EmployeeName[this]; }
            set { Fields.EmployeeName[this] = value; }
        }

        [DisplayName("Employee DepartmentId"), Expression("jEmployee.[DepartmentId]")]
        public Int32? EmployeeDepartmentId
        {
            get { return Fields.EmployeeDepartmentId[this]; }
            set { Fields.EmployeeDepartmentId[this] = value; }
        }

        [DisplayName("Employee EmpCode"), Expression("jEmployee.[EmpCode]")]
        public String EmployeeEmpCode
        {
            get { return Fields.EmployeeEmpCode[this]; }
            set { Fields.EmployeeEmpCode[this] = value; }
        }

        [DisplayName("Employee Phone"), Expression("jEmployee.[Phone]")]
        public String EmployeePhone
        {
            get { return Fields.EmployeePhone[this]; }
            set { Fields.EmployeePhone[this] = value; }
        }

        [DisplayName("Employee Email"), Expression("jEmployee.[Email]")]
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

        [DisplayName("Employee ProfessionalEmail"), Expression("jEmployee.[ProfessionalEmail]")]
        public String EmployeeProfessionalEmail
        {
            get { return Fields.EmployeeProfessionalEmail[this]; }
            set { Fields.EmployeeProfessionalEmail[this] = value; }
        }

        [DisplayName("Employee CityID"), Expression("jEmployee.[CityId]")]
        public Int32? EmployeeCityId
        {
            get { return Fields.EmployeeCityId[this]; }
            set { Fields.EmployeeCityId[this] = value; }
        }

        [DisplayName("Employee StateId"), Expression("jEmployee.[StateId]")]
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

        [DisplayName("Employee AdditionalInfo"), Expression("jEmployee.[AdditionalInfo]")]
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

        [DisplayName("Employee AreaId"), Expression("jEmployee.[AreaId]")]
        public Int32? EmployeeAreaId
        {
            get { return Fields.EmployeeAreaId[this]; }
            set { Fields.EmployeeAreaId[this] = value; }
        }

        [DisplayName("Employee MaritalStatus"), Expression("jEmployee.[MaritalStatus]")]
        public Int32? EmployeeMaritalStatus
        {
            get { return Fields.EmployeeMaritalStatus[this]; }
            set { Fields.EmployeeMaritalStatus[this] = value; }
        }

        [DisplayName("Employee MarriageAnniversary"), Expression("jEmployee.[MarriageAnniversary]")]
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

        [DisplayName("Employee DateOfJoining"), Expression("jEmployee.[DateOfJoining]")]
        public DateTime? EmployeeDateOfJoining
        {
            get { return Fields.EmployeeDateOfJoining[this]; }
            set { Fields.EmployeeDateOfJoining[this] = value; }
        }

        [DisplayName("Employee CompanyId"), Expression("jEmployee.[CompanyId]")]
        public Int32? EmployeeCompanyId
        {
            get { return Fields.EmployeeCompanyId[this]; }
            set { Fields.EmployeeCompanyId[this] = value; }
        }

        [DisplayName("Employee RolesId"), Expression("jEmployee.[RolesId]")]
        public Int32? EmployeeRolesId
        {
            get { return Fields.EmployeeRolesId[this]; }
            set { Fields.EmployeeRolesId[this] = value; }
        }

        [DisplayName("Employee OwnerId"), Expression("jEmployee.[OwnerId]")]
        public Int32? EmployeeOwnerId
        {
            get { return Fields.EmployeeOwnerId[this]; }
            set { Fields.EmployeeOwnerId[this] = value; }
        }

        [DisplayName("Employee AdhaarNo"), Expression("jEmployee.[AdhaarNo]")]
        public String EmployeeAdhaarNo
        {
            get { return Fields.EmployeeAdhaarNo[this]; }
            set { Fields.EmployeeAdhaarNo[this] = value; }
        }

        [DisplayName("Employee PanNo"), Expression("jEmployee.[PanNo]")]
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

        [DisplayName("Employee BankName"), Expression("jEmployee.[BankName]")]
        public String EmployeeBankName
        {
            get { return Fields.EmployeeBankName[this]; }
            set { Fields.EmployeeBankName[this] = value; }
        }
        [DisplayName("Employee AccountNumber"), Expression("jEmployee.[AccountNumber]")]
        public String EmployeeAccountNumber
        {
            get { return Fields.EmployeeAccountNumber[this]; }
            set { Fields.EmployeeAccountNumber[this] = value; }
        }
        [DisplayName("Employee Ifsc"), Expression("jEmployee.[Ifsc]")]
        public String EmployeeIfsc
        {
            get { return Fields.EmployeeIfsc[this]; }
            set { Fields.EmployeeIfsc[this] = value; }
        }
        [DisplayName("Employee BankType"), Expression("jEmployee.[BankType]")]
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

        [DisplayName("Employee TehsilID"), Expression("jEmployee.[TehsilId]")]
        public Int32? EmployeeTehsilId
        {
            get { return Fields.EmployeeTehsilId[this]; }
            set { Fields.EmployeeTehsilId[this] = value; }
        }

        [DisplayName("Employee VillageId"), Expression("jEmployee.[VillageId]")]
        public Int32? EmployeeVillageId
        {
            get { return Fields.EmployeeVillageId[this]; }
            set { Fields.EmployeeVillageId[this] = value; }
        }

        [DisplayName("Representative"), ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jRepresentative"), TextualField("RepresentativeUsername")]
        [UserEditor]
        public Int32? RepresentativeId
        {
            get { return Fields.RepresentativeId[this]; }
            set { Fields.RepresentativeId[this] = value; }
        }

        [DisplayName("Representative"), Expression("jRepresentative.[Username]")]
        public String RepresentativeUsername
        {
            get { return Fields.RepresentativeUsername[this]; }
            set { Fields.RepresentativeUsername[this] = value; }
        }

        [DisplayName("Representative"), Expression("jRepresentative.[DisplayName]")]
        public String RepresentativeDisplayName
        {
            get { return Fields.RepresentativeDisplayName[this]; }
            set { Fields.RepresentativeDisplayName[this] = value; }
        }

        [DisplayName("Representative Email"), Expression("jRepresentative.[Email]")]
        public String RepresentativeEmail
        {
            get { return Fields.RepresentativeEmail[this]; }
            set { Fields.RepresentativeEmail[this] = value; }
        }

        [DisplayName("Representative Source"), Expression("jRepresentative.[Source]")]
        public String RepresentativeSource
        {
            get { return Fields.RepresentativeSource[this]; }
            set { Fields.RepresentativeSource[this] = value; }
        }

        [DisplayName("Representative Password Hash"), Expression("jRepresentative.[PasswordHash]")]
        public String RepresentativePasswordHash
        {
            get { return Fields.RepresentativePasswordHash[this]; }
            set { Fields.RepresentativePasswordHash[this] = value; }
        }

        [DisplayName("Representative Password Salt"), Expression("jRepresentative.[PasswordSalt]")]
        public String RepresentativePasswordSalt
        {
            get { return Fields.RepresentativePasswordSalt[this]; }
            set { Fields.RepresentativePasswordSalt[this] = value; }
        }

        [DisplayName("Representative Last Directory Update"), Expression("jRepresentative.[LastDirectoryUpdate]")]
        public DateTime? RepresentativeLastDirectoryUpdate
        {
            get { return Fields.RepresentativeLastDirectoryUpdate[this]; }
            set { Fields.RepresentativeLastDirectoryUpdate[this] = value; }
        }

        [DisplayName("Representative User Image"), Expression("jRepresentative.[UserImage]")]
        public String RepresentativeUserImage
        {
            get { return Fields.RepresentativeUserImage[this]; }
            set { Fields.RepresentativeUserImage[this] = value; }
        }

        [DisplayName("Representative Insert Date"), Expression("jRepresentative.[InsertDate]")]
        public DateTime? RepresentativeInsertDate
        {
            get { return Fields.RepresentativeInsertDate[this]; }
            set { Fields.RepresentativeInsertDate[this] = value; }
        }

        [DisplayName("Representative Insert User Id"), Expression("jRepresentative.[InsertUserId]")]
        public Int32? RepresentativeInsertUserId
        {
            get { return Fields.RepresentativeInsertUserId[this]; }
            set { Fields.RepresentativeInsertUserId[this] = value; }
        }

        [DisplayName("Representative Update Date"), Expression("jRepresentative.[UpdateDate]")]
        public DateTime? RepresentativeUpdateDate
        {
            get { return Fields.RepresentativeUpdateDate[this]; }
            set { Fields.RepresentativeUpdateDate[this] = value; }
        }

        [DisplayName("Representative Update User Id"), Expression("jRepresentative.[UpdateUserId]")]
        public Int32? RepresentativeUpdateUserId
        {
            get { return Fields.RepresentativeUpdateUserId[this]; }
            set { Fields.RepresentativeUpdateUserId[this] = value; }
        }

        [DisplayName("Representative Is Active"), Expression("jRepresentative.[IsActive]")]
        public Int16? RepresentativeIsActive
        {
            get { return Fields.RepresentativeIsActive[this]; }
            set { Fields.RepresentativeIsActive[this] = value; }
        }

        [DisplayName("Representative Upper Level"), Expression("jRepresentative.[UpperLevel]")]
        public Int32? RepresentativeUpperLevel
        {
            get { return Fields.RepresentativeUpperLevel[this]; }
            set { Fields.RepresentativeUpperLevel[this] = value; }
        }

        [DisplayName("Representative Upper Level2"), Expression("jRepresentative.[UpperLevel2]")]
        public Int32? RepresentativeUpperLevel2
        {
            get { return Fields.RepresentativeUpperLevel2[this]; }
            set { Fields.RepresentativeUpperLevel2[this] = value; }
        }

        [DisplayName("Representative Upper Level3"), Expression("jRepresentative.[UpperLevel3]")]
        public Int32? RepresentativeUpperLevel3
        {
            get { return Fields.RepresentativeUpperLevel3[this]; }
            set { Fields.RepresentativeUpperLevel3[this] = value; }
        }

        [DisplayName("Representative Upper Level4"), Expression("jRepresentative.[UpperLevel4]")]
        public Int32? RepresentativeUpperLevel4
        {
            get { return Fields.RepresentativeUpperLevel4[this]; }
            set { Fields.RepresentativeUpperLevel4[this] = value; }
        }

        [DisplayName("Representative Upper Level5"), Expression("jRepresentative.[UpperLevel5]")]
        public Int32? RepresentativeUpperLevel5
        {
            get { return Fields.RepresentativeUpperLevel5[this]; }
            set { Fields.RepresentativeUpperLevel5[this] = value; }
        }

        [DisplayName("Representative Host"), Expression("jRepresentative.[Host]")]
        public String RepresentativeHost
        {
            get { return Fields.RepresentativeHost[this]; }
            set { Fields.RepresentativeHost[this] = value; }
        }

        [DisplayName("Representative Port"), Expression("jRepresentative.[Port]")]
        public Int32? RepresentativePort
        {
            get { return Fields.RepresentativePort[this]; }
            set { Fields.RepresentativePort[this] = value; }
        }

        [DisplayName("Representative SSL"), Expression("jRepresentative.[SSL]")]
        public Boolean? RepresentativeSsl
        {
            get { return Fields.RepresentativeSsl[this]; }
            set { Fields.RepresentativeSsl[this] = value; }
        }

        [DisplayName("Representative Email Id"), Expression("jRepresentative.[EmailId]")]
        public String RepresentativeEmailId
        {
            get { return Fields.RepresentativeEmailId[this]; }
            set { Fields.RepresentativeEmailId[this] = value; }
        }

        [DisplayName("Representative Email Password"), Expression("jRepresentative.[EmailPassword]")]
        public String RepresentativeEmailPassword
        {
            get { return Fields.RepresentativeEmailPassword[this]; }
            set { Fields.RepresentativeEmailPassword[this] = value; }
        }

        [DisplayName("Representative Phone"), Expression("jRepresentative.[Phone]")]
        public String RepresentativePhone
        {
            get { return Fields.RepresentativePhone[this]; }
            set { Fields.RepresentativePhone[this] = value; }
        }

        [DisplayName("Representative Mcsmtp Server"), Expression("jRepresentative.[MCSMTPServer]")]
        public String RepresentativeMcsmtpServer
        {
            get { return Fields.RepresentativeMcsmtpServer[this]; }
            set { Fields.RepresentativeMcsmtpServer[this] = value; }
        }

        [DisplayName("Representative Mcsmtp Port"), Expression("jRepresentative.[MCSMTPPort]")]
        public Int32? RepresentativeMcsmtpPort
        {
            get { return Fields.RepresentativeMcsmtpPort[this]; }
            set { Fields.RepresentativeMcsmtpPort[this] = value; }
        }

        [DisplayName("Representative Mcimap Server"), Expression("jRepresentative.[MCIMAPServer]")]
        public String RepresentativeMcimapServer
        {
            get { return Fields.RepresentativeMcimapServer[this]; }
            set { Fields.RepresentativeMcimapServer[this] = value; }
        }

        [DisplayName("Representative Mcimap Port"), Expression("jRepresentative.[MCIMAPPort]")]
        public Int32? RepresentativeMcimapPort
        {
            get { return Fields.RepresentativeMcimapPort[this]; }
            set { Fields.RepresentativeMcimapPort[this] = value; }
        }

        [DisplayName("Representative Mc Username"), Expression("jRepresentative.[MCUsername]")]
        public String RepresentativeMcUsername
        {
            get { return Fields.RepresentativeMcUsername[this]; }
            set { Fields.RepresentativeMcUsername[this] = value; }
        }

        [DisplayName("Representative Mc Password"), Expression("jRepresentative.[MCPassword]")]
        public String RepresentativeMcPassword
        {
            get { return Fields.RepresentativeMcPassword[this]; }
            set { Fields.RepresentativeMcPassword[this] = value; }
        }

        [DisplayName("Representative Start Time"), Expression("jRepresentative.[StartTime]")]
        public String RepresentativeStartTime
        {
            get { return Fields.RepresentativeStartTime[this]; }
            set { Fields.RepresentativeStartTime[this] = value; }
        }

        [DisplayName("Representative End Time"), Expression("jRepresentative.[EndTime]")]
        public String RepresentativeEndTime
        {
            get { return Fields.RepresentativeEndTime[this]; }
            set { Fields.RepresentativeEndTime[this] = value; }
        }

        [DisplayName("Representative Branch Id"), Expression("jRepresentative.[BranchId]")]
        public Int32? RepresentativeBranchId
        {
            get { return Fields.RepresentativeBranchId[this]; }
            set { Fields.RepresentativeBranchId[this] = value; }
        }

        [DisplayName("Representative Uid"), Expression("jRepresentative.[UID]")]
        public String RepresentativeUid
        {
            get { return Fields.RepresentativeUid[this]; }
            set { Fields.RepresentativeUid[this] = value; }
        }

        [DisplayName("Representative Non Operational"), Expression("jRepresentative.[NonOperational]")]
        public Boolean? RepresentativeNonOperational
        {
            get { return Fields.RepresentativeNonOperational[this]; }
            set { Fields.RepresentativeNonOperational[this] = value; }
        }
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
       

        

        public CashbookRow()
            : base(Fields)
        {
        }
        public CashbookRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public DateTimeField Date;
            public Int32Field Type;
            public Int32Field Head;
            public Int32Field ContactsId;
            public StringField InvoiceNo;
            public DoubleField CashIn;
            public DoubleField CashOut;
            public StringField Narration;
            public Int32Field BankId;

            public StringField Head1;
            public Int32Field HeadType;
            public Int32Field CompanyId;

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

            public StringField BankBankName;
            public StringField BankAccountNumber;
            public StringField BankIfsc;
            public StringField BankType;
            public StringField BankBranch;
            public StringField BankAdditionalInfo;

            public Int32Field ProjectId;
            public StringField ProjectName;

            public Int32Field EmployeeId;
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

            public DoubleField ProjectAmtIn;
            public BooleanField IsCashIn;
            public StringField Purpose;

            public Int32Field RepresentativeId;
            public StringField RepresentativeUsername;
            public StringField RepresentativeDisplayName;
            public StringField RepresentativeEmail;
            public StringField RepresentativeSource;
            public StringField RepresentativePasswordHash;
            public StringField RepresentativePasswordSalt;
            public DateTimeField RepresentativeLastDirectoryUpdate;
            public StringField RepresentativeUserImage;
            public DateTimeField RepresentativeInsertDate;
            public Int32Field RepresentativeInsertUserId;
            public DateTimeField RepresentativeUpdateDate;
            public Int32Field RepresentativeUpdateUserId;
            public Int16Field RepresentativeIsActive;
            public Int32Field RepresentativeUpperLevel;
            public Int32Field RepresentativeUpperLevel2;
            public Int32Field RepresentativeUpperLevel3;
            public Int32Field RepresentativeUpperLevel4;
            public Int32Field RepresentativeUpperLevel5;
            public StringField RepresentativeHost;
            public Int32Field RepresentativePort;
            public BooleanField RepresentativeSsl;
            public StringField RepresentativeEmailId;
            public StringField RepresentativeEmailPassword;
            public StringField RepresentativePhone;
            public StringField RepresentativeMcsmtpServer;
            public Int32Field RepresentativeMcsmtpPort;
            public StringField RepresentativeMcimapServer;
            public Int32Field RepresentativeMcimapPort;
            public StringField RepresentativeMcUsername;
            public StringField RepresentativeMcPassword;
            public StringField RepresentativeStartTime;
            public StringField RepresentativeEndTime;
            public Int32Field RepresentativeBranchId;
            public StringField RepresentativeUid;
            public BooleanField RepresentativeNonOperational;



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

        }
    }
}
