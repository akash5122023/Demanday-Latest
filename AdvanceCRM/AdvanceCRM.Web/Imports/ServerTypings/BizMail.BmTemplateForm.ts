namespace AdvanceCRM.BizMail {
    export interface BmTemplateForm {
        TemplateUid: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Content: Serenity.TextAreaEditor;
        InlineCss: Serenity.IntegerEditor;
    }

    export class BmTemplateForm extends Serenity.PrefixedContext {
        static formKey = 'Default.BmTemplate';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BmTemplateForm.init)  {
                BmTemplateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.IntegerEditor;

                Q.initFormType(BmTemplateForm, [
                    'TemplateUid', w0,
                    'Name', w0,
                    'Content', w1,
                    'InlineCss', w2
                ]);
            }
        }
    }
}
