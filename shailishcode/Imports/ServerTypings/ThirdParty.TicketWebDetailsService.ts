namespace AdvanceCRM.ThirdParty {
    export namespace TicketWebDetailsService {
        export const baseUrl = 'ThirdParty/TicketWebDetails';

        export declare function Create(request: Serenity.SaveRequest<TicketWebDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<TicketWebDetailsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<TicketWebDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<TicketWebDetailsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToTicket(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "ThirdParty/TicketWebDetails/Create",
            Update = "ThirdParty/TicketWebDetails/Update",
            Delete = "ThirdParty/TicketWebDetails/Delete",
            Retrieve = "ThirdParty/TicketWebDetails/Retrieve",
            List = "ThirdParty/TicketWebDetails/List",
            MoveToTicket = "ThirdParty/TicketWebDetails/MoveToTicket"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToTicket'
        ].forEach(x => {
            (<any>TicketWebDetailsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
