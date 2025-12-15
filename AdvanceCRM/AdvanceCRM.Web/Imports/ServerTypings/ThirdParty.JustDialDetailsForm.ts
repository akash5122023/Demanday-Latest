namespace AdvanceCRM.ThirdParty {
    export interface JustDialDetailsForm {
        LeadId: Serenity.StringEditor;
        LeadType: Serenity.StringEditor;
        Prefix: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Mobile: Serenity.StringEditor;
        Landline: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        DateTime: Serenity.DateEditor;
        Category: Serenity.StringEditor;
        City: Serenity.StringEditor;
        Area: Serenity.StringEditor;
        BranchArea: Serenity.StringEditor;
        DcnMobile: Serenity.BooleanEditor;
        DcnPhone: Serenity.BooleanEditor;
        Company: Serenity.StringEditor;
        Pin: Serenity.StringEditor;
        BranhPin: Serenity.StringEditor;
        ParentId: Serenity.StringEditor;
        Feedback: Serenity.TextAreaEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class JustDialDetailsForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.JustDialDetails';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!JustDialDetailsForm.init)  {
                JustDialDetailsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateEditor;
                var w2 = s.BooleanEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(JustDialDetailsForm, [
                    'LeadId', w0,
                    'LeadType', w0,
                    'Prefix', w0,
                    'Name', w0,
                    'Mobile', w0,
                    'Landline', w0,
                    'Email', w0,
                    'DateTime', w1,
                    'Category', w0,
                    'City', w0,
                    'Area', w0,
                    'BranchArea', w0,
                    'DcnMobile', w2,
                    'DcnPhone', w2,
                    'Company', w0,
                    'Pin', w0,
                    'BranhPin', w0,
                    'ParentId', w0,
                    'Feedback', w3,
                    'IsMoved', w2
                ]);
            }
        }
    }
}
