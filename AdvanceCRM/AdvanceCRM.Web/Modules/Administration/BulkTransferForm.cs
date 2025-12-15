using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvanceCRM.Common.Forms
{
    [FormScript("Common.BulkTransfer")]
    public class BulkTransferForm
    {
        [DisplayName("From"), Required]
        [Administration.UserEditor]
        public Int32? From { get; set; }
        [DisplayName("To"), Required]
        [Administration.UserEditor]
        public Int32? To { get; set; }
        [DisplayName("Module"), Required]
        public Masters.ModulesTypeMaster? Module { get; set; }
    }
}