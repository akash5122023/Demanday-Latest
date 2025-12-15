namespace AdvanceCRM.Sales {
    export interface SalesListRequest extends Serenity.ListRequest {
        ProductsId?: number;
        AreaId?: number;
        DivisionId?: number;
    }
}
