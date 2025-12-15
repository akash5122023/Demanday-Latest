namespace AdvanceCRM.ThirdParty {
    export namespace IndiaMartDetailsService {
        export const baseUrl = 'ThirdParty/IndiaMartDetails';

        export declare function Create(request: Serenity.SaveRequest<IndiaMartDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<IndiaMartDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<IndiaMartDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<IndiaMartDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync2(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/IndiaMartDetails/Create",
            Update = "ThirdParty/IndiaMartDetails/Update",
            Delete = "ThirdParty/IndiaMartDetails/Delete",
            Retrieve = "ThirdParty/IndiaMartDetails/Retrieve",
            List = "ThirdParty/IndiaMartDetails/List",
            Sync2 = "ThirdParty/IndiaMartDetails/Sync2",
            Sync = "ThirdParty/IndiaMartDetails/Sync",
            MoveToEnquiry = "ThirdParty/IndiaMartDetails/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/IndiaMartDetails/BulkMoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Sync2', 
            'Sync', 
            'MoveToEnquiry', 
            'BulkMoveToEnquiry'
        ].forEach(x => {
            (<any>IndiaMartDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
