using Serenity.Services;

namespace AdvanceCRM.Sales
{
    public class SalesListRequest : ListRequest
    {
        public int? ProductsId { get; set; }
        public int? AreaId { get; set; }
        public int? DivisionId { get; set; }
    }
}