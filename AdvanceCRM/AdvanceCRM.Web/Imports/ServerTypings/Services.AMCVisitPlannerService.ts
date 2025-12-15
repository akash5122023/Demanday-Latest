namespace AdvanceCRM.Services {
    export namespace AMCVisitPlannerService {
        export const baseUrl = 'Services/AMCVisitPlanner';

        export declare function Create(request: Serenity.SaveRequest<AMCVisitPlannerRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<AMCVisitPlannerRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<AMCVisitPlannerRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<AMCVisitPlannerRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function SendVisitSMS(request: SendSMSRequest, onSuccess?: (response: StandardResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Services/AMCVisitPlanner/Create",
            Update = "Services/AMCVisitPlanner/Update",
            Delete = "Services/AMCVisitPlanner/Delete",
            Retrieve = "Services/AMCVisitPlanner/Retrieve",
            List = "Services/AMCVisitPlanner/List",
            SendVisitSMS = "Services/AMCVisitPlanner/SendVisitSMS"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List', 
            'SendVisitSMS'
        ].forEach(x => {
            (<any>AMCVisitPlannerService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
