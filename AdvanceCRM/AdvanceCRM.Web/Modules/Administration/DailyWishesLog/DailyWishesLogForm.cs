
namespace AdvanceCRM.Administration.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Administration.DailyWishesLog")]
    [BasedOnRow(typeof(DailyWishesLogRow), CheckNames = true)]
    public class DailyWishesLogForm
    {
        public DateTime Date { get; set; }
        public String Log { get; set; }
    }
}