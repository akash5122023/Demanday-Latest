namespace AdvanceCRM.Sales {
    export namespace SalesFollowupsService {
        export const baseUrl = 'Sales/SalesFollowups';

        export declare function Create(request: Serenity.SaveRequest<SalesFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<SalesFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SalesFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SalesFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMSReminder(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Sales/SalesFollowups/Create",
            Update = "Sales/SalesFollowups/Update",
            Delete = "Sales/SalesFollowups/Delete",
            Retrieve = "Sales/SalesFollowups/Retrieve",
            List = "Sales/SalesFollowups/List",
            SendSMSReminder = "Sales/SalesFollowups/SendSMSReminder"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'SendSMSReminder'
        ].forEach(x => {
            (<any>SalesFollowupsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
