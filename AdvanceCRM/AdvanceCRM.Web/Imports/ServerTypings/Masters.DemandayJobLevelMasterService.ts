namespace AdvanceCRM.Masters {
    export namespace DemandayJobLevelMasterService {
        export const baseUrl = 'Masters/DemandayJobLevelMaster';

        export declare function Create(request: Serenity.SaveRequest<DemandayJobLevelMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayJobLevelMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayJobLevelMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayJobLevelMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Masters/DemandayJobLevelMaster/Create",
            Update = "Masters/DemandayJobLevelMaster/Update",
            Delete = "Masters/DemandayJobLevelMaster/Delete",
            Retrieve = "Masters/DemandayJobLevelMaster/Retrieve",
            List = "Masters/DemandayJobLevelMaster/List",
            ImportExcel = "Masters/DemandayJobLevelMaster/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ImportExcel'
        ].forEach(x => {
            (<any>DemandayJobLevelMasterService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
