namespace AdvanceCRM.Quotation {
    export interface QuotationListRequest extends Serenity.ListRequest {
        ProductsId?: number;
        AreaId?: number;
        DivisionId?: number;
    }
}
