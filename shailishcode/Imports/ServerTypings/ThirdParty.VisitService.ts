namespace AdvanceCRM.ThirdParty {
    export namespace VisitService {
        export const baseUrl = 'ThirdParty/Visit';

        export declare function Create(request: Serenity.SaveRequest<VisitRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<VisitRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<VisitRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<VisitRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/Visit/Create",
            Update = "ThirdParty/Visit/Update",
            Delete = "ThirdParty/Visit/Delete",
            Retrieve = "ThirdParty/Visit/Retrieve",
            List = "ThirdParty/Visit/List",
            MoveToEnquiry = "ThirdParty/Visit/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/Visit/BulkMoveToEnquiry"
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
            (<any>VisitService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
