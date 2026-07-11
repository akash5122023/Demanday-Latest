namespace AdvanceCRM.EBBCheck {
    export namespace EBBCheckService {
        export const baseUrl = 'EBBCheck/EBBCheck';

        export declare function Create(request: Serenity.SaveRequest<EBBCheckRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<EBBCheckRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<EBBCheckRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<EBBCheckRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "EBBCheck/EBBCheck/Create",
            Update = "EBBCheck/EBBCheck/Update",
            Delete = "EBBCheck/EBBCheck/Delete",
            Retrieve = "EBBCheck/EBBCheck/Retrieve",
            List = "EBBCheck/EBBCheck/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>EBBCheckService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
