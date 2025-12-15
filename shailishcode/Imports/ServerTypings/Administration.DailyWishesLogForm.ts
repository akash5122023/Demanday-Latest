namespace AdvanceCRM.Administration {
    export interface DailyWishesLogForm {
        Date: Serenity.DateEditor;
        Log: Serenity.StringEditor;
    }

    export class DailyWishesLogForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.DailyWishesLog';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DailyWishesLogForm.init)  {
                DailyWishesLogForm.init = true;

                var s = Serenity;
                var w0 = s.DateEditor;
                var w1 = s.StringEditor;

                Q.initFormType(DailyWishesLogForm, [
                    'Date', w0,
                    'Log', w1
                ]);
            }
        }
    }
}
