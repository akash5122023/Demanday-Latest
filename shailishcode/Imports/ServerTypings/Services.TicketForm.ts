namespace AdvanceCRM.Services {
    export interface TicketForm {
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        ProductsId: Serenity.LookupEditor;
        ComplaintDetails: Serenity.TextAreaEditor;
        Priority: Serenity.EnumEditor;
        AdditionalDetails: Serenity.TextAreaEditor;
        AssignedId: Administration.UserEditor;
    }

    export class TicketForm extends Serenity.PrefixedContext {
        static formKey = 'Services.Ticket';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TicketForm.init)  {
                TicketForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.EnumEditor;
                var w4 = Administration.UserEditor;

                Q.initFormType(TicketForm, [
                    'Name', w0,
                    'Phone', w0,
                    'ProductsId', w1,
                    'ComplaintDetails', w2,
                    'Priority', w3,
                    'AdditionalDetails', w2,
                    'AssignedId', w4
                ]);
            }
        }
    }
}
