namespace AdvanceCRM.Masters {
    export interface DemandayJobFunctionMasterForm {
        Name: Serenity.StringEditor;
    }

    export class DemandayJobFunctionMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.DemandayJobFunctionMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayJobFunctionMasterForm.init)  {
                DemandayJobFunctionMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(DemandayJobFunctionMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
