namespace AdvanceCRM.Sales {
    export interface InvoiceAppointmentsForm {
        Details: Serenity.TextAreaEditor;
        AppointmentDate: Serenity.DateTimeEditor;
        Status: Serenity.EnumEditor;
        RepresentativeId: Administration.UserEditor;
        MinutesOfMeeting: Serenity.TextAreaEditor;
        Reason: Serenity.TextAreaEditor;
        NoteList: Common.NotesEditor;
        InvoiceId: Serenity.IntegerEditor;
    }

    export class InvoiceAppointmentsForm extends Serenity.PrefixedContext {
        static formKey = 'Sales.InvoiceAppointments';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!InvoiceAppointmentsForm.init)  {
                InvoiceAppointmentsForm.init = true;

                var s = Serenity;
                var w0 = s.TextAreaEditor;
                var w1 = s.DateTimeEditor;
                var w2 = s.EnumEditor;
                var w3 = Administration.UserEditor;
                var w4 = Common.NotesEditor;
                var w5 = s.IntegerEditor;

                Q.initFormType(InvoiceAppointmentsForm, [
                    'Details', w0,
                    'AppointmentDate', w1,
                    'Status', w2,
                    'RepresentativeId', w3,
                    'MinutesOfMeeting', w0,
                    'Reason', w0,
                    'NoteList', w4,
                    'InvoiceId', w5
                ]);
            }
        }
    }
}
