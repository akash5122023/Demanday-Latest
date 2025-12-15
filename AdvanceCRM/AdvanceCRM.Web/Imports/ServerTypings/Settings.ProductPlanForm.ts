namespace AdvanceCRM.Settings {
    export interface ProductPlanForm {
        Name: Serenity.StringEditor;
        PricePerUser: Serenity.DecimalEditor;
        TrialDays: Serenity.IntegerEditor;
        UserLimit: Serenity.IntegerEditor;
        NonOperationalUsers: Serenity.IntegerEditor;
        Currency: Serenity.StringEditor;
        IsActive: Serenity.BooleanEditor;
        SortOrder: Serenity.IntegerEditor;
        BadgeLabel: Serenity.EnumEditor;
        BadgeHighlight: Serenity.BooleanEditor;
        ModuleList: Serenity.LookupEditor;
        FeatureList: Serenity.LookupEditor;
    }

    export class ProductPlanForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.ProductPlan';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ProductPlanForm.init)  {
                ProductPlanForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.IntegerEditor;
                var w3 = s.BooleanEditor;
                var w4 = s.EnumEditor;
                var w5 = s.LookupEditor;

                Q.initFormType(ProductPlanForm, [
                    'Name', w0,
                    'PricePerUser', w1,
                    'TrialDays', w2,
                    'UserLimit', w2,
                    'NonOperationalUsers', w2,
                    'Currency', w0,
                    'IsActive', w3,
                    'SortOrder', w2,
                    'BadgeLabel', w4,
                    'BadgeHighlight', w3,
                    'ModuleList', w5,
                    'FeatureList', w5
                ]);
            }
        }
    }
}
