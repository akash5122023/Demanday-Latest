namespace AdvanceCRM.Contacts {
    export namespace ContactsService {
        export const baseUrl = 'Contacts/Contacts';

        export declare function Create(request: Serenity.SaveRequest<ContactsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ContactsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: ContactsListRequest, onSuccess?: (response: Serenity.ListResponse<ContactsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendBulkSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function AddToMailChimp(request: MailChimpRequest, onSuccess?: (response: MailChimpResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ExcelImport(request: ExcelImportRequest, onSuccess?: (response: ExcelImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ExcelImportSubContacts(request: ExcelImportRequest, onSuccess?: (response: ExcelImportResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ContactsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Contacts/Contacts/Create",
            Update = "Contacts/Contacts/Update",
            Delete = "Contacts/Contacts/Delete",
            List = "Contacts/Contacts/List",
            SendBulkSMS = "Contacts/Contacts/SendBulkSMS",
            AddToMailChimp = "Contacts/Contacts/AddToMailChimp",
            ExcelImport = "Contacts/Contacts/ExcelImport",
            ExcelImportSubContacts = "Contacts/Contacts/ExcelImportSubContacts",
            Retrieve = "Contacts/Contacts/Retrieve"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'List', 
            'SendBulkSMS', 
            'AddToMailChimp', 
            'ExcelImport', 
            'ExcelImportSubContacts', 
            'Retrieve'
        ].forEach(x => {
            (<any>ContactsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
