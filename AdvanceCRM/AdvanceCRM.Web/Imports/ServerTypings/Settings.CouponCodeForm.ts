namespace AdvanceCRM.Settings {
    export interface CouponCodeForm {
        Code: Serenity.StringEditor;
        DiscountType: Serenity.LookupEditor;
        DiscountValue: Serenity.DecimalEditor;
        MaxUsageCount: Serenity.IntegerEditor;
        ExpiryDate: Serenity.DateEditor;
        IsActive: Serenity.BooleanEditor;
    }

    export class CouponCodeForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.CouponCode';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CouponCodeForm.init)  {
                CouponCodeForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.LookupEditor;
                var w2 = s.DecimalEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.DateEditor;
                var w5 = s.BooleanEditor;

                Q.initFormType(CouponCodeForm, [
                    'Code', w0,
                    'DiscountType', w1,
                    'DiscountValue', w2,
                    'MaxUsageCount', w3,
                    'ExpiryDate', w4,
                    'IsActive', w5
                ]);
            }
        }
    }
}
