namespace AdvanceCRM.Masters {
    export interface DemandayJobLevelMasterForm {
        Name: Serenity.StringEditor;
    }

    export class DemandayJobLevelMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.DemandayJobLevelMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayJobLevelMasterForm.init)  {
                DemandayJobLevelMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(DemandayJobLevelMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
