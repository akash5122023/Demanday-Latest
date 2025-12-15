namespace AdvanceCRM.Quotation {
    export namespace QuotationFollowupsService {
        export const baseUrl = 'Quotation/QuotationFollowups';

        export declare function Create(request: Serenity.SaveRequest<QuotationFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<QuotationFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<QuotationFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<QuotationFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMSReminder(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Quotation/QuotationFollowups/Create",
            Update = "Quotation/QuotationFollowups/Update",
            Delete = "Quotation/QuotationFollowups/Delete",
            Retrieve = "Quotation/QuotationFollowups/Retrieve",
            List = "Quotation/QuotationFollowups/List",
            SendSMSReminder = "Quotation/QuotationFollowups/SendSMSReminder"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'SendSMSReminder'
        ].forEach(x => {
            (<any>QuotationFollowupsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
