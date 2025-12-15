namespace AdvanceCRM.ThirdParty {
    export namespace KnowlarityDetailsService {
        export const baseUrl = 'ThirdParty/KnowlarityDetails';

        export declare function Create(request: Serenity.SaveRequest<KnowlarityDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<KnowlarityDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<KnowlarityDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<KnowlarityDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function play(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToCMS(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function BulkMoveToEnquiry(request: BulkRequest, onSuccess?: (response: BulkImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/KnowlarityDetails/Create",
            Update = "ThirdParty/KnowlarityDetails/Update",
            Delete = "ThirdParty/KnowlarityDetails/Delete",
            Retrieve = "ThirdParty/KnowlarityDetails/Retrieve",
            List = "ThirdParty/KnowlarityDetails/List",
            Sync = "ThirdParty/KnowlarityDetails/Sync",
            play = "ThirdParty/KnowlarityDetails/play",
            MoveToCMS = "ThirdParty/KnowlarityDetails/MoveToCMS",
            MoveToEnquiry = "ThirdParty/KnowlarityDetails/MoveToEnquiry",
            BulkMoveToEnquiry = "ThirdParty/KnowlarityDetails/BulkMoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Sync', 
            'play', 
            'MoveToCMS', 
            'MoveToEnquiry', 
            'BulkMoveToEnquiry'
        ].forEach(x => {
            (<any>KnowlarityDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
