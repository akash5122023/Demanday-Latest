namespace AdvanceCRM.Toolkit {
    export namespace DemandayCompetitorService {
        export const baseUrl = 'Toolkit/DemandayCompetitor';

        export declare function Create(request: Serenity.SaveRequest<DemandayCompetitorRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayCompetitorRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayCompetitorRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayCompetitorRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ExcelImport(request: DemandayCompetitorExcelImportRequest, onSuccess?: (response: ExcelImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Toolkit/DemandayCompetitor/Create",
            Update = "Toolkit/DemandayCompetitor/Update",
            Delete = "Toolkit/DemandayCompetitor/Delete",
            Retrieve = "Toolkit/DemandayCompetitor/Retrieve",
            List = "Toolkit/DemandayCompetitor/List",
            ExcelImport = "Toolkit/DemandayCompetitor/ExcelImport"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ExcelImport'
        ].forEach(x => {
            (<any>DemandayCompetitorService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
