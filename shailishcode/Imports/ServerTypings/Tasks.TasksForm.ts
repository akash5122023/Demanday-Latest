namespace AdvanceCRM.Tasks {
    export interface TasksForm {
        Recurring: BooleanSwitchEditor;
        Period: Serenity.EnumEditor;
        ProjectId: Serenity.LookupEditor;
        TaskTitle: Serenity.StringEditor;
        TaskId: Serenity.LookupEditor;
        StatusId: Serenity.LookupEditor;
        TypeId: Serenity.LookupEditor;
        Priority: Serenity.EnumEditor;
        ContactsId: Serenity.LookupEditor;
        ProductId: Serenity.LookupEditor;
        CreationDate: Serenity.DateTimeEditor;
        ExpectedCompletion: Serenity.DateTimeEditor;
        CompletionDate: Serenity.DateTimeEditor;
        Details: Serenity.TextAreaEditor;
        Attachments: Serenity.MultipleImageUploadEditor;
        Resolution: Serenity.TextAreaEditor;
        AssignedBy: Administration.UserEditor;
        AssignedTo: Administration.UserEditor;
        WatcherList: Serenity.LookupEditor;
        NoteList: Common.NotesEditor;
        Timeline: Common.TimelineEditor;
    }

    export class TasksForm extends Serenity.PrefixedContext {
        static formKey = 'Tasks.Tasks';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TasksForm.init)  {
                TasksForm.init = true;

                var s = Serenity;
                var w0 = BooleanSwitchEditor;
                var w1 = s.EnumEditor;
                var w2 = s.LookupEditor;
                var w3 = s.StringEditor;
                var w4 = s.DateTimeEditor;
                var w5 = s.TextAreaEditor;
                var w6 = s.MultipleImageUploadEditor;
                var w7 = Administration.UserEditor;
                var w8 = Common.NotesEditor;
                var w9 = Common.TimelineEditor;

                Q.initFormType(TasksForm, [
                    'Recurring', w0,
                    'Period', w1,
                    'ProjectId', w2,
                    'TaskTitle', w3,
                    'TaskId', w2,
                    'StatusId', w2,
                    'TypeId', w2,
                    'Priority', w1,
                    'ContactsId', w2,
                    'ProductId', w2,
                    'CreationDate', w4,
                    'ExpectedCompletion', w4,
                    'CompletionDate', w4,
                    'Details', w5,
                    'Attachments', w6,
                    'Resolution', w5,
                    'AssignedBy', w7,
                    'AssignedTo', w7,
                    'WatcherList', w2,
                    'NoteList', w8,
                    'Timeline', w9
                ]);
            }
        }
    }
}
