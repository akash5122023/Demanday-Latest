namespace AdvanceCRM.Administration {
    export interface BranchForm {
        Branch: Serenity.StringEditor;
        Phone: Serenity.MaskedEditor;
        Email: Serenity.EmailEditor;
        Address: Serenity.TextAreaEditor;
        Country: Serenity.EnumEditor;
        StateId: Serenity.LookupEditor;
        CityId: Serenity.LookupEditor;
        Pin: Serenity.StringEditor;
        CompanyId: Serenity.IntegerEditor;
    }

    export class BranchForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.Branch';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BranchForm.init)  {
                BranchForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.MaskedEditor;
                var w2 = s.EmailEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.EnumEditor;
                var w5 = s.LookupEditor;
                var w6 = s.IntegerEditor;

                Q.initFormType(BranchForm, [
                    'Branch', w0,
                    'Phone', w1,
                    'Email', w2,
                    'Address', w3,
                    'Country', w4,
                    'StateId', w5,
                    'CityId', w5,
                    'Pin', w0,
                    'CompanyId', w6
                ]);
            }
        }
    }
}
