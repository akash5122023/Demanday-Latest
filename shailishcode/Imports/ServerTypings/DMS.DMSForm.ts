namespace AdvanceCRM.DMS {
    export interface DMSForm {
        Title: Serenity.StringEditor;
        Files: Serenity.MultipleImageUploadEditor;
        OwnerId: Administration.UserEditor;
        CreateDate: Serenity.DateEditor;
        LastUpdatedId: Administration.UserEditor;
        UpdateDate: Serenity.DateEditor;
        AssignedId: Administration.UserEditor;
    }

    export class DMSForm extends Serenity.PrefixedContext {
        static formKey = 'DMS.DMS';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DMSForm.init)  {
                DMSForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.MultipleImageUploadEditor;
                var w2 = Administration.UserEditor;
                var w3 = s.DateEditor;

                Q.initFormType(DMSForm, [
                    'Title', w0,
                    'Files', w1,
                    'OwnerId', w2,
                    'CreateDate', w3,
                    'LastUpdatedId', w2,
                    'UpdateDate', w3,
                    'AssignedId', w2
                ]);
            }
        }
    }
}
