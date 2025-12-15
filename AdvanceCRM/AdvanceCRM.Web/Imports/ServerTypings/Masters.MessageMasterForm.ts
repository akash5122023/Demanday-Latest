namespace AdvanceCRM.Masters {
    export interface MessageMasterForm {
        Message: Serenity.StringEditor;
    }

    export class MessageMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.MessageMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!MessageMasterForm.init)  {
                MessageMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(MessageMasterForm, [
                    'Message', w0
                ]);
            }
        }
    }
}
