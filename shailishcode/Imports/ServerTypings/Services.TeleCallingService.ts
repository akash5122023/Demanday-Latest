namespace AdvanceCRM.Services {
    export namespace TeleCallingService {
        export const baseUrl = 'Services/TeleCalling';

        export declare function Create(request: Serenity.SaveRequest<TeleCallingRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<TeleCallingRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<TeleCallingRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<TeleCallingRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ExcelImport(request: ExcelImportRequest, onSuccess?: (response: ExcelImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendMail(request: SendMailRequest, onSuccess?: (response: SendMailResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ClickToCall(request: CallRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Services/TeleCalling/Create",
            Update = "Services/TeleCalling/Update",
            Delete = "Services/TeleCalling/Delete",
            Retrieve = "Services/TeleCalling/Retrieve",
            List = "Services/TeleCalling/List",
            ExcelImport = "Services/TeleCalling/ExcelImport",
            SendSMS = "Services/TeleCalling/SendSMS",
            SendMail = "Services/TeleCalling/SendMail",
            ClickToCall = "Services/TeleCalling/ClickToCall",
            MoveToEnquiry = "Services/TeleCalling/MoveToEnquiry"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ExcelImport', 
            'SendSMS', 
            'SendMail', 
            'ClickToCall', 
            'MoveToEnquiry'
        ].forEach(x => {
            (<any>TeleCallingService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
