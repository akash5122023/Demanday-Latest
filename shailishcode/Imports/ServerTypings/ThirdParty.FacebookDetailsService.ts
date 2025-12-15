namespace AdvanceCRM.ThirdParty {
    export namespace FacebookDetailsService {
        export const baseUrl = 'ThirdParty/FacebookDetails';

        export declare function Create(request: Serenity.SaveRequest<FacebookDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<FacebookDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<FacebookDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<FacebookDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/FacebookDetails/Create",
            Update = "ThirdParty/FacebookDetails/Update",
            Delete = "ThirdParty/FacebookDetails/Delete",
            Retrieve = "ThirdParty/FacebookDetails/Retrieve",
            List = "ThirdParty/FacebookDetails/List",
            MoveToEnquiry = "ThirdParty/FacebookDetails/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/FacebookDetails/BulkMoveToEnquiry",
            Sync = "ThirdParty/FacebookDetails/Sync"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToEnquiry', 
            'BulkMoveToEnquiry', 
            'Sync'
        ].forEach(x => {
            (<any>FacebookDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
