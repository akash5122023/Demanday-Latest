namespace AdvanceCRM.Premium {
    export interface TargetSettingForm {
        Type: Serenity.EnumEditor;
        MonthlyTarget: Serenity.IntegerEditor;
        MonthlyTargetAmount: Serenity.DecimalEditor;
        Representative: Administration.UserEditor;
    }

    export class TargetSettingForm extends Serenity.PrefixedContext {
        static formKey = 'Premium.TargetSetting';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TargetSettingForm.init)  {
                TargetSettingForm.init = true;

                var s = Serenity;
                var w0 = s.EnumEditor;
                var w1 = s.IntegerEditor;
                var w2 = s.DecimalEditor;
                var w3 = Administration.UserEditor;

                Q.initFormType(TargetSettingForm, [
                    'Type', w0,
                    'MonthlyTarget', w1,
                    'MonthlyTargetAmount', w2,
                    'Representative', w3
                ]);
            }
        }
    }
}
