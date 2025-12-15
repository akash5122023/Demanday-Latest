
namespace AdvanceCRM.Feedback.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Feedback.FeedbackDetails")]
    [BasedOnRow(typeof(FeedbackDetailsRow), CheckNames = true)]
    public class FeedbackDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Name { get; set; }
        public String Phone { get; set; }
        public Int32 Service { get; set; }
        public Boolean Refer { get; set; }
        public String Details { get; set; }
    }
}