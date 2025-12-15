namespace AdvanceCRM.ThirdParty {
    export namespace MailInboxDetailsService {
        export const baseUrl = 'ThirdParty/MailInboxDetails';

        export declare function Create(request: Serenity.SaveRequest<MailInboxDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<MailInboxDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<MailInboxDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<MailInboxDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToTicket(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/MailInboxDetails/Create",
            Update = "ThirdParty/MailInboxDetails/Update",
            Delete = "ThirdParty/MailInboxDetails/Delete",
            Retrieve = "ThirdParty/MailInboxDetails/Retrieve",
            List = "ThirdParty/MailInboxDetails/List",
            Sync = "ThirdParty/MailInboxDetails/Sync",
            MoveToTicket = "ThirdParty/MailInboxDetails/MoveToTicket"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Sync', 
            'MoveToTicket'
        ].forEach(x => {
            (<any>MailInboxDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
