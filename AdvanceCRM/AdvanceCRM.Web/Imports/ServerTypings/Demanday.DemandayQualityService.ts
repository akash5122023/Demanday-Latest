namespace AdvanceCRM.Demanday {
    export namespace DemandayQualityService {
        export const baseUrl = 'Demanday/DemandayQuality';

        export declare function Create(request: Serenity.SaveRequest<DemandayQualityRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayQualityRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayQualityRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function QaStatusSummary(request: Serenity.ServiceRequest, onSuccess?: (response: QaStatusSummaryResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayQualityRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToMIS(request: MoveToMISRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Demanday/DemandayQuality/Create",
            Update = "Demanday/DemandayQuality/Update",
            Delete = "Demanday/DemandayQuality/Delete",
            Retrieve = "Demanday/DemandayQuality/Retrieve",
            QaStatusSummary = "Demanday/DemandayQuality/QaStatusSummary",
            List = "Demanday/DemandayQuality/List",
            MoveToMIS = "Demanday/DemandayQuality/MoveToMIS",
            ImportExcel = "Demanday/DemandayQuality/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'QaStatusSummary', 
            'List', 
            'MoveToMIS', 
            'ImportExcel'
        ].forEach(x => {
            (<any>DemandayQualityService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
