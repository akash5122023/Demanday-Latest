namespace AdvanceCRM.Masters {
    export namespace DemandayEmployeeSizeMasterService {
        export const baseUrl = 'Masters/DemandayEmployeeSizeMaster';

        export declare function Create(request: Serenity.SaveRequest<DemandayEmployeeSizeMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayEmployeeSizeMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayEmployeeSizeMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayEmployeeSizeMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Masters/DemandayEmployeeSizeMaster/Create",
            Update = "Masters/DemandayEmployeeSizeMaster/Update",
            Delete = "Masters/DemandayEmployeeSizeMaster/Delete",
            Retrieve = "Masters/DemandayEmployeeSizeMaster/Retrieve",
            List = "Masters/DemandayEmployeeSizeMaster/List",
            ImportExcel = "Masters/DemandayEmployeeSizeMaster/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ImportExcel'
        ].forEach(x => {
            (<any>DemandayEmployeeSizeMasterService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
