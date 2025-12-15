namespace AdvanceCRM.BizMail {
    export namespace BmTemplateService {
        export const baseUrl = 'BizMail/BmTemplate';

        export declare function Create(request: Serenity.SaveRequest<BmTemplateRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<BmTemplateRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<BmTemplateRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<BmTemplateRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Sync(request: Serenity.ServiceRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "BizMail/BmTemplate/Create",
            Update = "BizMail/BmTemplate/Update",
            Delete = "BizMail/BmTemplate/Delete",
            Retrieve = "BizMail/BmTemplate/Retrieve",
            List = "BizMail/BmTemplate/List",
            Sync = "BizMail/BmTemplate/Sync"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'Sync'
        ].forEach(x => {
            (<any>BmTemplateService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
