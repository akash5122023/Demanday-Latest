namespace AdvanceCRM.Masters {
    export interface ProjectForm {
        Project: Serenity.StringEditor;
    }

    export class ProjectForm extends Serenity.PrefixedContext {
        static formKey = 'Masters.Project';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!ProjectForm.init)  {
                ProjectForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;

                Q.initFormType(ProjectForm, [
                    'Project', w0
                ]);
            }
        }
    }
}
