namespace AdvanceCRM.Masters {
    export namespace ContactMeetingModeMasterService {
        export const baseUrl = 'Masters/ContactMeetingModeMaster';

        export declare function Create(request: Serenity.SaveRequest<ContactMeetingModeMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ContactMeetingModeMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ContactMeetingModeMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ContactMeetingModeMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Masters/ContactMeetingModeMaster/Create",
            Update = "Masters/ContactMeetingModeMaster/Update",
            Delete = "Masters/ContactMeetingModeMaster/Delete",
            Retrieve = "Masters/ContactMeetingModeMaster/Retrieve",
            List = "Masters/ContactMeetingModeMaster/List",
            ImportExcel = "Masters/ContactMeetingModeMaster/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ImportExcel'
        ].forEach(x => {
            (<any>ContactMeetingModeMasterService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
