namespace AdvanceCRM.ThirdParty {
    export namespace KnowlarityIvrService {
        export const baseUrl = 'ThirdParty/KnowlarityIvr';

        export declare function Create(request: Serenity.SaveRequest<KnowlarityIvrRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<KnowlarityIvrRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<KnowlarityIvrRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<KnowlarityIvrRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/KnowlarityIvr/Create",
            Update = "ThirdParty/KnowlarityIvr/Update",
            Delete = "ThirdParty/KnowlarityIvr/Delete",
            Retrieve = "ThirdParty/KnowlarityIvr/Retrieve",
            List = "ThirdParty/KnowlarityIvr/List",
            Sync = "ThirdParty/KnowlarityIvr/Sync"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Sync'
        ].forEach(x => {
            (<any>KnowlarityIvrService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
