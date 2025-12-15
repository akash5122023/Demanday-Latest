namespace AdvanceCRM.Accounting {
    export namespace ExpenseManagementService {
        export const baseUrl = 'Accounting/ExpenseManagement';

        export declare function Create(request: Serenity.SaveRequest<ExpenseManagementRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ExpenseManagementRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ExpenseManagementRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ExpenseManagementRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Approve(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Accounting/ExpenseManagement/Create",
            Update = "Accounting/ExpenseManagement/Update",
            Delete = "Accounting/ExpenseManagement/Delete",
            Retrieve = "Accounting/ExpenseManagement/Retrieve",
            List = "Accounting/ExpenseManagement/List",
            Approve = "Accounting/ExpenseManagement/Approve"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Approve'
        ].forEach(x => {
            (<any>ExpenseManagementService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
