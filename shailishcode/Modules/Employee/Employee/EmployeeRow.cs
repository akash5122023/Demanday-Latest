
namespace AdvanceCRM.Employee
{
    using Serenity;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Administration;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;
    using CityRow = Masters.CityRow;
    using AdvanceCRM.Administration.Entities;

    [ConnectionKey("Default"), Module("Employee"), TableName("[dbo].[Employee]")]
    [DisplayName("Employee"), InstanceName("Employee")]
    [ReadPermission("Employee:Read")]
    [InsertPermission("Employee:Insert")]
    [UpdatePermission("Employee:Update")]
    [DeletePermission("Employee:Delete")]
    [LookupScript("Employee.Employee", Permission = "?", Expiration = -1)]
    public sealed class EmployeeRow : Row<EmployeeRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Department"), ForeignKey("[dbo].[Department]", "Id"), LeftJoin("jDepartment"), TextualField("Department"), QuickFilter]
        [LookupEditor(typeof(DepartmentRow), InplaceAdd = true)]
        public Int32? DepartmentId
        {
            get { return Fields.DepartmentId[this]; }
            set { Fields.DepartmentId[this] = value; }
        }

        [DisplayName("Name"), Size(150), NotNull,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Emp Code"), Size(20), LookupInclude]
        public String EmpCode
        {
            get { return Fields.EmpCode[this]; }
            set { Fields.EmpCode[this] = value; }
        }


        [DisplayName("Phone"), Size(100), LookupInclude]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Email"), Size(100), LookupInclude]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Address"), Size(500), LookupInclude]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("Professional Email"), Size(100)]
        public String ProfessionalEmail
        {
            get { return Fields.ProfessionalEmail[this]; }
            set { Fields.ProfessionalEmail[this] = value; }
        }

        [DisplayName("City"), ForeignKey("[dbo].[City]", "Id"), LeftJoin("jCity"), TextualField("City"), QuickFilter]
        [LookupEditor(typeof(CityRow), InplaceAdd = true, CascadeFrom = "StateId", CascadeValue = "StateId")]
        public Int32? CityId
        {
            get { return Fields.CityId[this]; }
            set { Fields.CityId[this] = value; }
        }

        [DisplayName("State"), ForeignKey("[dbo].[State]", "Id"), LeftJoin("jState"), TextualField("State"), QuickFilter]
        [LookupEditor(typeof(StateRow), InplaceAdd = true)]
        public Int32? StateId
        {
            get { return Fields.StateId[this]; }
            set { Fields.StateId[this] = value; }
        }

        [DisplayName("Pin"), Size(50)]
        public String Pin
        {
            get { return Fields.Pin[this]; }
            set { Fields.Pin[this] = value; }
        }

        [DisplayName("Country")]
        public Masters.CountryMaster? Country
        {
            get { return (Masters.CountryMaster?)Fields.Country[this]; }
            set { Fields.Country[this] = (Int32?)value; }
        }

        [DisplayName("Additional Info"), Size(5000), TextAreaEditor(Rows = 4)]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }
        [DisplayName("Gender"), QuickFilter]
        public Masters.GenderMaster? Gender
        {
            get { return (Masters.GenderMaster?)Fields.Gender[this]; }
            set { Fields.Gender[this] = (Int32?)value; }
        }

        [DisplayName("Religion")]
        public Masters.ReligionMaster? Religion
        {
            get { return (Masters.ReligionMaster?)Fields.Religion[this]; }
            set { Fields.Religion[this] = (Int32?)value; }
        }

        [DisplayName("Area"), ForeignKey("[dbo].[Area]", "Id"), LeftJoin("jArea"), TextualField("Area")]
        [LookupEditor(typeof(AreaRow), InplaceAdd = true)]
        public Int32? AreaId
        {
            get { return Fields.AreaId[this]; }
            set { Fields.AreaId[this] = value; }
        }

        [DisplayName("Marital Status"), QuickFilter]
        public Masters.MaritalMaster? MaritalStatus
        {
            get { return (Masters.MaritalMaster?)Fields.MaritalStatus[this]; }
            set { Fields.MaritalStatus[this] = (Int32?)value; }
        }

        [DisplayName("Marriage Anniversary")]
        public DateTime? MarriageAnniversary
        {
            get { return Fields.MarriageAnniversary[this]; }
            set { Fields.MarriageAnniversary[this] = value; }
        }

        [DisplayName("Birthdate")]
        public DateTime? Birthdate
        {
            get { return Fields.Birthdate[this]; }
            set { Fields.Birthdate[this] = value; }
        }

        [DisplayName("Date Of Joining"), QuickFilter]
        public DateTime? DateOfJoining
        {
            get { return Fields.DateOfJoining[this]; }
            set { Fields.DateOfJoining[this] = value; }
        }

        [DisplayName("Company"), NotNull, ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), QuickFilter]
        [LookupEditor(typeof(CompanyDetailsRow), InplaceAdd = false)]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }

        [DisplayName("Roles"), ForeignKey("[dbo].[Roles]", "RoleId"), LeftJoin("jRoles"), TextualField("RolesRoleName"), QuickFilter]
        [LookupEditor(typeof(RoleRow), InplaceAdd = true)]
        public Int32? RolesId
        {
            get { return Fields.RolesId[this]; }
            set { Fields.RolesId[this] = value; }
        }

        [DisplayName("Owner"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername"), QuickFilter]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get { return Fields.OwnerId[this]; }
            set { Fields.OwnerId[this] = value; }
        }

        [DisplayName("Adhaar No"), Size(50)]
        public String AdhaarNo
        {
            get { return Fields.AdhaarNo[this]; }
            set { Fields.AdhaarNo[this] = value; }
        }

        [DisplayName("Pan No"), Column("PANNo"), Size(50)]
        public String PanNo
        {
            get { return Fields.PanNo[this]; }
            set { Fields.PanNo[this] = value; }
        }

        [DisplayName("Attachment"), Size(500)]
        [MultipleImageUploadEditor(FilenameFormat = "Contacts/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("Bank Name"), Size(100)]
        public String BankName
        {
            get { return Fields.BankName[this]; }
            set { Fields.BankName[this] = value; }
        }

        [DisplayName("Account Number"), Size(100)]
        public String AccountNumber
        {
            get { return Fields.AccountNumber[this]; }
            set { Fields.AccountNumber[this] = value; }
        }

        [DisplayName("Ifsc"), Column("IFSC"), Size(100)]
        public String Ifsc
        {
            get { return Fields.Ifsc[this]; }
            set { Fields.Ifsc[this] = value; }
        }

        [DisplayName("Bank Type"), Size(100)]
        public String BankType
        {
            get { return Fields.BankType[this]; }
            set { Fields.BankType[this] = value; }
        }

        [DisplayName("Branch"), Size(100)]
        public String Branch
        {
            get { return Fields.Branch[this]; }
            set { Fields.Branch[this] = value; }
        }

        [DisplayName("Tehsil"), ForeignKey("[dbo].[Tehsil]", "Id"), LeftJoin("jTehsil"), TextualField("Tehsil")]
        public Int32? TehsilId
        {
            get { return Fields.TehsilId[this]; }
            set { Fields.TehsilId[this] = value; }
        }

        [DisplayName("Village"), ForeignKey("[dbo].[Village]", "Id"), LeftJoin("jVillage"), TextualField("Village")]
        public Int32? VillageId
        {
            get { return Fields.VillageId[this]; }
            set { Fields.VillageId[this] = value; }
        }

        // Expression uses the correct column name from the Department table
        [DisplayName("Department"), Expression("jDepartment.[DepartmentName]")]
        public String Department
        {
            get { return Fields.Department[this]; }
            set { Fields.Department[this] = value; }
        }

        [DisplayName("City"), Expression("jCity.[City]")]
        public String City
        {
            get { return Fields.City[this]; }
            set { Fields.City[this] = value; }
        }

        [DisplayName("City State Id"), Expression("jCity.[StateId]")]
        public Int32? CityStateId
        {
            get { return Fields.CityStateId[this]; }
            set { Fields.CityStateId[this] = value; }
        }

        [DisplayName("State"), Expression("jState.[State]")]
        public String State
        {
            get { return Fields.State[this]; }
            set { Fields.State[this] = value; }
        }

        [DisplayName("Area"), Expression("jArea.[Area]")]
        public String Area
        {
            get { return Fields.Area[this]; }
            set { Fields.Area[this] = value; }
        }

        [DisplayName("Company Name"), Expression("jCompany.[Name]")]
        public String CompanyName
        {
            get { return Fields.CompanyName[this]; }
            set { Fields.CompanyName[this] = value; }
        }

        [DisplayName("Company Slogan"), Expression("jCompany.[Slogan]")]
        public String CompanySlogan
        {
            get { return Fields.CompanySlogan[this]; }
            set { Fields.CompanySlogan[this] = value; }
        }

        [DisplayName("Company Address"), Expression("jCompany.[Address]")]
        public String CompanyAddress
        {
            get { return Fields.CompanyAddress[this]; }
            set { Fields.CompanyAddress[this] = value; }
        }

        [DisplayName("Company Phone"), Expression("jCompany.[Phone]")]
        public String CompanyPhone
        {
            get { return Fields.CompanyPhone[this]; }
            set { Fields.CompanyPhone[this] = value; }
        }

        [DisplayName("Company Logo"), Expression("jCompany.[Logo]")]
        public String CompanyLogo
        {
            get { return Fields.CompanyLogo[this]; }
            set { Fields.CompanyLogo[this] = value; }
        }

        [DisplayName("Company Logo Height"), Expression("jCompany.[LogoHeight]")]
        public Int32? CompanyLogoHeight
        {
            get { return Fields.CompanyLogoHeight[this]; }
            set { Fields.CompanyLogoHeight[this] = value; }
        }

        [DisplayName("Company Logo Width"), Expression("jCompany.[LogoWidth]")]
        public Int32? CompanyLogoWidth
        {
            get { return Fields.CompanyLogoWidth[this]; }
            set { Fields.CompanyLogoWidth[this] = value; }
        }

        [DisplayName("Company Header Image"), Expression("jCompany.[HeaderImage]")]
        public String CompanyHeaderImage
        {
            get { return Fields.CompanyHeaderImage[this]; }
            set { Fields.CompanyHeaderImage[this] = value; }
        }

        [DisplayName("Company Header Height"), Expression("jCompany.[HeaderHeight]")]
        public Int32? CompanyHeaderHeight
        {
            get { return Fields.CompanyHeaderHeight[this]; }
            set { Fields.CompanyHeaderHeight[this] = value; }
        }

        [DisplayName("Company Header Width"), Expression("jCompany.[HeaderWidth]")]
        public Int32? CompanyHeaderWidth
        {
            get { return Fields.CompanyHeaderWidth[this]; }
            set { Fields.CompanyHeaderWidth[this] = value; }
        }

        [DisplayName("Company Footer Image"), Expression("jCompany.[FooterImage]")]
        public String CompanyFooterImage
        {
            get { return Fields.CompanyFooterImage[this]; }
            set { Fields.CompanyFooterImage[this] = value; }
        }

        [DisplayName("Company Footer Height"), Expression("jCompany.[FooterHeight]")]
        public Int32? CompanyFooterHeight
        {
            get { return Fields.CompanyFooterHeight[this]; }
            set { Fields.CompanyFooterHeight[this] = value; }
        }

        [DisplayName("Company Footer Width"), Expression("jCompany.[FooterWidth]")]
        public Int32? CompanyFooterWidth
        {
            get { return Fields.CompanyFooterWidth[this]; }
            set { Fields.CompanyFooterWidth[this] = value; }
        }

        [DisplayName("Company Gstin"), Expression("jCompany.[GSTIN]")]
        public String CompanyGstin
        {
            get { return Fields.CompanyGstin[this]; }
            set { Fields.CompanyGstin[this] = value; }
        }

        [DisplayName("Company Pan No"), Expression("jCompany.[PANNo]")]
        public String CompanyPanNo
        {
            get { return Fields.CompanyPanNo[this]; }
            set { Fields.CompanyPanNo[this] = value; }
        }

        [DisplayName("Company Enquiry Follwup Mandatory"), Expression("jCompany.[EnquiryFollwupMandatory]")]
        public Boolean? CompanyEnquiryFollwupMandatory
        {
            get { return Fields.CompanyEnquiryFollwupMandatory[this]; }
            set { Fields.CompanyEnquiryFollwupMandatory[this] = value; }
        }

        [DisplayName("Company Quotation Follwup Mandatory"), Expression("jCompany.[QuotationFollwupMandatory]")]
        public Boolean? CompanyQuotationFollwupMandatory
        {
            get { return Fields.CompanyQuotationFollwupMandatory[this]; }
            set { Fields.CompanyQuotationFollwupMandatory[this] = value; }
        }

        [DisplayName("Company Enquiry Products Mandatory"), Expression("jCompany.[EnquiryProductsMandatory]")]
        public Boolean? CompanyEnquiryProductsMandatory
        {
            get { return Fields.CompanyEnquiryProductsMandatory[this]; }
            set { Fields.CompanyEnquiryProductsMandatory[this] = value; }
        }

        [DisplayName("Company Quotation Products Mandatory"), Expression("jCompany.[QuotationProductsMandatory]")]
        public Boolean? CompanyQuotationProductsMandatory
        {
            get { return Fields.CompanyQuotationProductsMandatory[this]; }
            set { Fields.CompanyQuotationProductsMandatory[this] = value; }
        }

        [DisplayName("Company Roundup In Sales"), Expression("jCompany.[RoundupInSales]")]
        public Boolean? CompanyRoundupInSales
        {
            get { return Fields.CompanyRoundupInSales[this]; }
            set { Fields.CompanyRoundupInSales[this] = value; }
        }

        [DisplayName("Company Packaging In Sales"), Expression("jCompany.[PackagingInSales]")]
        public Boolean? CompanyPackagingInSales
        {
            get { return Fields.CompanyPackagingInSales[this]; }
            set { Fields.CompanyPackagingInSales[this] = value; }
        }

        [DisplayName("Company Freight In Sales"), Expression("jCompany.[FreightInSales]")]
        public Boolean? CompanyFreightInSales
        {
            get { return Fields.CompanyFreightInSales[this]; }
            set { Fields.CompanyFreightInSales[this] = value; }
        }

        [DisplayName("Company Due Date In Sales"), Expression("jCompany.[DueDateInSales]")]
        public Boolean? CompanyDueDateInSales
        {
            get { return Fields.CompanyDueDateInSales[this]; }
            set { Fields.CompanyDueDateInSales[this] = value; }
        }

        [DisplayName("Company Dispatch In Sales"), Expression("jCompany.[DispatchInSales]")]
        public Boolean? CompanyDispatchInSales
        {
            get { return Fields.CompanyDispatchInSales[this]; }
            set { Fields.CompanyDispatchInSales[this] = value; }
        }

        [DisplayName("Company Gst Details In Sales"), Expression("jCompany.[GSTDetailsInSales]")]
        public Boolean? CompanyGstDetailsInSales
        {
            get { return Fields.CompanyGstDetailsInSales[this]; }
            set { Fields.CompanyGstDetailsInSales[this] = value; }
        }

        [DisplayName("Company Followups In Sales"), Expression("jCompany.[FollowupsInSales]")]
        public Boolean? CompanyFollowupsInSales
        {
            get { return Fields.CompanyFollowupsInSales[this]; }
            set { Fields.CompanyFollowupsInSales[this] = value; }
        }

        [DisplayName("Company Terms In Sales"), Expression("jCompany.[TermsInSales]")]
        public Boolean? CompanyTermsInSales
        {
            get { return Fields.CompanyTermsInSales[this]; }
            set { Fields.CompanyTermsInSales[this] = value; }
        }

        [DisplayName("Company Advance In Sales"), Expression("jCompany.[AdvanceInSales]")]
        public Boolean? CompanyAdvanceInSales
        {
            get { return Fields.CompanyAdvanceInSales[this]; }
            set { Fields.CompanyAdvanceInSales[this] = value; }
        }

        [DisplayName("Company Stage In Sales"), Expression("jCompany.[StageInSales]")]
        public Boolean? CompanyStageInSales
        {
            get { return Fields.CompanyStageInSales[this]; }
            set { Fields.CompanyStageInSales[this] = value; }
        }

        [DisplayName("Company Code In Sales"), Expression("jCompany.[CodeInSales]")]
        public Boolean? CompanyCodeInSales
        {
            get { return Fields.CompanyCodeInSales[this]; }
            set { Fields.CompanyCodeInSales[this] = value; }
        }

        [DisplayName("Company Serial In Sales"), Expression("jCompany.[SerialInSales]")]
        public Boolean? CompanySerialInSales
        {
            get { return Fields.CompanySerialInSales[this]; }
            set { Fields.CompanySerialInSales[this] = value; }
        }

        [DisplayName("Company Batch In Sales"), Expression("jCompany.[BatchInSales]")]
        public Boolean? CompanyBatchInSales
        {
            get { return Fields.CompanyBatchInSales[this]; }
            set { Fields.CompanyBatchInSales[this] = value; }
        }

        [DisplayName("Company Discount In Sales"), Expression("jCompany.[DiscountInSales]")]
        public Boolean? CompanyDiscountInSales
        {
            get { return Fields.CompanyDiscountInSales[this]; }
            set { Fields.CompanyDiscountInSales[this] = value; }
        }

        [DisplayName("Company Tax In Sales"), Expression("jCompany.[TAXInSales]")]
        public Boolean? CompanyTaxInSales
        {
            get { return Fields.CompanyTaxInSales[this]; }
            set { Fields.CompanyTaxInSales[this] = value; }
        }

        [DisplayName("Company Warranty In Sales"), Expression("jCompany.[WarrantyInSales]")]
        public Boolean? CompanyWarrantyInSales
        {
            get { return Fields.CompanyWarrantyInSales[this]; }
            set { Fields.CompanyWarrantyInSales[this] = value; }
        }

        [DisplayName("Company Description In Sales"), Expression("jCompany.[DescriptionInSales]")]
        public Boolean? CompanyDescriptionInSales
        {
            get { return Fields.CompanyDescriptionInSales[this]; }
            set { Fields.CompanyDescriptionInSales[this] = value; }
        }

        [DisplayName("Company Appointment In Enquiry"), Expression("jCompany.[AppointmentInEnquiry]")]
        public Boolean? CompanyAppointmentInEnquiry
        {
            get { return Fields.CompanyAppointmentInEnquiry[this]; }
            set { Fields.CompanyAppointmentInEnquiry[this] = value; }
        }

        [DisplayName("Company Appointment In Quotation"), Expression("jCompany.[AppointmentInQuotation]")]
        public Boolean? CompanyAppointmentInQuotation
        {
            get { return Fields.CompanyAppointmentInQuotation[this]; }
            set { Fields.CompanyAppointmentInQuotation[this] = value; }
        }

        [DisplayName("Company Appointment In Proforma"), Expression("jCompany.[AppointmentInProforma]")]
        public Boolean? CompanyAppointmentInProforma
        {
            get { return Fields.CompanyAppointmentInProforma[this]; }
            set { Fields.CompanyAppointmentInProforma[this] = value; }
        }

        [DisplayName("Company Appointment In Sales"), Expression("jCompany.[AppointmentInSales]")]
        public Boolean? CompanyAppointmentInSales
        {
            get { return Fields.CompanyAppointmentInSales[this]; }
            set { Fields.CompanyAppointmentInSales[this] = value; }
        }

        [DisplayName("Company Appointment In Tele Calling"), Expression("jCompany.[AppointmentInTeleCalling]")]
        public Boolean? CompanyAppointmentInTeleCalling
        {
            get { return Fields.CompanyAppointmentInTeleCalling[this]; }
            set { Fields.CompanyAppointmentInTeleCalling[this] = value; }
        }

        [DisplayName("Company Requirement In Enquiry"), Expression("jCompany.[RequirementInEnquiry]")]
        public Boolean? CompanyRequirementInEnquiry
        {
            get { return Fields.CompanyRequirementInEnquiry[this]; }
            set { Fields.CompanyRequirementInEnquiry[this] = value; }
        }

        [DisplayName("Company Tax In Stock Transfer"), Expression("jCompany.[TAXInStockTransfer]")]
        public Boolean? CompanyTaxInStockTransfer
        {
            get { return Fields.CompanyTaxInStockTransfer[this]; }
            set { Fields.CompanyTaxInStockTransfer[this] = value; }
        }

        [DisplayName("Company Phone Compulsory"), Expression("jCompany.[PhoneCompulsory]")]
        public Boolean? CompanyPhoneCompulsory
        {
            get { return Fields.CompanyPhoneCompulsory[this]; }
            set { Fields.CompanyPhoneCompulsory[this] = value; }
        }

        [DisplayName("Company Email Compulsory"), Expression("jCompany.[EmailCompulsory]")]
        public Boolean? CompanyEmailCompulsory
        {
            get { return Fields.CompanyEmailCompulsory[this]; }
            set { Fields.CompanyEmailCompulsory[this] = value; }
        }

        [DisplayName("Company Auto Sms Appointments"), Expression("jCompany.[AutoSMSAppointments]")]
        public Boolean? CompanyAutoSmsAppointments
        {
            get { return Fields.CompanyAutoSmsAppointments[this]; }
            set { Fields.CompanyAutoSmsAppointments[this] = value; }
        }

        [DisplayName("Company Allow Moving Non Closed Records"), Expression("jCompany.[AllowMovingNonClosedRecords]")]
        public Boolean? CompanyAllowMovingNonClosedRecords
        {
            get { return Fields.CompanyAllowMovingNonClosedRecords[this]; }
            set { Fields.CompanyAllowMovingNonClosedRecords[this] = value; }
        }

        [DisplayName("Company State Compulsory In Contacts"), Expression("jCompany.[StateCompulsoryInContacts]")]
        public Boolean? CompanyStateCompulsoryInContacts
        {
            get { return Fields.CompanyStateCompulsoryInContacts[this]; }
            set { Fields.CompanyStateCompulsoryInContacts[this] = value; }
        }

        [DisplayName("Company Enable Address In Transactions"), Expression("jCompany.[EnableAddressInTransactions]")]
        public Boolean? CompanyEnableAddressInTransactions
        {
            get { return Fields.CompanyEnableAddressInTransactions[this]; }
            set { Fields.CompanyEnableAddressInTransactions[this] = value; }
        }

        [DisplayName("Company Auto Email Enquiry"), Expression("jCompany.[AutoEmailEnquiry]")]
        public Boolean? CompanyAutoEmailEnquiry
        {
            get { return Fields.CompanyAutoEmailEnquiry[this]; }
            set { Fields.CompanyAutoEmailEnquiry[this] = value; }
        }

        [DisplayName("Company Auto Sms Enquiry"), Expression("jCompany.[AutoSMSEnquiry]")]
        public Boolean? CompanyAutoSmsEnquiry
        {
            get { return Fields.CompanyAutoSmsEnquiry[this]; }
            set { Fields.CompanyAutoSmsEnquiry[this] = value; }
        }

        [DisplayName("Company Auto Email Quotation"), Expression("jCompany.[AutoEmailQuotation]")]
        public Boolean? CompanyAutoEmailQuotation
        {
            get { return Fields.CompanyAutoEmailQuotation[this]; }
            set { Fields.CompanyAutoEmailQuotation[this] = value; }
        }

        [DisplayName("Company Auto Sms Quotation"), Expression("jCompany.[AutoSMSQuotation]")]
        public Boolean? CompanyAutoSmsQuotation
        {
            get { return Fields.CompanyAutoSmsQuotation[this]; }
            set { Fields.CompanyAutoSmsQuotation[this] = value; }
        }

        [DisplayName("Company Auto Email Proforma"), Expression("jCompany.[AutoEmailProforma]")]
        public Boolean? CompanyAutoEmailProforma
        {
            get { return Fields.CompanyAutoEmailProforma[this]; }
            set { Fields.CompanyAutoEmailProforma[this] = value; }
        }

        [DisplayName("Company Auto Sms Proforma"), Expression("jCompany.[AutoSMSProforma]")]
        public Boolean? CompanyAutoSmsProforma
        {
            get { return Fields.CompanyAutoSmsProforma[this]; }
            set { Fields.CompanyAutoSmsProforma[this] = value; }
        }

        [DisplayName("Company Auto Email Invoice"), Expression("jCompany.[AutoEmailInvoice]")]
        public Boolean? CompanyAutoEmailInvoice
        {
            get { return Fields.CompanyAutoEmailInvoice[this]; }
            set { Fields.CompanyAutoEmailInvoice[this] = value; }
        }

        [DisplayName("Company Auto Sms Invoice"), Expression("jCompany.[AutoSMSInvoice]")]
        public Boolean? CompanyAutoSmsInvoice
        {
            get { return Fields.CompanyAutoSmsInvoice[this]; }
            set { Fields.CompanyAutoSmsInvoice[this] = value; }
        }

        [DisplayName("Company Hide Description In Sales"), Expression("jCompany.[HideDescriptionInSales]")]
        public Boolean? CompanyHideDescriptionInSales
        {
            get { return Fields.CompanyHideDescriptionInSales[this]; }
            set { Fields.CompanyHideDescriptionInSales[this] = value; }
        }

        [DisplayName("Company Hide Description In Proforma"), Expression("jCompany.[HideDescriptionInProforma]")]
        public Boolean? CompanyHideDescriptionInProforma
        {
            get { return Fields.CompanyHideDescriptionInProforma[this]; }
            set { Fields.CompanyHideDescriptionInProforma[this] = value; }
        }

        [DisplayName("Company Hide Description In Challan"), Expression("jCompany.[HideDescriptionInChallan]")]
        public Boolean? CompanyHideDescriptionInChallan
        {
            get { return Fields.CompanyHideDescriptionInChallan[this]; }
            set { Fields.CompanyHideDescriptionInChallan[this] = value; }
        }

        [DisplayName("Company Quotation Template"), Expression("jCompany.[QuotationTemplate]")]
        public Int32? CompanyQuotationTemplate
        {
            get { return Fields.CompanyQuotationTemplate[this]; }
            set { Fields.CompanyQuotationTemplate[this] = value; }
        }

        [DisplayName("Company Country"), Expression("jCompany.[Country]")]
        public Int32? CompanyCountry
        {
            get { return Fields.CompanyCountry[this]; }
            set { Fields.CompanyCountry[this] = value; }
        }

        [DisplayName("Company Multi Currency"), Expression("jCompany.[MultiCurrency]")]
        public Boolean? CompanyMultiCurrency
        {
            get { return Fields.CompanyMultiCurrency[this]; }
            set { Fields.CompanyMultiCurrency[this] = value; }
        }

        [DisplayName("Company Project With Contacts"), Expression("jCompany.[ProjectWithContacts]")]
        public Boolean? CompanyProjectWithContacts
        {
            get { return Fields.CompanyProjectWithContacts[this]; }
            set { Fields.CompanyProjectWithContacts[this] = value; }
        }

        [DisplayName("Company State Id"), Expression("jCompany.[StateId]")]
        public Int32? CompanyStateId
        {
            get { return Fields.CompanyStateId[this]; }
            set { Fields.CompanyStateId[this] = value; }
        }

        [DisplayName("Company Roundup In Purchase"), Expression("jCompany.[RoundupInPurchase]")]
        public Boolean? CompanyRoundupInPurchase
        {
            get { return Fields.CompanyRoundupInPurchase[this]; }
            set { Fields.CompanyRoundupInPurchase[this] = value; }
        }

        [DisplayName("Company Invoice Template"), Expression("jCompany.[InvoiceTemplate]")]
        public Int32? CompanyInvoiceTemplate
        {
            get { return Fields.CompanyInvoiceTemplate[this]; }
            set { Fields.CompanyInvoiceTemplate[this] = value; }
        }

        [DisplayName("Company Company Details"), Expression("jCompany.[CompanyDetails]")]
        public Boolean? CompanyCompanyDetails
        {
            get { return Fields.CompanyCompanyDetails[this]; }
            set { Fields.CompanyCompanyDetails[this] = value; }
        }

        [DisplayName("Company Enable Additional Charges"), Expression("jCompany.[EnableAdditionalCharges]")]
        public Boolean? CompanyEnableAdditionalCharges
        {
            get { return Fields.CompanyEnableAdditionalCharges[this]; }
            set { Fields.CompanyEnableAdditionalCharges[this] = value; }
        }

        [DisplayName("Company Enable Additional Concessions"), Expression("jCompany.[EnableAdditionalConcessions]")]
        public Boolean? CompanyEnableAdditionalConcessions
        {
            get { return Fields.CompanyEnableAdditionalConcessions[this]; }
            set { Fields.CompanyEnableAdditionalConcessions[this] = value; }
        }

        [DisplayName("Company Additional Images"), Expression("jCompany.[AdditionalImages]")]
        public String CompanyAdditionalImages
        {
            get { return Fields.CompanyAdditionalImages[this]; }
            set { Fields.CompanyAdditionalImages[this] = value; }
        }

        [DisplayName("Company Header Content"), Expression("jCompany.[HeaderContent]")]
        public String CompanyHeaderContent
        {
            get { return Fields.CompanyHeaderContent[this]; }
            set { Fields.CompanyHeaderContent[this] = value; }
        }

        [DisplayName("Company Footer Content"), Expression("jCompany.[FooterContent]")]
        public String CompanyFooterContent
        {
            get { return Fields.CompanyFooterContent[this]; }
            set { Fields.CompanyFooterContent[this] = value; }
        }

        [DisplayName("Company Quotation Suffix"), Expression("jCompany.[QuotationSuffix]")]
        public String CompanyQuotationSuffix
        {
            get { return Fields.CompanyQuotationSuffix[this]; }
            set { Fields.CompanyQuotationSuffix[this] = value; }
        }

        [DisplayName("Company Quotation Prefix"), Expression("jCompany.[QuotationPrefix]")]
        public String CompanyQuotationPrefix
        {
            get { return Fields.CompanyQuotationPrefix[this]; }
            set { Fields.CompanyQuotationPrefix[this] = value; }
        }

        [DisplayName("Company Invoice Suffix"), Expression("jCompany.[InvoiceSuffix]")]
        public String CompanyInvoiceSuffix
        {
            get { return Fields.CompanyInvoiceSuffix[this]; }
            set { Fields.CompanyInvoiceSuffix[this] = value; }
        }

        [DisplayName("Company Invoice Prefix"), Expression("jCompany.[InvoicePrefix]")]
        public String CompanyInvoicePrefix
        {
            get { return Fields.CompanyInvoicePrefix[this]; }
            set { Fields.CompanyInvoicePrefix[this] = value; }
        }

        [DisplayName("Company Challan Suffix"), Expression("jCompany.[ChallanSuffix]")]
        public String CompanyChallanSuffix
        {
            get { return Fields.CompanyChallanSuffix[this]; }
            set { Fields.CompanyChallanSuffix[this] = value; }
        }

        [DisplayName("Company Challan Prefix"), Expression("jCompany.[ChallanPrefix]")]
        public String CompanyChallanPrefix
        {
            get { return Fields.CompanyChallanPrefix[this]; }
            set { Fields.CompanyChallanPrefix[this] = value; }
        }

        [DisplayName("Company Quotation Tax Column Included"), Expression("jCompany.[QuotationTaxColumnIncluded]")]
        public Boolean? CompanyQuotationTaxColumnIncluded
        {
            get { return Fields.CompanyQuotationTaxColumnIncluded[this]; }
            set { Fields.CompanyQuotationTaxColumnIncluded[this] = value; }
        }

        [DisplayName("Company Invoice Tax Column Included"), Expression("jCompany.[InvoiceTaxColumnIncluded]")]
        public Boolean? CompanyInvoiceTaxColumnIncluded
        {
            get { return Fields.CompanyInvoiceTaxColumnIncluded[this]; }
            set { Fields.CompanyInvoiceTaxColumnIncluded[this] = value; }
        }

        [DisplayName("Company Quotation Discounted Price Included"), Expression("jCompany.[QuotationDiscountedPriceIncluded]")]
        public Boolean? CompanyQuotationDiscountedPriceIncluded
        {
            get { return Fields.CompanyQuotationDiscountedPriceIncluded[this]; }
            set { Fields.CompanyQuotationDiscountedPriceIncluded[this] = value; }
        }

        [DisplayName("Company Invoice Discounted Price Included"), Expression("jCompany.[InvoiceDiscountedPriceIncluded]")]
        public Boolean? CompanyInvoiceDiscountedPriceIncluded
        {
            get { return Fields.CompanyInvoiceDiscountedPriceIncluded[this]; }
            set { Fields.CompanyInvoiceDiscountedPriceIncluded[this] = value; }
        }

        [DisplayName("Company Roundup In Quotation"), Expression("jCompany.[RoundupInQuotation]")]
        public Boolean? CompanyRoundupInQuotation
        {
            get { return Fields.CompanyRoundupInQuotation[this]; }
            set { Fields.CompanyRoundupInQuotation[this] = value; }
        }

        [DisplayName("Company Quotation Contact Included"), Expression("jCompany.[QuotationContactIncluded]")]
        public Boolean? CompanyQuotationContactIncluded
        {
            get { return Fields.CompanyQuotationContactIncluded[this]; }
            set { Fields.CompanyQuotationContactIncluded[this] = value; }
        }

        [DisplayName("Company Company Type"), Expression("jCompany.[CompanyType]")]
        public Int32? CompanyCompanyType
        {
            get { return Fields.CompanyCompanyType[this]; }
            set { Fields.CompanyCompanyType[this] = value; }
        }

        [DisplayName("Company Enquiry Suffix"), Expression("jCompany.[EnquirySuffix]")]
        public String CompanyEnquirySuffix
        {
            get { return Fields.CompanyEnquirySuffix[this]; }
            set { Fields.CompanyEnquirySuffix[this] = value; }
        }

        [DisplayName("Company Enquiry Prefix"), Expression("jCompany.[EnquiryPrefix]")]
        public String CompanyEnquiryPrefix
        {
            get { return Fields.CompanyEnquiryPrefix[this]; }
            set { Fields.CompanyEnquiryPrefix[this] = value; }
        }

        [DisplayName("Company Country Mandatory"), Expression("jCompany.[CountryMandatory]")]
        public Boolean? CompanyCountryMandatory
        {
            get { return Fields.CompanyCountryMandatory[this]; }
            set { Fields.CompanyCountryMandatory[this] = value; }
        }

        [DisplayName("Company Pincode Mandatory"), Expression("jCompany.[PincodeMandatory]")]
        public Boolean? CompanyPincodeMandatory
        {
            get { return Fields.CompanyPincodeMandatory[this]; }
            set { Fields.CompanyPincodeMandatory[this] = value; }
        }

        [DisplayName("Company City Mandatory"), Expression("jCompany.[CityMandatory]")]
        public Boolean? CompanyCityMandatory
        {
            get { return Fields.CompanyCityMandatory[this]; }
            set { Fields.CompanyCityMandatory[this] = value; }
        }

        [DisplayName("Company Capacity In Products"), Expression("jCompany.[CapacityInProducts]")]
        public Boolean? CompanyCapacityInProducts
        {
            get { return Fields.CompanyCapacityInProducts[this]; }
            set { Fields.CompanyCapacityInProducts[this] = value; }
        }

        [DisplayName("Company Year In Prefix"), Expression("jCompany.[YearInPrefix]")]
        public Int32? CompanyYearInPrefix
        {
            get { return Fields.CompanyYearInPrefix[this]; }
            set { Fields.CompanyYearInPrefix[this] = value; }
        }

        [DisplayName("Company Invoice Header Content"), Expression("jCompany.[InvoiceHeaderContent]")]
        public String CompanyInvoiceHeaderContent
        {
            get { return Fields.CompanyInvoiceHeaderContent[this]; }
            set { Fields.CompanyInvoiceHeaderContent[this] = value; }
        }

        [DisplayName("Company Invoice Footer Content"), Expression("jCompany.[InvoiceFooterContent]")]
        public String CompanyInvoiceFooterContent
        {
            get { return Fields.CompanyInvoiceFooterContent[this]; }
            set { Fields.CompanyInvoiceFooterContent[this] = value; }
        }

        [DisplayName("Company Invoice Header Image"), Expression("jCompany.[InvoiceHeaderImage]")]
        public String CompanyInvoiceHeaderImage
        {
            get { return Fields.CompanyInvoiceHeaderImage[this]; }
            set { Fields.CompanyInvoiceHeaderImage[this] = value; }
        }

        [DisplayName("Company Invoice Header Height"), Expression("jCompany.[InvoiceHeaderHeight]")]
        public Int32? CompanyInvoiceHeaderHeight
        {
            get { return Fields.CompanyInvoiceHeaderHeight[this]; }
            set { Fields.CompanyInvoiceHeaderHeight[this] = value; }
        }

        [DisplayName("Company Invoice Header Width"), Expression("jCompany.[InvoiceHeaderWidth]")]
        public Int32? CompanyInvoiceHeaderWidth
        {
            get { return Fields.CompanyInvoiceHeaderWidth[this]; }
            set { Fields.CompanyInvoiceHeaderWidth[this] = value; }
        }

        [DisplayName("Company Invoice Footer Image"), Expression("jCompany.[InvoiceFooterImage]")]
        public String CompanyInvoiceFooterImage
        {
            get { return Fields.CompanyInvoiceFooterImage[this]; }
            set { Fields.CompanyInvoiceFooterImage[this] = value; }
        }

        [DisplayName("Company Invoice Footer Height"), Expression("jCompany.[InvoiceFooterHeight]")]
        public Int32? CompanyInvoiceFooterHeight
        {
            get { return Fields.CompanyInvoiceFooterHeight[this]; }
            set { Fields.CompanyInvoiceFooterHeight[this] = value; }
        }

        [DisplayName("Company Invoice Footer Width"), Expression("jCompany.[InvoiceFooterWidth]")]
        public Int32? CompanyInvoiceFooterWidth
        {
            get { return Fields.CompanyInvoiceFooterWidth[this]; }
            set { Fields.CompanyInvoiceFooterWidth[this] = value; }
        }

        [DisplayName("Company Dc Header Content"), Expression("jCompany.[DCHeaderContent]")]
        public String CompanyDcHeaderContent
        {
            get { return Fields.CompanyDcHeaderContent[this]; }
            set { Fields.CompanyDcHeaderContent[this] = value; }
        }

        [DisplayName("Company Dc Footer Content"), Expression("jCompany.[DCFooterContent]")]
        public String CompanyDcFooterContent
        {
            get { return Fields.CompanyDcFooterContent[this]; }
            set { Fields.CompanyDcFooterContent[this] = value; }
        }

        [DisplayName("Company Dc Header Image"), Expression("jCompany.[DCHeaderImage]")]
        public String CompanyDcHeaderImage
        {
            get { return Fields.CompanyDcHeaderImage[this]; }
            set { Fields.CompanyDcHeaderImage[this] = value; }
        }

        [DisplayName("Company Dc Header Height"), Expression("jCompany.[DCHeaderHeight]")]
        public Int32? CompanyDcHeaderHeight
        {
            get { return Fields.CompanyDcHeaderHeight[this]; }
            set { Fields.CompanyDcHeaderHeight[this] = value; }
        }

        [DisplayName("Company Dc Header Width"), Expression("jCompany.[DCHeaderWidth]")]
        public Int32? CompanyDcHeaderWidth
        {
            get { return Fields.CompanyDcHeaderWidth[this]; }
            set { Fields.CompanyDcHeaderWidth[this] = value; }
        }

        [DisplayName("Company Dc Footer Image"), Expression("jCompany.[DCFooterImage]")]
        public String CompanyDcFooterImage
        {
            get { return Fields.CompanyDcFooterImage[this]; }
            set { Fields.CompanyDcFooterImage[this] = value; }
        }

        [DisplayName("Company Dc Footer Height"), Expression("jCompany.[DCFooterHeight]")]
        public Int32? CompanyDcFooterHeight
        {
            get { return Fields.CompanyDcFooterHeight[this]; }
            set { Fields.CompanyDcFooterHeight[this] = value; }
        }

        [DisplayName("Company Dc Footer Width"), Expression("jCompany.[DCFooterWidth]")]
        public Int32? CompanyDcFooterWidth
        {
            get { return Fields.CompanyDcFooterWidth[this]; }
            set { Fields.CompanyDcFooterWidth[this] = value; }
        }

        [DisplayName("Company Addinfo2"), Expression("jCompany.[Addinfo2]")]
        public Boolean? CompanyAddinfo2
        {
            get { return Fields.CompanyAddinfo2[this]; }
            set { Fields.CompanyAddinfo2[this] = value; }
        }

        [DisplayName("Company Multi Add Info"), Expression("jCompany.[MultiAddInfo]")]
        public Boolean? CompanyMultiAddInfo
        {
            get { return Fields.CompanyMultiAddInfo[this]; }
            set { Fields.CompanyMultiAddInfo[this] = value; }
        }

        [DisplayName("Company Quotation Total"), Expression("jCompany.[QuotationTotal]")]
        public Boolean? CompanyQuotationTotal
        {
            get { return Fields.CompanyQuotationTotal[this]; }
            set { Fields.CompanyQuotationTotal[this] = value; }
        }

        [DisplayName("Company Valid Date"), Expression("jCompany.[ValidDate]")]
        public Boolean? CompanyValidDate
        {
            get { return Fields.CompanyValidDate[this]; }
            set { Fields.CompanyValidDate[this] = value; }
        }

        [DisplayName("Company Dealer In Cms"), Expression("jCompany.[DealerInCMS]")]
        public Boolean? CompanyDealerInCms
        {
            get { return Fields.CompanyDealerInCms[this]; }
            set { Fields.CompanyDealerInCms[this] = value; }
        }

        [DisplayName("Roles Role Name"), Expression("jRoles.[RoleName]")]
        public String RolesRoleName
        {
            get { return Fields.RolesRoleName[this]; }
            set { Fields.RolesRoleName[this] = value; }
        }

        [DisplayName("Roles Company Id"), Expression("jRoles.[CompanyId]")]
        public Int32? RolesCompanyId
        {
            get { return Fields.RolesCompanyId[this]; }
            set { Fields.RolesCompanyId[this] = value; }
        }

        [DisplayName("Owner Username"), Expression("jOwner.[Username]")]
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

        [DisplayName("Owner Ssl"), Expression("jOwner.[SSL]")]
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

        [DisplayName("Owner Branch Id"), Expression("jOwner.[BranchId]")]
        public Int32? OwnerBranchId
        {
            get { return Fields.OwnerBranchId[this]; }
            set { Fields.OwnerBranchId[this] = value; }
        }

        [DisplayName("Owner Company Id"), Expression("jOwner.[CompanyId]")]
        public Int32? OwnerCompanyId
        {
            get { return Fields.OwnerCompanyId[this]; }
            set { Fields.OwnerCompanyId[this] = value; }
        }

        [DisplayName("Tehsil"), Expression("jTehsil.[Tehsil]")]
        public String Tehsil
        {
            get { return Fields.Tehsil[this]; }
            set { Fields.Tehsil[this] = value; }
        }

        [DisplayName("Tehsil State Id"), Expression("jTehsil.[StateId]")]
        public Int32? TehsilStateId
        {
            get { return Fields.TehsilStateId[this]; }
            set { Fields.TehsilStateId[this] = value; }
        }

        [DisplayName("Tehsil City Id"), Expression("jTehsil.[CityId]")]
        public Int32? TehsilCityId
        {
            get { return Fields.TehsilCityId[this]; }
            set { Fields.TehsilCityId[this] = value; }
        }

        [DisplayName("Village"), Expression("jVillage.[Village]")]
        public String Village
        {
            get { return Fields.Village[this]; }
            set { Fields.Village[this] = value; }
        }

        [DisplayName("Village State Id"), Expression("jVillage.[StateId]")]
        public Int32? VillageStateId
        {
            get { return Fields.VillageStateId[this]; }
            set { Fields.VillageStateId[this] = value; }
        }

        [DisplayName("Village City Id"), Expression("jVillage.[CityId]")]
        public Int32? VillageCityId
        {
            get { return Fields.VillageCityId[this]; }
            set { Fields.VillageCityId[this] = value; }
        }

        [DisplayName("Village Tehsil Id"), Expression("jVillage.[TehsilId]")]
        public Int32? VillageTehsilId
        {
            get { return Fields.VillageTehsilId[this]; }
            set { Fields.VillageTehsilId[this] = value; }
        }

        [DisplayName("Village Pin"), Expression("jVillage.[PIN]")]
        public String VillagePin
        {
            get { return Fields.VillagePin[this]; }
            set { Fields.VillagePin[this] = value; }
        }

       
        public EmployeeRow()
            : base(Fields)
        {
        }
        
        public EmployeeRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField EmpCode;
            public Int32Field DepartmentId;
            public StringField Name;
            public StringField Phone;
            public StringField Email;
            public StringField Address;
            public StringField ProfessionalEmail;
            public Int32Field CityId;
            public Int32Field StateId;
            public StringField Pin;
            public Int32Field Country;
            public StringField AdditionalInfo;
            public Int32Field Gender;
            public Int32Field Religion;
            public Int32Field AreaId;
            public Int32Field MaritalStatus;
            public DateTimeField MarriageAnniversary;
            public DateTimeField Birthdate;
            public DateTimeField DateOfJoining;
            public Int32Field CompanyId;
            public Int32Field RolesId;
            public Int32Field OwnerId;
            public StringField AdhaarNo;
            public StringField PanNo;
            public StringField Attachment;
            public StringField BankName;
            public StringField AccountNumber;
            public StringField Ifsc;
            public StringField BankType;
            public StringField Branch;
            public Int32Field TehsilId;
            public Int32Field VillageId;

            public StringField Department;

            public StringField City;
            public Int32Field CityStateId;

            public StringField State;

            public StringField Area;

            public StringField CompanyName;
            public StringField CompanySlogan;
            public StringField CompanyAddress;
            public StringField CompanyPhone;
            public StringField CompanyLogo;
            public Int32Field CompanyLogoHeight;
            public Int32Field CompanyLogoWidth;
            public StringField CompanyHeaderImage;
            public Int32Field CompanyHeaderHeight;
            public Int32Field CompanyHeaderWidth;
            public StringField CompanyFooterImage;
            public Int32Field CompanyFooterHeight;
            public Int32Field CompanyFooterWidth;
            public StringField CompanyGstin;
            public StringField CompanyPanNo;
            public BooleanField CompanyEnquiryFollwupMandatory;
            public BooleanField CompanyQuotationFollwupMandatory;
            public BooleanField CompanyEnquiryProductsMandatory;
            public BooleanField CompanyQuotationProductsMandatory;
            public BooleanField CompanyRoundupInSales;
            public BooleanField CompanyPackagingInSales;
            public BooleanField CompanyFreightInSales;
            public BooleanField CompanyDueDateInSales;
            public BooleanField CompanyDispatchInSales;
            public BooleanField CompanyGstDetailsInSales;
            public BooleanField CompanyFollowupsInSales;
            public BooleanField CompanyTermsInSales;
            public BooleanField CompanyAdvanceInSales;
            public BooleanField CompanyStageInSales;
            public BooleanField CompanyCodeInSales;
            public BooleanField CompanySerialInSales;
            public BooleanField CompanyBatchInSales;
            public BooleanField CompanyDiscountInSales;
            public BooleanField CompanyTaxInSales;
            public BooleanField CompanyWarrantyInSales;
            public BooleanField CompanyDescriptionInSales;
            public BooleanField CompanyAppointmentInEnquiry;
            public BooleanField CompanyAppointmentInQuotation;
            public BooleanField CompanyAppointmentInProforma;
            public BooleanField CompanyAppointmentInSales;
            public BooleanField CompanyAppointmentInTeleCalling;
            public BooleanField CompanyRequirementInEnquiry;
            public BooleanField CompanyTaxInStockTransfer;
            public BooleanField CompanyPhoneCompulsory;
            public BooleanField CompanyEmailCompulsory;
            public BooleanField CompanyAutoSmsAppointments;
            public BooleanField CompanyAllowMovingNonClosedRecords;
            public BooleanField CompanyStateCompulsoryInContacts;
            public BooleanField CompanyEnableAddressInTransactions;
            public BooleanField CompanyAutoEmailEnquiry;
            public BooleanField CompanyAutoSmsEnquiry;
            public BooleanField CompanyAutoEmailQuotation;
            public BooleanField CompanyAutoSmsQuotation;
            public BooleanField CompanyAutoEmailProforma;
            public BooleanField CompanyAutoSmsProforma;
            public BooleanField CompanyAutoEmailInvoice;
            public BooleanField CompanyAutoSmsInvoice;
            public BooleanField CompanyHideDescriptionInSales;
            public BooleanField CompanyHideDescriptionInProforma;
            public BooleanField CompanyHideDescriptionInChallan;
            public Int32Field CompanyQuotationTemplate;
            public Int32Field CompanyCountry;
            public BooleanField CompanyMultiCurrency;
            public BooleanField CompanyProjectWithContacts;
            public Int32Field CompanyStateId;
            public BooleanField CompanyRoundupInPurchase;
            public Int32Field CompanyInvoiceTemplate;
            public BooleanField CompanyCompanyDetails;
            public BooleanField CompanyEnableAdditionalCharges;
            public BooleanField CompanyEnableAdditionalConcessions;
            public StringField CompanyAdditionalImages;
            public StringField CompanyHeaderContent;
            public StringField CompanyFooterContent;
            public StringField CompanyQuotationSuffix;
            public StringField CompanyQuotationPrefix;
            public StringField CompanyInvoiceSuffix;
            public StringField CompanyInvoicePrefix;
            public StringField CompanyChallanSuffix;
            public StringField CompanyChallanPrefix;
            public BooleanField CompanyQuotationTaxColumnIncluded;
            public BooleanField CompanyInvoiceTaxColumnIncluded;
            public BooleanField CompanyQuotationDiscountedPriceIncluded;
            public BooleanField CompanyInvoiceDiscountedPriceIncluded;
            public BooleanField CompanyRoundupInQuotation;
            public BooleanField CompanyQuotationContactIncluded;
            public Int32Field CompanyCompanyType;
            public StringField CompanyEnquirySuffix;
            public StringField CompanyEnquiryPrefix;
            public BooleanField CompanyCountryMandatory;
            public BooleanField CompanyPincodeMandatory;
            public BooleanField CompanyCityMandatory;
            public BooleanField CompanyCapacityInProducts;
            public Int32Field CompanyYearInPrefix;
            public StringField CompanyInvoiceHeaderContent;
            public StringField CompanyInvoiceFooterContent;
            public StringField CompanyInvoiceHeaderImage;
            public Int32Field CompanyInvoiceHeaderHeight;
            public Int32Field CompanyInvoiceHeaderWidth;
            public StringField CompanyInvoiceFooterImage;
            public Int32Field CompanyInvoiceFooterHeight;
            public Int32Field CompanyInvoiceFooterWidth;
            public StringField CompanyDcHeaderContent;
            public StringField CompanyDcFooterContent;
            public StringField CompanyDcHeaderImage;
            public Int32Field CompanyDcHeaderHeight;
            public Int32Field CompanyDcHeaderWidth;
            public StringField CompanyDcFooterImage;
            public Int32Field CompanyDcFooterHeight;
            public Int32Field CompanyDcFooterWidth;
            public BooleanField CompanyAddinfo2;
            public BooleanField CompanyMultiAddInfo;
            public BooleanField CompanyQuotationTotal;
            public BooleanField CompanyValidDate;
            public BooleanField CompanyDealerInCms;

            public StringField RolesRoleName;
            public Int32Field RolesCompanyId;

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
            public StringField OwnerUid;
            public BooleanField OwnerNonOperational;
            public Int32Field OwnerBranchId;
            public Int32Field OwnerCompanyId;

            public StringField Tehsil;
            public Int32Field TehsilStateId;
            public Int32Field TehsilCityId;

            public StringField Village;
            public Int32Field VillageStateId;
            public Int32Field VillageCityId;
            public Int32Field VillageTehsilId;
            public StringField VillagePin;
        }
    }
}
