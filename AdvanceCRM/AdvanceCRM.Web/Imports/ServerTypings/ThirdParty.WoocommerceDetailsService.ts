namespace AdvanceCRM.ThirdParty {
    export namespace WoocommerceDetailsService {
        export const baseUrl = 'ThirdParty/WoocommerceDetails';

        export declare function Create(request: Serenity.SaveRequest<WoocommerceDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<WoocommerceDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WoocommerceDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WoocommerceDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/WoocommerceDetails/Create",
            Update = "ThirdParty/WoocommerceDetails/Update",
            Delete = "ThirdParty/WoocommerceDetails/Delete",
            Retrieve = "ThirdParty/WoocommerceDetails/Retrieve",
            List = "ThirdParty/WoocommerceDetails/List",
            MoveToEnquiry = "ThirdParty/WoocommerceDetails/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/WoocommerceDetails/BulkMoveToEnquiry",
            Sync = "ThirdParty/WoocommerceDetails/Sync"
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
            (<any>WoocommerceDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
