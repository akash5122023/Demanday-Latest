namespace AdvanceCRM.Services {
    export interface TeleCallingFollowupsForm {
        FollowupNote: Serenity.StringEditor;
        Details: Serenity.TextAreaEditor;
        FollowupDate: Serenity.DateTimeEditor;
        Status: Serenity.EnumEditor;
        RepresentativeId: Administration.UserEditor;
        ClosingDate: Serenity.DateTimeEditor;
        NoteList: Common.NotesEditor;
        TeleCallingId: Serenity.IntegerEditor;
        ContactPhone: Serenity.StringEditor;
        ContactEmail: Serenity.StringEditor;
    }

    export class TeleCallingFollowupsForm extends Serenity.PrefixedContext {
        static formKey = 'Services.TeleCallingFollowups';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TeleCallingFollowupsForm.init)  {
                TeleCallingFollowupsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.DateTimeEditor;
                var w3 = s.EnumEditor;
                var w4 = Administration.UserEditor;
                var w5 = Common.NotesEditor;
                var w6 = s.IntegerEditor;

                Q.initFormType(TeleCallingFollowupsForm, [
                    'FollowupNote', w0,
                    'Details', w1,
                    'FollowupDate', w2,
                    'Status', w3,
                    'RepresentativeId', w4,
                    'ClosingDate', w2,
                    'NoteList', w5,
                    'TeleCallingId', w6,
                    'ContactPhone', w0,
                    'ContactEmail', w0
                ]);
            }
        }
    }
}
