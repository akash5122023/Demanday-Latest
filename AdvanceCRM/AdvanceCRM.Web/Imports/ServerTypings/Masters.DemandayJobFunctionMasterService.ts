namespace AdvanceCRM.Masters {
    export namespace DemandayJobFunctionMasterService {
        export const baseUrl = 'Masters/DemandayJobFunctionMaster';

        export declare function Create(request: Serenity.SaveRequest<DemandayJobFunctionMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayJobFunctionMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayJobFunctionMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayJobFunctionMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Masters/DemandayJobFunctionMaster/Create",
            Update = "Masters/DemandayJobFunctionMaster/Update",
            Delete = "Masters/DemandayJobFunctionMaster/Delete",
            Retrieve = "Masters/DemandayJobFunctionMaster/Retrieve",
            List = "Masters/DemandayJobFunctionMaster/List",
            ImportExcel = "Masters/DemandayJobFunctionMaster/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ImportExcel'
        ].forEach(x => {
            (<any>DemandayJobFunctionMasterService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
