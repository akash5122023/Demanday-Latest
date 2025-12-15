namespace AdvanceCRM.Employee {
    export interface EmployeeForm {
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Address: Serenity.StringEditor;
        Country: Serenity.EnumEditor;
        StateId: Serenity.LookupEditor;
        CityId: Serenity.LookupEditor;
        TehsilId: Serenity.IntegerEditor;
        VillageId: Serenity.IntegerEditor;
        Pin: Serenity.StringEditor;
        AdditionalInfo: Serenity.TextAreaEditor;
        CompanyId: Serenity.LookupEditor;
        EmpCode: Serenity.StringEditor;
        ProfessionalEmail: Serenity.StringEditor;
        DepartmentId: Serenity.LookupEditor;
        RolesId: Serenity.LookupEditor;
        OwnerId: Administration.UserEditor;
        Gender: Serenity.EnumEditor;
        Religion: Serenity.EnumEditor;
        AreaId: Serenity.LookupEditor;
        MaritalStatus: Serenity.EnumEditor;
        MarriageAnniversary: Serenity.DateEditor;
        Birthdate: Serenity.DateEditor;
        DateOfJoining: Serenity.DateEditor;
        Attachment: Serenity.MultipleImageUploadEditor;
        AdhaarNo: Serenity.StringEditor;
        PanNo: Serenity.StringEditor;
        BankName: Serenity.StringEditor;
        AccountNumber: Serenity.StringEditor;
        Ifsc: Serenity.StringEditor;
        BankType: Serenity.StringEditor;
        Branch: Serenity.StringEditor;
    }

    export class EmployeeForm extends Serenity.PrefixedContext {
        static formKey = 'Employee.Employee';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EmployeeForm.init)  {
                EmployeeForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.EnumEditor;
                var w2 = s.LookupEditor;
                var w3 = s.IntegerEditor;
                var w4 = s.TextAreaEditor;
                var w5 = Administration.UserEditor;
                var w6 = s.DateEditor;
                var w7 = s.MultipleImageUploadEditor;

                Q.initFormType(EmployeeForm, [
                    'Name', w0,
                    'Phone', w0,
                    'Email', w0,
                    'Address', w0,
                    'Country', w1,
                    'StateId', w2,
                    'CityId', w2,
                    'TehsilId', w3,
                    'VillageId', w3,
                    'Pin', w0,
                    'AdditionalInfo', w4,
                    'CompanyId', w2,
                    'EmpCode', w0,
                    'ProfessionalEmail', w0,
                    'DepartmentId', w2,
                    'RolesId', w2,
                    'OwnerId', w5,
                    'Gender', w1,
                    'Religion', w1,
                    'AreaId', w2,
                    'MaritalStatus', w1,
                    'MarriageAnniversary', w6,
                    'Birthdate', w6,
                    'DateOfJoining', w6,
                    'Attachment', w7,
                    'AdhaarNo', w0,
                    'PanNo', w0,
                    'BankName', w0,
                    'AccountNumber', w0,
                    'Ifsc', w0,
                    'BankType', w0,
                    'Branch', w0
                ]);
            }
        }
    }
}
