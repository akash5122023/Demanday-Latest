namespace AdvanceCRM.ThirdParty {
    export namespace InstamojoDetailsService {
        export const baseUrl = 'ThirdParty/InstamojoDetails';

        export declare function Create(request: Serenity.SaveRequest<InstamojoDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<InstamojoDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<InstamojoDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<InstamojoDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/InstamojoDetails/Create",
            Update = "ThirdParty/InstamojoDetails/Update",
            Delete = "ThirdParty/InstamojoDetails/Delete",
            Retrieve = "ThirdParty/InstamojoDetails/Retrieve",
            List = "ThirdParty/InstamojoDetails/List",
            Sync = "ThirdParty/InstamojoDetails/Sync",
            MoveToEnquiry = "ThirdParty/InstamojoDetails/MoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Sync', 
            'MoveToEnquiry'
        ].forEach(x => {
            (<any>InstamojoDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
