namespace AdvanceCRM.Masters {
    export interface TeamsForm {
        Team: Serenity.StringEditor;
        UserId: Administration.UserEditor;
    }

    export class TeamsForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Teams';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TeamsForm.init)  {
                TeamsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = Administration.UserEditor;

                Q.initFormType(TeamsForm, [
                    'Team', w0,
                    'UserId', w1
                ]);
            }
        }
    }
}
