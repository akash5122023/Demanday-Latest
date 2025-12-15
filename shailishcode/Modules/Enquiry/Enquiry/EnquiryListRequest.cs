using Serenity.Services;

namespace AdvanceCRM.Enquiry
{
    public class EnquiryListRequest : ListRequest
    {
        public int? ProductsId { get; set; }
        public int? AreaId { get; set; }
        public int? DivisionId { get; set; }
    }
}