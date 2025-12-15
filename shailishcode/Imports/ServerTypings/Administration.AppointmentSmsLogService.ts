namespace AdvanceCRM.Administration {
    export namespace AppointmentSmsLogService {
        export const baseUrl = 'Administration/AppointmentSmsLog';

        export declare function Create(request: Serenity.SaveRequest<AppointmentSmsLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<AppointmentSmsLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<AppointmentSmsLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<AppointmentSmsLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Administration/AppointmentSmsLog/Create",
            Update = "Administration/AppointmentSmsLog/Update",
            Delete = "Administration/AppointmentSmsLog/Delete",
            Retrieve = "Administration/AppointmentSmsLog/Retrieve",
            List = "Administration/AppointmentSmsLog/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>AppointmentSmsLogService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
