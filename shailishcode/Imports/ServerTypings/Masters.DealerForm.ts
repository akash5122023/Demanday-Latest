namespace AdvanceCRM.Masters {
    export interface DealerForm {
        DealerName: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        Country: Serenity.EnumEditor;
        StateId: Serenity.LookupEditor;
        CityId: Serenity.LookupEditor;
        Pin: Serenity.StringEditor;
        AdditionalInfo: Serenity.StringEditor;
    }

    export class DealerForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Dealer';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DealerForm.init)  {
                DealerForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.EnumEditor;
                var w2 = s.LookupEditor;

                Q.initFormType(DealerForm, [
                    'DealerName', w0,
                    'Phone', w0,
                    'Email', w0,
                    'Address', w0,
                    'Country', w1,
                    'StateId', w2,
                    'CityId', w2,
                    'Pin', w0,
                    'AdditionalInfo', w0
                ]);
            }
        }
    }
}
