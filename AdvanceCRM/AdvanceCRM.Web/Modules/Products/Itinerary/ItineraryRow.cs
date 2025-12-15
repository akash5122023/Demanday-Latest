
namespace AdvanceCRM.Products
{
    using AdvanceCRM.Itinerary;
    using AdvanceCRM.Masters;
    using AdvanceCRM.Scripts;
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Products"), TableName("[dbo].[Itinerary]")]
    [DisplayName("Itinerary"), InstanceName("Itinerary")]
    [ReadPermission("Itinerary:Read")]
    [ModifyPermission("Itinerary:Modify")]
    [LookupScript("Products.Itinerary", Permission = "?" )]

    public sealed class ItineraryRow : Row<ItineraryRow.RowFields>, IIdRow, INameRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Headline"), Size(50), QuickSearch,NameProperty]
        public String Headline
        {
            get { return Fields.Headline[this]; }
            set { Fields.Headline[this] = value; }
        }

        [DisplayName("Date")]
        public DateTime? Date
        {
            get { return Fields.Date[this]; }
            set { Fields.Date[this] = value; }
        }

        //[DisplayName("Days"), ForeignKey("[dbo].[Days]", "Id"), LeftJoin("jDays"), TextualField("DaysTitle")]
        //[LookupEditor(typeof(QuotationTermsMasterRow), Multiple = true, InplaceAdd = true), NotMapped]
        ////[LinkingSetRelation(typeof(QuotationTermsRow), "QuotationId", "TermsId")]
        //public Int32? DaysId
        //{
        //    get { return Fields.DaysId[this]; }
        //    set { Fields.DaysId[this] = value; }
        //}
        //[DisplayName("Terms")]
        //[LookupEditor(typeof(QuotationTermsMasterRow), Multiple = true, InplaceAdd = true), NotMapped]
        //[LinkingSetRelation(typeof(QuotationTermsRow), "QuotationId", "TermsId")]
        //public List<Int32> TermsList
        //{
        //    get { return Fields.TermsList[this]; }
        //    set { Fields.TermsList[this] = value; }
        //}


        [DisplayName("Days")]
        [LookupEditor(typeof(DaysRow), Multiple = true, InplaceAdd = true), NotMapped]
        [LinkingSetRelation(typeof(ItineraryTermRow), "ItineraryId", "DaysId")]
        public List<Int32> DaysId
        {
            get { return Fields.DaysId[this]; }
            set { Fields.DaysId[this] = value; }
        }
        [DisplayName("From"), Size(100)]
        public String From
        {
            get { return Fields.From[this]; }
            set { Fields.From[this] = value; }
        }

        [DisplayName("To"), Size(100)]
        public String To
        {
            get { return Fields.To[this]; }
            set { Fields.To[this] = value; }
        }

        [DisplayName("Adults"), Size(50)]
        public String Adults
        {
            get { return Fields.Adults[this]; }
            set { Fields.Adults[this] = value; }
        }

        [DisplayName("Childrens"), Size(50)]
        public String Childrens
        {
            get { return Fields.Childrens[this]; }
            set { Fields.Childrens[this] = value; }
        }

        [DisplayName("Destination"), Size(100)]
        public String Destination
        {
            get { return Fields.Destination[this]; }
            set { Fields.Destination[this] = value; }
        }

        [DisplayName("Nights"), Size(50)]
        public String Nights
        {
            get { return Fields.Nights[this]; }
            set { Fields.Nights[this] = value; }
        }

        [DisplayName("Hotel Name"), Size(100)]
        public String HotelName
        {
            get { return Fields.HotelName[this]; }
            set { Fields.HotelName[this] = value; }
        }

        [DisplayName("Meal Plan"), Size(100)]
        public String MealPlan
        {
            get { return Fields.MealPlan[this]; }
            set { Fields.MealPlan[this] = value; }
        }

        [DisplayName("Amount"), Size(19), Scale(5)]
        public Double? Amount
        {
            get { return Fields.Amount[this]; }
            set { Fields.Amount[this] = value; }
        }

        [DisplayName("Terms And Conditions"), Size(1000)]
        public String TermsAndConditions
        {
            get { return Fields.TermsAndConditions[this]; }
            set { Fields.TermsAndConditions[this] = value; }
        }

        //[DisplayName("Days Title"), Expression("jDays.[Title]")]
        //public String DaysTitle
        //{
        //    get { return Fields.DaysTitle[this]; }
        //    set { Fields.DaysTitle[this] = value; }
        //}

        //[DisplayName("Days Heading"), Expression("jDays.[Heading]")]
        //public String DaysHeading
        //{
        //    get { return Fields.DaysHeading[this]; }
        //    set { Fields.DaysHeading[this] = value; }
        //}

        //[DisplayName("Days Description"), Expression("jDays.[Description]")]
        //public String DaysDescription
        //{
        //    get { return Fields.DaysDescription[this]; }
        //    set { Fields.DaysDescription[this] = value; }
        //}

        //[DisplayName("Days File Attachments"), Expression("jDays.[FileAttachments]")]
        //public String DaysFileAttachments
        //{
        //    get { return Fields.DaysFileAttachments[this]; }
        //    set { Fields.DaysFileAttachments[this] = value; }
        //}

      

        public object ProjectId { get; internal set; }
        public object AssignedId { get; internal set; }

      
        public ItineraryRow()
            : base(Fields)
        {
        }
        public ItineraryRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public StringField Headline;
            public DateTimeField Date;
            public readonly ListField<Int32> DaysId;
            public StringField From;
            public StringField To;
            public StringField Adults;
            public StringField Childrens;
            public StringField Destination;
            public StringField Nights;
            public StringField HotelName;
            public StringField MealPlan;
            public DoubleField Amount;
            public StringField TermsAndConditions;

            //public StringField DaysTitle;
            //public StringField DaysHeading;
            //public StringField DaysDescription;
            //public StringField DaysFileAttachments;

            
        }
    }
}
