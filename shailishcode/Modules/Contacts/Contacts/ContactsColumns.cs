
namespace AdvanceCRM.Contacts.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("Contacts.Contacts")]
    [BasedOnRow(typeof(ContactsRow), CheckNames = true)]
    public class ContactsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, SortOrder(1, true)]
        public Int32 Id { get; set; }
        [Width(120), QuickFilter]
        public Masters.CTypeMaster ContactType { get; set; }
        [Width(120), QuickFilter]
        public Masters.ContactTypeMaster CustomerType { get; set; }
        [QuickFilter]
        public DateTime DateCreated { get; set; }
        [EditLink, Width(120), QuickFilter, QuickSearch]
        public String Name { get; set; }
        [QuickSearch]
        public String Phone { get; set; }
        [QuickSearch]
        public String Whatsapp { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        [QuickFilter]
        public String City { get; set; }
        [QuickFilter]
        public String State { get; set; }
        [QuickSearch]
        public String Pin { get; set; }
        [QuickFilter]
        public Masters.CountryMaster Country { get; set; }
        public String Website { get; set; }
        [QuickFilter, TextAreaEditor(Rows = 1)]
        public String AdditionalInfo { get; set; }
        [QuickFilter, TextAreaEditor(Rows = 1)]
        public String AdditionalInfo2 { get; set; }

        [QuickSearch]
        public String ResidentialPhone { get; set; }
        [QuickSearch]
        public String OfficePhone { get; set; }
        [QuickFilter]
        public Masters.GenderMaster Gender { get; set; }
        public String Area { get; set; }
        [QuickFilter]
        public String Category { get; set; }
        [QuickFilter]
        public String Grade { get; set; }
        [QuickFilter]
        public Masters.TypeMaster Type { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
        [QuickFilter]
        public String AssignedUsername { get; set; }
        public String GSTIN { get; set; }
        public String EComGSTIN { get; set; }
        public String PANNo { get; set; }
        [Hidden]
        public String Tehsil { get; set; }
        [Hidden]
        public String Village { get; set; }
        [Hidden]
        public Int32 Religion { get; set; }
        [Hidden]
        public Int32 MaritalStatus { get; set; }
        [Hidden]
        public DateTime MarriageAnniversary { get; set; }
        [Hidden]
        public DateTime Birthdate { get; set; }
        [Hidden]
        public Masters.ChannelCategory ChannelCategory { get; set; }
        [Hidden]
        public Int32 CreditDays { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public String AadharNo { get; set; }
    }
}