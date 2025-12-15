namespace AdvanceCRM.Services {
    export interface CMSFollowupsForm {
        FollowupNote: Serenity.StringEditor;
        Details: Serenity.TextAreaEditor;
        FollowupDate: Serenity.DateTimeEditor;
        Status: Serenity.EnumEditor;
        RepresentativeId: Serenity.LookupEditor;
        ClosingDate: Serenity.DateTimeEditor;
        NoteList: Common.NotesEditor;
        CMSId: Serenity.IntegerEditor;
        ContactPhone: Serenity.StringEditor;
        ContactEmail: Serenity.StringEditor;
    }

    export class CMSFollowupsForm extends Serenity.PrefixedContext {
        static formKey = 'Services.CMSFollowups';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!CMSFollowupsForm.init)  {
                CMSFollowupsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.DateTimeEditor;
                var w3 = s.EnumEditor;
                var w4 = s.LookupEditor;
                var w5 = Common.NotesEditor;
                var w6 = s.IntegerEditor;

                Q.initFormType(CMSFollowupsForm, [
                    'FollowupNote', w0,
                    'Details', w1,
                    'FollowupDate', w2,
                    'Status', w3,
                    'RepresentativeId', w4,
                    'ClosingDate', w2,
                    'NoteList', w5,
                    'CMSId', w6,
                    'ContactPhone', w0,
                    'ContactEmail', w0
                ]);
            }
        }
    }
}
