
namespace AdvanceCRM.Administration
{
    using AdvanceCRM.Masters;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Administration"), TableName("[dbo].[CompanyDetails]")]
    [DisplayName("Company"), InstanceName("Company")]
    [ReadPermission(PermissionKeys.General)]
    [UpdatePermission(PermissionKeys.General)]
    [DeletePermission(PermissionKeys.Company)]
    [InsertPermission(PermissionKeys.Company)]
    [LookupScript("Administration.CompanyDetails", Permission = "*")]
    public sealed class CompanyDetailsRow : Row<CompanyDetailsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }
        [DisplayName("Mail To Organisation"), NotNull,LookupInclude, DefaultValue(true)]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? MailToOrganisation
        {
            get { return Fields.MailToOrganisation[this]; }
            set { Fields.MailToOrganisation[this] = value; }
        }
        [DisplayName("Name"), Size(250), NotNull, QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Slogan"), Size(250)]
        public String Slogan
        {
            get { return Fields.Slogan[this]; }
            set { Fields.Slogan[this] = value; }
        }

        [DisplayName("Address"), Size(500), NotNull]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("Phone"), Size(50), NotNull]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Logo"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String Logo
        {
            get { return Fields.Logo[this]; }
            set { Fields.Logo[this] = value; }
        }

        [DisplayName("Logo Height")]
        public Int32? LogoHeight
        {
            get { return Fields.LogoHeight[this]; }
            set { Fields.LogoHeight[this] = value; }
        }

        [DisplayName("Logo Width")]
        public Int32? LogoWidth
        {
            get { return Fields.LogoWidth[this]; }
            set { Fields.LogoWidth[this] = value; }
        }

        [DisplayName("Header Image"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String HeaderImage
        {
            get { return Fields.HeaderImage[this]; }
            set { Fields.HeaderImage[this] = value; }
        }

        [DisplayName("Header Height")]
        public Int32? HeaderHeight
        {
            get { return Fields.HeaderHeight[this]; }
            set { Fields.HeaderHeight[this] = value; }
        }
        [DisplayName("Remove Gt Column"), Column("RemoveGTColumn"), NotNull,DefaultValue(false)]
        public Boolean? RemoveGtColumn
        {
            get { return Fields.RemoveGtColumn[this]; }
            set { Fields.RemoveGtColumn[this] = value; }
        }
        [DisplayName("Header Width")]
        public Int32? HeaderWidth
        {
            get { return Fields.HeaderWidth[this]; }
            set { Fields.HeaderWidth[this] = value; }
        }

        [DisplayName("Footer Image"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String FooterImage
        {
            get { return Fields.FooterImage[this]; }
            set { Fields.FooterImage[this] = value; }
        }

        [DisplayName("Footer Height")]
        public Int32? FooterHeight
        {
            get { return Fields.FooterHeight[this]; }
            set { Fields.FooterHeight[this] = value; }
        }

        [DisplayName("Footer Width")]
        public Int32? FooterWidth
        {
            get { return Fields.FooterWidth[this]; }
            set { Fields.FooterWidth[this] = value; }
        }

        [DisplayName("GSTIN"), Column("GSTIN"), Size(100)]
        public String GSTIN
        {
            get { return Fields.GSTIN[this]; }
            set { Fields.GSTIN[this] = value; }
        }

        [DisplayName("Pan No"), Column("PANNo"), Size(100)]
        public String PANNo
        {
            get { return Fields.PANNo[this]; }
            set { Fields.PANNo[this] = value; }
        }

        [DisplayName("Enquiry Follwup Mandatory")]
        public Boolean? EnquiryFollwupMandatory
        {
            get { return Fields.EnquiryFollwupMandatory[this]; }
            set { Fields.EnquiryFollwupMandatory[this] = value; }
        }

        [DisplayName("Quotation Follwup Mandatory")]
        public Boolean? QuotationFollwupMandatory
        {
            get { return Fields.QuotationFollwupMandatory[this]; }
            set { Fields.QuotationFollwupMandatory[this] = value; }
        }

        [DisplayName("Enquiry Products Mandatory")]
        public Boolean? EnquiryProductsMandatory
        {
            get { return Fields.EnquiryProductsMandatory[this]; }
            set { Fields.EnquiryProductsMandatory[this] = value; }
        }

        [DisplayName("Quotation Products Mandatory")]
        public Boolean? QuotationProductsMandatory
        {
            get { return Fields.QuotationProductsMandatory[this]; }
            set { Fields.QuotationProductsMandatory[this] = value; }
        }

        [DisplayName("Remove Roundup In Sales"), LookupInclude]
        public Boolean? RoundupInSales
        {
            get { return Fields.RoundupInSales[this]; }
            set { Fields.RoundupInSales[this] = value; }
        }

        [DisplayName("Remove Packaging In Sales"), LookupInclude]
        public Boolean? PackagingInSales
        {
            get { return Fields.PackagingInSales[this]; }
            set { Fields.PackagingInSales[this] = value; }
        }

        [DisplayName("Remove Freight In Sales"), LookupInclude]
        public Boolean? FreightInSales
        {
            get { return Fields.FreightInSales[this]; }
            set { Fields.FreightInSales[this] = value; }
        }

        [DisplayName("Remove Due Date In Sales"), LookupInclude]
        public Boolean? DueDateInSales
        {
            get { return Fields.DueDateInSales[this]; }
            set { Fields.DueDateInSales[this] = value; }
        }

        [DisplayName("Remove Dispatch In Sales"), LookupInclude]
        public Boolean? DispatchInSales
        {
            get { return Fields.DispatchInSales[this]; }
            set { Fields.DispatchInSales[this] = value; }
        }

        [DisplayName("Remove GST In Sales"), Column("GSTDetailsInSales"), LookupInclude]
        public Boolean? GstDetailsInSales
        {
            get { return Fields.GstDetailsInSales[this]; }
            set { Fields.GstDetailsInSales[this] = value; }
        }

        [DisplayName("Remove Followups In Sales"), LookupInclude]
        public Boolean? FollowupsInSales
        {
            get { return Fields.FollowupsInSales[this]; }
            set { Fields.FollowupsInSales[this] = value; }
        }

        [DisplayName("Remove Terms In Sales"), LookupInclude]
        public Boolean? TermsInSales
        {
            get { return Fields.TermsInSales[this]; }
            set { Fields.TermsInSales[this] = value; }
        }

        [DisplayName("Remove Advance In Sales"), LookupInclude]
        public Boolean? AdvanceInSales
        {
            get { return Fields.AdvanceInSales[this]; }
            set { Fields.AdvanceInSales[this] = value; }
        }

        [DisplayName("Enquiry Suffix"), Size(20)]
        public String EnquirySuffix
        {
            get { return Fields.EnquirySuffix[this]; }
            set { Fields.EnquirySuffix[this] = value; }
        }

        [DisplayName("Enquiry Prefix"), Size(20)]
        public String EnquiryPrefix
        {
            get { return Fields.EnquiryPrefix[this]; }
            set { Fields.EnquiryPrefix[this] = value; }
        }

        [DisplayName("Country Mandatory")]
        public Boolean? CountryMandatory
        {
            get { return Fields.CountryMandatory[this]; }
            set { Fields.CountryMandatory[this] = value; }
        }
        [DisplayName("Capacity In Products")]
        public Boolean? CapacityInProducts
        {
            get { return Fields.CapacityInProducts[this]; }
            set { Fields.CapacityInProducts[this] = value; }
        }
        [DisplayName("Year In Prefix")]
        public Masters.YearInPrefix? YearInPrefix
        {           
            get { return (Masters.YearInPrefix?)Fields.YearInPrefix[this]; }
            set { Fields.YearInPrefix[this] = (Int32?)value; }
        }
        [DisplayName("Pincode Mandatory")]
        public Boolean? PincodeMandatory
        {
            get { return Fields.PincodeMandatory[this]; }
            set { Fields.PincodeMandatory[this] = value; }
        }

        [DisplayName("City Mandatory")]
        public Boolean? CityMandatory
        {
            get { return Fields.CityMandatory[this]; }
            set { Fields.CityMandatory[this] = value; }
        }


        [DisplayName("Remove Stage In Sales"), LookupInclude]
        public Boolean? StageInSales
        {
            get { return Fields.StageInSales[this]; }
            set { Fields.StageInSales[this] = value; }
        }

        [DisplayName("Remove Code In Sales"), LookupInclude]
        public Boolean? CodeInSales
        {
            get { return Fields.CodeInSales[this]; }
            set { Fields.CodeInSales[this] = value; }
        }
        [DisplayName("Service Person"),LookupInclude]
        public Int32? ServicePerson
        {
            get { return Fields.ServicePerson[this]; }
            set { Fields.ServicePerson[this] = value; }
        }


        [DisplayName("Remove Serial In Sales"), LookupInclude]
        public Boolean? SerialInSales
        {
            get { return Fields.SerialInSales[this]; }
            set { Fields.SerialInSales[this] = value; }
        }

        [DisplayName("Remove Batch In Sales"), LookupInclude]
        public Boolean? BatchInSales
        {
            get { return Fields.BatchInSales[this]; }
            set { Fields.BatchInSales[this] = value; }
        }

        [DisplayName("Remove Discount In Sales"), LookupInclude]
        public Boolean? DiscountInSales
        {
            get { return Fields.DiscountInSales[this]; }
            set { Fields.DiscountInSales[this] = value; }
        }

        [DisplayName("Remove Tax In Sales"), Column("TAXInSales"), LookupInclude]
        public Boolean? TAXInSales
        {
            get { return Fields.TAXInSales[this]; }
            set { Fields.TAXInSales[this] = value; }
        }

        [DisplayName("Remove Warranty In Sales"), LookupInclude]
        public Boolean? WarrantyInSales
        {
            get { return Fields.WarrantyInSales[this]; }
            set { Fields.WarrantyInSales[this] = value; }
        }

        [DisplayName("Remove Description In Sales"), LookupInclude]
        public Boolean? DescriptionInSales
        {
            get { return Fields.DescriptionInSales[this]; }
            set { Fields.DescriptionInSales[this] = value; }
        }

        [DisplayName("Appointment In Enquiry"), LookupInclude]
        public Boolean? AppointmentInEnquiry
        {
            get { return Fields.AppointmentInEnquiry[this]; }
            set { Fields.AppointmentInEnquiry[this] = value; }
        }

        [DisplayName("Appointment In Quotation"), LookupInclude]
        public Boolean? AppointmentInQuotation
        {
            get { return Fields.AppointmentInQuotation[this]; }
            set { Fields.AppointmentInQuotation[this] = value; }
        }

        [DisplayName("Appointment In Proforma"), LookupInclude]
        public Boolean? AppointmentInProforma
        {
            get { return Fields.AppointmentInProforma[this]; }
            set { Fields.AppointmentInProforma[this] = value; }
        }

        [DisplayName("Appointment In Sales")]
        public Boolean? AppointmentInSales
        {
            get { return Fields.AppointmentInSales[this]; }
            set { Fields.AppointmentInSales[this] = value; }
        }

        [DisplayName("Appointment In Tele Calling"), LookupInclude]
        public Boolean? AppointmentInTeleCalling
        {
            get { return Fields.AppointmentInTeleCalling[this]; }
            set { Fields.AppointmentInTeleCalling[this] = value; }
        }

        [DisplayName("Products In Enquiry"), LookupInclude]
        public Boolean? RequirementInEnquiry
        {
            get { return Fields.RequirementInEnquiry[this]; }
            set { Fields.RequirementInEnquiry[this] = value; }
        }

        [DisplayName("Tax In Stock Transfer"), Column("TAXInStockTransfer")]
        public Boolean? TaxInStockTransfer
        {
            get { return Fields.TaxInStockTransfer[this]; }
            set { Fields.TaxInStockTransfer[this] = value; }
        }

        [DisplayName("Phone Compulsory"), LookupInclude]
        public Boolean? PhoneCompulsory
        {
            get { return Fields.PhoneCompulsory[this]; }
            set { Fields.PhoneCompulsory[this] = value; }
        }

        [DisplayName("Email Compulsory"), LookupInclude]
        public Boolean? EmailCompulsory
        {
            get { return Fields.EmailCompulsory[this]; }
            set { Fields.EmailCompulsory[this] = value; }
        }

        [DisplayName("Auto SMS Appointments"), Column("AutoSMSAppointments")]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? AutoSMSAppointments
        {
            get { return Fields.AutoSMSAppointments[this]; }
            set { Fields.AutoSMSAppointments[this] = value; }
        }


        [DisplayName("Addinfo2"),LookupInclude]
        public Boolean? Addinfo2
        {
            get { return Fields.Addinfo2[this]; }
            set { Fields.Addinfo2[this] = value; }
        }

        [DisplayName("Multi Add Info"), LookupInclude]
        public Boolean? MultiAddInfo
        {
            get { return Fields.MultiAddInfo[this]; }
            set { Fields.MultiAddInfo[this] = value; }
        }

        [DisplayName("Allow Moving Non Closed Records")]
        public Boolean? AllowMovingNonClosedRecords
        {
            get { return Fields.AllowMovingNonClosedRecords[this]; }
            set { Fields.AllowMovingNonClosedRecords[this] = value; }
        }

        [DisplayName("State Compulsory In Contacts"), LookupInclude]
        public Boolean? StateCompulsoryInContacts
        {
            get { return Fields.StateCompulsoryInContacts[this]; }
            set { Fields.StateCompulsoryInContacts[this] = value; }
        }

        [DisplayName("Enable Address In Transactions"), LookupInclude]
        public Boolean? EnableAddressInTransactions
        {
            get { return Fields.EnableAddressInTransactions[this]; }
            set { Fields.EnableAddressInTransactions[this] = value; }
        }

        [DisplayName("Auto Email Enquiry")]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? AutoEmailEnquiry
        {
            get { return Fields.AutoEmailEnquiry[this]; }
            set { Fields.AutoEmailEnquiry[this] = value; }
        }

        [DisplayName("Auto SMS Enquiry"), Column("AutoSMSEnquiry")]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? AutoSMSEnquiry
        {
            get { return Fields.AutoSMSEnquiry[this]; }
            set { Fields.AutoSMSEnquiry[this] = value; }
        }

        [DisplayName("Auto Email Quotation")]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? AutoEmailQuotation
        {
            get { return Fields.AutoEmailQuotation[this]; }
            set { Fields.AutoEmailQuotation[this] = value; }
        }

        [DisplayName("Auto SMS Quotation"), Column("AutoSMSQuotation")]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? AutoSMSQuotation
        {
            get { return Fields.AutoSMSQuotation[this]; }
            set { Fields.AutoSMSQuotation[this] = value; }
        }

        [DisplayName("Auto Email Proforma")]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? AutoEmailProforma
        {
            get { return Fields.AutoEmailProforma[this]; }
            set { Fields.AutoEmailProforma[this] = value; }
        }

        [DisplayName("Auto SMS Proforma"), Column("AutoSMSProforma")]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? AutoSMSProforma
        {
            get { return Fields.AutoSMSProforma[this]; }
            set { Fields.AutoSMSProforma[this] = value; }
        }

        [DisplayName("Auto Email Invoice")]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? AutoEmailInvoice
        {
            get { return Fields.AutoEmailInvoice[this]; }
            set { Fields.AutoEmailInvoice[this] = value; }
        }

        [DisplayName("Auto SMS Invoice"), Column("AutoSMSInvoice")]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? AutoSMSInvoice
        {
            get { return Fields.AutoSMSInvoice[this]; }
            set { Fields.AutoSMSInvoice[this] = value; }
        }

        [DisplayName("Hide Description In Sales")]
        public Boolean? HideDescriptionInSales
        {
            get { return Fields.HideDescriptionInSales[this]; }
            set { Fields.HideDescriptionInSales[this] = value; }
        }

        [DisplayName("Hide Description In Proforma")]
        public Boolean? HideDescriptionInProforma
        {
            get { return Fields.HideDescriptionInProforma[this]; }
            set { Fields.HideDescriptionInProforma[this] = value; }
        }

        [DisplayName("Hide Description In Challan")]
        public Boolean? HideDescriptionInChallan
        {
            get { return Fields.HideDescriptionInChallan[this]; }
            set { Fields.HideDescriptionInChallan[this] = value; }
        }

        [DisplayName("Quotation Template")]
        public Masters.PrintTemplates? QuotationTemplate
        {
            get { return (Masters.PrintTemplates?)Fields.QuotationTemplate[this]; }
            set { Fields.QuotationTemplate[this] = (Int32?)value; }
        }

        [DisplayName("Country"), LookupInclude]
        public Masters.CountryMaster? Country
        {
            get { return (Masters.CountryMaster ? )Fields.Country[this]; }
            set { Fields.Country[this] = (Int32?)value; }
        }

        [DisplayName("Enable Multi Currency"), LookupInclude]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? MultiCurrency
        {
            get { return Fields.MultiCurrency[this]; }
            set { Fields.MultiCurrency[this] = value; }
        }

        [DisplayName("Enable Project In Contacts"), LookupInclude]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? ProjectWithContacts
        {
            get { return Fields.ProjectWithContacts[this]; }
            set { Fields.ProjectWithContacts[this] = value; }
        }

        [DisplayName("State"), ForeignKey("[dbo].[State]", "Id"), LeftJoin("jState"), TextualField("State")]
        [LookupEditor(typeof(StateRow), InplaceAdd = true)]
        public Int32? StateId
        {
            get { return Fields.StateId[this]; }
            set { Fields.StateId[this] = value; }
        }

        [DisplayName("Remove Roundup In Purchase"), LookupInclude]
        public Boolean? RoundupInPurchase
        {
            get { return Fields.RoundupInPurchase[this]; }
            set { Fields.RoundupInPurchase[this] = value; }
        }

        [DisplayName("Invoice Template")]
        public Masters.PrintTemplates? InvoiceTemplate
        {
            get { return (Masters.PrintTemplates?)Fields.InvoiceTemplate[this]; }
            set { Fields.InvoiceTemplate[this] = (Int32?)value; }
        }

        [DisplayName("Auto Company Details")]
        [BooleanSwitchEditor]
        public Boolean? CompanyDetails
        {
            get { return Fields.CompanyDetails[this]; }
            set { Fields.CompanyDetails[this] = value; }
        }

        [DisplayName("Enable Additional Charges"), LookupInclude]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? EnableAdditionalCharges
        {
            get { return Fields.EnableAdditionalCharges[this]; }
            set { Fields.EnableAdditionalCharges[this] = value; }
        }

        [DisplayName("Enable Additional Concessions"), LookupInclude]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? EnableAdditionalConcessions
        {
            get { return Fields.EnableAdditionalConcessions[this]; }
            set { Fields.EnableAdditionalConcessions[this] = value; }
        }

        [DisplayName("Additional Images")]
       
        public String AdditionalImages
        {
            get { return Fields.AdditionalImages[this]; }
            set { Fields.AdditionalImages[this] = value; }
        }

        [DisplayName("Header Content")]
        public String HeaderContent
        {
            get { return Fields.HeaderContent[this]; }
            set { Fields.HeaderContent[this] = value; }
        }

        [DisplayName("Footer Content")]
        public String FooterContent
        {
            get { return Fields.FooterContent[this]; }
            set { Fields.FooterContent[this] = value; }
        }

        [DisplayName("Quotation Suffix"), Size(10)]
        public String QuotationSuffix
        {
            get { return Fields.QuotationSuffix[this]; }
            set { Fields.QuotationSuffix[this] = value; }
        }

        [DisplayName("Quotation Prefix"), Size(15)]
        public String QuotationPrefix
        {
            get { return Fields.QuotationPrefix[this]; }
            set { Fields.QuotationPrefix[this] = value; }
        }

        [DisplayName("Invoice Suffix"), Size(10)]
        public String InvoiceSuffix
        {
            get { return Fields.InvoiceSuffix[this]; }
            set { Fields.InvoiceSuffix[this] = value; }
        }

        [DisplayName("Invoice Prefix"), Size(10)]
        public String InvoicePrefix
        {
            get { return Fields.InvoicePrefix[this]; }
            set { Fields.InvoicePrefix[this] = value; }
        }

        [DisplayName("Challan Suffix"), Size(10)]
        public String ChallanSuffix
        {
            get { return Fields.ChallanSuffix[this]; }
            set { Fields.ChallanSuffix[this] = value; }
        }

        [DisplayName("Challan Prefix"), Size(10)]
        public String ChallanPrefix
        {
            get { return Fields.ChallanPrefix[this]; }
            set { Fields.ChallanPrefix[this] = value; }
        }

        [DisplayName("Include Tax Column"), NotNull, DefaultValue(true)]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? QuotationTaxColumnIncluded
        {
            get { return Fields.QuotationTaxColumnIncluded[this]; }
            set { Fields.QuotationTaxColumnIncluded[this] = value; }
        }

        [DisplayName("Include Tax Column"), NotNull, DefaultValue(true)]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? InvoiceTaxColumnIncluded
        {
            get { return Fields.InvoiceTaxColumnIncluded[this]; }
            set { Fields.InvoiceTaxColumnIncluded[this] = value; }
        }

        [DisplayName("Include Tax Column"), NotNull, DefaultValue(true)]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? ChallanTaxColumnIncluded
        {
            get { return Fields.ChallanTaxColumnIncluded[this]; }
            set { Fields.ChallanTaxColumnIncluded[this] = value; }
        }

        [DisplayName("Include Discounted Price"), NotNull]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? QuotationDiscountedPriceIncluded
        {
            get { return Fields.QuotationDiscountedPriceIncluded[this]; }
            set { Fields.QuotationDiscountedPriceIncluded[this] = value; }
        }

        [DisplayName("Include Discounted Price"), NotNull]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? InvoiceDiscountedPriceIncluded
        {
            get { return Fields.InvoiceDiscountedPriceIncluded[this]; }
            set { Fields.InvoiceDiscountedPriceIncluded[this] = value; }
        }

        [DisplayName("Include Contact Info"), DefaultValue(true)]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? QuotationContactIncluded
        {
            get { return Fields.QuotationContactIncluded[this]; }
            set { Fields.QuotationContactIncluded[this] = value; }
        }

        [DisplayName("Remove Roundup In Quotation"), LookupInclude]
        public Boolean? RoundupInQuotation
        {
            get { return Fields.RoundupInQuotation[this]; }
            set { Fields.RoundupInQuotation[this] = value; }
        }
        [DisplayName("Invoice Header Content")]
        public String InvoiceHeaderContent
        {
            get { return Fields.InvoiceHeaderContent[this]; }
            set { Fields.InvoiceHeaderContent[this] = value; }
        }

        [DisplayName("Invoice Footer Content")]
        public String InvoiceFooterContent
        {
            get { return Fields.InvoiceFooterContent[this]; }
            set { Fields.InvoiceFooterContent[this] = value; }
        }

        [DisplayName("Invoice Header Image"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String InvoiceHeaderImage
        {
            get { return Fields.InvoiceHeaderImage[this]; }
            set { Fields.InvoiceHeaderImage[this] = value; }
        }

        [DisplayName("Invoice Header Height")]
        public Int32? InvoiceHeaderHeight
        {
            get { return Fields.InvoiceHeaderHeight[this]; }
            set { Fields.InvoiceHeaderHeight[this] = value; }
        }

        [DisplayName("Invoice Header Width")]
        public Int32? InvoiceHeaderWidth
        {
            get { return Fields.InvoiceHeaderWidth[this]; }
            set { Fields.InvoiceHeaderWidth[this] = value; }
        }

        [DisplayName("Invoice Footer Image"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String InvoiceFooterImage
        {
            get { return Fields.InvoiceFooterImage[this]; }
            set { Fields.InvoiceFooterImage[this] = value; }
        }

        [DisplayName("Invoice Footer Height")]
        public Int32? InvoiceFooterHeight
        {
            get { return Fields.InvoiceFooterHeight[this]; }
            set { Fields.InvoiceFooterHeight[this] = value; }
        }

        [DisplayName("Invoice Footer Width")]
        public Int32? InvoiceFooterWidth
        {
            get { return Fields.InvoiceFooterWidth[this]; }
            set { Fields.InvoiceFooterWidth[this] = value; }
        }

        [DisplayName("Dc Header Content"), Column("DCHeaderContent")]
        public String DcHeaderContent
        {
            get { return Fields.DcHeaderContent[this]; }
            set { Fields.DcHeaderContent[this] = value; }
        }

        [DisplayName("Dc Footer Content"), Column("DCFooterContent")]
        public String DcFooterContent
        {
            get { return Fields.DcFooterContent[this]; }
            set { Fields.DcFooterContent[this] = value; }
        }

        [DisplayName("Dc Header Image"), Column("DCHeaderImage"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String DcHeaderImage
        {
            get { return Fields.DcHeaderImage[this]; }
            set { Fields.DcHeaderImage[this] = value; }
        }

        [DisplayName("Dc Header Height"), Column("DCHeaderHeight")]
        public Int32? DcHeaderHeight
        {
            get { return Fields.DcHeaderHeight[this]; }
            set { Fields.DcHeaderHeight[this] = value; }
        }

        [DisplayName("Dc Header Width"), Column("DCHeaderWidth")]
        public Int32? DcHeaderWidth
        {
            get { return Fields.DcHeaderWidth[this]; }
            set { Fields.DcHeaderWidth[this] = value; }
        }

        [DisplayName("Dc Footer Image"), Column("DCFooterImage"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String DcFooterImage
        {
            get { return Fields.DcFooterImage[this]; }
            set { Fields.DcFooterImage[this] = value; }
        }

        [DisplayName("Dc Footer Height"), Column("DCFooterHeight")]
        public Int32? DcFooterHeight
        {
            get { return Fields.DcFooterHeight[this]; }
            set { Fields.DcFooterHeight[this] = value; }
        }

        [DisplayName("Quotation Total"),LookupInclude]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? QuotationTotal
        {
            get { return Fields.QuotationTotal[this]; }
            set { Fields.QuotationTotal[this] = value; }
        }

        [DisplayName("Mail To Sub Contacts"), NotNull,LookupInclude, DefaultValue(false)]
        [BSSwitchEditor(OnText = "Yes", OffText = "No", Animate = true)]
        public Boolean? MailToSubContacts
        {
            get { return Fields.MailToSubContacts[this]; }
            set { Fields.MailToSubContacts[this] = value; }
        }


        [DisplayName("Dc Footer Width"), Column("DCFooterWidth")]
        public Int32? DcFooterWidth
        {
            get { return Fields.DcFooterWidth[this]; }
            set { Fields.DcFooterWidth[this] = value; }
        }
        [DisplayName("Valid Date")]
        public Int32? ValidDate
        {
            get { return Fields.ValidDate[this]; }
            set { Fields.ValidDate[this] = value; }
        }

        [DisplayName("Cms Start No"), Column("CMSStartNo")]
        public Int32? CmsStartNo
        {
            get { return Fields.CmsStartNo[this]; }
            set { Fields.CmsStartNo[this] = value; }
        }

        //[DisplayName("Cms Edit No"), Column("CMSEditNo"), NotNull, DefaultValue(false)]
        [DisplayName("Cms Edit No"), NotNull, LookupInclude, DefaultValue(false)]
        public Boolean? CmsEditNo
        {
            get { return Fields.CmsEditNo[this]; }
            set { Fields.CmsEditNo[this] = value; }
        }

        [DisplayName("Cms Suffix"), Column("CMSSuffix"), Size(20)]
        public String CmsSuffix
        {
            get { return Fields.CmsSuffix[this]; }
            set { Fields.CmsSuffix[this] = value; }
        }

        [DisplayName("Cm Sprefix"), Column("CMSprefix"), Size(20)]
        public String CmSprefix
        {
            get { return Fields.CmSprefix[this]; }
            set { Fields.CmSprefix[this] = value; }
        }
        [DisplayName("Dealer In Cms"), Column("DealerInCMS"), LookupInclude,DefaultValue(false)]
        public Int32? DealerInCms
        {
            get { return Fields.DealerInCms[this]; }
            set { Fields.DealerInCms[this] = value; }
        }
        [DisplayName("Enq Start No"), LookupInclude]
        public Int32? EnqStartNo
        {
            get { return Fields.EnqStartNo[this]; }
            set { Fields.EnqStartNo[this] = value; }
        }

        [DisplayName("Enq Edit No"), NotNull, LookupInclude, DefaultValue(false)]
        public Boolean? EnqEditNo
        {
            get { return Fields.EnqEditNo[this]; }
            set { Fields.EnqEditNo[this] = value; }
        }

        [DisplayName("Quo Start No"), LookupInclude]
        public Int32? QuoStartNo
        {
            get { return Fields.QuoStartNo[this]; }
            set { Fields.QuoStartNo[this] = value; }
        }

        [DisplayName("Quo Edit No"), NotNull, LookupInclude, DefaultValue(false)]
        public Boolean? QuoEditNo
        {
            get { return Fields.QuoEditNo[this]; }
            set { Fields.QuoEditNo[this] = value; }
        }

        [DisplayName("Products In Cms"), Column("ProductsInCMS"), NotNull, DefaultValue(false)]
        public Boolean? ProductsInCms
        {
            get { return Fields.ProductsInCms[this]; }
            set { Fields.ProductsInCms[this] = value; }
        }

        [DisplayName("Inv Start No"), LookupInclude]
        public Int32? InvStartNo
        {
            get { return Fields.InvStartNo[this]; }
            set { Fields.InvStartNo[this] = value; }
        }

        [DisplayName("Inv Edit No"), NotNull, LookupInclude]
        public Boolean? InvEditNo
        {
            get { return Fields.InvEditNo[this]; }
            set { Fields.InvEditNo[this] = value; }
        }

        [DisplayName("Dc Start No"), Column("DCStartNo"), LookupInclude]
        public Int32? DcStartNo
        {
            get { return Fields.DcStartNo[this]; }
            set { Fields.DcStartNo[this] = value; }
        }

        [DisplayName("Dc Edit No"), Column("DCEditNo"), NotNull, LookupInclude, DefaultValue(false)]
        public Boolean? DcEditNo
        {
            get { return Fields.DcEditNo[this]; }
            set { Fields.DcEditNo[this] = value; }
        }

        [DisplayName("Dealer In Enquiry"), NotNull, LookupInclude,DefaultValue(false)]
        public Boolean? DealerInEnquiry
        {
            get { return Fields.DealerInEnquiry[this]; }
            set { Fields.DealerInEnquiry[this] = value; }
        }

        [DisplayName("Dealer In Quotation"), NotNull, LookupInclude, DefaultValue(false)]
        public Boolean? DealerInQuotation
        {
            get { return Fields.DealerInQuotation[this]; }
            set { Fields.DealerInQuotation[this] = value; }
        }

        [DisplayName("Dealer In Sales"), NotNull, LookupInclude, DefaultValue(false)]
        public Boolean? DealerInSales
        {
            get { return Fields.DealerInSales[this]; }
            set { Fields.DealerInSales[this] = value; }
        }

        [DisplayName("Dealer In Invoice"), NotNull, LookupInclude, DefaultValue(false)]
        public Boolean? DealerInInvoice
        {
            get { return Fields.DealerInInvoice[this]; }
            set { Fields.DealerInInvoice[this] = value; }
        }

        [DisplayName("Project In Cms"), Column("ProjectInCMS"), NotNull, LookupInclude, DefaultValue(false)]
        public Boolean? ProjectInCms
        {
            get { return Fields.ProjectInCms[this]; }
            set { Fields.ProjectInCms[this] = value; }
        }

        [DisplayName("State"), Expression("jState.[State]")]
        public String State
        {
            get { return Fields.State[this]; }
            set { Fields.State[this] = value; }
        }
        [DisplayName("Remove Purchase Date"), NotNull,LookupInclude, DefaultValue(false)]
        public Boolean? RemovePurchaseDate
        {
            get { return Fields.RemovePurchaseDate[this]; }
            set { Fields.RemovePurchaseDate[this] = value; }
        }

        [DisplayName("Remove Invoice No"), NotNull,LookupInclude, DefaultValue(false)]
        public Boolean? RemoveInvoiceNo
        {
            get { return Fields.RemoveInvoiceNo[this]; }
            set { Fields.RemoveInvoiceNo[this] = value; }
        }

        [DisplayName("Passport Details"), NotNull, LookupInclude, DefaultValue(false)]
        public Boolean? PassportDetails
        {
            get { return Fields.PassportDetails[this]; }
            set { Fields.PassportDetails[this] = value; }
        }

        [DisplayName("Task Title In Task"), LookupInclude, DefaultValue(true)]
        public Boolean? TaskTitleInTask
        {
            get { return Fields.TaskTitleInTask[this]; }
            set { Fields.TaskTitleInTask[this] = value; }
        }

        [DisplayName("Task Master In Task"), LookupInclude, DefaultValue(false)]
        public Boolean? TaskMasterInTask
        {
            get { return Fields.TaskMasterInTask[this]; }
            set { Fields.TaskMasterInTask[this] = value; }
        }

        [DisplayName("Win Percentage In Enquiry"), LookupInclude]
        public Boolean? WinPercentageInEnquiry
        {
            get { return Fields.WinPercentageInEnquiry[this]; }
            set { Fields.WinPercentageInEnquiry[this] = value; }
        }

        [DisplayName("Expected Closing Date In Enquiry"), LookupInclude]
        public Boolean? ExpectedClosingDateInEnquiry
        {
            get { return Fields.ExpectedClosingDateInEnquiry[this]; }
            set { Fields.ExpectedClosingDateInEnquiry[this] = value; }
        }

        [DisplayName("Win Percentage In Quotation"), LookupInclude]
        public Boolean? WinPercentageInQuotation
        {
            get { return Fields.WinPercentageInQuotation[this]; }
            set { Fields.WinPercentageInQuotation[this] = value; }
        }

        [DisplayName("Expected Closing Date In Quotation"), LookupInclude]
        public Boolean? ExpectedClosingDateInQuotation
        {
            get { return Fields.ExpectedClosingDateInQuotation[this]; }
            set { Fields.ExpectedClosingDateInQuotation[this] = value; }
        }
        [DisplayName("HSN"), LookupInclude]
        public Boolean? HSN
        {
            get { return Fields.HSN[this]; }
            set { Fields.HSN[this] = value; }
        }
        [DisplayName("Code"), LookupInclude]
        public Boolean? Code
        {
            get { return Fields.Code[this]; }
            set { Fields.Code[this] = value; }
        }
        [DisplayName("Unit"),  LookupInclude]
        public Boolean? Unit
        {
            get { return Fields.Unit[this]; }
            set { Fields.Unit[this] = value; }
        }
        [DisplayName("Opening Stock"), LookupInclude]
        public Boolean? OpeningStock
        {
            get { return Fields.OpeningStock[this]; }
            set { Fields.OpeningStock[this] = value; }
        }
        [DisplayName("Raw Material"), LookupInclude, DefaultValue(false)]
        public Boolean? RawMaterial
        {
            get { return Fields.RawMaterial[this]; }
            set { Fields.RawMaterial[this] = value; }
        }
        [DisplayName("Group"), LookupInclude, DefaultValue(false)]
        public Boolean? Group
        {
            get { return Fields.Group[this]; }
            set { Fields.Group[this] = value; }
        }
        [DisplayName("ToInvoice"), LookupInclude, DefaultValue(false)]
        public Boolean? ToInvoice
        {
            get { return Fields.ToInvoice[this]; }
            set { Fields.ToInvoice[this] = value; }
        }
        [DisplayName("ToPerforma"), LookupInclude, DefaultValue(false)]
        public Boolean? ToPerforma
        {
            get { return Fields.ToPerforma[this]; }
            set { Fields.ToPerforma[this] = value; }
        }
        [DisplayName("Capacity"), LookupInclude, DefaultValue(false)]
        public Boolean? Capacity
        {
            get { return Fields.Capacity[this]; }
            set { Fields.Capacity[this] = value; }
        }
        [DisplayName("MRP"), LookupInclude, DefaultValue(false)]
        public Boolean? MRP
        {
            get { return Fields.MRP[this]; }
            set { Fields.MRP[this] = value; }
        }
        [DisplayName("SellingPrice"), LookupInclude, DefaultValue(false)]
        public Boolean? SellingPrice
        {
            get { return Fields.SellingPrice[this]; }
            set { Fields.SellingPrice[this] = value; }
        }
        [DisplayName("Travels"), LookupInclude, DefaultValue(false)]
        public Boolean? Travels
        {
            get { return Fields.Travels[this]; }
            set { Fields.Travels[this] = value; }
        }

        [DisplayName("Itinerary Header Content")]
        public String ItineraryHeaderContent
        {
            get { return Fields.ItineraryHeaderContent[this]; }
            set { Fields.ItineraryHeaderContent[this] = value; }
        }

        [DisplayName("Itinerary Footer Content")]
        public String ItineraryFooterContent
        {
            get { return Fields.ItineraryFooterContent[this]; }
            set { Fields.ItineraryFooterContent[this] = value; }
        }

        [DisplayName("Itinerary Header Image"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String ItineraryHeaderImage
        {
            get { return Fields.ItineraryHeaderImage[this]; }
            set { Fields.ItineraryHeaderImage[this] = value; }
        }

        [DisplayName("Itinerary Header Height")]
        public Int32? ItineraryHeaderHeight
        {
            get { return Fields.ItineraryHeaderHeight[this]; }
            set { Fields.ItineraryHeaderHeight[this] = value; }
        }

        [DisplayName("Itinerary Header Width")]
        public Int32? ItineraryHeaderWidth
        {
            get { return Fields.ItineraryHeaderWidth[this]; }
            set { Fields.ItineraryHeaderWidth[this] = value; }
        }

        [DisplayName("Itinerary Footer Image"), Size(500)]
        [ImageUploadEditor(FilenameFormat = "Images/~", CopyToHistory = true)]
        public String ItineraryFooterImage
        {
            get { return Fields.ItineraryFooterImage[this]; }
            set { Fields.ItineraryFooterImage[this] = value; }
        }

        [DisplayName("Itinerary Footer Height")]
        public Int32? ItineraryFooterHeight
        {
            get { return Fields.ItineraryFooterHeight[this]; }
            set { Fields.ItineraryFooterHeight[this] = value; }
        }

        [DisplayName("Itinerary Footer Width")]
        public Int32? ItineraryFooterWidth
        {
            get { return Fields.ItineraryFooterWidth[this]; }
            set { Fields.ItineraryFooterWidth[this] = value; }
        }

        //[DisplayName("Itinerary Template")]
        //public Masters.PrintTemplates? ItineraryTemplate
        //{
        //    get { return (Masters.PrintTemplates?)Fields.ItineraryTemplate[this]; }
        //    set { Fields.ItineraryTemplate[this] = (Int32?)value; }
        //}



       

      //  public static readonly RowFields Fields = new RowFields().Init();

        public CompanyDetailsRow()
            : base(Fields)
        {
        }
         public CompanyDetailsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Name;
            public StringField Slogan;
            public StringField Address;
            public StringField Phone;
            public StringField Logo;
            public Int32Field LogoHeight;
            public Int32Field LogoWidth;
            public StringField HeaderImage;
            public Int32Field HeaderHeight;
            public Int32Field HeaderWidth;
            public StringField FooterImage;
            public Int32Field FooterHeight;
            public Int32Field FooterWidth;
            public StringField GSTIN;
            public StringField PANNo;
            public BooleanField Addinfo2;
            public BooleanField MultiAddInfo;
            public BooleanField EnquiryFollwupMandatory;
            public BooleanField QuotationFollwupMandatory;
            public BooleanField EnquiryProductsMandatory;
            public BooleanField QuotationProductsMandatory;
            public BooleanField RoundupInSales;
            public BooleanField PackagingInSales;
            public BooleanField FreightInSales;
            public BooleanField DueDateInSales;
            public BooleanField DispatchInSales;
            public BooleanField GstDetailsInSales;
            public BooleanField FollowupsInSales;
            public BooleanField TermsInSales;
            public BooleanField AdvanceInSales;
            public BooleanField StageInSales;
            public BooleanField CodeInSales;
            public BooleanField SerialInSales;
            public BooleanField BatchInSales;
            public BooleanField DiscountInSales;
            public BooleanField TAXInSales;
            public BooleanField WarrantyInSales;
            public BooleanField DescriptionInSales;
            public BooleanField AppointmentInEnquiry;
            public BooleanField AppointmentInQuotation;
            public BooleanField AppointmentInProforma;
            public BooleanField AppointmentInSales;
            public BooleanField AppointmentInTeleCalling;
            public BooleanField RequirementInEnquiry;
            public BooleanField TaxInStockTransfer;
            public BooleanField PhoneCompulsory;
            public BooleanField EmailCompulsory;
            public BooleanField AutoSMSAppointments;
            public BooleanField AllowMovingNonClosedRecords;
            public BooleanField StateCompulsoryInContacts;
            public BooleanField EnableAddressInTransactions;
            public BooleanField AutoEmailEnquiry;
            public BooleanField AutoSMSEnquiry;
            public BooleanField AutoEmailQuotation;
            public BooleanField AutoSMSQuotation;
            public BooleanField AutoEmailProforma;
            public BooleanField AutoSMSProforma;
            public BooleanField AutoEmailInvoice;
            public BooleanField AutoSMSInvoice;
            public BooleanField HideDescriptionInSales;
            public BooleanField HideDescriptionInProforma;
            public BooleanField HideDescriptionInChallan;
            public Int32Field QuotationTemplate;
            public Int32Field Country;
            public BooleanField QuotationTotal;
            public BooleanField MultiCurrency;
            public BooleanField ProjectWithContacts;
            public Int32Field StateId;
            public BooleanField RoundupInPurchase;
            public Int32Field InvoiceTemplate;
            public BooleanField CompanyDetails;
            public BooleanField EnableAdditionalCharges;
            public BooleanField EnableAdditionalConcessions;
            public StringField AdditionalImages;
            public StringField HeaderContent;
            public StringField FooterContent;
            public StringField QuotationSuffix;
            public StringField QuotationPrefix;
            public StringField InvoiceSuffix;
            public StringField InvoicePrefix;
            public StringField ChallanSuffix;
            public StringField ChallanPrefix;
            public BooleanField QuotationTaxColumnIncluded;
            public BooleanField InvoiceTaxColumnIncluded;
            public BooleanField QuotationDiscountedPriceIncluded;
            public BooleanField InvoiceDiscountedPriceIncluded;
            public BooleanField RoundupInQuotation;
            public BooleanField QuotationContactIncluded;
            public StringField EnquirySuffix;
            public StringField EnquiryPrefix;
            public BooleanField CountryMandatory;
            public BooleanField PincodeMandatory;
            public BooleanField CityMandatory;
            public BooleanField CapacityInProducts;
            public Int32Field YearInPrefix;
            public StringField InvoiceHeaderContent;
            public StringField InvoiceFooterContent;
            public StringField InvoiceHeaderImage;
            public Int32Field InvoiceHeaderHeight;
            public Int32Field InvoiceHeaderWidth;
            public StringField InvoiceFooterImage;
            public Int32Field InvoiceFooterHeight;
            public Int32Field InvoiceFooterWidth;
            public StringField DcHeaderContent;
            public StringField DcFooterContent;
            public StringField DcHeaderImage;
            public Int32Field DcHeaderHeight;
            public Int32Field DcHeaderWidth;
            public StringField DcFooterImage;
            public Int32Field DcFooterHeight;
            public Int32Field DcFooterWidth;
            public Int32Field ValidDate;
            public Int32Field DealerInCms;
            public Int32Field? ServicePerson;
            public Int32Field EnqStartNo;
            public BooleanField EnqEditNo;
            public Int32Field QuoStartNo;
            public BooleanField RemoveGtColumn;
            public BooleanField QuoEditNo;
            public Int32Field InvStartNo;
            public BooleanField InvEditNo;
            public Int32Field DcStartNo;
            public BooleanField DcEditNo;
            public BooleanField DealerInEnquiry;
            public BooleanField DealerInQuotation;
            public BooleanField DealerInSales;
            public BooleanField DealerInInvoice;
            public BooleanField ProjectInCms;
            public Int32Field CmsStartNo;
            public BooleanField CmsEditNo;
            public StringField CmsSuffix;
            public StringField CmSprefix;
            public BooleanField ProductsInCms;
            public BooleanField RemovePurchaseDate;
            public BooleanField RemoveInvoiceNo;
            public BooleanField MailToSubContacts;
            public BooleanField MailToOrganisation;

            public StringField State;

            public BooleanField ChallanTaxColumnIncluded;

            public BooleanField PassportDetails;
            public BooleanField TaskTitleInTask;
            public BooleanField TaskMasterInTask;

            public BooleanField WinPercentageInEnquiry;
            public BooleanField ExpectedClosingDateInEnquiry;
            public BooleanField WinPercentageInQuotation;
            public BooleanField ExpectedClosingDateInQuotation;

            public BooleanField HSN;
            public BooleanField Code;
            public BooleanField Unit;
            public BooleanField OpeningStock;
            public BooleanField RawMaterial;
            public BooleanField Group;
            public BooleanField ToInvoice;
            public BooleanField ToPerforma;
            public BooleanField Capacity;
            public BooleanField MRP;
            public BooleanField SellingPrice;
            public BooleanField Travels;

            public StringField ItineraryHeaderContent;
            public StringField ItineraryFooterContent;
            public StringField ItineraryHeaderImage;
            public Int32Field ItineraryHeaderHeight;
            public Int32Field ItineraryHeaderWidth;
            public StringField ItineraryFooterImage;
            public Int32Field ItineraryFooterHeight;
            public Int32Field ItineraryFooterWidth;
            //public Int32Field ItineraryTemplate;
        }
    }
}
