namespace AdvanceCRM.Demanday {
    export namespace DemandayTeleMarketingMISService {
        export const baseUrl = 'Demanday/DemandayTeleMarketingMIS';

        export declare function Create(request: Serenity.SaveRequest<DemandayTeleMarketingMISRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayTeleMarketingMISRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayTeleMarketingMISRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayTeleMarketingMISRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Demanday/DemandayTeleMarketingMIS/Create",
            Update = "Demanday/DemandayTeleMarketingMIS/Update",
            Delete = "Demanday/DemandayTeleMarketingMIS/Delete",
            Retrieve = "Demanday/DemandayTeleMarketingMIS/Retrieve",
            List = "Demanday/DemandayTeleMarketingMIS/List",
            ImportExcel = "Demanday/DemandayTeleMarketingMIS/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ImportExcel'
        ].forEach(x => {
            (<any>DemandayTeleMarketingMISService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
