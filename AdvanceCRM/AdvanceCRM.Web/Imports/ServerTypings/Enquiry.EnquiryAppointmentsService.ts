namespace AdvanceCRM.Enquiry {
    export namespace EnquiryAppointmentsService {
        export const baseUrl = 'Enquiry/EnquiryAppointments';

        export declare function Create(request: Serenity.SaveRequest<EnquiryAppointmentsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<EnquiryAppointmentsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<EnquiryAppointmentsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<EnquiryAppointmentsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendMail(request: SendMailRequest, onSuccess?: (response: SendMailResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Enquiry/EnquiryAppointments/Create",
            Update = "Enquiry/EnquiryAppointments/Update",
            Delete = "Enquiry/EnquiryAppointments/Delete",
            Retrieve = "Enquiry/EnquiryAppointments/Retrieve",
            List = "Enquiry/EnquiryAppointments/List",
            SendSMS = "Enquiry/EnquiryAppointments/SendSMS",
            SendMail = "Enquiry/EnquiryAppointments/SendMail"
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
            (<any>EnquiryAppointmentsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
