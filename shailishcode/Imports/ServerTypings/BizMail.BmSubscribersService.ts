namespace AdvanceCRM.BizMail {
    export namespace BmSubscribersService {
        export const baseUrl = 'BizMail/BmSubscribers';

        export declare function Create(request: Serenity.SaveRequest<BmSubscribersRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<BmSubscribersRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<BmSubscribersRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<BmSubscribersRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "BizMail/BmSubscribers/Create",
            Update = "BizMail/BmSubscribers/Update",
            Delete = "BizMail/BmSubscribers/Delete",
            Retrieve = "BizMail/BmSubscribers/Retrieve",
            List = "BizMail/BmSubscribers/List",
            MoveToEnquiry = "BizMail/BmSubscribers/MoveToEnquiry",
            Sync = "BizMail/BmSubscribers/Sync"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToEnquiry', 
            'Sync'
        ].forEach(x => {
            (<any>BmSubscribersService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
