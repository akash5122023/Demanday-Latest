namespace AdvanceCRM.Toolkit {
    export namespace DemandaySpecsService {
        export const baseUrl = 'Toolkit/DemandaySpecs';

        export declare function Create(request: Serenity.SaveRequest<DemandaySpecsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandaySpecsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandaySpecsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandaySpecsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ExcelImport(request: DemandaySpecsExcelImportRequest, onSuccess?: (response: ExcelImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Toolkit/DemandaySpecs/Create",
            Update = "Toolkit/DemandaySpecs/Update",
            Delete = "Toolkit/DemandaySpecs/Delete",
            Retrieve = "Toolkit/DemandaySpecs/Retrieve",
            List = "Toolkit/DemandaySpecs/List",
            ExcelImport = "Toolkit/DemandaySpecs/ExcelImport"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ExcelImport'
        ].forEach(x => {
            (<any>DemandaySpecsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
