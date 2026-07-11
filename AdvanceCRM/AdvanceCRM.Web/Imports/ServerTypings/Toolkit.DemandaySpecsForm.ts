namespace AdvanceCRM.Toolkit {
    export interface DemandaySpecsForm {
        SrNo: Serenity.IntegerEditor;
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
                var w0 = s.IntegerEditor;
                var w1 = s.StringEditor;
                var w2 = Administration.UserEditor;

                Q.initFormType(DemandaySpecsForm, [
                    'SrNo', w0,
                    'OrderId', w1,
                    'JobTitle', w1,
                    'JobLevel', w1,
                    'JobFunction', w1,
                    'Industry', w1,
                    'CompanyEmployeeSize', w1,
                    'AnnualRevenue', w1,
                    'Address', w1,
                    'City', w1,
                    'State', w1,
                    'ZipCode', w1,
                    'Country', w1,
                    'Comments', w1,
                    'AdditionalNotes', w1,
                    'OwnerId', w2
                ]);
            }
        }
    }
}
