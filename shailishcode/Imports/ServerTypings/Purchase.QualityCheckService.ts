namespace AdvanceCRM.Purchase {
    export namespace QualityCheckService {
        export const baseUrl = 'Purchase/QualityCheck';

        export declare function Create(request: Serenity.SaveRequest<QualityCheckRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<QualityCheckRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<QualityCheckRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<QualityCheckRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToRejectionOutward(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetNextNumber(request: GetNextNumberRequest, onSuccess?: (response: GetNextNumberResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Purchase/QualityCheck/Create",
            Update = "Purchase/QualityCheck/Update",
            Delete = "Purchase/QualityCheck/Delete",
            Retrieve = "Purchase/QualityCheck/Retrieve",
            List = "Purchase/QualityCheck/List",
            MoveToRejectionOutward = "Purchase/QualityCheck/MoveToRejectionOutward",
            GetNextNumber = "Purchase/QualityCheck/GetNextNumber"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToRejectionOutward', 
            'GetNextNumber'
        ].forEach(x => {
            (<any>QualityCheckService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
