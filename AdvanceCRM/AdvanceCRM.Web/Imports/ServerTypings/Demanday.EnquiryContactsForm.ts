namespace AdvanceCRM.Demanday {
    export interface EnquiryContactsForm {
        CompanyName: Serenity.StringEditor;
        AccountTypeId: Serenity.LookupEditor;
        Country: Serenity.StringEditor;
        AccountStatusId: Serenity.LookupEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Title: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        WorkPhone: Serenity.StringEditor;
        AlternativeNumber: Serenity.StringEditor;
        Industry: Serenity.StringEditor;
        CompanyEmployeeSize: Serenity.StringEditor;
        ProfileLink: Serenity.StringEditor;
        CompanyLink: Serenity.StringEditor;
        OutreachModeId: Serenity.LookupEditor;
        DispositionId: Serenity.LookupEditor;
        Comments: Serenity.TextAreaEditor;
        L1Meeting: Serenity.DateTimeEditor;
        MeetingModeId: Serenity.LookupEditor;
        MeetingStatus: Serenity.TextAreaEditor;
        FollowUpDate: Serenity.DateTimeEditor;
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
                var w1 = s.LookupEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.DateTimeEditor;
                var w4 = Administration.UserEditor;

                Q.initFormType(EnquiryContactsForm, [
                    'CompanyName', w0,
                    'AccountTypeId', w1,
                    'Country', w0,
                    'AccountStatusId', w1,
                    'FirstName', w0,
                    'LastName', w0,
                    'Title', w0,
                    'Email', w0,
                    'WorkPhone', w0,
                    'AlternativeNumber', w0,
                    'Industry', w0,
                    'CompanyEmployeeSize', w0,
                    'ProfileLink', w0,
                    'CompanyLink', w0,
                    'OutreachModeId', w1,
                    'DispositionId', w1,
                    'Comments', w2,
                    'L1Meeting', w3,
                    'MeetingModeId', w1,
                    'MeetingStatus', w2,
                    'FollowUpDate', w3,
                    'OwnerId', w4
                ]);
            }
        }
    }
}
