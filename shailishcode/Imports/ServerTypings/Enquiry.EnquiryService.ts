namespace AdvanceCRM.Enquiry {
    export namespace EnquiryService {
        export const baseUrl = 'Enquiry/Enquiry';

        export declare function Create(request: Serenity.SaveRequest<EnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<EnquiryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<EnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: EnquiryListRequest, onSuccess?: (response: Serenity.ListResponse<EnquiryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function FBEnquiry(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendMail(request: SendMailRequest, onSuccess?: (response: SendMailResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendWA(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendWati(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkMail(request: SendEmailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ExcelImport(request: ExcelImportWithUsersRequest, onSuccess?: (response: ExcelImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToQuotation(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToEnquiry(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function AddToMailChimp(request: MailChimpRequest, onSuccess?: (response: MailChimpResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ClickToCall(request: CallRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetNextNumber(request: GetNextNumberRequest, onSuccess?: (response: GetNextNumberResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Enquiry/Enquiry/Create",
            Update = "Enquiry/Enquiry/Update",
            Delete = "Enquiry/Enquiry/Delete",
            Retrieve = "Enquiry/Enquiry/Retrieve",
            List = "Enquiry/Enquiry/List",
            FBEnquiry = "Enquiry/Enquiry/FBEnquiry",
            SendMail = "Enquiry/Enquiry/SendMail",
            SendWA = "Enquiry/Enquiry/SendWA",
            SendSMS = "Enquiry/Enquiry/SendSMS",
            SendWati = "Enquiry/Enquiry/SendWati",
            SendBulkSMS = "Enquiry/Enquiry/SendBulkSMS",
            SendBulkMail = "Enquiry/Enquiry/SendBulkMail",
            ExcelImport = "Enquiry/Enquiry/ExcelImport",
            MoveToQuotation = "Enquiry/Enquiry/MoveToQuotation",
            MoveToEnquiry = "Enquiry/Enquiry/MoveToEnquiry",
            AddToMailChimp = "Enquiry/Enquiry/AddToMailChimp",
            ClickToCall = "Enquiry/Enquiry/ClickToCall",
            GetNextNumber = "Enquiry/Enquiry/GetNextNumber"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'FBEnquiry', 
            'SendMail', 
            'SendWA', 
            'SendSMS', 
            'SendWati', 
            'SendBulkSMS', 
            'SendBulkMail', 
            'ExcelImport', 
            'MoveToQuotation', 
            'MoveToEnquiry', 
            'AddToMailChimp', 
            'ClickToCall', 
            'GetNextNumber'
        ].forEach(x => {
            (<any>EnquiryService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
