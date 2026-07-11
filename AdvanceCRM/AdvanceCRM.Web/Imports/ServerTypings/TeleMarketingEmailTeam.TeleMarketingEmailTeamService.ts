namespace AdvanceCRM.TeleMarketingEmailTeam {
    export namespace TeleMarketingEmailTeamService {
        export const baseUrl = 'TeleMarketingEmailTeam/TeleMarketingEmailTeam';

        export declare function Create(request: Serenity.SaveRequest<TeleMarketingEmailTeamRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<TeleMarketingEmailTeamRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<TeleMarketingEmailTeamRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<TeleMarketingEmailTeamRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "TeleMarketingEmailTeam/TeleMarketingEmailTeam/Create",
            Update = "TeleMarketingEmailTeam/TeleMarketingEmailTeam/Update",
            Delete = "TeleMarketingEmailTeam/TeleMarketingEmailTeam/Delete",
            Retrieve = "TeleMarketingEmailTeam/TeleMarketingEmailTeam/Retrieve",
            List = "TeleMarketingEmailTeam/TeleMarketingEmailTeam/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>TeleMarketingEmailTeamService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
