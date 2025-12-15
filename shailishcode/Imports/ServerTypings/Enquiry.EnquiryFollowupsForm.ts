namespace AdvanceCRM.Enquiry {
    export interface EnquiryFollowupsForm {
        FollowupNote: Serenity.StringEditor;
        Details: Serenity.TextAreaEditor;
        FollowupDate: Serenity.DateTimeEditor;
        Status: Serenity.EnumEditor;
        RepresentativeId: Administration.UserEditor;
        ClosingDate: Serenity.DateTimeEditor;
        NoteList: Common.NotesEditor;
        EnquiryId: Serenity.IntegerEditor;
        ContactPhone: Serenity.StringEditor;
        ContactEmail: Serenity.StringEditor;
        ContactPersonPhone: Serenity.StringEditor;
        ContactPersonEmail: Serenity.StringEditor;
    }

    export class EnquiryFollowupsForm extends Serenity.PrefixedContext {
        static formKey = 'Enquiry.EnquiryFollowups';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EnquiryFollowupsForm.init)  {
                EnquiryFollowupsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.TextAreaEditor;
                var w2 = s.DateTimeEditor;
                var w3 = s.EnumEditor;
                var w4 = Administration.UserEditor;
                var w5 = Common.NotesEditor;
                var w6 = s.IntegerEditor;

                Q.initFormType(EnquiryFollowupsForm, [
                    'FollowupNote', w0,
                    'Details', w1,
                    'FollowupDate', w2,
                    'Status', w3,
                    'RepresentativeId', w4,
                    'ClosingDate', w2,
                    'NoteList', w5,
                    'EnquiryId', w6,
                    'ContactPhone', w0,
                    'ContactEmail', w0,
                    'ContactPersonPhone', w0,
                    'ContactPersonEmail', w0
                ]);
            }
        }
    }
}
