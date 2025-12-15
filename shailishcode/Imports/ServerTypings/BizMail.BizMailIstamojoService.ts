namespace AdvanceCRM.BizMail {
    export namespace BizMailIstamojoService {
        export const baseUrl = 'BizMail/BizMailIstamojo';

        export declare function Create(request: Serenity.SaveRequest<BizMailIstamojoRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<BizMailIstamojoRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<BizMailIstamojoRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<BizMailIstamojoRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "BizMail/BizMailIstamojo/Create",
            Update = "BizMail/BizMailIstamojo/Update",
            Delete = "BizMail/BizMailIstamojo/Delete",
            Retrieve = "BizMail/BizMailIstamojo/Retrieve",
            List = "BizMail/BizMailIstamojo/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>BizMailIstamojoService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
