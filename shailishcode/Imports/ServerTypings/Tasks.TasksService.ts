namespace AdvanceCRM.Tasks {
    export namespace TasksService {
        export const baseUrl = 'Tasks/Tasks';

        export declare function Create(request: Serenity.SaveRequest<TasksRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<TasksRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<TasksRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<TasksRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendMail(request: SendMailRequest, onSuccess?: (response: SendMailResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendWati(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Tasks/Tasks/Create",
            Update = "Tasks/Tasks/Update",
            Delete = "Tasks/Tasks/Delete",
            Retrieve = "Tasks/Tasks/Retrieve",
            List = "Tasks/Tasks/List",
            SendMail = "Tasks/Tasks/SendMail",
            SendSMS = "Tasks/Tasks/SendSMS",
            SendWati = "Tasks/Tasks/SendWati",
            SendBulkSMS = "Tasks/Tasks/SendBulkSMS"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'SendMail', 
            'SendSMS', 
            'SendWati', 
            'SendBulkSMS'
        ].forEach(x => {
            (<any>TasksService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
