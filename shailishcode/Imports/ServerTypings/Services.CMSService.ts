namespace AdvanceCRM.Services {
    export namespace CMSService {
        export const baseUrl = 'Services/CMS';

        export declare function Create(request: Serenity.SaveRequest<CMSRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendMail(request: SendMailRequest, onSuccess?: (response: SendMailResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<CMSRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CMSRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CMSRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToQuotation(request: StandardRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetNextNumber(request: GetNextNumberRequest, onSuccess?: (response: GetNextNumberResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Services/CMS/Create",
            SendMail = "Services/CMS/SendMail",
            SendSMS = "Services/CMS/SendSMS",
            Update = "Services/CMS/Update",
            Delete = "Services/CMS/Delete",
            Retrieve = "Services/CMS/Retrieve",
            List = "Services/CMS/List",
            MoveToQuotation = "Services/CMS/MoveToQuotation",
            GetNextNumber = "Services/CMS/GetNextNumber"
        }

        [
            'Create', 
            'SendMail', 
            'SendSMS', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToQuotation', 
            'GetNextNumber'
        ].forEach(x => {
            (<any>CMSService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
