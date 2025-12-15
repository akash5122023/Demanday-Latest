namespace AdvanceCRM.ThirdParty.Forms
{
    using Serenity.ComponentModel;
  using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using AdvanceCRM.Administration;

    [FormScript("ThirdParty.IndiaMartBulk")]
    public class IndiaMartBulkForm
    {
        [LookupEditor(typeof(UserRow), Multiple = true), DisplayName("Representatives")]
        public List<int> UIds { get; set; }
        
       
    }
}