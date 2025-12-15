
namespace AdvanceCRM.Itinerary
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;
    using System.IO;

    [ConnectionKey("Default"), Module("Itinerary"), TableName("[dbo].[ItineraryTerm]")]
    [DisplayName("Itinerary Term"), InstanceName("Itinerary Term")]
    [ReadPermission("Itinerary:Read")]
    [ModifyPermission("Itinerary:Read")]
    public sealed class ItineraryTermRow : Row<ItineraryTermRow.RowFields>, IIdRow
    {
        [DisplayName("Id"), Identity,IdProperty]
        public Int32? Id
        {
            get { return Fields.Id[this]; }
            set { Fields.Id[this] = value; }
        }

        [DisplayName("Days"), ForeignKey("[dbo].[Days]", "Id"), LeftJoin("jDays"), TextualField("DaysTitle")]
        public Int32? DaysId
        {
            get { return Fields.DaysId[this]; }
            set { Fields.DaysId[this] = value; }
        }

        [DisplayName("Itinerary"), ForeignKey("[dbo].[Itinerary]", "Id"), LeftJoin("jItinerary"), TextualField("ItineraryHeadline")]
        public Int32? ItineraryId
        {
            get { return Fields.ItineraryId[this]; }
            set { Fields.ItineraryId[this] = value; }
        }

        [DisplayName("Days Title"), Expression("jDays.[Title]")]
        public String DaysTitle
        {
            get { return Fields.DaysTitle[this]; }
            set { Fields.DaysTitle[this] = value; }
        }

        [DisplayName("Days Heading"), Expression("jDays.[Heading]")]
        public String DaysHeading
        {
            get { return Fields.DaysHeading[this]; }
            set { Fields.DaysHeading[this] = value; }
        }

        [DisplayName("Days Description"), Expression("jDays.[Description]")]
        public String DaysDescription
        {
            get { return Fields.DaysDescription[this]; }
            set { Fields.DaysDescription[this] = value; }
        }

        [DisplayName("Days File Attachments"), Expression("jDays.[FileAttachments]")]
        public String DaysFileAttachments
        {
            get { return Fields.DaysFileAttachments[this]; }
            set { Fields.DaysFileAttachments[this] = value; }
        }

        [DisplayName("Itinerary Headline"), Expression("jItinerary.[Headline]")]
        public String ItineraryHeadline
        {
            get { return Fields.ItineraryHeadline[this]; }
            set { Fields.ItineraryHeadline[this] = value; }
        }

        [DisplayName("Itinerary Date"), Expression("jItinerary.[Date]")]
        public DateTime? ItineraryDate
        {
            get { return Fields.ItineraryDate[this]; }
            set { Fields.ItineraryDate[this] = value; }
        }

        [DisplayName("Itinerary Days Id"), Expression("jItinerary.[DaysId]")]
        public Int32? ItineraryDaysId
        {
            get { return Fields.ItineraryDaysId[this]; }
            set { Fields.ItineraryDaysId[this] = value; }
        }

        [DisplayName("Itinerary From"), Expression("jItinerary.[From]")]
        public String ItineraryFrom
        {
            get { return Fields.ItineraryFrom[this]; }
            set { Fields.ItineraryFrom[this] = value; }
        }

        [DisplayName("Itinerary To"), Expression("jItinerary.[To]")]
        public String ItineraryTo
        {
            get { return Fields.ItineraryTo[this]; }
            set { Fields.ItineraryTo[this] = value; }
        }

        [DisplayName("Itinerary Adults"), Expression("jItinerary.[Adults]")]
        public String ItineraryAdults
        {
            get { return Fields.ItineraryAdults[this]; }
            set { Fields.ItineraryAdults[this] = value; }
        }

        [DisplayName("Itinerary Childrens"), Expression("jItinerary.[Childrens]")]
        public String ItineraryChildrens
        {
            get { return Fields.ItineraryChildrens[this]; }
            set { Fields.ItineraryChildrens[this] = value; }
        }

        [DisplayName("Itinerary Destination"), Expression("jItinerary.[Destination]")]
        public String ItineraryDestination
        {
            get { return Fields.ItineraryDestination[this]; }
            set { Fields.ItineraryDestination[this] = value; }
        }

        [DisplayName("Itinerary Nights"), Expression("jItinerary.[Nights]")]
        public String ItineraryNights
        {
            get { return Fields.ItineraryNights[this]; }
            set { Fields.ItineraryNights[this] = value; }
        }

        [DisplayName("Itinerary Hotel Name"), Expression("jItinerary.[HotelName]")]
        public String ItineraryHotelName
        {
            get { return Fields.ItineraryHotelName[this]; }
            set { Fields.ItineraryHotelName[this] = value; }
        }

        [DisplayName("Itinerary Meal Plan"), Expression("jItinerary.[MealPlan]")]
        public String ItineraryMealPlan
        {
            get { return Fields.ItineraryMealPlan[this]; }
            set { Fields.ItineraryMealPlan[this] = value; }
        }

        [DisplayName("Itinerary Amount"), Expression("jItinerary.[Amount]")]
        public Decimal? ItineraryAmount
        {
            get { return Fields.ItineraryAmount[this]; }
            set { Fields.ItineraryAmount[this] = value; }
        }

        [DisplayName("Itinerary Terms And Conditions"), Expression("jItinerary.[TermsAndConditions]")]
        public String ItineraryTermsAndConditions
        {
            get { return Fields.ItineraryTermsAndConditions[this]; }
            set { Fields.ItineraryTermsAndConditions[this] = value; }
        }

       

        public ItineraryTermRow()
            : base(Fields)
        {
        }
        
        public ItineraryTermRow(RowFields fields)
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field Id;
            public Int32Field DaysId;
            public Int32Field ItineraryId;

            public StringField DaysTitle;
            public StringField DaysHeading;
            public StringField DaysDescription;
            public StringField DaysFileAttachments;

            public StringField ItineraryHeadline;
            public DateTimeField ItineraryDate;
            public Int32Field ItineraryDaysId;
            public StringField ItineraryFrom;
            public StringField ItineraryTo;
            public StringField ItineraryAdults;
            public StringField ItineraryChildrens;
            public StringField ItineraryDestination;
            public StringField ItineraryNights;
            public StringField ItineraryHotelName;
            public StringField ItineraryMealPlan;
            public DecimalField ItineraryAmount;
            public StringField ItineraryTermsAndConditions;
        }
    }
}
