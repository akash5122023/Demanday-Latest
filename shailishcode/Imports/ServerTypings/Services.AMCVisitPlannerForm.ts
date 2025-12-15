namespace AdvanceCRM.Services {
    export interface AMCVisitPlannerForm {
        VisitDate: Serenity.DateEditor;
        Status: Serenity.EnumEditor;
        Serial: Serenity.StringEditor;
        CompletionDate: Serenity.DateEditor;
        VisitDetails: Serenity.TextAreaEditor;
        Attachment: Serenity.MultipleImageUploadEditor;
        AssignedTo: Administration.UserEditor;
        RepresentativeId: Administration.UserEditor;
        NoteList: Common.NotesEditor;
        AMCId: Serenity.IntegerEditor;
        ContactPhone: Serenity.StringEditor;
        ContactEmail: Serenity.StringEditor;
    }

    export class AMCVisitPlannerForm extends Serenity.PrefixedContext {
        static formKey = 'Services.AMCVisitPlanner';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!AMCVisitPlannerForm.init)  {
                AMCVisitPlannerForm.init = true;

                var s = Serenity;
                var w0 = s.DateEditor;
                var w1 = s.EnumEditor;
                var w2 = s.StringEditor;
                var w3 = s.TextAreaEditor;
                var w4 = s.MultipleImageUploadEditor;
                var w5 = Administration.UserEditor;
                var w6 = Common.NotesEditor;
                var w7 = s.IntegerEditor;

                Q.initFormType(AMCVisitPlannerForm, [
                    'VisitDate', w0,
                    'Status', w1,
                    'Serial', w2,
                    'CompletionDate', w0,
                    'VisitDetails', w3,
                    'Attachment', w4,
                    'AssignedTo', w5,
                    'RepresentativeId', w5,
                    'NoteList', w6,
                    'AMCId', w7,
                    'ContactPhone', w2,
                    'ContactEmail', w2
                ]);
            }
        }
    }
}
