namespace AdvanceCRM.Administration {
    export interface AppointmentSmsLogForm {
        Date: Serenity.DateEditor;
        Log: Serenity.StringEditor;
    }

    export class AppointmentSmsLogForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.AppointmentSmsLog';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AppointmentSmsLogForm.init)  {
                AppointmentSmsLogForm.init = true;

                var s = Serenity;
                var w0 = s.DateEditor;
                var w1 = s.StringEditor;

                Q.initFormType(AppointmentSmsLogForm, [
                    'Date', w0,
                    'Log', w1
                ]);
            }
        }
    }
}
