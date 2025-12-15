namespace AdvanceCRM.Sales {
    export namespace OutwardService {
        export const baseUrl = 'Sales/Outward';

        export declare function Create(request: Serenity.SaveRequest<OutwardRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<OutwardRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<OutwardRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<OutwardRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function MoveToInward(request: SendMailRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function GetNextNumber(request: GetNextNumberRequest, onSuccess?: (response: GetNextNumberResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Sales/Outward/Create",
            Update = "Sales/Outward/Update",
            Delete = "Sales/Outward/Delete",
            Retrieve = "Sales/Outward/Retrieve",
            List = "Sales/Outward/List",
            MoveToInward = "Sales/Outward/MoveToInward",
            GetNextNumber = "Sales/Outward/GetNextNumber"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'MoveToInward', 
            'GetNextNumber'
        ].forEach(x => {
            (<any>OutwardService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
