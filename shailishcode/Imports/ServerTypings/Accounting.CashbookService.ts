namespace AdvanceCRM.Accounting {
    export namespace CashbookService {
        export const baseUrl = 'Accounting/Cashbook';

        export declare function Create(request: Serenity.SaveRequest<CashbookRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<CashbookRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CashbookRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CashbookRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetOutstandingBalance(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetOutstandingCreditBalance(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Approve(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Accounting/Cashbook/Create",
            Update = "Accounting/Cashbook/Update",
            Delete = "Accounting/Cashbook/Delete",
            Retrieve = "Accounting/Cashbook/Retrieve",
            List = "Accounting/Cashbook/List",
            GetOutstandingBalance = "Accounting/Cashbook/GetOutstandingBalance",
            GetOutstandingCreditBalance = "Accounting/Cashbook/GetOutstandingCreditBalance",
            Approve = "Accounting/Cashbook/Approve"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'GetOutstandingBalance', 
            'GetOutstandingCreditBalance', 
            'Approve'
        ].forEach(x => {
            (<any>CashbookService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
