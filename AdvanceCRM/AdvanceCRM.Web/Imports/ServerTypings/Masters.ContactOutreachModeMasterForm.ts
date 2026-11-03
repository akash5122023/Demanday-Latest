namespace AdvanceCRM.Masters {
    export interface ContactOutreachModeMasterForm {
        Name: Serenity.StringEditor;
    }

    export class ContactOutreachModeMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ContactOutreachModeMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ContactOutreachModeMasterForm.init)  {
                ContactOutreachModeMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ContactOutreachModeMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
