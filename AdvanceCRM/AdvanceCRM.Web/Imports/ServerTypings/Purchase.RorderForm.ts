namespace AdvanceCRM.Purchase {
    export interface RorderForm {
        Name: Serenity.StringEditor;
        MinimumStock: Serenity.DecimalEditor;
        MaximumStock: Serenity.DecimalEditor;
        SellingPrice: Serenity.DecimalEditor;
    }

    export class RorderForm extends Serenity.PrefixedContext {
        static formKey = 'Purchase.Rorder';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!RorderForm.init)  {
                RorderForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DecimalEditor;

                Q.initFormType(RorderForm, [
                    'Name', w0,
                    'MinimumStock', w1,
                    'MaximumStock', w1,
                    'SellingPrice', w1
                ]);
            }
        }
    }
}
