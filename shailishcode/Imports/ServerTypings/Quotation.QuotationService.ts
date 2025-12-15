namespace AdvanceCRM.Quotation {
    export namespace QuotationService {
        export const baseUrl = 'Quotation/Quotation';

        export declare function Create(request: Serenity.SaveRequest<QuotationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<QuotationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<QuotationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: QuotationListRequest, onSuccess?: (response: Serenity.ListResponse<QuotationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Approve(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendWati(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function AddToMailChimp(request: MailChimpRequest, onSuccess?: (response: MailChimpResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkMail(request: SendEmailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToInvoice(request: SendMailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToPurchase(request: SendMailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToQuotation(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetNextNumber(request: GetNextNumberRequest, onSuccess?: (response: GetNextNumberResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Quotation/Quotation/Create",
            Update = "Quotation/Quotation/Update",
            Delete = "Quotation/Quotation/Delete",
            Retrieve = "Quotation/Quotation/Retrieve",
            List = "Quotation/Quotation/List",
            Approve = "Quotation/Quotation/Approve",
            SendSMS = "Quotation/Quotation/SendSMS",
            SendWati = "Quotation/Quotation/SendWati",
            SendBulkSMS = "Quotation/Quotation/SendBulkSMS",
            AddToMailChimp = "Quotation/Quotation/AddToMailChimp",
            SendBulkMail = "Quotation/Quotation/SendBulkMail",
            MoveToInvoice = "Quotation/Quotation/MoveToInvoice",
            MoveToPurchase = "Quotation/Quotation/MoveToPurchase",
            MoveToQuotation = "Quotation/Quotation/MoveToQuotation",
            GetNextNumber = "Quotation/Quotation/GetNextNumber"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Approve', 
            'SendSMS', 
            'SendWati', 
            'SendBulkSMS', 
            'AddToMailChimp', 
            'SendBulkMail', 
            'MoveToInvoice', 
            'MoveToPurchase', 
            'MoveToQuotation', 
            'GetNextNumber'
        ].forEach(x => {
            (<any>QuotationService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
