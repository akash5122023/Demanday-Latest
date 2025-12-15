namespace AdvanceCRM.ThirdParty {
    export namespace JustDialDetailsService {
        export const baseUrl = 'ThirdParty/JustDialDetails';

        export declare function Create(request: Serenity.SaveRequest<JustDialDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<JustDialDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<JustDialDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<JustDialDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/JustDialDetails/Create",
            Update = "ThirdParty/JustDialDetails/Update",
            Delete = "ThirdParty/JustDialDetails/Delete",
            Retrieve = "ThirdParty/JustDialDetails/Retrieve",
            List = "ThirdParty/JustDialDetails/List",
            MoveToEnquiry = "ThirdParty/JustDialDetails/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/JustDialDetails/BulkMoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToEnquiry', 
            'BulkMoveToEnquiry'
        ].forEach(x => {
            (<any>JustDialDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
