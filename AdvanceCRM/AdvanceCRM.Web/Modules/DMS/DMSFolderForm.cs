
namespace AdvanceCRM.DMS.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("DMS.Folder")]
    [BasedOnRow(typeof(DMSRow), CheckNames = true)]
    public class DMSFolderForm
    {
        public Int32? ParentId { get; set; }
        public String Title { get; set; }
        [HalfWidth(UntilNext = true)]
        [ReadOnly(true)]
        public Int32 OwnerId { get; set; }
        [DefaultValue("now"), ReadOnly(true)]
        public DateTime CreateDate { get; set; }
    }
}