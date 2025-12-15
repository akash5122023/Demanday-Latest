namespace AdvanceCRM.Common {
    export namespace CommonService {
        export const baseUrl = 'Common';

        export declare function UploadImage(request: Modules.Common.FileUploadRequest, onSuccess?: (response: Modules.Common.FileUploadResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendMail(request: SendMailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendWati(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendIntractWa(request: SendIntractSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBizMail(request: SendMailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetTemplateImage(request: GetTemplateImageRequest, onSuccess?: (response: GetTemplateImageResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            UploadImage = "Common/UploadImage",
            SendMail = "Common/SendMail",
            SendWati = "Common/SendWati",
            SendIntractWa = "Common/SendIntractWa",
            SendSMS = "Common/SendSMS",
            SendBizMail = "Common/SendBizMail",
            GetTemplateImage = "Common/GetTemplateImage"
        }

        [
            'UploadImage', 
            'SendMail', 
            'SendWati', 
            'SendIntractWa', 
            'SendSMS', 
            'SendBizMail', 
            'GetTemplateImage'
        ].forEach(x => {
            (<any>CommonService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
