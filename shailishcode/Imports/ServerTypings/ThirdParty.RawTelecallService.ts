namespace AdvanceCRM.ThirdParty {
    export namespace RawTelecallService {
        export const baseUrl = 'ThirdParty/RawTelecall';

        export declare function Create(request: Serenity.SaveRequest<RawTelecallRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<RawTelecallRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<RawTelecallRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<RawTelecallRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ClickToCall(request: CallRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ExcelImport(request: ExcelImportWithUsersRequest, onSuccess?: (response: ExcelImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/RawTelecall/Create",
            Update = "ThirdParty/RawTelecall/Update",
            Delete = "ThirdParty/RawTelecall/Delete",
            Retrieve = "ThirdParty/RawTelecall/Retrieve",
            List = "ThirdParty/RawTelecall/List",
            ClickToCall = "ThirdParty/RawTelecall/ClickToCall",
            ExcelImport = "ThirdParty/RawTelecall/ExcelImport",
            MoveToEnquiry = "ThirdParty/RawTelecall/MoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ClickToCall', 
            'ExcelImport', 
            'MoveToEnquiry'
        ].forEach(x => {
            (<any>RawTelecallService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
