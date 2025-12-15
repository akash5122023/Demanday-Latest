namespace AdvanceCRM.Administration {
    export namespace CompanyDetailsService {
        export const baseUrl = 'Administration/CompanyDetails';

        export declare function Create(request: Serenity.SaveRequest<CompanyDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<CompanyDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CompanyDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CompanyDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkTransfer(request: TransferRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Administration/CompanyDetails/Create",
            Update = "Administration/CompanyDetails/Update",
            Delete = "Administration/CompanyDetails/Delete",
            Retrieve = "Administration/CompanyDetails/Retrieve",
            List = "Administration/CompanyDetails/List",
            BulkTransfer = "Administration/CompanyDetails/BulkTransfer"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'BulkTransfer'
        ].forEach(x => {
            (<any>CompanyDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
