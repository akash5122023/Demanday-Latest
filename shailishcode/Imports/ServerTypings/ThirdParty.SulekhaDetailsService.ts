namespace AdvanceCRM.ThirdParty {
    export namespace SulekhaDetailsService {
        export const baseUrl = 'ThirdParty/SulekhaDetails';

        export declare function Create(request: Serenity.SaveRequest<SulekhaDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<SulekhaDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SulekhaDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SulekhaDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/SulekhaDetails/Create",
            Update = "ThirdParty/SulekhaDetails/Update",
            Delete = "ThirdParty/SulekhaDetails/Delete",
            Retrieve = "ThirdParty/SulekhaDetails/Retrieve",
            List = "ThirdParty/SulekhaDetails/List",
            MoveToEnquiry = "ThirdParty/SulekhaDetails/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/SulekhaDetails/BulkMoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToEnquiry', 
            'BulkMoveToEnquiry'
        ].forEach(x => {
            (<any>SulekhaDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
