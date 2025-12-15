namespace AdvanceCRM.Products {
    export interface ItineraryRow {
        Id?: number;
        Headline?: string;
        Date?: string;
        DaysId?: number[];
        From?: string;
        To?: string;
        Adults?: string;
        Childrens?: string;
        Destination?: string;
        Nights?: string;
        HotelName?: string;
        MealPlan?: string;
        Amount?: number;
        TermsAndConditions?: string;
    }

    export namespace ItineraryRow {
        export const idProperty = 'Id';
        export const nameProperty = 'Headline';
        export const localTextPrefix = 'Products.Itinerary';
        export const lookupKey = 'Products.Itinerary';

        export function getLookup(): Q.Lookup<ItineraryRow> {
            return Q.getLookup<ItineraryRow>('Products.Itinerary');
        }
        export const deletePermission = 'Itinerary:Modify';
        export const insertPermission = 'Itinerary:Modify';
        export const readPermission = 'Itinerary:Read';
        export const updatePermission = 'Itinerary:Modify';

        export declare const enum Fields {
            Id = "Id",
            Headline = "Headline",
            Date = "Date",
            DaysId = "DaysId",
            From = "From",
            To = "To",
            Adults = "Adults",
            Childrens = "Childrens",
            Destination = "Destination",
            Nights = "Nights",
            HotelName = "HotelName",
            MealPlan = "MealPlan",
            Amount = "Amount",
            TermsAndConditions = "TermsAndConditions"
        }
    }
}
