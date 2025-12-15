
namespace AdvanceCRM.Contacts.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;


    [FormScript("Contacts.Contacts")]
    [BasedOnRow(typeof(ContactsRow), CheckNames = true)]
    public class ContactsForm
    {
       
        [Tab("General")]        
        [HalfWidth]
        public Int32 ContactType { get; set; }
        [HalfWidth, DateTimeEditor, DefaultValue("now")]
        public DateTime DateCreated { get; set; }
        [HalfWidth]
        public Int32 CustomerType { get; set; }
        [HalfWidth, DefaultValue("0")]
        public Int32 CategoryId { get; set; }
        [HalfWidth, DefaultValue("0")]
        public Int32 GradeId { get; set; }      

        
        [Category("Basic Details")]
        [DisplayName("CompanyName/Name")]
        public String Name { get; set; }
        [HalfWidth]
        public String Phone { get; set; }
        [HalfWidth]
        public String Email { get; set; }
        public String Address { get; set; }
        [HalfWidth, DefaultValue(81)]
        public Int32 Country { get; set; }
        [HalfWidth]
        public Int32 StateId { get; set; }
        [HalfWidth]
        public Int32 CityId { get; set; }
        [HalfWidth]
        public Int32 TehsilId { get; set; }
        [HalfWidth]
        public Int32 VillageId { get; set; }
        [HalfWidth]
        public String Whatsapp { get; set; }
        [HalfWidth]
        public String Pin { get; set; }
        [HalfWidth]
        public String Website { get; set; }
        public String AdditionalInfo { get; set; }
        public String AdditionalInfo2 { get; set; }

        public List<Int32> ContactAddinfoList { get; set; }
        [SubContactsEditor]
        public List<SubContactsRow> SubContacts { get; set; }
        [Category("Representative")]
        [OneThirdWidth]
        public Int32 OwnerId { get; set; }
        [OneThirdWidth]
        public Int32 AssignedId { get; set; }
        [OneThirdWidth]
        public List<Int32> MultiAssignList { get; set; }
        [Tab("Additional Details")]
        [Category("Additional Details")]
        [HalfWidth]
        public String GSTIN { get; set; }
        [HalfWidth]
        public String EComGSTIN { get; set; }
        [HalfWidth]
        public String PANNo { get; set; }
        [HalfWidth]
        public String AadharNo { get; set; }
        [HalfWidth, MaskedEditor(Mask = "9999999999")]
        public String ResidentialPhone { get; set; }
        [HalfWidth, MaskedEditor(Mask = "9999999999")]
        public String OfficePhone { get; set; }
        [HalfWidth]
        public Int32 Gender { get; set; }
        [HalfWidth]
        public Int32 Religion { get; set; }
        [HalfWidth]
        public Int32 Type { get; set; }
        [HalfWidth]
        public Int32 AreaId { get; set; }
        [HalfWidth]
        public Int32 TrasportationId { get; set; }
        public String Attachment { get; set; }
        [Category("Account Details")]
        [DecimalEditor(MinValue = "-9999999999", MaxValue = "9999999999")]
        [OneThirdWidth]
        public Double CreditorsOpening { get; set; }
        [DecimalEditor(MinValue = "-9999999999", MaxValue = "9999999999")]
        [OneThirdWidth]
        public Double DebtorsOpening { get; set; }
        [OneThirdWidth]
        public Int32 CreditDays { get; set; }
        [Category("PassportDetails")]
        [OneThirdWidth]
        public String PassportNumber { get; set; }
        [OneThirdWidth]
        public String FirstName { get; set; }
        [OneThirdWidth]
        public String LastName { get; set; }
        [OneThirdWidth]
        public DateTime ExpiryDate { get; set; }

        [Category("Anniversaries")]
        [OneThirdWidth]
        public Int32 MaritalStatus { get; set; }
        [OneThirdWidth]
        public DateTime MarriageAnniversary { get; set; }
        [OneThirdWidth]
        public DateTime Birthdate { get; set; }
        [OneThirdWidth]
        public DateTime DateOfIncorporation { get; set; }
        [Tab("Business Details")]
        [Category("Emails")]
        [HalfWidth]
        public String AccountsEmail { get; set; }
        [HalfWidth]
        public String PurchaseEmail { get; set; }
        [HalfWidth]
        public String SalesEmail { get; set; }
        [HalfWidth]
        public String ServiceEmail { get; set; }
        public String CCEmails { get; set; }
        public String BCCEmails { get; set; }
        [Category("Channels")]
        public Int32 ChannelCategory { get; set; }
        [HalfWidth]
        public Int32 NationalDistributor { get; set; }
        [HalfWidth]
        public Int32 Stockist { get; set; }
        [HalfWidth]
        public Int32 Distributor { get; set; }
        [HalfWidth]
        public Int32 Dealer { get; set; }
        [HalfWidth]
        public Int32 Wholesaler { get; set; }
        [HalfWidth]
        public Int32 Reseller { get; set; }
        [Category("Bank Details")]
        public String BankName { get; set; }
        [HalfWidth]
        public String AccountNumber { get; set; }
        [HalfWidth]
        public String IFSC { get; set; }
        [HalfWidth]
        public String BankType { get; set; }
        [HalfWidth]
        public String Branch { get; set; }
        [Category("Notes")]
        public List<object> NoteList { get; set; }
    }
}