namespace AdvanceCRM.Masters {
    export interface ContactAccountTypeMasterForm {
        Name: Serenity.StringEditor;
    }

    export class ContactAccountTypeMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ContactAccountTypeMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ContactAccountTypeMasterForm.init)  {
                ContactAccountTypeMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ContactAccountTypeMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
