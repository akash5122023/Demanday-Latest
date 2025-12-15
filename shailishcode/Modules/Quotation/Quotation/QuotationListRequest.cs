using Serenity.Services;

namespace AdvanceCRM.Quotation
{
    public class QuotationListRequest : ListRequest
    {
        public int? ProductsId { get; set; }
        public int? AreaId { get; set; }
        public int? DivisionId { get; set; }
    }
}