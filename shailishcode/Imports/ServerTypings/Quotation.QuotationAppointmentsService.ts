namespace AdvanceCRM.Quotation {
    export namespace QuotationAppointmentsService {
        export const baseUrl = 'Quotation/QuotationAppointments';

        export declare function Create(request: Serenity.SaveRequest<QuotationAppointmentsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<QuotationAppointmentsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<QuotationAppointmentsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<QuotationAppointmentsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendMail(request: SendMailRequest, onSuccess?: (response: SendMailResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Quotation/QuotationAppointments/Create",
            Update = "Quotation/QuotationAppointments/Update",
            Delete = "Quotation/QuotationAppointments/Delete",
            Retrieve = "Quotation/QuotationAppointments/Retrieve",
            List = "Quotation/QuotationAppointments/List",
            SendSMS = "Quotation/QuotationAppointments/SendSMS",
            SendMail = "Quotation/QuotationAppointments/SendMail"
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
            (<any>QuotationAppointmentsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
