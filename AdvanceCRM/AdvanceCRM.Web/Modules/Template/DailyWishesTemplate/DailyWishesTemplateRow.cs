
namespace AdvanceCRM.Template
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Template"), TableName("[dbo].[DailyWishesTemplate]")]
    [DisplayName("Daily Wishes Template"), InstanceName("Daily Wishes Template")]
    [ReadPermission("Template:DailyWishes")]
    [ModifyPermission("Template:DailyWishes")]
    [LookupScript("Template.DailyWishesTemplateRow", Permission = "?")]
    public sealed class DailyWishesTemplateRow : Row<DailyWishesTemplateRow.RowFields>, IIdRow, INameRow, _Ext.IAuditLog
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }


        [DisplayName("Birth Temp Id"), Column("BirthTempID"), Size(20)]
        public String BirthTempId
        {
            get { return Fields.BirthTempId[this]; }
            set { Fields.BirthTempId[this] = value; }
        }

        [DisplayName("Marriage Temp Id"), Column("MarriageTempID"), Size(20)]
        public String MarriageTempId
        {
            get { return Fields.MarriageTempId[this]; }
            set { Fields.MarriageTempId[this] = value; }
        }

        [DisplayName("Dof Temp Id"), Column("DOFTempID"), Size(20)]
        public String DofTempId
        {
            get { return Fields.DofTempId[this]; }
            set { Fields.DofTempId[this] = value; }
        }

        [DisplayName("From"), Size(500), NotNull, QuickSearch,NameProperty]
        public String From
        {
            get { return Fields.From[this]; }
            set { Fields.From[this] = value; }
        }

        [DisplayName("Subject"), Size(500), NotNull]
        public String Subject
        {
            get { return Fields.Subject[this]; }
            set { Fields.Subject[this] = value; }
        }

        [DisplayName("Birthday SMS"), Column("BirthdaySMS"), Size(500), NotNull, TextAreaEditor(Rows = 4)]
        public String BirthdaySMS
        {
            get { return Fields.BirthdaySMS[this]; }
            set { Fields.BirthdaySMS[this] = value; }
        }

        [DisplayName("Marriage SMS"), Column("MarriageSMS"), Size(500), NotNull, TextAreaEditor(Rows = 4)]
        public String MarriageSMS
        {
            get { return Fields.MarriageSMS[this]; }
            set { Fields.MarriageSMS[this] = value; }
        }

        [DisplayName("Company Anniversary SMS"), Column("DOFAnniversarySMS"), Size(500), NotNull, TextAreaEditor(Rows = 4)]
        public String DofAnniversarySMS
        {
            get { return Fields.DofAnniversarySMS[this]; }
            set { Fields.DofAnniversarySMS[this] = value; }
        }
        [DisplayName("Birthday Email"), NotNull, TextAreaEditor(Rows = 4)]
        public String BirthdayEmail
        {
            get { return Fields.BirthdayEmail[this]; }
            set { Fields.BirthdayEmail[this] = value; }
        }

        [DisplayName("Marriage Email"), NotNull, TextAreaEditor(Rows = 4)]
        public String MarriageEmail
        {
            get { return Fields.MarriageEmail[this]; }
            set { Fields.MarriageEmail[this] = value; }
        }

        [DisplayName("Dof Anniversary Email"), Column("DOFAnniversaryEmail"), NotNull, TextAreaEditor(Rows = 4)]
        public String DofAnniversaryEmail
        {
            get { return Fields.DofAnniversaryEmail[this]; }
            set { Fields.DofAnniversaryEmail[this] = value; }
        }

        //[DisplayName("Birthday Email"), Size(2500), NotNull, TextAreaEditor(Rows = 4)]
        //public String BirthdayEmail
        //{
        //    get { return Fields.BirthdayEmail[this]; }
        //    set { Fields.BirthdayEmail[this] = value; }
        //}

        //[DisplayName("Marriage Email"), Size(2500), NotNull, TextAreaEditor(Rows = 4)]
        //public String MarriageEmail
        //{
        //    get { return Fields.MarriageEmail[this]; }
        //    set { Fields.MarriageEmail[this] = value; }
        //}

        //[DisplayName("Company Anniversary Email"), Column("DOFAnniversaryEmail"), Size(2500), NotNull, TextAreaEditor(Rows = 4)]
        //public String DofAnniversaryEmail
        //{
        //    get { return Fields.DofAnniversaryEmail[this]; }
        //    set { Fields.DofAnniversaryEmail[this] = value; }
        //}

        [DisplayName("Company"), ForeignKey("[dbo].[CompanyDetails]", "Id"), LeftJoin("jCompany"), TextualField("CompanyName"), NotNull, LookupInclude]
        public Int32? CompanyId
        {
            get { return Fields.CompanyId[this]; }
            set { Fields.CompanyId[this] = value; }
        }

        [DisplayName("WhatsApp Birthday Template"), Column("WABirTemplate"), TextAreaEditor(Rows = 4)]
        public String WaBirTemplate
        {
            get { return Fields.WaBirTemplate[this]; }
            set { Fields.WaBirTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Birthday TemplateId"), Column("WABirTemplateId"), Size(100)]
        public String WaBirTemplateId
        {
            get { return Fields.WaBirTemplateId[this]; }
            set { Fields.WaBirTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Marriage Template"), Column("WAMarTemplate"), TextAreaEditor(Rows = 4)]
        public String WaMarTemplate
        {
            get { return Fields.WaMarTemplate[this]; }
            set { Fields.WaMarTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Marriage TemplateId"), Column("WAMarTemplateId"), Size(100)]
        public String WaMarTemplateId
        {
            get { return Fields.WaMarTemplateId[this]; }
            set { Fields.WaMarTemplateId[this] = value; }
        }

        [DisplayName("WhatsApp Anniversary Template"), Column("WAAnnTemplate"), TextAreaEditor(Rows = 4)]
        public String WaAnnTemplate
        {
            get { return Fields.WaAnnTemplate[this]; }
            set { Fields.WaAnnTemplate[this] = value; }
        }

        [DisplayName("WhatsApp Anniversary TemplateId"), Column("WAAnnTemplateId"), Size(100)]
        public String WaAnnTemplateId
        {
            get { return Fields.WaAnnTemplateId[this]; }
            set { Fields.WaAnnTemplateId[this] = value; }
        }
      
        public DailyWishesTemplateRow()
            : base(Fields)
        {
        }
        public DailyWishesTemplateRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField From;
            public StringField Subject;
            public StringField BirthdaySMS;
            public StringField MarriageSMS;
            public StringField DofAnniversarySMS;
            public StringField BirthdayEmail;
            public StringField MarriageEmail;
            public StringField DofAnniversaryEmail;
            public Int32Field CompanyId;
            public StringField BirthTempId;
            public StringField MarriageTempId;
            public StringField DofTempId;

            public StringField WaBirTemplate;
            public StringField WaBirTemplateId;
            public StringField WaMarTemplate;
            public StringField WaMarTemplateId;
            public StringField WaAnnTemplate;
            public StringField WaAnnTemplateId;
        }
    }
}
