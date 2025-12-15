namespace AdvanceCRM.Masters {
    export interface TaskTypeForm {
        Type: Serenity.StringEditor;
    }

    export class TaskTypeForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.TaskType';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TaskTypeForm.init)  {
                TaskTypeForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(TaskTypeForm, [
                    'Type', w0
                ]);
            }
        }
    }
}
