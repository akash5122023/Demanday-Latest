namespace AdvanceCRM.EBBCheck {
    export interface EBBCheckForm {
        FirstName: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Date: Serenity.DateTimeEditor;
        Status: Serenity.EnumEditor;
        OwnerId: Administration.UserEditor;
    }

    export class EBBCheckForm extends Serenity.PrefixedContext {
        static formKey = 'EBBCheck.EBBCheck';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EBBCheckForm.init)  {
                EBBCheckForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateTimeEditor;
                var w2 = s.EnumEditor;
                var w3 = Administration.UserEditor;

                Q.initFormType(EBBCheckForm, [
                    'FirstName', w0,
                    'Email', w0,
                    'Date', w1,
                    'Status', w2,
                    'OwnerId', w3
                ]);
            }
        }
    }
}
