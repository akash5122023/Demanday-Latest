namespace AdvanceCRM.Settings {
    export interface KnowlarityAgentsForm {
        Name: Serenity.StringEditor;
        Number: Serenity.StringEditor;
    }

    export class KnowlarityAgentsForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.KnowlarityAgents';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!KnowlarityAgentsForm.init)  {
                KnowlarityAgentsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(KnowlarityAgentsForm, [
                    'Name', w0,
                    'Number', w0
                ]);
            }
        }
    }
}
