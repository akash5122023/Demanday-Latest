namespace AdvanceCRM.BizMail {
    export namespace BizMailTradeIdiaService {
        export const baseUrl = 'BizMail/BizMailTradeIdia';

        export declare function Create(request: Serenity.SaveRequest<BizMailTradeIdiaRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<BizMailTradeIdiaRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<BizMailTradeIdiaRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<BizMailTradeIdiaRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "BizMail/BizMailTradeIdia/Create",
            Update = "BizMail/BizMailTradeIdia/Update",
            Delete = "BizMail/BizMailTradeIdia/Delete",
            Retrieve = "BizMail/BizMailTradeIdia/Retrieve",
            List = "BizMail/BizMailTradeIdia/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>BizMailTradeIdiaService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
