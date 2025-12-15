
namespace AdvanceCRM.Common.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Common.NotificationUsers")]
    [BasedOnRow(typeof(NotificationUsersRow), CheckNames = true)]
    public class NotificationUsersColumns
    {
        [QuickFilter]
        public Int32 NotificationsModule { get; set; }
        [DateTimeEditor, QuickFilter]
        public DateTime NotificationsInsertDate { get; set; }
        public String NotificationsText { get; set; }
        public String InsertUser { get; set; }
        [Hidden]
        public String NotificationsUrl { get; set; }
    }
}