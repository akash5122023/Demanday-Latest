namespace AdvanceCRM.Services {
    export namespace TeleCallingFollowupsService {
        export const baseUrl = 'Services/TeleCallingFollowups';

        export declare function Create(request: Serenity.SaveRequest<TeleCallingFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<TeleCallingFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<TeleCallingFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<TeleCallingFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMSReminder(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Services/TeleCallingFollowups/Create",
            Update = "Services/TeleCallingFollowups/Update",
            Delete = "Services/TeleCallingFollowups/Delete",
            Retrieve = "Services/TeleCallingFollowups/Retrieve",
            List = "Services/TeleCallingFollowups/List",
            SendSMSReminder = "Services/TeleCallingFollowups/SendSMSReminder"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'SendSMSReminder'
        ].forEach(x => {
            (<any>TeleCallingFollowupsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
