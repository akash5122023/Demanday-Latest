namespace AdvanceCRM.Services {
    export namespace TeleCallingAppointmentsService {
        export const baseUrl = 'Services/TeleCallingAppointments';

        export declare function Create(request: Serenity.SaveRequest<TeleCallingAppointmentsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<TeleCallingAppointmentsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<TeleCallingAppointmentsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<TeleCallingAppointmentsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendMail(request: SendMailRequest, onSuccess?: (response: SendMailResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Services/TeleCallingAppointments/Create",
            Update = "Services/TeleCallingAppointments/Update",
            Delete = "Services/TeleCallingAppointments/Delete",
            Retrieve = "Services/TeleCallingAppointments/Retrieve",
            List = "Services/TeleCallingAppointments/List",
            SendSMS = "Services/TeleCallingAppointments/SendSMS",
            SendMail = "Services/TeleCallingAppointments/SendMail"
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
            (<any>TeleCallingAppointmentsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
