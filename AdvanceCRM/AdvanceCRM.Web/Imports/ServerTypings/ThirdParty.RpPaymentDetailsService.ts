namespace AdvanceCRM.ThirdParty {
    export namespace RpPaymentDetailsService {
        export const baseUrl = 'ThirdParty/RpPaymentDetails';

        export declare function Create(request: Serenity.SaveRequest<RpPaymentDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<RpPaymentDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<RpPaymentDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<RpPaymentDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/RpPaymentDetails/Create",
            Update = "ThirdParty/RpPaymentDetails/Update",
            Delete = "ThirdParty/RpPaymentDetails/Delete",
            Retrieve = "ThirdParty/RpPaymentDetails/Retrieve",
            List = "ThirdParty/RpPaymentDetails/List",
            MoveToEnquiry = "ThirdParty/RpPaymentDetails/MoveToEnquiry",
            Sync = "ThirdParty/RpPaymentDetails/Sync"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToEnquiry', 
            'Sync'
        ].forEach(x => {
            (<any>RpPaymentDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
