namespace AdvanceCRM.ThirdParty {
    export namespace WcOrderDetailsService {
        export const baseUrl = 'ThirdParty/WcOrderDetails';

        export declare function Create(request: Serenity.SaveRequest<WcOrderDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<WcOrderDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WcOrderDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WcOrderDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/WcOrderDetails/Create",
            Update = "ThirdParty/WcOrderDetails/Update",
            Delete = "ThirdParty/WcOrderDetails/Delete",
            Retrieve = "ThirdParty/WcOrderDetails/Retrieve",
            List = "ThirdParty/WcOrderDetails/List",
            MoveToEnquiry = "ThirdParty/WcOrderDetails/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/WcOrderDetails/BulkMoveToEnquiry",
            Sync = "ThirdParty/WcOrderDetails/Sync"
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
            (<any>WcOrderDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
