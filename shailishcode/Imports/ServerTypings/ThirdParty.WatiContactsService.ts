namespace AdvanceCRM.ThirdParty {
    export namespace WatiContactsService {
        export const baseUrl = 'ThirdParty/WatiContacts';

        export declare function Create(request: Serenity.SaveRequest<WatiContactsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<WatiContactsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WatiContactsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WatiContactsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/WatiContacts/Create",
            Update = "ThirdParty/WatiContacts/Update",
            Delete = "ThirdParty/WatiContacts/Delete",
            Retrieve = "ThirdParty/WatiContacts/Retrieve",
            List = "ThirdParty/WatiContacts/List",
            MoveToEnquiry = "ThirdParty/WatiContacts/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/WatiContacts/BulkMoveToEnquiry",
            Sync = "ThirdParty/WatiContacts/Sync"
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
            (<any>WatiContactsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
