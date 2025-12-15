
namespace AdvanceCRM.Contacts.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Contacts.SubContacts")]
    [BasedOnRow(typeof(SubContactsRow), CheckNames = true)]
    public class SubContactsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, SortOrder(1, true)]
        //public Int32 Id { get; set; }
        [EditLink, Width(120)]
        public String Name { get; set; }
        [Width(120)]
        public String Phone { get; set; }
        [Width(120)]
        public String Whatsapp { get; set; }
        [Width(120)]
        public String ResidentialPhone { get; set; }
        [Width(120)]
        public String Email { get; set; }
        [Width(120)]
        public String Designation { get; set; }
        [Width(120)]
        public String Project { get; set; }
        [Width(120)]
        public String Address { get; set; }
        [Width(120)]
        public Int32 Gender { get; set; }
        [Width(120)]
        public Int32 Religion { get; set; }
        [Width(120)]
        public Int32 MaritalStatus { get; set; }
        [Width(120)]
        public DateTime MarriageAnniversary { get; set; }
        [Width(120)]
        public DateTime Birthdate { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public String PANNo { get; set; }
        public String AadharNo { get; set; }
        [DisplayName("Attachments")]
        public String FileAttachments { get; set; }

    }
    //[Width(120)]
    //public DateTime ContactsName { get; set; }
}
