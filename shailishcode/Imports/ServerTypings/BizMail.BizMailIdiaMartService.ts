namespace AdvanceCRM.BizMail {
    export namespace BizMailIdiaMartService {
        export const baseUrl = 'BizMail/BizMailIdiaMart';

        export declare function Create(request: Serenity.SaveRequest<BizMailIdiaMartRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<BizMailIdiaMartRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<BizMailIdiaMartRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<BizMailIdiaMartRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "BizMail/BizMailIdiaMart/Create",
            Update = "BizMail/BizMailIdiaMart/Update",
            Delete = "BizMail/BizMailIdiaMart/Delete",
            Retrieve = "BizMail/BizMailIdiaMart/Retrieve",
            List = "BizMail/BizMailIdiaMart/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>BizMailIdiaMartService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
