namespace AdvanceCRM.ThirdParty {
    export namespace SIndiaMartDetailsService {
        export const baseUrl = 'ThirdParty/SIndiaMartDetails';

        export declare function Create(request: Serenity.SaveRequest<SIndiaMartDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<SIndiaMartDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SIndiaMartDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SIndiaMartDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync2(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/SIndiaMartDetails/Create",
            Update = "ThirdParty/SIndiaMartDetails/Update",
            Delete = "ThirdParty/SIndiaMartDetails/Delete",
            Retrieve = "ThirdParty/SIndiaMartDetails/Retrieve",
            List = "ThirdParty/SIndiaMartDetails/List",
            Sync = "ThirdParty/SIndiaMartDetails/Sync",
            MoveToEnquiry = "ThirdParty/SIndiaMartDetails/MoveToEnquiry",
            Sync2 = "ThirdParty/SIndiaMartDetails/Sync2",
            BulkMoveToEnquiry = "ThirdParty/SIndiaMartDetails/BulkMoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Sync', 
            'MoveToEnquiry', 
            'Sync2', 
            'BulkMoveToEnquiry'
        ].forEach(x => {
            (<any>SIndiaMartDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
