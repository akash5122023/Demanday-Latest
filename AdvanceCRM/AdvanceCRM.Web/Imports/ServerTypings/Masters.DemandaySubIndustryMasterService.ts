namespace AdvanceCRM.Masters {
    export namespace DemandaySubIndustryMasterService {
        export const baseUrl = 'Masters/DemandaySubIndustryMaster';

        export declare function Create(request: Serenity.SaveRequest<DemandaySubIndustryMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandaySubIndustryMasterRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandaySubIndustryMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandaySubIndustryMasterRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Masters/DemandaySubIndustryMaster/Create",
            Update = "Masters/DemandaySubIndustryMaster/Update",
            Delete = "Masters/DemandaySubIndustryMaster/Delete",
            Retrieve = "Masters/DemandaySubIndustryMaster/Retrieve",
            List = "Masters/DemandaySubIndustryMaster/List",
            ImportExcel = "Masters/DemandaySubIndustryMaster/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ImportExcel'
        ].forEach(x => {
            (<any>DemandaySubIndustryMasterService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
