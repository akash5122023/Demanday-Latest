namespace AdvanceCRM.Settings {
    export namespace TradeIndiaConfigurationService {
        export const baseUrl = 'Settings/TradeIndiaConfiguration';

        export declare function Create(request: Serenity.SaveRequest<TradeIndiaConfigurationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<TradeIndiaConfigurationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<TradeIndiaConfigurationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<TradeIndiaConfigurationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Settings/TradeIndiaConfiguration/Create",
            Update = "Settings/TradeIndiaConfiguration/Update",
            Delete = "Settings/TradeIndiaConfiguration/Delete",
            Retrieve = "Settings/TradeIndiaConfiguration/Retrieve",
            List = "Settings/TradeIndiaConfiguration/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>TradeIndiaConfigurationService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
