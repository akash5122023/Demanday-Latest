namespace AdvanceCRM.Settings {

    export interface CouponCodeForm {
        Code: Serenity.StringEditor;
        DiscountType: Serenity.StringEditor;
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

            if (!CouponCodeForm.init) {
                CouponCodeForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.IntegerEditor;
                var w3 = s.DateEditor;
                var w4 = s.BooleanEditor;

                Q.initFormType(CouponCodeForm, [
                    'Code', w0,
                    'DiscountType', w0,
                    'DiscountValue', w1,
                    'MaxUsageCount', w2,
                    'ExpiryDate', w3,
                    'IsActive', w4
                ]);
            }
        }
    }
}
