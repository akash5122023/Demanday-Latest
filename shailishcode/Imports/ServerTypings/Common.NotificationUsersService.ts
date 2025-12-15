namespace AdvanceCRM.Common {
    export namespace NotificationUsersService {
        export const baseUrl = 'Common/NotificationUsers';

        export declare function Create(request: Serenity.SaveRequest<NotificationUsersRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<NotificationUsersRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<NotificationUsersRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<NotificationUsersRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ClearNotification(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Common/NotificationUsers/Create",
            Update = "Common/NotificationUsers/Update",
            Delete = "Common/NotificationUsers/Delete",
            Retrieve = "Common/NotificationUsers/Retrieve",
            List = "Common/NotificationUsers/List",
            ClearNotification = "Common/NotificationUsers/ClearNotification"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ClearNotification'
        ].forEach(x => {
            (<any>NotificationUsersService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
