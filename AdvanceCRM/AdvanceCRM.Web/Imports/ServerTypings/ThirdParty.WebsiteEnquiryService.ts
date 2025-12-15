namespace AdvanceCRM.ThirdParty {
    export namespace WebsiteEnquiryService {
        export const baseUrl = 'ThirdParty/WebsiteEnquiry';

        export declare function Create(request: Serenity.SaveRequest<WebsiteEnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<WebsiteEnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WebsiteEnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WebsiteEnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/WebsiteEnquiry/Create",
            Update = "ThirdParty/WebsiteEnquiry/Update",
            Delete = "ThirdParty/WebsiteEnquiry/Delete",
            Retrieve = "ThirdParty/WebsiteEnquiry/Retrieve",
            List = "ThirdParty/WebsiteEnquiry/List",
            MoveToEnquiry = "ThirdParty/WebsiteEnquiry/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/WebsiteEnquiry/BulkMoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToEnquiry', 
            'BulkMoveToEnquiry'
        ].forEach(x => {
            (<any>WebsiteEnquiryService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
