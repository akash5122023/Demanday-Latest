namespace AdvanceCRM.Masters {
    export interface ContactDispositionMasterForm {
        Name: Serenity.StringEditor;
    }

    export class ContactDispositionMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ContactDispositionMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ContactDispositionMasterForm.init)  {
                ContactDispositionMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ContactDispositionMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
