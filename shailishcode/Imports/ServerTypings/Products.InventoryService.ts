namespace AdvanceCRM.Products {
    export namespace InventoryService {
        export const baseUrl = 'Products/Inventory';

        export declare function Create(request: Serenity.SaveRequest<InventoryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<InventoryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ExcelImport(request: ExcelImportRequest, onSuccess?: (response: ExcelImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<InventoryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<InventoryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Products/Inventory/Create",
            Update = "Products/Inventory/Update",
            Delete = "Products/Inventory/Delete",
            ExcelImport = "Products/Inventory/ExcelImport",
            Retrieve = "Products/Inventory/Retrieve",
            List = "Products/Inventory/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'ExcelImport', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>InventoryService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
