namespace AdvanceCRM.Masters {
    export namespace DemandayTeleMarketingEnquiryQuestionAnswersService {
        export const baseUrl = 'Masters/DemandayTeleMarketingEnquiryQuestionAnswers';

        export declare function Create(request: Serenity.SaveRequest<DemandayTeleMarketingEnquiryQuestionAnswersRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayTeleMarketingEnquiryQuestionAnswersRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayTeleMarketingEnquiryQuestionAnswersRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayTeleMarketingEnquiryQuestionAnswersRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Masters/DemandayTeleMarketingEnquiryQuestionAnswers/Create",
            Update = "Masters/DemandayTeleMarketingEnquiryQuestionAnswers/Update",
            Delete = "Masters/DemandayTeleMarketingEnquiryQuestionAnswers/Delete",
            Retrieve = "Masters/DemandayTeleMarketingEnquiryQuestionAnswers/Retrieve",
            List = "Masters/DemandayTeleMarketingEnquiryQuestionAnswers/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>DemandayTeleMarketingEnquiryQuestionAnswersService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
