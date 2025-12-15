namespace AdvanceCRM.BizMail {
    export namespace BmListService {
        export const baseUrl = 'BizMail/BmList';

        export declare function Create(request: Serenity.SaveRequest<BmListRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<BmListRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<BmListRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<BmListRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function PostData(request: Serenity.ServiceRequest, onSuccess?: (response: System.Threading.Tasks.Task) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "BizMail/BmList/Create",
            Update = "BizMail/BmList/Update",
            Delete = "BizMail/BmList/Delete",
            Retrieve = "BizMail/BmList/Retrieve",
            List = "BizMail/BmList/List",
            PostData = "BizMail/BmList/PostData",
            Sync = "BizMail/BmList/Sync"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'PostData', 
            'Sync'
        ].forEach(x => {
            (<any>BmListService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
