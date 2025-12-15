namespace AdvanceCRM.Demanday {
    export namespace DemandayTeleMarketingQualiltyService {
        export const baseUrl = 'Demanday/DemandayTeleMarketingQualilty';

        export declare function Create(request: Serenity.SaveRequest<DemandayTeleMarketingQualiltyRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayTeleMarketingQualiltyRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayTeleMarketingQualiltyRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayTeleMarketingQualiltyRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToTeleMarketingMIS(request: MoveToTeleMarketingMISRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Demanday/DemandayTeleMarketingQualilty/Create",
            Update = "Demanday/DemandayTeleMarketingQualilty/Update",
            Delete = "Demanday/DemandayTeleMarketingQualilty/Delete",
            Retrieve = "Demanday/DemandayTeleMarketingQualilty/Retrieve",
            List = "Demanday/DemandayTeleMarketingQualilty/List",
            MoveToTeleMarketingMIS = "Demanday/DemandayTeleMarketingQualilty/MoveToTeleMarketingMIS"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToTeleMarketingMIS'
        ].forEach(x => {
            (<any>DemandayTeleMarketingQualiltyService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
