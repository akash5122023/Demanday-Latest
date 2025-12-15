namespace AdvanceCRM.Products {
    export interface ItineraryForm {
        Headline: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        From: Serenity.StringEditor;
        To: Serenity.StringEditor;
        Destination: Serenity.StringEditor;
        HotelName: Serenity.StringEditor;
        Nights: Serenity.StringEditor;
        Adults: Serenity.StringEditor;
        Childrens: Serenity.StringEditor;
        MealPlan: Serenity.StringEditor;
        DaysId: Serenity.LookupEditor;
        Amount: Serenity.DecimalEditor;
        TermsAndConditions: Serenity.TextAreaEditor;
    }

    export class ItineraryForm extends Serenity.PrefixedContext {
        static formKey = 'Products.Itinerary';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ItineraryForm.init)  {
                ItineraryForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.LookupEditor;
                var w3 = s.DecimalEditor;
                var w4 = s.TextAreaEditor;

                Q.initFormType(ItineraryForm, [
                    'Headline', w0,
                    'Date', w1,
                    'From', w0,
                    'To', w0,
                    'Destination', w0,
                    'HotelName', w0,
                    'Nights', w0,
                    'Adults', w0,
                    'Childrens', w0,
                    'MealPlan', w0,
                    'DaysId', w2,
                    'Amount', w3,
                    'TermsAndConditions', w4
                ]);
            }
        }
    }
}
