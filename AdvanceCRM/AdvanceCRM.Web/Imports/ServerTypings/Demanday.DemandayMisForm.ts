namespace AdvanceCRM.Demanday {
    export interface DemandayMisForm {
        Slot: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Title: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        WorkPhone: Serenity.StringEditor;
        AlternativeNumber: Serenity.StringEditor;
        CompanyName: Serenity.StringEditor;
        Industry: Serenity.StringEditor;
        Revenue: Serenity.DecimalEditor;
        CompanyEmployeeSize: Serenity.IntegerEditor;
        Street: Serenity.StringEditor;
        City: Serenity.StringEditor;
        State: Serenity.StringEditor;
        ZipCode: Serenity.StringEditor;
        Country: Serenity.StringEditor;
        ProfileLink: Serenity.StringEditor;
        CompanyLink: Serenity.StringEditor;
        RevenueLink: Serenity.StringEditor;
        AdressLink: Serenity.StringEditor;
        EmailFormat: Serenity.StringEditor;
        Link: Serenity.StringEditor;
        QaStatus: Serenity.StringEditor;
        DeliveryStatus: Serenity.StringEditor;
        Category: Serenity.StringEditor;
        CallDate: Serenity.DateTimeEditor;
        DateAudited: Serenity.DateTimeEditor;
        DeliveryDate: Serenity.DateTimeEditor;
        AgentName: Serenity.StringEditor;
        QaName: Serenity.StringEditor;
        TlName: Serenity.StringEditor;
        PrimaryReason: Serenity.StringEditor;
        Comments: Serenity.TextAreaEditor;
        Source: Serenity.StringEditor;
        VerificationMode: Serenity.StringEditor;
        Asset1: Serenity.StringEditor;
        Asset2: Serenity.StringEditor;
        Tenurity: Serenity.StringEditor;
        Code: Serenity.StringEditor;
        Md5: Serenity.StringEditor;
        OwnerId: Serenity.LookupEditor;
    }

    export class DemandayMisForm extends Serenity.PrefixedContext {
        static formKey = 'Demanday.DemandayMis';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayMisForm.init)  {
                DemandayMisForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DecimalEditor;
                var w2 = s.IntegerEditor;
                var w3 = s.DateTimeEditor;
                var w4 = s.TextAreaEditor;
                var w5 = s.LookupEditor;

                Q.initFormType(DemandayMisForm, [
                    'Slot', w0,
                    'FirstName', w0,
                    'LastName', w0,
                    'Title', w0,
                    'Email', w0,
                    'WorkPhone', w0,
                    'AlternativeNumber', w0,
                    'CompanyName', w0,
                    'Industry', w0,
                    'Revenue', w1,
                    'CompanyEmployeeSize', w2,
                    'Street', w0,
                    'City', w0,
                    'State', w0,
                    'ZipCode', w0,
                    'Country', w0,
                    'ProfileLink', w0,
                    'CompanyLink', w0,
                    'RevenueLink', w0,
                    'AdressLink', w0,
                    'EmailFormat', w0,
                    'Link', w0,
                    'QaStatus', w0,
                    'DeliveryStatus', w0,
                    'Category', w0,
                    'CallDate', w3,
                    'DateAudited', w3,
                    'DeliveryDate', w3,
                    'AgentName', w0,
                    'QaName', w0,
                    'TlName', w0,
                    'PrimaryReason', w0,
                    'Comments', w4,
                    'Source', w0,
                    'VerificationMode', w0,
                    'Asset1', w0,
                    'Asset2', w0,
                    'Tenurity', w0,
                    'Code', w0,
                    'Md5', w0,
                    'OwnerId', w5
                ]);
            }
        }
    }
}
