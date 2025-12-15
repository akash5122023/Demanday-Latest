namespace AdvanceCRM.Demanday {
    export namespace DemandayTeleMarketingTeamLeaderService {
        export const baseUrl = 'Demanday/DemandayTeleMarketingTeamLeader';

        export declare function Create(request: Serenity.SaveRequest<DemandayTeleMarketingTeamLeaderRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayTeleMarketingTeamLeaderRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayTeleMarketingTeamLeaderRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayTeleMarketingTeamLeaderRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToQuality(request: MoveToQualityRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Demanday/DemandayTeleMarketingTeamLeader/Create",
            Update = "Demanday/DemandayTeleMarketingTeamLeader/Update",
            Delete = "Demanday/DemandayTeleMarketingTeamLeader/Delete",
            Retrieve = "Demanday/DemandayTeleMarketingTeamLeader/Retrieve",
            List = "Demanday/DemandayTeleMarketingTeamLeader/List",
            MoveToQuality = "Demanday/DemandayTeleMarketingTeamLeader/MoveToQuality"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToQuality'
        ].forEach(x => {
            (<any>DemandayTeleMarketingTeamLeaderService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
