namespace AdvanceCRM.ThirdParty {
    export namespace TradeIndiaDetailsService {
        export const baseUrl = 'ThirdParty/TradeIndiaDetails';

        export declare function Create(request: Serenity.SaveRequest<TradeIndiaDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<TradeIndiaDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<TradeIndiaDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<TradeIndiaDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/TradeIndiaDetails/Create",
            Update = "ThirdParty/TradeIndiaDetails/Update",
            Delete = "ThirdParty/TradeIndiaDetails/Delete",
            Retrieve = "ThirdParty/TradeIndiaDetails/Retrieve",
            List = "ThirdParty/TradeIndiaDetails/List",
            Sync = "ThirdParty/TradeIndiaDetails/Sync",
            MoveToEnquiry = "ThirdParty/TradeIndiaDetails/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/TradeIndiaDetails/BulkMoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Sync', 
            'MoveToEnquiry', 
            'BulkMoveToEnquiry'
        ].forEach(x => {
            (<any>TradeIndiaDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
