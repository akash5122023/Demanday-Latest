namespace AdvanceCRM.Employee {
    export interface EmployeeAssestsForm {
        Items: Serenity.StringEditor;
        Quantity: Serenity.IntegerEditor;
        Description: Serenity.StringEditor;
        EmployeeId: Serenity.IntegerEditor;
    }

    export class EmployeeAssestsForm extends Serenity.PrefixedContext {
        static formKey = 'Employee.EmployeeAssests';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EmployeeAssestsForm.init)  {
                EmployeeAssestsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.IntegerEditor;

                Q.initFormType(EmployeeAssestsForm, [
                    'Items', w0,
                    'Quantity', w1,
                    'Description', w0,
                    'EmployeeId', w1
                ]);
            }
        }
    }
}
