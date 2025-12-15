namespace AdvanceCRM.Demanday {
    export namespace DemandayTeleMarketingContactsService {
        export const baseUrl = 'Demanday/DemandayTeleMarketingContacts';

        export declare function Create(request: Serenity.SaveRequest<DemandayTeleMarketingContactsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<DemandayTeleMarketingContactsRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<DemandayTeleMarketingContactsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<DemandayTeleMarketingContactsRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function ImportExcel(request: Serenity.ServiceRequest, onSuccess?: (response: Microsoft.AspNetCore.Mvc.IActionResult) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Demanday/DemandayTeleMarketingContacts/Create",
            Update = "Demanday/DemandayTeleMarketingContacts/Update",
            Delete = "Demanday/DemandayTeleMarketingContacts/Delete",
            Retrieve = "Demanday/DemandayTeleMarketingContacts/Retrieve",
            List = "Demanday/DemandayTeleMarketingContacts/List",
            ImportExcel = "Demanday/DemandayTeleMarketingContacts/ImportExcel"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'ImportExcel'
        ].forEach(x => {
            (<any>DemandayTeleMarketingContactsService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
