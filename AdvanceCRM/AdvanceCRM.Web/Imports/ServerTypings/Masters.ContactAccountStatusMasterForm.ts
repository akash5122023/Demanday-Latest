namespace AdvanceCRM.Masters {
    export interface ContactAccountStatusMasterForm {
        Name: Serenity.StringEditor;
    }

    export class ContactAccountStatusMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ContactAccountStatusMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ContactAccountStatusMasterForm.init)  {
                ContactAccountStatusMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ContactAccountStatusMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
