namespace AdvanceCRM.Masters {
    export interface DemandaySubIndustryMasterForm {
        Name: Serenity.StringEditor;
    }

    export class DemandaySubIndustryMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.DemandaySubIndustryMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandaySubIndustryMasterForm.init)  {
                DemandaySubIndustryMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(DemandaySubIndustryMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
