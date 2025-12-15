
namespace AdvanceCRM.Contacts
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Contacts"), TableName("[dbo].[ContactsMultiInfo]")]
    [DisplayName("Contacts Multi Info"), InstanceName("Contacts Multi Info")]
    [ReadPermission("Contacts:Read")]
    [InsertPermission("Contacts:Insert")]
    [UpdatePermission("Contacts:Update")]
    [DeletePermission("Contacts:Delete")]
    public sealed class ContactsMultiInfoRow : Row<ContactsMultiInfoRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Additional Info"), NotNull, ForeignKey("[dbo].[AdditionalInfo]", "Id"), LeftJoin("jAdditionalInfo"), TextualField("AdditionalInfo")]
        public Int32? AdditionalInfoId
        {
            get { return Fields.AdditionalInfoId[this]; }
            set { Fields.AdditionalInfoId[this] = value; }
        }

        [DisplayName("Contacts"), NotNull, ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jContacts"), TextualField("ContactsName")]
        public Int32? ContactsId
        {
            get { return Fields.ContactsId[this]; }
            set { Fields.ContactsId[this] = value; }
        }

        [DisplayName("Additional Info"), Expression("jAdditionalInfo.[AdditionalInfo]")]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
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

        [DisplayName("Contacts Phone"), Expression("jContacts.[Phone]")]
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

        [DisplayName("Contacts Address"), Expression("jContacts.[Address]")]
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

        [DisplayName("Contacts Gstin"), Expression("jContacts.[GSTIN]")]
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

        [DisplayName("Contacts E Com Gstin"), Expression("jContacts.[EComGSTIN]")]
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

        [DisplayName("Contacts Additional Info2"), Expression("jContacts.[AdditionalInfo2]")]
        public String ContactsAdditionalInfo2
        {
            get { return Fields.ContactsAdditionalInfo2[this]; }
            set { Fields.ContactsAdditionalInfo2[this] = value; }
        }

        [DisplayName("Contacts Date Created"), Expression("jContacts.[DateCreated]")]
        public DateTime? ContactsDateCreated
        {
            get { return Fields.ContactsDateCreated[this]; }
            set { Fields.ContactsDateCreated[this] = value; }
        }

      

        public ContactsMultiInfoRow()
            : base(Fields)
        {
        }
        
        public ContactsMultiInfoRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field AdditionalInfoId;
            public Int32Field ContactsId;

            public StringField AdditionalInfo;

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
            public StringField ContactsAdditionalInfo2;
            public DateTimeField ContactsDateCreated;
        }
    }
}
