namespace AdvanceCRM.Demanday {
    export namespace DemandayTeleMarketingEnquiryQADetailsService {
        export const baseUrl = 'Demanday/DemandayTeleMarketingEnquiryQADetails';

        export declare function Create(request: Serenity.SaveRequest<DemandayTeleMarketingEnquiryQADetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayTeleMarketingEnquiryQADetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayTeleMarketingEnquiryQADetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayTeleMarketingEnquiryQADetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Demanday/DemandayTeleMarketingEnquiryQADetails/Create",
            Update = "Demanday/DemandayTeleMarketingEnquiryQADetails/Update",
            Delete = "Demanday/DemandayTeleMarketingEnquiryQADetails/Delete",
            Retrieve = "Demanday/DemandayTeleMarketingEnquiryQADetails/Retrieve",
            List = "Demanday/DemandayTeleMarketingEnquiryQADetails/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>DemandayTeleMarketingEnquiryQADetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
