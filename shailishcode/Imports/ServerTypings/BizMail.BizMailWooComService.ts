namespace AdvanceCRM.BizMail {
    export namespace BizMailWooComService {
        export const baseUrl = 'BizMail/BizMailWooCom';

        export declare function Create(request: Serenity.SaveRequest<BizMailWooComRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<BizMailWooComRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<BizMailWooComRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<BizMailWooComRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "BizMail/BizMailWooCom/Create",
            Update = "BizMail/BizMailWooCom/Update",
            Delete = "BizMail/BizMailWooCom/Delete",
            Retrieve = "BizMail/BizMailWooCom/Retrieve",
            List = "BizMail/BizMailWooCom/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>BizMailWooComService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
