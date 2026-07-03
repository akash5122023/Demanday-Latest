namespace AdvanceCRM.Masters {
    export interface DemandayCountryMasterForm {
        Name: Serenity.StringEditor;
    }

    export class DemandayCountryMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.DemandayCountryMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayCountryMasterForm.init)  {
                DemandayCountryMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(DemandayCountryMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
