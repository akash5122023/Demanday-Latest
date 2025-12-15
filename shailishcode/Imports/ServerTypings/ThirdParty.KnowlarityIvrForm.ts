namespace AdvanceCRM.ThirdParty {
    export interface KnowlarityIvrForm {
        Name: Serenity.StringEditor;
        Mobile: Serenity.StringEditor;
        EmpMobile: Serenity.StringEditor;
        IvrNo: Serenity.StringEditor;
        Recording: Serenity.StringEditor;
        Date: Serenity.StringEditor;
        Duration: Serenity.StringEditor;
    }

    export class KnowlarityIvrForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.KnowlarityIvr';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!KnowlarityIvrForm.init)  {
                KnowlarityIvrForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(KnowlarityIvrForm, [
                    'Name', w0,
                    'Mobile', w0,
                    'EmpMobile', w0,
                    'IvrNo', w0,
                    'Recording', w0,
                    'Date', w0,
                    'Duration', w0
                ]);
            }
        }
    }
}
