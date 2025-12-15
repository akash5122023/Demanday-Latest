
namespace AdvanceCRM.Contacts
{
    using _Ext;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Administration;
    using AdvanceCRM.Common;
    using AdvanceCRM.Common;
    using AdvanceCRM.Masters;
    using Serenity;
    using AdvanceCRM.Scripts;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Contacts"), TableName("[dbo].[Contacts]")]
    [DisplayName("Contacts"), InstanceName("Contacts")]
    [ReadPermission("Contacts:Read")]
    [InsertPermission("Contacts:Insert")]
    [UpdatePermission("Contacts:Update")]
    [DeletePermission("Contacts:Delete")]
    [LookupScript("Contacts.Contacts", Permission = "?",Expiration =-1)]
    public sealed class ContactsRow : Row<ContactsRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity, SortOrder(1, true),IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Contact Type"), DefaultValue("1"), NotNull, LookupInclude]
        public Masters.CTypeMaster? ContactType
        {
            get { return (Masters.CTypeMaster?)Fields.ContactType[this]; }
            set { Fields.ContactType[this] = (Int32?)value; }
        }

        [DisplayName("Name"), Size(150), NotNull,LookupInclude, QuickSearch,NameProperty]
        public String Name
        {
            get { return Fields.Name[this]; }
            set { Fields.Name[this] = value; }
        }

        [DisplayName("Phone"), Size(15), NotNull, QuickSearch, LookupInclude, Unique]
        public String Phone
        {
            get { return Fields.Phone[this]; }
            set { Fields.Phone[this] = value; }
        }

        [DisplayName("Email"), Size(100), EmailEditor,LookupInclude]
        public String Email
        {
            get { return Fields.Email[this]; }
            set { Fields.Email[this] = value; }
        }

        [DisplayName("Address"), Size(500), LookupInclude, TextAreaEditor(Rows = 4)]
        public String Address
        {
            get { return Fields.Address[this]; }
            set { Fields.Address[this] = value; }
        }

        [DisplayName("City/District"), ForeignKey("[dbo].[City]", "Id"), LeftJoin("jCity"), TextualField("City")]
        [LookupEditor(typeof(CityRow), InplaceAdd = true, CascadeFrom = "StateId", CascadeValue = "StateId")]
        public Int32? CityId
        {
            get { return Fields.CityId[this]; }
            set { Fields.CityId[this] = value; }
        }

        [DisplayName("State"), ForeignKey("[dbo].[State]", "Id"), LeftJoin("jState"), TextualField("State")]
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

        [DisplayName("Website"), Size(150)]
        public String Website
        {
            get { return Fields.Website[this]; }
            set { Fields.Website[this] = value; }
        }

        [DisplayName("Additional Info"), Size(5000), TextAreaEditor(Rows = 4)]
        public String AdditionalInfo
        {
            get { return Fields.AdditionalInfo[this]; }
            set { Fields.AdditionalInfo[this] = value; }
        }

        [DisplayName("Residential Phone"), Size(100)]
        public String ResidentialPhone
        {
            get { return Fields.ResidentialPhone[this]; }
            set { Fields.ResidentialPhone[this] = value; }
        }

        [DisplayName("Office Phone"), Size(100)]
        public String OfficePhone
        {
            get { return Fields.OfficePhone[this]; }
            set { Fields.OfficePhone[this] = value; }
        }

        [DisplayName("Gender")]
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

        [DisplayName("Marital Status")]
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

        [DisplayName("Date Of Incorporation")]
        public DateTime? DateOfIncorporation
        {
            get { return Fields.DateOfIncorporation[this]; }
            set { Fields.DateOfIncorporation[this] = value; }
        }

        [DisplayName("Category"), ForeignKey("[dbo].[Category]", "Id"), LeftJoin("jCategory"), TextualField("Category"), QuickFilter]
        [LookupEditor(typeof(CategoryRow), InplaceAdd = true)]
        public Int32? CategoryId
        {
            get { return Fields.CategoryId[this]; }
            set { Fields.CategoryId[this] = value; }
        }

        [DisplayName("Grade"), ForeignKey("[dbo].[Grade]", "Id"), LeftJoin("jGrade"), TextualField("Grade")]
        [LookupEditor(typeof(GradeRow), InplaceAdd = true)]
        public Int32? GradeId
        {
            get { return Fields.GradeId[this]; }
            set { Fields.GradeId[this] = value; }
        }

        [DisplayName("Type")]
        public Masters.TypeMaster? Type
        {
            get { return (Masters.TypeMaster?)Fields.Type[this]; }
            set { Fields.Type[this] = (Int32?)value; }
        }

        [DisplayName("Created By"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jOwner"), TextualField("OwnerUsername"), ReadOnly(true)]
        [Administration.UserEditor]
        public Int32? OwnerId
        {
            get { return Fields.OwnerId[this]; }
            set { Fields.OwnerId[this] = value; }
        }

        [DisplayName("Assigned To"), NotNull, ForeignKey("[dbo].[Users]", "UserId"), LeftJoin("jAssigned"), TextualField("AssignedUsername")]
        [Administration.UserEditor]
        public Int32? AssignedId
        {
            get { return Fields.AssignedId[this]; }
            set { Fields.AssignedId[this] = value; }
        }

        [DisplayName("Additional Info2"), Size(2000), TextAreaEditor(Rows = 3)]
        public String AdditionalInfo2
        {
            get { return Fields.AdditionalInfo2[this]; }
            set { Fields.AdditionalInfo2[this] = value; }
        }

        [DisplayName("Date Created")]
        public DateTime? DateCreated
        {
            get { return Fields.DateCreated[this]; }
            set { Fields.DateCreated[this] = value; }
        }

        [DisplayName("Channel Category"), LookupInclude]
        public Masters.ChannelCategory? ChannelCategory
        {
            get { return (Masters.ChannelCategory?)Fields.ChannelCategory[this]; }
            set { Fields.ChannelCategory[this] = (Int32?)value; }
        }

        [DisplayName("National Distributor"), ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jND"), TextualField("NationalDistributorName")]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? NationalDistributor
        {
            get { return Fields.NationalDistributor[this]; }
            set { Fields.NationalDistributor[this] = value; }
        }

        [DisplayName("Stockist"), ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jStockist"), TextualField("StockistName")]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? Stockist
        {
            get { return Fields.Stockist[this]; }
            set { Fields.Stockist[this] = value; }
        }

        [DisplayName("Distributor"), ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jDistributor"), TextualField("DistributorName")]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? Distributor
        {
            get { return Fields.Distributor[this]; }
            set { Fields.Distributor[this] = value; }
        }

        [DisplayName("Dealer"), ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jDealer"), TextualField("DealerName")]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? Dealer
        {
            get { return Fields.Dealer[this]; }
            set { Fields.Dealer[this] = value; }
        }

        [DisplayName("Wholesaler"), ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jWholesaler"), TextualField("WholesalerName")]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? Wholesaler
        {
            get { return Fields.Wholesaler[this]; }
            set { Fields.Wholesaler[this] = value; }
        }

        [DisplayName("Reseller"), ForeignKey("[dbo].[Contacts]", "Id"), LeftJoin("jReseller"), TextualField("ResellerName")]
        [LookupEditor(typeof(ContactsRow))]
        public Int32? Reseller
        {
            get { return Fields.Reseller[this]; }
            set { Fields.Reseller[this] = value; }
        }

        [DisplayName("GSTIN"), Column("GSTIN"), Size(50)]
        public String GSTIN
        {
            get { return Fields.GSTIN[this]; }
            set { Fields.GSTIN[this] = value; }
        }

        [DisplayName("Pan No"), Column("PANNo"), Size(50)]
        public String PANNo
        {
            get { return Fields.PANNo[this]; }
            set { Fields.PANNo[this] = value; }
        }

        [DisplayName("CCEmails"), Size(500), TextAreaEditor(Rows = 4), Placeholder("Add multiple emails seperated by comma")]
        public String CCEmails
        {
            get { return Fields.CCEmails[this]; }
            set { Fields.CCEmails[this] = value; }
        }

        [DisplayName("BCCEmails"), Size(500), TextAreaEditor(Rows = 4), Placeholder("Add multiple emails seperated by comma")]
        public String BCCEmails
        {
            get { return Fields.BCCEmails[this]; }
            set { Fields.BCCEmails[this] = value; }
        }

        [DisplayName("Attachment"), Size(500)]
        [MultipleImageUploadEditor(FilenameFormat = "Contacts/~", CopyToHistory = true, AllowNonImage = true)]
        public String Attachment
        {
            get { return Fields.Attachment[this]; }
            set { Fields.Attachment[this] = value; }
        }

        [DisplayName("E Com GSTIN"), Column("EComGSTIN"), Size(100)]
        public String EComGSTIN
        {
            get { return Fields.EComGSTIN[this]; }
            set { Fields.EComGSTIN[this] = value; }
        }

        [DisplayName("Creditors Opening")]
        public Double? CreditorsOpening
        {
            get { return Fields.CreditorsOpening[this]; }
            set { Fields.CreditorsOpening[this] = value; }
        }

        [DisplayName("Debtors Opening")]
        public Double? DebtorsOpening
        {
            get { return Fields.DebtorsOpening[this]; }
            set { Fields.DebtorsOpening[this] = value; }
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

        [DisplayName("IFSC"), Column("IFSC"), Size(100)]
        public String IFSC
        {
            get { return Fields.IFSC[this]; }
            set { Fields.IFSC[this] = value; }
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

        [DisplayName("Accounts Email"), Size(100), EmailEditor]
        public String AccountsEmail
        {
            get { return Fields.AccountsEmail[this]; }
            set { Fields.AccountsEmail[this] = value; }
        }

        [DisplayName("Purchase Email"), Size(100), EmailEditor]
        public String PurchaseEmail
        {
            get { return Fields.PurchaseEmail[this]; }
            set { Fields.PurchaseEmail[this] = value; }
        }

        [DisplayName("Service Email"), Size(100), EmailEditor]
        public String ServiceEmail
        {
            get { return Fields.ServiceEmail[this]; }
            set { Fields.ServiceEmail[this] = value; }
        }

        [DisplayName("Sales Email"), Size(100), EmailEditor]
        public String SalesEmail
        {
            get { return Fields.SalesEmail[this]; }
            set { Fields.SalesEmail[this] = value; }
        }

        [DisplayName("Credit Days")]
        public Int32? CreditDays
        {
            get { return Fields.CreditDays[this]; }
            set { Fields.CreditDays[this] = value; }
        }

        [DisplayName("Customer Type"), DefaultValue("1"), NotNull]
        public Masters.ContactTypeMaster? CustomerType
        {
            get { return (Masters.ContactTypeMaster?)Fields.CustomerType[this]; }
            set { Fields.CustomerType[this] = (Int32?)value; }
        }

        [DisplayName("Transportation"), ForeignKey("[dbo].[Transportation]", "Id"), LeftJoin("jTransportation"), TextualField("Name"), LookupInclude]
        [LookupEditor(typeof(TransportationRow), InplaceAdd = true)]
        public Int32? TrasportationId
        {
            get { return Fields.TrasportationId[this]; }
            set { Fields.TrasportationId[this] = value; }
        }

        [DisplayName("Tehsil"), ForeignKey("[dbo].[Tehsil]", "Id"), LeftJoin("jTehsil"), TextualField("Tehsil")]
        [LookupEditor(typeof(TehsilRow), InplaceAdd = true, CascadeFrom = "CityId", CascadeValue = "CityId")]
        public Int32? TehsilId
        {
            get { return Fields.TehsilId[this]; }
            set { Fields.TehsilId[this] = value; }
        }

        [DisplayName("Village"), ForeignKey("[dbo].[Village]", "Id"), LeftJoin("jVillage"), TextualField("Village")]
        [LookupEditor(typeof(VillageRow), InplaceAdd = true, CascadeFrom = "TehsilId", CascadeValue = "TehsilId")]
        public Int32? VillageId
        {
            get { return Fields.VillageId[this]; }
            set { Fields.VillageId[this] = value; }
        }

        [DisplayName("Whatsapp"), Size(100), LookupInclude]
        public String Whatsapp
        {
            get { return Fields.Whatsapp[this]; }
            set { Fields.Whatsapp[this] = value; }
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

        [DisplayName("Category"), Expression("jCategory.[Category]")]
        public String Category
        {
            get { return Fields.Category[this]; }
            set { Fields.Category[this] = value; }
        }

        [DisplayName("Category Type"), Expression("jCategory.[Type]")]
        public Int32? CategoryType
        {
            get { return Fields.CategoryType[this]; }
            set { Fields.CategoryType[this] = value; }
        }

        [DisplayName("Grade"), Expression("jGrade.[Grade]")]
        public String Grade
        {
            get { return Fields.Grade[this]; }
            set { Fields.Grade[this] = value; }
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

        [DisplayName("Sub Contacts"), MasterDetailRelation(foreignKey: "ContactsId", IncludeColumns = "Name"), NotMapped]
        public List<SubContactsRow> SubContacts 
        { 
        	get { return Fields.SubContacts[this]; } 
        	set { Fields.SubContacts[this] = value; } 
        }

        [DisplayName("Multi Aditional Info")]
        [LookupEditor(typeof(AdditionalInfoRow), Multiple = true, InplaceAdd = true, FilterField = "Type", FilterValue = Masters.AddInfoTypeMaster.Contact), NotMapped]
        [LinkingSetRelation(typeof(ContactsMultiInfoRow), "ContactsId", "AdditionalInfoId")]
        public List<Int32> ContactAddinfoList
        {
            get { return Fields.ContactAddinfoList[this]; }
            set { Fields.ContactAddinfoList[this] = value; }
        }

        [DisplayName("Multi Assign")]
        [LookupEditor(typeof(UserRow), Multiple = true, FilterField = "IsActive", FilterValue = 1), NotMapped]
        [LinkingSetRelation(typeof(MultiRepContactsRow), "ContactsId", "AssignedId")]
        public List<Int32> MultiAssignList
        {
            get { return Fields.MultiAssignList[this]; }
            set { Fields.MultiAssignList[this] = value; }
        }
        [DisplayName("Passport Number"), Size(50)]
        public String PassportNumber
        {
            get { return Fields.PassportNumber[this]; }
            set { Fields.PassportNumber[this] = value; }
        }
        [DisplayName("First Name"), LookupInclude, QuickSearch]
        public String FirstName
        {
            get { return Fields.FirstName[this]; }
            set { Fields.FirstName[this] = value; }
        }
        [DisplayName("Last Name"), LookupInclude, QuickSearch]
        public String LastName
        {
            get { return Fields.LastName[this]; }
            set { Fields.LastName[this] = value; }
        }

        [DisplayName("Expiry Date")]
        public DateTime? ExpiryDate
        {
            get { return Fields.ExpiryDate[this]; }
            set { Fields.ExpiryDate[this] = value; }
        }

        [DisplayName("Aadhar No")]
        public String AadharNo
        {
            get { return Fields.AadharNo[this]; }
            set { Fields.AadharNo[this] = value; }
        }


        //[DisplayName("PhonePlusName"), Expression("(t0.Phone)"), NotMapped, MinSelectLevel(SelectLevel.List), LookupInclude]
        //public String PhonePlusName
        //{
        //    get { return Fields.PhonePlusName[this]; }
        //    set { Fields.PhonePlusName[this] = value; }
        //}

        //[DisplayName("IdPlusName"), Expression("(t0.Name)"), NotMapped, MinSelectLevel(SelectLevel.List), LookupInclude]
        //public String IdPlusName
        //{
        //    get { return Fields.IdPlusName[this]; }
        //    set { Fields.IdPlusName[this] = value; }
        //}

        //[DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        //[Insertable(false), Updatable(false)]
        //public Int32? CompanyId
        //{
        //    get { return Fields.CompanyId[this]; }
        //    set { Fields.CompanyId[this] = value; }
        //}
        //public Int32Field CompanyIdField
        //{
        //    get { return Fields.CompanyId; }
        //}

        [NotesEditor, NotMapped]
        public List<NoteRow> NoteList
        {
            get { return Fields.NoteList[this]; }
            set { Fields.NoteList[this] = value; }
        }

       

        public ContactsRow()
            : base(Fields)
        {
        }
        public ContactsRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field ContactType;
            public StringField Name;
            public StringField Phone;
            public StringField Email;
            public StringField Address;
            public Int32Field CityId;
            public Int32Field StateId;
            public StringField Pin;
            public Int32Field Country;
            public StringField Website;
            public StringField AdditionalInfo;
            public StringField ResidentialPhone;
            public StringField OfficePhone;
            public Int32Field Gender;
            public Int32Field Religion;
            public Int32Field AreaId;
            public Int32Field MaritalStatus;
            public DateTimeField MarriageAnniversary;
            public DateTimeField Birthdate;
            public DateTimeField DateOfIncorporation;
            public Int32Field CategoryId;
            public Int32Field GradeId;
            public Int32Field Type;
            public Int32Field OwnerId;
            public Int32Field AssignedId;
            public Int32Field ChannelCategory;
            public Int32Field NationalDistributor;
            public Int32Field Stockist;
            public Int32Field Distributor;
            public Int32Field Dealer;
            public Int32Field Wholesaler;
            public Int32Field Reseller;
            public StringField GSTIN;
            public StringField PANNo;
            public StringField CCEmails;
            public StringField BCCEmails;
            public StringField Attachment;
            public StringField EComGSTIN;
            public DoubleField CreditorsOpening;
            public DoubleField DebtorsOpening;
            public StringField BankName;
            public StringField AccountNumber;
            public StringField IFSC;
            public StringField BankType;
            public StringField Branch;
            public StringField AccountsEmail;
            public StringField PurchaseEmail;
            public StringField ServiceEmail;
            public StringField SalesEmail;
            public Int32Field CreditDays;
            public Int32Field CustomerType;
            public Int32Field TrasportationId;
            public Int32Field TehsilId;
            public Int32Field VillageId;
            public StringField Whatsapp;
            public StringField AdditionalInfo2;
            public DateTimeField DateCreated;
            //public StringField PhonePlusName;
            //public StringField IdPlusName;
            // public Int32Field CompanyId;

            public StringField City;
            public Int32Field CityStateId;

            public StringField State;

            public StringField Area;

            public StringField Category;
            public Int32Field CategoryType;

            public StringField Grade;

            public StringField OwnerUsername;
            public StringField OwnerDisplayName;
            public StringField OwnerEmail;

            public StringField AssignedUsername;
            public StringField AssignedDisplayName;
            public StringField AssignedEmail;

            public StringField Tehsil;
            public Int32Field TehsilStateId;
            public Int32Field TehsilCityId;

            public StringField Village;
            public Int32Field VillageStateId;
            public Int32Field VillageCityId;
            public Int32Field VillageTehsilId;
            
            public readonly RowListField<SubContactsRow> SubContacts;
            public readonly ListField<Int32> MultiAssignList;
            public readonly ListField<Int32> ContactAddinfoList;

            public RowListField<NoteRow> NoteList;

            public StringField PassportNumber;
            public StringField FirstName;
            public StringField LastName;
            public DateTimeField ExpiryDate;

            public StringField AadharNo;
        }
    }
}
