
namespace AdvanceCRM.DMS.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("DMS.DMS")]
    [BasedOnRow(typeof(DMSRow), CheckNames = true)]
    public class DMSForm
    {
        public String Title { get; set; }
        public String Files { get; set; }
        [Category("Information")]
        [HalfWidth(UntilNext = true)]
        [ReadOnly(true)]
        public Int32 OwnerId { get; set; }
        [DefaultValue("now"), ReadOnly(true)]
        public DateTime CreateDate { get; set; }
        [ReadOnly(true)]
        public Int32 LastUpdatedId { get; set; }
        [DefaultValue("now"), ReadOnly(true)]
        public DateTime UpdateDate { get; set; }
        public Int32 AssignedId { get; set; }
    }
}