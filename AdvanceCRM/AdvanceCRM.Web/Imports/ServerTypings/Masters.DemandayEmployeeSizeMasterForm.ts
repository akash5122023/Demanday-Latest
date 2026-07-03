namespace AdvanceCRM.Masters {
    export interface DemandayEmployeeSizeMasterForm {
        Name: Serenity.StringEditor;
    }

    export class DemandayEmployeeSizeMasterForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.DemandayEmployeeSizeMaster';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayEmployeeSizeMasterForm.init)  {
                DemandayEmployeeSizeMasterForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(DemandayEmployeeSizeMasterForm, [
                    'Name', w0
                ]);
            }
        }
    }
}
