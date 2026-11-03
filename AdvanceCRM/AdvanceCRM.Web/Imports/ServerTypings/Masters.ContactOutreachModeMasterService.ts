namespace AdvanceCRM.Masters {
    export namespace ContactOutreachModeMasterService {
        export const baseUrl = 'Masters/ContactOutreachModeMaster';

        export declare function Create(request: Serenity.SaveRequest<ContactOutreachModeMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ContactOutreachModeMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ContactOutreachModeMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ContactOutreachModeMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Masters/ContactOutreachModeMaster/Create",
            Update = "Masters/ContactOutreachModeMaster/Update",
            Delete = "Masters/ContactOutreachModeMaster/Delete",
            Retrieve = "Masters/ContactOutreachModeMaster/Retrieve",
            List = "Masters/ContactOutreachModeMaster/List",
            ImportExcel = "Masters/ContactOutreachModeMaster/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ImportExcel'
        ].forEach(x => {
            (<any>ContactOutreachModeMasterService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
