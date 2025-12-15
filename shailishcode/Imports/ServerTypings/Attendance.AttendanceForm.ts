namespace AdvanceCRM.Attendance {
    export interface AttendanceForm {
        Name: Administration.UserEditor;
        Type: Serenity.EnumEditor;
        DateNTime: Serenity.DateEditor;
        Coordinates: Serenity.StringEditor;
        Location: Serenity.TextAreaEditor;
        PunchIn: Serenity.DateTimeEditor;
        PunchOut: Serenity.DateTimeEditor;
        Distance: Serenity.DecimalEditor;
    }

    export class AttendanceForm extends Serenity.PrefixedContext {
        static formKey = 'Attendance.Attendance';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AttendanceForm.init)  {
                AttendanceForm.init = true;

                var s = Serenity;
                var w0 = Administration.UserEditor;
                var w1 = s.EnumEditor;
                var w2 = s.DateEditor;
                var w3 = s.StringEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.DateTimeEditor;
                var w6 = s.DecimalEditor;

                Q.initFormType(AttendanceForm, [
                    'Name', w0,
                    'Type', w1,
                    'DateNTime', w2,
                    'Coordinates', w3,
                    'Location', w4,
                    'PunchIn', w5,
                    'PunchOut', w5,
                    'Distance', w6
                ]);
            }
        }
    }
}
