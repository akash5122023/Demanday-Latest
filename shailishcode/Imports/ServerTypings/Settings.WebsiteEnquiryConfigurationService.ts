namespace AdvanceCRM.Settings {
    export namespace WebsiteEnquiryConfigurationService {
        export const baseUrl = 'Settings/WebsiteEnquiryConfiguration';

        export declare function Create(request: Serenity.SaveRequest<WebsiteEnquiryConfigurationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<WebsiteEnquiryConfigurationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WebsiteEnquiryConfigurationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WebsiteEnquiryConfigurationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Settings/WebsiteEnquiryConfiguration/Create",
            Update = "Settings/WebsiteEnquiryConfiguration/Update",
            Delete = "Settings/WebsiteEnquiryConfiguration/Delete",
            Retrieve = "Settings/WebsiteEnquiryConfiguration/Retrieve",
            List = "Settings/WebsiteEnquiryConfiguration/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>WebsiteEnquiryConfigurationService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
