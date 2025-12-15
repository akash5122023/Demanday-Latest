
namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using AdvanceCRM.Modules.ThirdParty.KnowlarityDetails;

    [ColumnsScript("ThirdParty.KnowlarityDetails")]
    [BasedOnRow(typeof(KnowlarityDetailsRow), CheckNames = true)]
    public class KnowlarityDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Name { get; set; }
        [QuickFilter, EditLink]
        public String CustomerNumber { get; set; }
        [QuickFilter]
        public String EmployeeName { get; set; }
        [Hidden]
        public Int32 CompanyType { get; set; }
        public String Email { get; set; }
        [QuickFilter, EditLink]
        public String EmployeeNumber { get; set; }
        public String Duration { get; set; }

        [FilterOnly, QuickFilter]
        public IvrCallDuration CallDurationState { get; set; }
        public String Recording { get; set; }
        [QuickFilter, DateTimeEditor, DateTimeFormatter(DisplayFormat = "dd/MM/yyyy HH:mm")]
        public DateTime DateTime { get; set; }
        [Hidden]
        public String Cmiuid { get; set; }
        [Hidden]
        public String BilledSec { get; set; }
        [Hidden]
        public String Rate { get; set; }
        public String Record { get; set; }
        [QuickFilter,DisplayName("From/IVRNo")]
        public String From { get; set; }
        public String To { get; set; }
        public String Type { get; set; }
        [QuickFilter]
        public Boolean IsMoved { get; set; }
    }
}