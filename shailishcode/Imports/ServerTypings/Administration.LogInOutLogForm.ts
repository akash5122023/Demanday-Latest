namespace AdvanceCRM.Administration {
    export interface LogInOutLogForm {
        Date: Serenity.DateEditor;
        Type: Serenity.EnumEditor;
        UserId: Serenity.IntegerEditor;
    }

    export class LogInOutLogForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.LogInOutLog';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!LogInOutLogForm.init)  {
                LogInOutLogForm.init = true;

                var s = Serenity;
                var w0 = s.DateEditor;
                var w1 = s.EnumEditor;
                var w2 = s.IntegerEditor;

                Q.initFormType(LogInOutLogForm, [
                    'Date', w0,
                    'Type', w1,
                    'UserId', w2
                ]);
            }
        }
    }
}
