namespace AdvanceCRM.Administration {
    export interface UserForm {
        CompanyId: Serenity.LookupEditor;
        Username: Serenity.StringEditor;
        DisplayName: Serenity.StringEditor;
        Email: Serenity.EmailEditor;
        Phone: Serenity.MaskedEditor;
        NonOperational: Serenity.BooleanEditor;
        TeamsId: Serenity.LookupEditor;
        UserImage: Serenity.ImageUploadEditor;
        Password: Serenity.PasswordEditor;
        PasswordConfirm: Serenity.PasswordEditor;
        BranchId: Serenity.LookupEditor;
        IsActive: BooleanSwitchEditor;
        Host: Serenity.StringEditor;
        Port: Serenity.IntegerEditor;
        SSL: Serenity.BooleanEditor;
        EmailId: Serenity.StringEditor;
        EmailPassword: Serenity.PasswordEditor;
        UpperLevel: HierarchyEditor;
        UpperLevel2: HierarchyEditor;
        UpperLevel3: HierarchyEditor;
        UpperLevel4: HierarchyEditor;
        UpperLevel5: HierarchyEditor;
        MCSMTPServer: Serenity.StringEditor;
        MCSMTPPort: Serenity.IntegerEditor;
        MCIMAPServer: Serenity.StringEditor;
        MCIMAPPort: Serenity.IntegerEditor;
        MCUsername: Serenity.StringEditor;
        MCPassword: Serenity.PasswordEditor;
        StartTime: Serenity.MaskedEditor;
        EndTime: Serenity.MaskedEditor;
        Location: Serenity.StringEditor;
        Coordinates: Serenity.StringEditor;
        Enquiry: BooleanSwitchEditor;
        Quotation: BooleanSwitchEditor;
        Tasks: BooleanSwitchEditor;
        Contacts: BooleanSwitchEditor;
        Purchase: BooleanSwitchEditor;
        Sales: BooleanSwitchEditor;
        Cms: BooleanSwitchEditor;
    }

    export class UserForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.User';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!UserForm.init)  {
                UserForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.EmailEditor;
                var w3 = s.MaskedEditor;
                var w4 = s.BooleanEditor;
                var w5 = s.ImageUploadEditor;
                var w6 = s.PasswordEditor;
                var w7 = BooleanSwitchEditor;
                var w8 = s.IntegerEditor;
                var w9 = HierarchyEditor;

                Q.initFormType(UserForm, [
                    'CompanyId', w0,
                    'Username', w1,
                    'DisplayName', w1,
                    'Email', w2,
                    'Phone', w3,
                    'NonOperational', w4,
                    'TeamsId', w0,
                    'UserImage', w5,
                    'Password', w6,
                    'PasswordConfirm', w6,
                    'BranchId', w0,
                    'IsActive', w7,
                    'Host', w1,
                    'Port', w8,
                    'SSL', w4,
                    'EmailId', w1,
                    'EmailPassword', w6,
                    'UpperLevel', w9,
                    'UpperLevel2', w9,
                    'UpperLevel3', w9,
                    'UpperLevel4', w9,
                    'UpperLevel5', w9,
                    'MCSMTPServer', w1,
                    'MCSMTPPort', w8,
                    'MCIMAPServer', w1,
                    'MCIMAPPort', w8,
                    'MCUsername', w1,
                    'MCPassword', w6,
                    'StartTime', w3,
                    'EndTime', w3,
                    'Location', w1,
                    'Coordinates', w1,
                    'Enquiry', w7,
                    'Quotation', w7,
                    'Tasks', w7,
                    'Contacts', w7,
                    'Purchase', w7,
                    'Sales', w7,
                    'Cms', w7
                ]);
            }
        }
    }
}
