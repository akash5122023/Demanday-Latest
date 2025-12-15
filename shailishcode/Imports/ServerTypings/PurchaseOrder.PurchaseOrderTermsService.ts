namespace AdvanceCRM.PurchaseOrder {
    export namespace PurchaseOrderTermsService {
        export const baseUrl = 'PurchaseOrder/PurchaseOrderTerms';

        export declare function Create(request: Serenity.SaveRequest<PurchaseOrderTermsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<PurchaseOrderTermsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<PurchaseOrderTermsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<PurchaseOrderTermsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "PurchaseOrder/PurchaseOrderTerms/Create",
            Update = "PurchaseOrder/PurchaseOrderTerms/Update",
            Delete = "PurchaseOrder/PurchaseOrderTerms/Delete",
            Retrieve = "PurchaseOrder/PurchaseOrderTerms/Retrieve",
            List = "PurchaseOrder/PurchaseOrderTerms/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>PurchaseOrderTermsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
