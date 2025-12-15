namespace AdvanceCRM.DMS {
    export interface DMSFolderForm {
        ParentId: Serenity.LookupEditor;
        Title: Serenity.StringEditor;
        OwnerId: Administration.UserEditor;
        CreateDate: Serenity.DateEditor;
    }

    export class DMSFolderForm extends Serenity.PrefixedContext {
        static formKey = 'DMS.Folder';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DMSFolderForm.init)  {
                DMSFolderForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = Administration.UserEditor;
                var w3 = s.DateEditor;

                Q.initFormType(DMSFolderForm, [
                    'ParentId', w0,
                    'Title', w1,
                    'OwnerId', w2,
                    'CreateDate', w3
                ]);
            }
        }
    }
}
