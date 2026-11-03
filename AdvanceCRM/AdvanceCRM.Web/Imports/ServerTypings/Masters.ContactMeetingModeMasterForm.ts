namespace AdvanceCRM.Masters {
    export interface ContactMeetingModeMasterForm {
        Name: Serenity.StringEditor;
    }

    export class ContactMeetingModeMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ContactMeetingModeMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ContactMeetingModeMasterForm.init)  {
                ContactMeetingModeMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ContactMeetingModeMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
