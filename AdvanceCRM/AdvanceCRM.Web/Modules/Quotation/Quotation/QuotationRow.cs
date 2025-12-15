
namespace AdvanceCRM.Quotation
{
    using AdvanceCRM.Administration;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Quotation;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Quotation"), TableName("[dbo].[Quotation]")]
    [DisplayName("Quotation"), InstanceName("Quotation")]
    [ReadPermission("Quotation:Read")]
    [InsertPermission("Quotation:Insert")]
    [UpdatePermission("Quotation:Update")]
    [DeletePermission("Quotation:Delete")]
    [LookupScript("Quotation.Quotation", Permission = "?")]

    public sealed class QuotationRow : Row<QuotationRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog,IMultiCompanyRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Contact"), NotNull, ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jContacts"), TextualField("ContactsName")]
        [LookupEditor(typeof(ContactsRow), InplaceAdd = true)]
        public Int32? ContactsId
        {
            get { return Fields.ContactsId[this]; }
            set { Fields.ContactsId[this] = value; }
        }

        [DisplayName("Date"), NotNull]
        public DateTime? Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
        }

        [DisplayName("Status"), NotNull]
        public Masters.StatusMaster? Status
        {
            get { return (Masters.StatusMaster?)Fields.Status[this]; }
            set { Fields.Status[this] = (Int32?)value; }
        }

        [DisplayName("Type")]
        public Masters.EnquiryTypeMaster? Type
        {
            get { return (Masters.EnquiryTypeMaster?)Fields.Type[this]; }
            set { Fields.Type[this] = (Int32?)value; }
        }

        [DisplayName("Additional Info"), Size(5000), TextAreaEditor(Rows = 4)]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }

        [DisplayName("Additional Info2"), Size(2000), TextAreaEditor(Rows = 3)]
        public String AdditionalInfo2
        {
            get { return Fields.AdditionalInfo2[this]; }
            set { Fields.AdditionalInfo2[this] = value; }
        }

        [DisplayName("Source"), NotNull, ForeignKey("[dbo].[Source]", "Id"), LeftJoin("jSource"), TextualField("Source")]
        [LookupEditor(typeof(SourceRow), InplaceAdd = true)]
        public Int32? SourceId
        {
            get { return Fields.SourceId[this]; }
            set { Fields.SourceId[this] = value; }
        }

        [DisplayName("Stage"), NotNull, ForeignKey("[dbo].[Stage]", "Id"), LeftJoin("jStage"), TextualField("Stage")]
        [LookupEditor(typeof(StageRow), InplaceAdd = true, FilterField = "Type", FilterValue = Masters.StageTypeMaster.Quotation)]
        public Int32? StageId
        {
            get { return Fields.StageId[this]; }
            set { Fields.StageId[this] = value; }
        }

        [DisplayName("Created-By Teams"), Expression("jOwner.[TeamsId]")]
        [LookupEditor(typeof(TeamsRow),InplaceAdd =false)]
        public Int32? OwnerTeamsId
        {
            get { return Fields.OwnerTeamsId[this]; }
            set { Fields.OwnerTeamsId[this] = value; }
        }


        [DisplayName("Assigned Teams "), Expression("jAssigned.[TeamsId]")]
        [LookupEditor(typeof(TeamsRow), InplaceAdd = false)]
        public Int32? AssignedTeamsId
        {
            get { return Fields.AssignedTeamsId[this]; }
            set { Fields.AssignedTeamsId[this] = value; }
        }

        [DisplayName("Branch"), ForeignKey("[dbo].[Branch]", "Id"), LeftJoin("jBranch"), TextualField("Branch")]
         [LookupEditor("Administration.BranchLookup")]
        public Int32? BranchId
        {
            get { return Fields.BranchId[this]; }
            set { Fields.BranchId[this] = value; }
        }

        [DisplayName("Created By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername")]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get { return Fields.OwnerId[this]; }
            set { Fields.OwnerId[this] = value; }
        }

        [DisplayName("Assigned To"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssigned"), TextualField("AssignedUsername"), QuickFilter]
        [Administration.UserEditor]
        public Int32? AssignedId
        {
            get { return Fields.AssignedId[this]; }
            set { Fields.AssignedId[this] = value; }
        }

        [DisplayName("Reference Name"), Size(100)]
        public String ReferenceName
        {
            get { return Fields.ReferenceName[this]; }
            set { Fields.ReferenceName[this] = value; }
        }

        [DisplayName("Reference Phone"), Size(50)]
        public String ReferencePhone
        {
            get { return Fields.ReferencePhone[this]; }
            set { Fields.ReferencePhone[this] = value; }
        }

        [DisplayName("Closing Type")]
        public Masters.ClosingTypeMaster? ClosingType
        {
            get { return (Masters.ClosingTypeMaster?)Fields.ClosingType[this]; }
            set { Fields.ClosingType[this] = (Int32?)value; }
        }

        [DisplayName("Lost Reason"), Size(450)]
        public String LostReason
        {
            get { return Fields.LostReason[this]; }
            set { Fields.LostReason[this] = value; }
        }

        [DisplayName("Subject"), Size(500)]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Reference"), Size(500)]
        public String Reference
        {
            get { return Fields.Reference[this]; }
            set { Fields.Reference[this] = value; }
        }

        [DisplayName("Attachment"), Size(500)]
        [MultipleImageUploadEditor(FilenameFormat = "Quotation/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("No Of Lines"), DefaultValue(10)]
        public Int32? Lines
        {
            get { return Fields.Lines[this]; }
            set { Fields.Lines[this] = value; }
        }

        [DisplayName("Contact Person"), ForeignKey("[dbo].[SubContacts]", "Id"), LeftJoin("jContactPerson"), TextualField("ContactPersonName")]
        [LookupEditor(typeof(SubContactsRow), CascadeFrom = "ContactsId", CascadeValue = "ContactsId")]
        public Int32? ContactPersonId
        {
            get { return Fields.ContactPersonId[this]; }
            set { Fields.ContactPersonId[this] = value; }
        }

        [DisplayName("Closing Date")]
        public DateTime? ClosingDate
        {
            get { return Fields.ClosingDate[this]; }
            set { Fields.ClosingDate[this] = value; }
        }

        [DisplayName("Enquiry No")]
        public Int32? EnquiryNo
        {
            get { return Fields.EnquiryNo[this]; }
            set { Fields.EnquiryNo[this] = value; }
        }

        [DisplayName("Enquiry Date")]
        public DateTime? EnquiryDate
        {
            get { return Fields.EnquiryDate[this]; }
            set { Fields.EnquiryDate[this] = value; }
        }

        [DisplayName("Exchange Rate"), DecimalEditor(Decimals = 12)]
        public Double? Conversion
        {
            get { return Fields.Conversion[this]; }
            set { Fields.Conversion[this] = value; }
        }

        [DisplayName("Currency Conversion")]
        [BooleanSwitchEditor]
        public Boolean? CurrencyConversion
        {
            get { return Fields.CurrencyConversion[this]; }
            set { Fields.CurrencyConversion[this] = value; }
        }

        [DisplayName("From Currency")]
        public Masters.Currencies? FromCurrency
        {
            get { return (Masters.Currencies?)Fields.FromCurrency[this]; }
            set { Fields.FromCurrency[this] = (Int32?)value; }
        }

        [DisplayName("To Currency")]
        public Masters.Currencies? ToCurrency
        {
            get { return (Masters.Currencies?)Fields.ToCurrency[this]; }
            set { Fields.ToCurrency[this] = (Int32?)value; }
        }

        [DisplayName("Taxable"), DefaultValue(true)]
        [BooleanSwitchEditor]
        public Boolean? Taxable
        {
            get { return Fields.Taxable[this]; }
            set { Fields.Taxable[this] = value; }
        }

        [DisplayName("No."), Required(true)]
        public Int32? QuotationNo
        {
            get { return Fields.QuotationNo[this]; }
            set { Fields.QuotationNo[this] = value; }
        }

        [DisplayName("Roundup"), DefaultValue(0)]
        public Double? Roundup
        {
            get { return Fields.Roundup[this]; }
            set { Fields.Roundup[this] = value; }
        }
        [DisplayName("Enquiry N"), Size(100)]
        public String EnquiryN
        {
            get { return Fields.EnquiryN[this]; }
            set { Fields.EnquiryN[this] = value; }
        }


        [DisplayName("Message"), ForeignKey("[dbo].[MessageMaster]", "Id"), LeftJoin("jMessage"), TextualField("Message")]
        [LookupEditor(typeof(MessageMasterRow), InplaceAdd = true)]
        public Int32? MessageId
        {
            get { return Fields.MessageId[this]; }
            set { Fields.MessageId[this] = value; }
        }
        [DisplayName("Quotation N"), Size(100),NotNull]
        public String QuotationN
        {
            get { return Fields.QuotationN[this]; }
            set { Fields.QuotationN[this] = value; }
        }

        [DisplayName("Contacts Contact Type"), Expression("jContacts.[ContactType]")]
        public Int32? ContactsContactType
        {
            get { return Fields.ContactsContactType[this]; }
            set { Fields.ContactsContactType[this] = value; }
        }

        [DisplayName("Contact"), Expression("jContacts.[Name]"), QuickSearch,NameProperty]
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

        [DisplayName("Multi Aditional Info")]
        [LookupEditor(typeof(AdditionalInfoRow), Multiple = true, InplaceAdd = true, FilterField = "Type", FilterValue = Masters.AddInfoTypeMaster.Quotation), NotMapped]
        [LinkingSetRelation(typeof(QuotationMultiInfoRow), "QuotationId", "AdditionalInfoId")]
        public List<Int32> QuotationAddinfoList
        {
            get { return Fields.QuotationAddinfoList[this]; }
            set { Fields.QuotationAddinfoList[this] = value; }
        }

        [DisplayName("Contacts Whatsapp"), Expression("jContacts.[Whatsapp]")]
        public String ContactsWhatsapp
        {
            get { return Fields.ContactsWhatsapp[this]; }
            set { Fields.ContactsWhatsapp[this] = value; }
        }

        [DisplayName("Source"), Expression("jSource.[Source]")]
        public String Source
        {
            get { return Fields.Source[this]; }
            set { Fields.Source[this] = value; }
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
        [DisplayName("Discount(%)")]
        public Double? PerDiscount
        {
            get { return Fields.PerDiscount[this]; }
            set { Fields.PerDiscount[this] = value; }
        }

        [DisplayName("Discount(Amount)")]
        public Double? DiscountAmt
        {
            get { return Fields.DiscountAmt[this]; }
            set { Fields.DiscountAmt[this] = value; }
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

        [DisplayName("Assigned Branch Id"), Expression("jAssigned.[BranchId]")]
        public Int32? AssignedBranchId
        {
            get { return Fields.AssignedBranchId[this]; }
            set { Fields.AssignedBranchId[this] = value; }
        }

        [DisplayName("Contact Person"), Expression("jContactPerson.[Name]")]
        public String ContactPersonName
        {
            get { return Fields.ContactPersonName[this]; }
            set { Fields.ContactPersonName[this] = value; }
        }

        [DisplayName("Contact Person Phone"), Expression("jContactPerson.[Phone]")]
        public String ContactPersonPhone
        {
            get { return Fields.ContactPersonPhone[this]; }
            set { Fields.ContactPersonPhone[this] = value; }
        }

        [DisplayName("Contact Person Residential Phone"), Expression("jContactPerson.[ResidentialPhone]")]
        public String ContactPersonResidentialPhone
        {
            get { return Fields.ContactPersonResidentialPhone[this]; }
            set { Fields.ContactPersonResidentialPhone[this] = value; }
        }

        [DisplayName("Contact Person Email"), Expression("jContactPerson.[Email]")]
        public String ContactPersonEmail
        {
            get { return Fields.ContactPersonEmail[this]; }
            set { Fields.ContactPersonEmail[this] = value; }
        }

        [DisplayName("Contact Person Designation"), Expression("jContactPerson.[Designation]")]
        public String ContactPersonDesignation
        {
            get { return Fields.ContactPersonDesignation[this]; }
            set { Fields.ContactPersonDesignation[this] = value; }
        }

        [DisplayName("Contact Person Address"), Expression("jContactPerson.[Address]")]
        public String ContactPersonAddress
        {
            get { return Fields.ContactPersonAddress[this]; }
            set { Fields.ContactPersonAddress[this] = value; }
        }

        [DisplayName("Contact Person Gender"), Expression("jContactPerson.[Gender]")]
        public Int32? ContactPersonGender
        {
            get { return Fields.ContactPersonGender[this]; }
            set { Fields.ContactPersonGender[this] = value; }
        }

        [DisplayName("Contact Person Religion"), Expression("jContactPerson.[Religion]")]
        public Int32? ContactPersonReligion
        {
            get { return Fields.ContactPersonReligion[this]; }
            set { Fields.ContactPersonReligion[this] = value; }
        }

        [DisplayName("Contact Person Marital Status"), Expression("jContactPerson.[MaritalStatus]")]
        public Int32? ContactPersonMaritalStatus
        {
            get { return Fields.ContactPersonMaritalStatus[this]; }
            set { Fields.ContactPersonMaritalStatus[this] = value; }
        }

        [DisplayName("Contact Person Marriage Anniversary"), Expression("jContactPerson.[MarriageAnniversary]")]
        public DateTime? ContactPersonMarriageAnniversary
        {
            get { return Fields.ContactPersonMarriageAnniversary[this]; }
            set { Fields.ContactPersonMarriageAnniversary[this] = value; }
        }

        [DisplayName("Contact Person Birthdate"), Expression("jContactPerson.[Birthdate]")]
        public DateTime? ContactPersonBirthdate
        {
            get { return Fields.ContactPersonBirthdate[this]; }
            set { Fields.ContactPersonBirthdate[this] = value; }
        }

        [DisplayName("Contact Person Contacts Id"), Expression("jContactPerson.[ContactsId]")]
        public Int32? ContactPersonContactsId
        {
            get { return Fields.ContactPersonContactsId[this]; }
            set { Fields.ContactPersonContactsId[this] = value; }
        }

        [DisplayName("Project"), Expression("jContactPerson.[Project]")]
        public String ContactPersonProject
        {
            get { return Fields.ContactPersonProject[this]; }
            set { Fields.ContactPersonProject[this] = value; }
        }

        [DisplayName("Contact Person Whatsapp"), Expression("jContactPerson.[Whatsapp]")]
        public String ContactPersonWhatsapp
        {
            get { return Fields.ContactPersonWhatsapp[this]; }
            set { Fields.ContactPersonWhatsapp[this] = value; }
        }

        [DisplayName("Message"), Expression("jMessage.[Message]")]
        public String Message
        {
            get { return Fields.Message[this]; }
            set { Fields.Message[this] = value; }
        }

        [DisplayName("State"), Expression("(SELECT State FROM State WHERE Id= (SELECT Top (1) StateID FROM Contacts WHERE Id = t0.[ContactsId]))")]
        public String ContactsState
        {
            get { return Fields.ContactsState[this]; }
            set { Fields.ContactsState[this] = value; }
        }

        [DisplayName("City"), Expression("(SELECT City FROM City WHERE Id= (SELECT Top (1) CityID FROM Contacts WHERE Id = t0.[ContactsId]))"), QuickSearch]
        public String ContactsCity
        {
            get { return Fields.ContactsCity[this]; }
            set { Fields.ContactsCity[this] = value; }
        }

        [DisplayName("Area"), Expression("(SELECT Area FROM Area WHERE Id= (SELECT Top (1) AreaID FROM Contacts WHERE Id = t0.[ContactsId]))")]
        public String ContactsArea
        {
            get { return Fields.ContactsArea[this]; }
            set { Fields.ContactsArea[this] = value; }
        }

        [DisplayName("Products"), MasterDetailRelation(foreignKey: "QuotationId", IncludeColumns = "ProductsName"), NotMapped]
        [QuotationProductsEditor]
        public List<QuotationProductsRow> Products 
        { 
        	get { return Fields.Products[this]; } 
        	set { Fields.Products[this] = value; } 
        }

        [DisplayName("Total"), Expression("(SELECT SUM((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) FROM QuotationProducts WHERE QuotationId=t0.[Id])"), AlignRight]
        public Double? Total { get { return Fields.Total[this]; } set { Fields.Total[this] = value; } }

        [DisplayName("Tax1"), Expression("(SELECT SUM(((Price * Quantity) - (DiscountAmount + (Price * Quantity * (Discount / 100)))) * (Percentage1 / 100)) FROM QuotationProducts WHERE QuotationId=t0.[Id])"), AlignRight]
        public Double? Tax1 { get { return Fields.Tax1[this]; } set { Fields.Tax1[this] = value; } }

        [DisplayName("Tax2"), Expression("(SELECT SUM(((Price * Quantity) - (DiscountAmount + (Price * Quantity * (Discount / 100)))) * (Percentage2 / 100)) FROM QuotationProducts WHERE QuotationId=t0.[Id])"), AlignRight]
        public Double? Tax2 { get { return Fields.Tax2[this]; } set { Fields.Tax2[this] = value; } }

        [DisplayName("Amount"), Expression("IIF([Taxable] = 0, (SELECT SUM((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) FROM QuotationProducts WHERE QuotationId=t0.[Id]), (SELECT SUM((((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) + (((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) * (Percentage1 / 100)) + (((Price * Quantity) - ((DiscountAmount) + ((Price * Quantity) * (Discount / 100)))) * (Percentage2 / 100)))) FROM QuotationProducts WHERE QuotationId=t0.[Id])) + COALESCE(t0.[Roundup], 0)"), AlignRight]
        public Double? GrandTotal { get { return Fields.GrandTotal[this]; } set { Fields.GrandTotal[this] = value; } }

        [DisplayName("Last Followup "), Expression("(SELECT Details FROM QuotationFollowups WHERE QuotationId=t0.[Id] AND Id=(SELECT MAX(Id) FROM QuotationFollowups WHERE QuotationId=t0.[Id]))"), MinSelectLevel(SelectLevel.List)]
        public String LastFollowup 
        { 
        	get { return Fields.LastFollowup[this]; } 
        	set { Fields.LastFollowup[this] = value; } 
        }

        [DisplayName("Company"), NotNull, ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), LookupInclude]
        [LookupEditor(typeof(CompanyDetailsRow), InplaceAdd = false)]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }


        [DisplayName("Terms")]
        [LookupEditor(typeof(QuotationTermsMasterRow), Multiple = true, InplaceAdd = true), NotMapped]
        [LinkingSetRelation(typeof(QuotationTermsRow), "QuotationId", "TermsId")]
        public List<Int32> TermsList 
        { 
        	get { return Fields.TermsList[this]; } 
        	set { Fields.TermsList[this] = value; }
        }

        [DisplayName("Multi Assign")]
        [LookupEditor(typeof(UserRow), Multiple = true, FilterField = "IsActive", FilterValue = 1), NotMapped]
        [LinkingSetRelation(typeof(MultiRepQuotationRow), "QuotationId", "AssignedId")]
        public List<Int32> MultiAssignList
        {
            get { return Fields.MultiAssignList[this]; }
            set { Fields.MultiAssignList[this] = value; }
        }

        [DisplayName("Additional Charges")]
        [LookupEditor(typeof(AdditionalChargesRow), Multiple = true, InplaceAdd = true), NotMapped]
        [LinkingSetRelation(typeof(QuotationChargesRow), "QuotationId", "ChargesId")]
        public List<Int32> ChargesList { get { return Fields.ChargesList[this]; } set { Fields.ChargesList[this] = value; } }

        [DisplayName("Additional Concession")]

        [LookupEditor(typeof(AdditionalConcessionRow), Multiple = true, InplaceAdd = true), NotMapped]
        [LinkingSetRelation(typeof(QuotationConcessionRow), "QuotationId", "ConcessionId")]
        public List<Int32> ConcessionList { get { return Fields.ConcessionList[this]; } set { Fields.ConcessionList[this] = value; } }

        [NotesEditor, NotMapped]
        [DisplayName("")]
        public List<NoteRow> NoteList
        {
            get { return Fields.NoteList[this]; }
            set { Fields.NoteList[this] = value; }
        }

        [DisplayName("Dealer Dealer Name"), Expression("jDealer.[DealerName]")]
        public String DealerDealerName
        {
            get { return Fields.DealerDealerName[this]; }
            set { Fields.DealerDealerName[this] = value; }
        }

        [DisplayName("Dealer Phone"), Expression("jDealer.[Phone]")]
        public String DealerPhone
        {
            get { return Fields.DealerPhone[this]; }
            set { Fields.DealerPhone[this] = value; }
        }

        [DisplayName("Dealer Email"), Expression("jDealer.[Email]")]
        public String DealerEmail
        {
            get { return Fields.DealerEmail[this]; }
            set { Fields.DealerEmail[this] = value; }
        }

        [DisplayName("Dealer Address"), Expression("jDealer.[Address]")]
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

        [DisplayName("GrandTotal")]
        public Double? DisGrandTotal
        {
            get { return Fields.DisGrandTotal[this]; }
            set { Fields.DisGrandTotal[this] = value; }
        }
        [DisplayName("Dealer State Id"), Expression("jDealer.[StateId]")]
        public Int32? DealerStateId
        {
            get { return Fields.DealerStateId[this]; }
            set { Fields.DealerStateId[this] = value; }
        }
        [DisplayName("Dealer"), ForeignKey("[dbo].[Dealer]", "Id"), LeftJoin("jDealer"), TextualField("DealerDealerName")]
        [LookupEditor(typeof(Masters.DealerRow),InplaceAdd =true)]
        public Int32? DealerId
        {
            get { return Fields.DealerId[this]; }
            set { Fields.DealerId[this] = value; }
        }
        [DisplayName("Project"), ForeignKey("[dbo].[MultiProjects]", "Id"), LeftJoin("jProject"), TextualField("Id")]
        [LookupEditor(typeof(ProjectRow))]
        public Int32? ProjectId
        {
            get { return Fields.ProjectId[this]; }
            set { Fields.ProjectId[this] = value; }
        }
        [DisplayName("Dealer Pin"), Expression("jDealer.[Pin]")]
        public String DealerPin
        {
            get { return Fields.DealerPin[this]; }
            set { Fields.DealerPin[this] = value; }
        }
        [DisplayName("Project Project Id"), Expression("jProject.[ProjectId]")]
        public Int32? ProjectProjectId
        {
            get { return Fields.ProjectProjectId[this]; }
            set { Fields.ProjectProjectId[this] = value; }
        }

        [DisplayName("Project Sub Contacts Id"), Expression("jProject.[SubContactsId]")]
        public Int32? ProjectSubContactsId
        {
            get { return Fields.ProjectSubContactsId[this]; }
            set { Fields.ProjectSubContactsId[this] = value; }
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

        [DisplayName("Expected Closing Date")]
        public DateTime? ExpectedClosingDate
        {
            get { return Fields.ExpectedClosingDate[this]; }
            set { Fields.ExpectedClosingDate[this] = value; }
        }

        [DisplayName("Win Percentage")]
        public Masters.WinPercentageMaster? WinPercentage
        {
            get { return (Masters.WinPercentageMaster?)Fields.WinPercentage[this]; }
            set { Fields.WinPercentage[this] = (Int32?)value; }
        }



        [TimelineEditor, NotMapped]
        [DisplayName("")]
        public List<TimelineRow> Timeline
        {
            get { return Fields.Timeline[this]; }
            set { Fields.Timeline[this] = value; }
        }

        public Int32Field CompanyIdField
        {
            get { return Fields.CompanyId; }
        }
       

        public QuotationRow()
            : base(Fields)
        {
        }
        public QuotationRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ContactsId;
            public DateTimeField Date;
            public Int32Field Status;
            public Int32Field Type;
            public StringField AdditionalInfo;
            public Int32Field SourceId;
            public Int32Field StageId;
            public Int32Field BranchId;
            public Int32Field OwnerId;
            public Int32Field AssignedId;
            public StringField ReferenceName;
            public StringField ReferencePhone;
            public Int32Field ClosingType;
            public StringField LostReason;
            public StringField Subject;
            public StringField Reference;
            public StringField Attachment;
            public Int32Field Lines;
            public Int32Field ContactPersonId;
            public DateTimeField ClosingDate;
            public StringField EnquiryN;
            public DoubleField PerDiscount;
            public DoubleField DiscountAmt;
            public DoubleField DisGrandTotal;

            public Int32Field ProjectId;
            public Int32Field ProjectProjectId;
            public Int32Field ProjectSubContactsId;

            //public StringField ProductsDivision;
            public StringField AdditionalInfo2;
            public Int32Field CompanyId;
            public Int32Field DealerId;


            public Int32Field EnquiryNo;
            public DateTimeField EnquiryDate;
            public DoubleField Conversion;
            public BooleanField CurrencyConversion;
            public Int32Field FromCurrency;
            public Int32Field ToCurrency;
            public BooleanField Taxable;
            public Int32Field QuotationNo;
            public DoubleField Roundup;
            public Int32Field MessageId;
            public StringField QuotationN;

            public readonly ListField<Int32> MultiAssignList;

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

            public StringField Source;

            public StringField Stage;
            public Int32Field StageType;

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
            public StringField OwnerUid;
            public BooleanField OwnerNonOperational;
            public Int32Field OwnerBranchId;

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
            public StringField AssignedUid;
            public BooleanField AssignedNonOperational;
            public Int32Field AssignedBranchId;

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
            public StringField ContactsState;
            public StringField ContactsCity;
            public StringField ContactsArea;

            public StringField Message;

            public DoubleField Total;
            public DoubleField Tax1;
            public DoubleField Tax2;
            public DoubleField GrandTotal;

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

            public StringField LastFollowup;
            public readonly ListField<Int32> TermsList;
            public readonly ListField<Int32> ChargesList;
            public readonly ListField<Int32> ConcessionList;
            public readonly ListField<Int32> QuotationAddinfoList;
            public readonly RowListField<QuotationProductsRow> Products;
            public RowListField<NoteRow> NoteList;
            public RowListField<TimelineRow> Timeline;

            public StringField DealerDealerName;
            public StringField DealerPhone;
            public StringField DealerEmail;
            public StringField DealerAddress;
            public Int32Field DealerCityId;
            public Int32Field DealerStateId;
            public StringField DealerPin;
            public Int32Field DealerCountry;
            public StringField DealerAdditionalInfo;

            public Int32Field OwnerTeamsId;
            public Int32Field AssignedTeamsId;

            public DateTimeField ExpectedClosingDate;
            public Int32Field WinPercentage;
        }
    }
}
