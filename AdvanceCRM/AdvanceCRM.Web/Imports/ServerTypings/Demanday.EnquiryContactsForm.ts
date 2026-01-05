namespace AdvanceCRM.Demanday {
    export interface EnquiryContactsForm {
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Title: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        WorkPhone: Serenity.StringEditor;
        AlternativeNumber: Serenity.StringEditor;
        CompanyName: Serenity.StringEditor;
        Industry: Serenity.StringEditor;
        Revenue: Serenity.StringEditor;
        CompanyEmployeeSize: Serenity.StringEditor;
        Street: Serenity.StringEditor;
        City: Serenity.StringEditor;
        State: Serenity.StringEditor;
        ZipCode: Serenity.StringEditor;
        Country: Serenity.StringEditor;
        ProfileLink: Serenity.StringEditor;
        CompanyLink: Serenity.StringEditor;
        RevenueLink: Serenity.StringEditor;
        AdressLink: Serenity.StringEditor;
        Tenurity: Serenity.StringEditor;
        Code: Serenity.StringEditor;
        Md5: Serenity.StringEditor;
        OwnerId: Administration.UserEditor;
    }

    export class EnquiryContactsForm extends Serenity.PrefixedContext {
        static formKey = 'Demanday.EnquiryContacts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!EnquiryContactsForm.init)  {
                EnquiryContactsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = Administration.UserEditor;

                Q.initFormType(EnquiryContactsForm, [
                    'FirstName', w0,
                    'LastName', w0,
                    'Title', w0,
                    'Email', w0,
                    'WorkPhone', w0,
                    'AlternativeNumber', w0,
                    'CompanyName', w0,
                    'Industry', w0,
                    'Revenue', w0,
                    'CompanyEmployeeSize', w0,
                    'Street', w0,
                    'City', w0,
                    'State', w0,
                    'ZipCode', w0,
                    'Country', w0,
                    'ProfileLink', w0,
                    'CompanyLink', w0,
                    'RevenueLink', w0,
                    'AdressLink', w0,
                    'Tenurity', w0,
                    'Code', w0,
                    'Md5', w0,
                    'OwnerId', w1
                ]);
            }
        }
    }
}
