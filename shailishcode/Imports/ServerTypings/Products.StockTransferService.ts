namespace AdvanceCRM.Products {
    export namespace StockTransferService {
        export const baseUrl = 'Products/StockTransfer';

        export declare function Create(request: Serenity.SaveRequest<StockTransferRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<StockTransferRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<StockTransferRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<StockTransferRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function CheckReorder(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Products/StockTransfer/Create",
            Update = "Products/StockTransfer/Update",
            Delete = "Products/StockTransfer/Delete",
            Retrieve = "Products/StockTransfer/Retrieve",
            List = "Products/StockTransfer/List",
            CheckReorder = "Products/StockTransfer/CheckReorder"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'CheckReorder'
        ].forEach(x => {
            (<any>StockTransferService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
