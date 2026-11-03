namespace AdvanceCRM.Masters {
    export namespace ContactAccountTypeMasterService {
        export const baseUrl = 'Masters/ContactAccountTypeMaster';

        export declare function Create(request: Serenity.SaveRequest<ContactAccountTypeMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ContactAccountTypeMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ContactAccountTypeMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ContactAccountTypeMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Masters/ContactAccountTypeMaster/Create",
            Update = "Masters/ContactAccountTypeMaster/Update",
            Delete = "Masters/ContactAccountTypeMaster/Delete",
            Retrieve = "Masters/ContactAccountTypeMaster/Retrieve",
            List = "Masters/ContactAccountTypeMaster/List",
            ImportExcel = "Masters/ContactAccountTypeMaster/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ImportExcel'
        ].forEach(x => {
            (<any>ContactAccountTypeMasterService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
