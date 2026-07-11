namespace AdvanceCRM.Toolkit {
    export namespace ClientSupressionService {
        export const baseUrl = 'Toolkit/ClientSupression';

        export declare function Create(request: Serenity.SaveRequest<ClientSupressionRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ClientSupressionRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ClientSupressionRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ClientSupressionRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ExcelImport(request: ClientSupressionExcelImportRequest, onSuccess?: (response: ExcelImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Toolkit/ClientSupression/Create",
            Update = "Toolkit/ClientSupression/Update",
            Delete = "Toolkit/ClientSupression/Delete",
            Retrieve = "Toolkit/ClientSupression/Retrieve",
            List = "Toolkit/ClientSupression/List",
            ExcelImport = "Toolkit/ClientSupression/ExcelImport"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ExcelImport'
        ].forEach(x => {
            (<any>ClientSupressionService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
