namespace AdvanceCRM.Settings {
    export interface ProductPlanModuleForm {
        PlanId: Serenity.LookupEditor;
        ModuleId: Serenity.LookupEditor;
    }

    export class ProductPlanModuleForm extends Serenity.PrefixedContext {
        static formKey = 'Settings.ProductPlanModule';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ProductPlanModuleForm.init)  {
                ProductPlanModuleForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(ProductPlanModuleForm, [
                    'PlanId', w0,
                    'ModuleId', w0
                ]);
            }
        }
    }
}
