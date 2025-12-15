namespace AdvanceCRM.Enquiry {
    export namespace EnquiryFollowupsService {
        export const baseUrl = 'Enquiry/EnquiryFollowups';

        export declare function Create(request: Serenity.SaveRequest<EnquiryFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<EnquiryFollowupsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<EnquiryFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<EnquiryFollowupsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendSMSReminder(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Enquiry/EnquiryFollowups/Create",
            Update = "Enquiry/EnquiryFollowups/Update",
            Delete = "Enquiry/EnquiryFollowups/Delete",
            Retrieve = "Enquiry/EnquiryFollowups/Retrieve",
            List = "Enquiry/EnquiryFollowups/List",
            SendSMSReminder = "Enquiry/EnquiryFollowups/SendSMSReminder"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'SendSMSReminder'
        ].forEach(x => {
            (<any>EnquiryFollowupsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
