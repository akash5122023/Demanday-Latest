namespace AdvanceCRM.Enquiry {
    export interface EnquiryListRequest extends Serenity.ListRequest {
        ProductsId?: number;
        AreaId?: number;
        DivisionId?: number;
    }
}
