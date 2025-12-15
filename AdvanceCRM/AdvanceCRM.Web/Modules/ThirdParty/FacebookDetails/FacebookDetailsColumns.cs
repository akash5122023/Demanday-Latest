

namespace AdvanceCRM.ThirdParty.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;
    using Serenity.Data.Mapping;

    [ColumnsScript("ThirdParty.FacebookDetails")]
    [BasedOnRow(typeof(FacebookDetailsRow), CheckNames = true)]
    public class FacebookDetailsColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink, QuickFilter, QuickSearch]
        public String Name { get; set; }
        [ QuickSearch]
        public String Phone { get; set; }
        public String Email { get; set; }
        [ QuickSearch]
        public String CompaignName { get; set; }
        [ QuickSearch]
        public String AdSetName { get; set; }
        public String Address { get; set; }
        [QuickFilter, DateTimeEditor, DateTimeFormatter(DisplayFormat = "dd/MM/yyyy HH:mm")]
        public DateTime CreatedTime { get; set; }      
        public String Company { get; set; }
        [ QuickSearch]
        public String AdId { get; set; }
        [QuickSearch]
        public String AdName { get; set; }
        [QuickSearch]
        public String AdSetId { get; set; }
        public String AdditionalDetails { get; set; }
        public String Feedback { get; set; }
        [QuickFilter]
        public Boolean IsMoved { get; set; }
    }
}