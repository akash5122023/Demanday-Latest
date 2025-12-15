namespace AdvanceCRM.Services {
    export namespace TicketService {
        export const baseUrl = 'Services/Ticket';

        export declare function Create(request: Serenity.SaveRequest<TicketRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<TicketRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<TicketRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<TicketRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToCMS(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Services/Ticket/Create",
            Update = "Services/Ticket/Update",
            Delete = "Services/Ticket/Delete",
            Retrieve = "Services/Ticket/Retrieve",
            List = "Services/Ticket/List",
            MoveToCMS = "Services/Ticket/MoveToCMS"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToCMS'
        ].forEach(x => {
            (<any>TicketService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
