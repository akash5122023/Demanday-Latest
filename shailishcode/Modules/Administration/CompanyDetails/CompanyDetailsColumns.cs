
namespace AdvanceCRM.Administration.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Administration.CompanyDetails")]
    [BasedOnRow(typeof(CompanyDetailsRow), CheckNames = true)]
    public class CompanyDetailsColumns
    {
        //[EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        //public Int32 Id { get; set; }
        [EditLink, Width(220)]
        public String Name { get; set; }
        public String Slogan { get; set; }
        public String Address { get; set; }
        public String Phone { get; set; }
        public String GSTIN { get; set; }
        public String PANNo { get; set; }
        //public String Logo { get; set; }
        //public Int32 LogoHeight { get; set; }
        //public Int32 LogoWidth { get; set; }
        //public String HeaderImage { get; set; }
        //public Int32 HeaderHeight { get; set; }
        //public Int32 HeaderWidth { get; set; }
        //public String FooterImage { get; set; }
        //public Int32 FooterHeight { get; set; }
        //public Int32 FooterWidth { get; set; }
    }
}