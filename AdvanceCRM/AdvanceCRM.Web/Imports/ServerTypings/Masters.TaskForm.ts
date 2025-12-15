namespace AdvanceCRM.Masters {
    export interface TaskForm {
        Task: Serenity.StringEditor;
    }

    export class TaskForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Task';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TaskForm.init)  {
                TaskForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(TaskForm, [
                    'Task', w0
                ]);
            }
        }
    }
}
