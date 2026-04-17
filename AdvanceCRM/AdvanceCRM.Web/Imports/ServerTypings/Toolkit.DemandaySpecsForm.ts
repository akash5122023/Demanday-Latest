namespace AdvanceCRM.Toolkit {
    export interface DemandaySpecsForm {
        OrderId: Serenity.StringEditor;
        JobTitle: Serenity.StringEditor;
        JobLevel: Serenity.StringEditor;
        JobFunction: Serenity.StringEditor;
        Industry: Serenity.StringEditor;
        CompanyEmployeeSize: Serenity.StringEditor;
        AnnualRevenue: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        City: Serenity.StringEditor;
        State: Serenity.StringEditor;
        ZipCode: Serenity.StringEditor;
        Country: Serenity.StringEditor;
        Comments: Serenity.StringEditor;
        AdditionalNotes: Serenity.StringEditor;
        OwnerId: Administration.UserEditor;
    }

    export class DemandaySpecsForm extends Serenity.PrefixedContext {
        static formKey = 'Toolkit.DemandaySpecs';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandaySpecsForm.init)  {
                DemandaySpecsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = Administration.UserEditor;

                Q.initFormType(DemandaySpecsForm, [
                    'OrderId', w0,
                    'JobTitle', w0,
                    'JobLevel', w0,
                    'JobFunction', w0,
                    'Industry', w0,
                    'CompanyEmployeeSize', w0,
                    'AnnualRevenue', w0,
                    'Address', w0,
                    'City', w0,
                    'State', w0,
                    'ZipCode', w0,
                    'Country', w0,
                    'Comments', w0,
                    'AdditionalNotes', w0,
                    'OwnerId', w1
                ]);
            }
        }
    }
}
