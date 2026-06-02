namespace AdvanceCRM.Demanday {
    export namespace DemandayEnquiryService {
        export const baseUrl = 'Demanday/DemandayEnquiry';

        export declare function Create(request: Serenity.SaveRequest<DemandayEnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayEnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayEnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayEnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToTeamLeader(request: MoveToTeamLeaderRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Demanday/DemandayEnquiry/Create",
            Update = "Demanday/DemandayEnquiry/Update",
            Delete = "Demanday/DemandayEnquiry/Delete",
            Retrieve = "Demanday/DemandayEnquiry/Retrieve",
            List = "Demanday/DemandayEnquiry/List",
            MoveToTeamLeader = "Demanday/DemandayEnquiry/MoveToTeamLeader",
            ImportExcel = "Demanday/DemandayEnquiry/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToTeamLeader', 
            'ImportExcel'
        ].forEach(x => {
            (<any>DemandayEnquiryService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
