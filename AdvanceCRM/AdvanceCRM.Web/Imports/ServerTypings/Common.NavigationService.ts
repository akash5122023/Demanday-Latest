namespace AdvanceCRM.Common {
    export namespace NavigationService {
        export const baseUrl = 'Common/Navigation';

        export declare function MultiCompany(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MultiLocation(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ChannelsManagement(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            MultiCompany = "Common/Navigation/MultiCompany",
            MultiLocation = "Common/Navigation/MultiLocation",
            ChannelsManagement = "Common/Navigation/ChannelsManagement"
        }

        [
            'MultiCompany', 
            'MultiLocation', 
            'ChannelsManagement'
        ].forEach(x => {
            (<any>NavigationService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
