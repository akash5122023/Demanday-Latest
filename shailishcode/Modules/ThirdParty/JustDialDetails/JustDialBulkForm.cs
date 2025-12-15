namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity.ComponentModel;
  using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using AdvanceCRM.Administration;

    [FormScript("ThirdParty.JustDialBulk")]
    public class JustDialBulkForm
    {
        [LookupEditor(typeof(UserRow), Multiple = true), DisplayName("Representatives")]
        public List<int> UIds { get; set; }
        //[DisplayName("To"), Required]
        //[Administration.UserEditor]
        //public Int32? To { get; set; }
       
    }
}