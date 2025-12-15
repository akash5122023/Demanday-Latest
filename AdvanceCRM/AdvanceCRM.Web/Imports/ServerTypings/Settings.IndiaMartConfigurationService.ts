namespace AdvanceCRM.Settings {
    export namespace IndiaMartConfigurationService {
        export const baseUrl = 'Settings/IndiaMartConfiguration';

        export declare function Create(request: Serenity.SaveRequest<IndiaMartConfigurationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<IndiaMartConfigurationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<IndiaMartConfigurationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<IndiaMartConfigurationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Settings/IndiaMartConfiguration/Create",
            Update = "Settings/IndiaMartConfiguration/Update",
            Delete = "Settings/IndiaMartConfiguration/Delete",
            Retrieve = "Settings/IndiaMartConfiguration/Retrieve",
            List = "Settings/IndiaMartConfiguration/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>IndiaMartConfigurationService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
