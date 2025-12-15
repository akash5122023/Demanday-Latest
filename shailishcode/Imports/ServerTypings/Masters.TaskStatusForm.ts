namespace AdvanceCRM.Masters {
    export interface TaskStatusForm {
        Status: Serenity.StringEditor;
    }

    export class TaskStatusForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.TaskStatus';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TaskStatusForm.init)  {
                TaskStatusForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(TaskStatusForm, [
                    'Status', w0
                ]);
            }
        }
    }
}
