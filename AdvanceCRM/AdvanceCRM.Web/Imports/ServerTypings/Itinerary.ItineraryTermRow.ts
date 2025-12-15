namespace AdvanceCRM.Itinerary {
    export interface ItineraryTermRow {
        Id?: number;
        DaysId?: number;
        ItineraryId?: number;
        DaysTitle?: string;
        DaysHeading?: string;
        DaysDescription?: string;
        DaysFileAttachments?: string;
        ItineraryHeadline?: string;
        ItineraryDate?: string;
        ItineraryDaysId?: number;
        ItineraryFrom?: string;
        ItineraryTo?: string;
        ItineraryAdults?: string;
        ItineraryChildrens?: string;
        ItineraryDestination?: string;
        ItineraryNights?: string;
        ItineraryHotelName?: string;
        ItineraryMealPlan?: string;
        ItineraryAmount?: number;
        ItineraryTermsAndConditions?: string;
    }

    export namespace ItineraryTermRow {
        export const idProperty = 'Id';
        export const localTextPrefix = 'Itinerary.ItineraryTerm';
        export const deletePermission = 'Itinerary:Read';
        export const insertPermission = 'Itinerary:Read';
        export const readPermission = 'Itinerary:Read';
        export const updatePermission = 'Itinerary:Read';

        export declare const enum Fields {
            Id = "Id",
            DaysId = "DaysId",
            ItineraryId = "ItineraryId",
            DaysTitle = "DaysTitle",
            DaysHeading = "DaysHeading",
            DaysDescription = "DaysDescription",
            DaysFileAttachments = "DaysFileAttachments",
            ItineraryHeadline = "ItineraryHeadline",
            ItineraryDate = "ItineraryDate",
            ItineraryDaysId = "ItineraryDaysId",
            ItineraryFrom = "ItineraryFrom",
            ItineraryTo = "ItineraryTo",
            ItineraryAdults = "ItineraryAdults",
            ItineraryChildrens = "ItineraryChildrens",
            ItineraryDestination = "ItineraryDestination",
            ItineraryNights = "ItineraryNights",
            ItineraryHotelName = "ItineraryHotelName",
            ItineraryMealPlan = "ItineraryMealPlan",
            ItineraryAmount = "ItineraryAmount",
            ItineraryTermsAndConditions = "ItineraryTermsAndConditions"
        }
    }
}
