namespace AdvanceCRM.Masters {
    export namespace DemandayTeleMarketingEnquiryCampaignQuestionsService {
        export const baseUrl = 'Masters/DemandayTeleMarketingEnquiryCampaignQuestions';

        export declare function Create(request: Serenity.SaveRequest<DemandayTeleMarketingEnquiryCampaignQuestionsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayTeleMarketingEnquiryCampaignQuestionsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayTeleMarketingEnquiryCampaignQuestionsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayTeleMarketingEnquiryCampaignQuestionsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Masters/DemandayTeleMarketingEnquiryCampaignQuestions/Create",
            Update = "Masters/DemandayTeleMarketingEnquiryCampaignQuestions/Update",
            Delete = "Masters/DemandayTeleMarketingEnquiryCampaignQuestions/Delete",
            Retrieve = "Masters/DemandayTeleMarketingEnquiryCampaignQuestions/Retrieve",
            List = "Masters/DemandayTeleMarketingEnquiryCampaignQuestions/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>DemandayTeleMarketingEnquiryCampaignQuestionsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
