namespace AdvanceCRM.Purchase {
    export namespace PurchaseService {
        export const baseUrl = 'Purchase/Purchase';

        export declare function Create(request: Serenity.SaveRequest<PurchaseRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<PurchaseRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<PurchaseRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<PurchaseRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Approve(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetNextNumber(request: GetNextNumberRequest, onSuccess?: (response: GetNextNumberResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Purchase/Purchase/Create",
            Update = "Purchase/Purchase/Update",
            Delete = "Purchase/Purchase/Delete",
            Retrieve = "Purchase/Purchase/Retrieve",
            List = "Purchase/Purchase/List",
            Approve = "Purchase/Purchase/Approve",
            GetNextNumber = "Purchase/Purchase/GetNextNumber"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Approve', 
            'GetNextNumber'
        ].forEach(x => {
            (<any>PurchaseService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
