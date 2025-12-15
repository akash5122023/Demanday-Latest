namespace AdvanceCRM.Services {
    export interface TeleCallingForm {
        ContactsId: Serenity.LookupEditor;
        ContactsName: Serenity.StringEditor;
        ContactsWhatsapp: Serenity.StringEditor;
        ContactsPhone: Serenity.StringEditor;
        Date: Serenity.DateEditor;
        AppointmentDate: Serenity.DateTimeEditor;
        ProductsId: Serenity.LookupEditor;
        Status: Serenity.EnumEditor;
        SourceId: Serenity.LookupEditor;
        StageId: Serenity.LookupEditor;
        BranchId: Serenity.LookupEditor;
        Details: Serenity.TextAreaEditor;
        Feedback: Serenity.TextAreaEditor;
        RepresentativeId: Administration.UserEditor;
        AssignedTo: Administration.UserEditor;
        NoteList: Common.NotesEditor;
        Timeline: Common.TimelineEditor;
    }

    export class TeleCallingForm extends Serenity.PrefixedContext {
        static formKey = 'Services.TeleCalling';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TeleCallingForm.init)  {
                TeleCallingForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateEditor;
                var w3 = s.DateTimeEditor;
                var w4 = s.EnumEditor;
                var w5 = s.TextAreaEditor;
                var w6 = Administration.UserEditor;
                var w7 = Common.NotesEditor;
                var w8 = Common.TimelineEditor;

                Q.initFormType(TeleCallingForm, [
                    'ContactsId', w0,
                    'ContactsName', w1,
                    'ContactsWhatsapp', w1,
                    'ContactsPhone', w1,
                    'Date', w2,
                    'AppointmentDate', w3,
                    'ProductsId', w0,
                    'Status', w4,
                    'SourceId', w0,
                    'StageId', w0,
                    'BranchId', w0,
                    'Details', w5,
                    'Feedback', w5,
                    'RepresentativeId', w6,
                    'AssignedTo', w6,
                    'NoteList', w7,
                    'Timeline', w8
                ]);
            }
        }
    }
}
