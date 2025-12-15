namespace AdvanceCRM.Masters {
    export interface StateForm {
        State: Serenity.StringEditor;
    }

    export class StateForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.State';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!StateForm.init)  {
                StateForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(StateForm, [
                    'State', w0
                ]);
            }
        }
    }
}
