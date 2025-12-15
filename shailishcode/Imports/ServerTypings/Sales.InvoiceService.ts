namespace AdvanceCRM.Sales {
    export namespace InvoiceService {
        export const baseUrl = 'Sales/Invoice';

        export declare function Create(request: Serenity.SaveRequest<InvoiceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<InvoiceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<InvoiceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<InvoiceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Approve(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function AddToMailChimp(request: MailChimpRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToInvoice(request: SendMailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendWati(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkMail(request: SendEmailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetNextNumber(request: GetNextNumberRequest, onSuccess?: (response: GetNextNumberResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToClone(request: SendMailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Sales/Invoice/Create",
            Update = "Sales/Invoice/Update",
            Delete = "Sales/Invoice/Delete",
            Retrieve = "Sales/Invoice/Retrieve",
            List = "Sales/Invoice/List",
            Approve = "Sales/Invoice/Approve",
            AddToMailChimp = "Sales/Invoice/AddToMailChimp",
            MoveToInvoice = "Sales/Invoice/MoveToInvoice",
            SendSMS = "Sales/Invoice/SendSMS",
            SendWati = "Sales/Invoice/SendWati",
            SendBulkSMS = "Sales/Invoice/SendBulkSMS",
            SendBulkMail = "Sales/Invoice/SendBulkMail",
            GetNextNumber = "Sales/Invoice/GetNextNumber",
            MoveToClone = "Sales/Invoice/MoveToClone"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Approve', 
            'AddToMailChimp', 
            'MoveToInvoice', 
            'SendSMS', 
            'SendWati', 
            'SendBulkSMS', 
            'SendBulkMail', 
            'GetNextNumber', 
            'MoveToClone'
        ].forEach(x => {
            (<any>InvoiceService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
