
namespace AdvanceCRM.Services.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Services.AMC")]
    [BasedOnRow(typeof(AMCRow), CheckNames = true)]
    public class AMCForm
    {
        [Category("General")]
        [OneThirdWidth(UntilNext = true)]
        public Int32 AMCNo { get; set; }
        
        public Int32 ContactsId { get; set; }
        [Hidden]
        public String ContactsName { get; set; }
        [ReadOnly(true)]
        public String ContactsPhone { get; set; }
        [Hidden]
        public String ContactsWhatsapp { get; set; }
        [DefaultValue("now")]
        public DateTime Date { get; set; }
        [DefaultValue("1")]
        public Int32 Status { get; set; }
        [DefaultValue("now")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [DisplayName("Valid Date")]
        public DateTime DueDate { get; set; }
        [DefaultValue(10)]
        public Int32 Lines { get; set; }
        [FullWidth]
        public String AdditionalInfo { get; set; }
        public String Attachment { get; set; }
        [Category("Products")]
        public List<AMCProductsRow> Products { get; set; }
        [DisplayName("Terms")]
        public List<Int32> TermsList { get; set; }
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
        [HalfWidth]
        public Int32 AssignedId { get; set; }
        public List<object> NoteList { get; set; }
        public List<object> Timeline { get; set; }
    }
}