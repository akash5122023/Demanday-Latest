namespace AdvanceCRM.Contacts {
    export interface SubContactsForm {
        Name: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        ResidentialPhone: Serenity.MaskedEditor;
        Whatsapp: Serenity.StringEditor;
        Email: Serenity.EmailEditor;
        Designation: Serenity.StringEditor;
        Project: Serenity.StringEditor;
        MultiProjectList: Serenity.LookupEditor;
        Address: Serenity.TextAreaEditor;
        Gender: Serenity.EnumEditor;
        Religion: Serenity.EnumEditor;
        MaritalStatus: Serenity.EnumEditor;
        MarriageAnniversary: Serenity.DateEditor;
        Birthdate: Serenity.DateEditor;
        PANNo: Serenity.StringEditor;
        AadharNo: Serenity.StringEditor;
        PassportNumber: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        ExpiryDate: Serenity.DateEditor;
        FileAttachments: Serenity.MultipleImageUploadEditor;
    }

    export class SubContactsForm extends Serenity.PrefixedContext {
        static formKey = 'Contacts.SubContacts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!SubContactsForm.init)  {
                SubContactsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.MaskedEditor;
                var w2 = s.EmailEditor;
                var w3 = s.LookupEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.EnumEditor;
                var w6 = s.DateEditor;
                var w7 = s.MultipleImageUploadEditor;

                Q.initFormType(SubContactsForm, [
                    'Name', w0,
                    'Phone', w0,
                    'ResidentialPhone', w1,
                    'Whatsapp', w0,
                    'Email', w2,
                    'Designation', w0,
                    'Project', w0,
                    'MultiProjectList', w3,
                    'Address', w4,
                    'Gender', w5,
                    'Religion', w5,
                    'MaritalStatus', w5,
                    'MarriageAnniversary', w6,
                    'Birthdate', w6,
                    'PANNo', w0,
                    'AadharNo', w0,
                    'PassportNumber', w0,
                    'FirstName', w0,
                    'LastName', w0,
                    'ExpiryDate', w6,
                    'FileAttachments', w7
                ]);
            }
        }
    }
}
