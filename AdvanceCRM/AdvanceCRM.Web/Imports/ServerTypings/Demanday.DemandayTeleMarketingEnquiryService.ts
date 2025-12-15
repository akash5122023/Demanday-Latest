namespace AdvanceCRM.Demanday {
    export namespace DemandayTeleMarketingEnquiryService {
        export const baseUrl = 'Demanday/DemandayTeleMarketingEnquiry';

        export declare function Create(request: Serenity.SaveRequest<DemandayTeleMarketingEnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayTeleMarketingEnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayTeleMarketingEnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayTeleMarketingEnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Demanday/DemandayTeleMarketingEnquiry/Create",
            Update = "Demanday/DemandayTeleMarketingEnquiry/Update",
            Delete = "Demanday/DemandayTeleMarketingEnquiry/Delete",
            Retrieve = "Demanday/DemandayTeleMarketingEnquiry/Retrieve",
            List = "Demanday/DemandayTeleMarketingEnquiry/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>DemandayTeleMarketingEnquiryService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
