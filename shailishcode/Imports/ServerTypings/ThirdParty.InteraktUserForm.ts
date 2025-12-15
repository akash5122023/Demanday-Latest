namespace AdvanceCRM.ThirdParty {
    export interface InteraktUserForm {
        InteraktId: Serenity.StringEditor;
        Created: Serenity.DateEditor;
        Modified: Serenity.DateEditor;
        Phone: Serenity.StringEditor;
        CountryCode: Serenity.StringEditor;
        UserId: Serenity.StringEditor;
        FullName: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        WpOptedIn: Serenity.BooleanEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class InteraktUserForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.InteraktUser';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InteraktUserForm.init)  {
                InteraktUserForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.BooleanEditor;

                Q.initFormType(InteraktUserForm, [
                    'InteraktId', w0,
                    'Created', w1,
                    'Modified', w1,
                    'Phone', w0,
                    'CountryCode', w0,
                    'UserId', w0,
                    'FullName', w0,
                    'Email', w0,
                    'WpOptedIn', w2,
                    'IsMoved', w2
                ]);
            }
        }
    }
}
