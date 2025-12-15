namespace AdvanceCRM.BizMail {
    export namespace CampaignBmService {
        export const baseUrl = 'BizMail/CampaignBm';

        export declare function Create(request: Serenity.SaveRequest<CampaignBmRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<CampaignBmRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CampaignBmRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CampaignBmRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "BizMail/CampaignBm/Create",
            Update = "BizMail/CampaignBm/Update",
            Delete = "BizMail/CampaignBm/Delete",
            Retrieve = "BizMail/CampaignBm/Retrieve",
            List = "BizMail/CampaignBm/List",
            Sync = "BizMail/CampaignBm/Sync"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Sync'
        ].forEach(x => {
            (<any>CampaignBmService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
