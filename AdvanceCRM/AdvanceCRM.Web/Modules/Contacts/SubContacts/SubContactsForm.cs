
namespace AdvanceCRM.Contacts.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Contacts.SubContacts")]
    [BasedOnRow(typeof(SubContactsRow), CheckNames = true)]
    public class SubContactsForm
    {
        [Category("Basic Details")]
        public String Name { get; set; }
        [HalfWidth]
        public String Phone { get; set; }
        [HalfWidth, MaskedEditor(Mask = "9999999999")]
        public String ResidentialPhone { get; set; }
        [HalfWidth]
        public String Whatsapp { get; set; }
        public String Email { get; set; }
        [HalfWidth]
        public String Designation { get; set; }
        [HalfWidth]
        public String Project { get; set; }

        public List<Int32> MultiProjectList { get; set; }
        public String Address { get; set; }
        [Category("Personal Details")]
        [HalfWidth]
        public Int32 Gender { get; set; }
        [HalfWidth]
        public Int32 Religion { get; set; }
        [OneThirdWidth]
        public Int32 MaritalStatus { get; set; }
        [OneThirdWidth]
        public DateTime MarriageAnniversary { get; set; }
        [OneThirdWidth]
        public DateTime Birthdate { get; set; }
        [HalfWidth]
        public String PANNo { get; set; }
        [HalfWidth]
        public String AadharNo { get; set; }
        [Category("PassportDetails")]
        [OneThirdWidth]
        public String PassportNumber { get; set; }
        [OneThirdWidth]
        public String FirstName { get; set; }
        [OneThirdWidth]
        public String LastName { get; set; }
        [OneThirdWidth]
        public DateTime ExpiryDate { get; set; }
        [DisplayName("Attachments")]
        public String FileAttachments { get; set; }
    }
}