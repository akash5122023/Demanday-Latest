namespace AdvanceCRM.Services {
    export namespace CMSFollowupsService {
        export const baseUrl = 'Services/CMSFollowups';

        export declare function Create(request: Serenity.SaveRequest<CMSFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<CMSFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CMSFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CMSFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMSReminder(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Services/CMSFollowups/Create",
            Update = "Services/CMSFollowups/Update",
            Delete = "Services/CMSFollowups/Delete",
            Retrieve = "Services/CMSFollowups/Retrieve",
            List = "Services/CMSFollowups/List",
            SendSMSReminder = "Services/CMSFollowups/SendSMSReminder"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'SendSMSReminder'
        ].forEach(x => {
            (<any>CMSFollowupsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
