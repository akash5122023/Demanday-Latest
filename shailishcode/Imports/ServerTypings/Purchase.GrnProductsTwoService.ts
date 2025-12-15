namespace AdvanceCRM.Purchase {
    export namespace GrnProductsTwoService {
        export const baseUrl = 'Purchase/GrnProductsTwo';

        export declare function Create(request: Serenity.SaveRequest<GrnProductsTwoRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<GrnProductsTwoRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<GrnProductsTwoRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<GrnProductsTwoRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Purchase/GrnProductsTwo/Create",
            Update = "Purchase/GrnProductsTwo/Update",
            Delete = "Purchase/GrnProductsTwo/Delete",
            Retrieve = "Purchase/GrnProductsTwo/Retrieve",
            List = "Purchase/GrnProductsTwo/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>GrnProductsTwoService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
