namespace AdvanceCRM.ThirdParty {
    export namespace InteraktUserService {
        export const baseUrl = 'ThirdParty/InteraktUser';

        export declare function Create(request: Serenity.SaveRequest<InteraktUserRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<InteraktUserRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<InteraktUserRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<InteraktUserRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/InteraktUser/Create",
            Update = "ThirdParty/InteraktUser/Update",
            Delete = "ThirdParty/InteraktUser/Delete",
            Retrieve = "ThirdParty/InteraktUser/Retrieve",
            List = "ThirdParty/InteraktUser/List",
            MoveToEnquiry = "ThirdParty/InteraktUser/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/InteraktUser/BulkMoveToEnquiry",
            Sync = "ThirdParty/InteraktUser/Sync"
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
            (<any>InteraktUserService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
