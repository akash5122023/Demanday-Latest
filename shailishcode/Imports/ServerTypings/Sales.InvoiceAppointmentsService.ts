namespace AdvanceCRM.Sales {
    export namespace InvoiceAppointmentsService {
        export const baseUrl = 'Sales/InvoiceAppointments';

        export declare function Create(request: Serenity.SaveRequest<InvoiceAppointmentsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<InvoiceAppointmentsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<InvoiceAppointmentsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<InvoiceAppointmentsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendMail(request: SendMailRequest, onSuccess?: (response: SendMailResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Sales/InvoiceAppointments/Create",
            Update = "Sales/InvoiceAppointments/Update",
            Delete = "Sales/InvoiceAppointments/Delete",
            Retrieve = "Sales/InvoiceAppointments/Retrieve",
            List = "Sales/InvoiceAppointments/List",
            SendSMS = "Sales/InvoiceAppointments/SendSMS",
            SendMail = "Sales/InvoiceAppointments/SendMail"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'SendSMS', 
            'SendMail'
        ].forEach(x => {
            (<any>InvoiceAppointmentsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
