namespace AdvanceCRM.Sales {
    export namespace SalesService {
        export const baseUrl = 'Sales/Sales';

        export declare function Create(request: Serenity.SaveRequest<SalesRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<SalesRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SalesRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: SalesListRequest, onSuccess?: (response: Serenity.ListResponse<SalesRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToClone(request: SendMailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkMail(request: SendEmailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function AddToMailChimp(request: MailChimpRequest, onSuccess?: (response: MailChimpResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetNextNumber(request: GetNextNumberRequest, onSuccess?: (response: GetNextNumberResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendWati(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Approve(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Sales/Sales/Create",
            Update = "Sales/Sales/Update",
            Delete = "Sales/Sales/Delete",
            Retrieve = "Sales/Sales/Retrieve",
            List = "Sales/Sales/List",
            MoveToClone = "Sales/Sales/MoveToClone",
            SendBulkMail = "Sales/Sales/SendBulkMail",
            AddToMailChimp = "Sales/Sales/AddToMailChimp",
            GetNextNumber = "Sales/Sales/GetNextNumber",
            SendSMS = "Sales/Sales/SendSMS",
            SendWati = "Sales/Sales/SendWati",
            SendBulkSMS = "Sales/Sales/SendBulkSMS",
            Approve = "Sales/Sales/Approve"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToClone', 
            'SendBulkMail', 
            'AddToMailChimp', 
            'GetNextNumber', 
            'SendSMS', 
            'SendWati', 
            'SendBulkSMS', 
            'Approve'
        ].forEach(x => {
            (<any>SalesService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
