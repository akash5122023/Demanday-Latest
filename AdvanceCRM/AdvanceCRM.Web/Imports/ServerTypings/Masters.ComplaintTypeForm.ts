namespace AdvanceCRM.Masters {
    export interface ComplaintTypeForm {
        ComplaintType: Serenity.StringEditor;
    }

    export class ComplaintTypeForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.ComplaintType';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ComplaintTypeForm.init)  {
                ComplaintTypeForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ComplaintTypeForm, [
                    'ComplaintType', w0
                ]);
            }
        }
    }
}
