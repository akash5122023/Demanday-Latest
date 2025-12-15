namespace AdvanceCRM.Common.Calendar
{
    using System.Collections.Generic;
    using AdvanceCRM.Contacts;
    using AdvanceCRM.Enquiry;
    using AdvanceCRM.Quotation;
    using AdvanceCRM.Sales;
    using AdvanceCRM.Services;

    public class CalendarModel
    {
        public List<EnquiryAppointmentsRow> Enquiry { get; set; }
        public List<QuotationAppointmentsRow> Quotation { get; set; }
        public List<InvoiceAppointmentsRow> Proforma { get; set; }
        public List<TeleCallingAppointmentsRow> TeleCalling { get; set; }
        public List<AMCVisitPlannerRow> AMC { get; set; }
        public List<ContactsRow> Contacts { get; set; }
        public List<string> Users { get; set; }
    }
}