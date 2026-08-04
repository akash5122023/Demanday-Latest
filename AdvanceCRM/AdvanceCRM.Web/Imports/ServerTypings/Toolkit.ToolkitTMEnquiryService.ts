namespace AdvanceCRM.Toolkit {
    export namespace ToolkitTMEnquiryService {
        export const baseUrl = 'Toolkit/ToolkitTMEnquiry';

        export declare function Create(request: Serenity.SaveRequest<ToolkitTMEnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ToolkitTMEnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ToolkitTMEnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ToolkitTMEnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Toolkit/ToolkitTMEnquiry/Create",
            Update = "Toolkit/ToolkitTMEnquiry/Update",
            Delete = "Toolkit/ToolkitTMEnquiry/Delete",
            Retrieve = "Toolkit/ToolkitTMEnquiry/Retrieve",
            List = "Toolkit/ToolkitTMEnquiry/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>ToolkitTMEnquiryService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
