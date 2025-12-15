namespace AdvanceCRM.Masters {
    export interface TransportationForm {
        Name: Serenity.StringEditor;
        Address: Serenity.TextAreaEditor;
        Phone: Serenity.MaskedEditor;
        Email: Serenity.EmailEditor;
        ContactPerson: Serenity.StringEditor;
        ContactPersonPhone: Serenity.MaskedEditor;
        GSTIN: Serenity.StringEditor;
    }

    export class TransportationForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Transportation';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TransportationForm.init)  {
                TransportationForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.MaskedEditor;
                var w3 = s.EmailEditor;

                Q.initFormType(TransportationForm, [
                    'Name', w0,
                    'Address', w1,
                    'Phone', w2,
                    'Email', w3,
                    'ContactPerson', w0,
                    'ContactPersonPhone', w2,
                    'GSTIN', w0
                ]);
            }
        }
    }
}
