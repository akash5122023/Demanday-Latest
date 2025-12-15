namespace AdvanceCRM.Itinerary {
    export namespace ItineraryTermService {
        export const baseUrl = 'Itinerary/ItineraryTerm';

        export declare function Create(request: Serenity.SaveRequest<ItineraryTermRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Update(request: Serenity.SaveRequest<ItineraryTermRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ItineraryTermRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        export declare function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ItineraryTermRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;

        export declare const enum Methods {
            Create = "Itinerary/ItineraryTerm/Create",
            Update = "Itinerary/ItineraryTerm/Update",
            Delete = "Itinerary/ItineraryTerm/Delete",
            Retrieve = "Itinerary/ItineraryTerm/Retrieve",
            List = "Itinerary/ItineraryTerm/List"
        }

        [
            'Create', 
            'Update', 
            'Delete', 
            'Retrieve', 
            'List'
        ].forEach(x => {
            (<any>ItineraryTermService)[x] = function (r, s, o) {
                return Q.serviceRequest(baseUrl + '/' + x, r, s, o);
            };
        });
    }
}
