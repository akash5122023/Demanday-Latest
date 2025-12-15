namespace AdvanceCRM.Settings {
    export interface ProductModuleForm {
        Name: Serenity.StringEditor;
        DisplayName: Serenity.StringEditor;
        Description: Serenity.TextAreaEditor;
        IsActive: Serenity.BooleanEditor;
        Price: Serenity.DecimalEditor;
        Currency: Serenity.StringEditor;
    }

    export class ProductModuleForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.ProductModule';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ProductModuleForm.init)  {
                ProductModuleForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.BooleanEditor;
                var w3 = s.DecimalEditor;

                Q.initFormType(ProductModuleForm, [
                    'Name', w0,
                    'DisplayName', w0,
                    'Description', w1,
                    'IsActive', w2,
                    'Price', w3,
                    'Currency', w0
                ]);
            }
        }
    }
}
