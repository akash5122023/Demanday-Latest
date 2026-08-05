namespace AdvanceCRM.Demanday {
    export namespace DemandayTeamLeaderService {
        export const baseUrl = 'Demanday/DemandayTeamLeader';

        export declare function Create(request: Serenity.SaveRequest<DemandayTeamLeaderRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayTeamLeaderRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayTeamLeaderRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function AccountCampaignSummary(request: Serenity.ServiceRequest, onSuccess?: (response: AccountCampaignSummaryResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayTeamLeaderRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToQuality(request: MoveToDemandayQualityRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Demanday/DemandayTeamLeader/Create",
            Update = "Demanday/DemandayTeamLeader/Update",
            Delete = "Demanday/DemandayTeamLeader/Delete",
            Retrieve = "Demanday/DemandayTeamLeader/Retrieve",
            AccountCampaignSummary = "Demanday/DemandayTeamLeader/AccountCampaignSummary",
            List = "Demanday/DemandayTeamLeader/List",
            MoveToQuality = "Demanday/DemandayTeamLeader/MoveToQuality",
            ImportExcel = "Demanday/DemandayTeamLeader/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'AccountCampaignSummary', 
            'List', 
            'MoveToQuality', 
            'ImportExcel'
        ].forEach(x => {
            (<any>DemandayTeamLeaderService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
