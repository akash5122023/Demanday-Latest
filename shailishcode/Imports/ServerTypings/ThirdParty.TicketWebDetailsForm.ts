namespace AdvanceCRM.ThirdParty {
    export interface TicketWebDetailsForm {
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        DateTime: Serenity.DateEditor;
        ProductName: Serenity.StringEditor;
        PurchaseDate: Serenity.DateEditor;
        Address: Serenity.TextAreaEditor;
        Requirement: Serenity.StringEditor;
        ComplaintDetails: Serenity.TextAreaEditor;
        Attachment: Serenity.MultipleImageUploadEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class TicketWebDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.TicketWebDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TicketWebDetailsForm.init)  {
                TicketWebDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.MultipleImageUploadEditor;
                var w4 = s.BooleanEditor;

                Q.initFormType(TicketWebDetailsForm, [
                    'Name', w0,
                    'Phone', w0,
                    'Email', w0,
                    'DateTime', w1,
                    'ProductName', w0,
                    'PurchaseDate', w1,
                    'Address', w2,
                    'Requirement', w0,
                    'ComplaintDetails', w2,
                    'Attachment', w3,
                    'IsMoved', w4
                ]);
            }
        }
    }
}
