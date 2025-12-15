namespace AdvanceCRM.Settings {
    export namespace ProductModulesService {
        export const baseUrl = 'Settings/ProductModules';

        export declare function Create(request: Serenity.SaveRequest<ProductModuleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ProductModuleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ProductModuleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ProductModuleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function UpdatePrices(request: ModulePriceUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Settings/ProductModules/Create",
            Update = "Settings/ProductModules/Update",
            Delete = "Settings/ProductModules/Delete",
            Retrieve = "Settings/ProductModules/Retrieve",
            List = "Settings/ProductModules/List",
            UpdatePrices = "Settings/ProductModules/UpdatePrices"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'UpdatePrices'
        ].forEach(x => {
            (<any>ProductModulesService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
