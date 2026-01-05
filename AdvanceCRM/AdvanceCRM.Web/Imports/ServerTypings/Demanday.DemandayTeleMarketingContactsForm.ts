namespace AdvanceCRM.Demanday {
    export interface DemandayTeleMarketingContactsForm {
        Slot: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Domain: Serenity.StringEditor;
        Title: Serenity.StringEditor;
        JobLevel: Serenity.StringEditor;
        JobFunctionRole: Serenity.StringEditor;
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
        Continents: Serenity.StringEditor;
        ProfileLink: Serenity.StringEditor;
        CompanyLink: Serenity.StringEditor;
        RevenueLink: Serenity.StringEditor;
        AdressLink: Serenity.StringEditor;
        ProspectUrl: Serenity.StringEditor;
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

    export class DemandayTeleMarketingContactsForm extends Serenity.PrefixedContext {
        static formKey = 'Demanday.DemandayTeleMarketingContacts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayTeleMarketingContactsForm.init)  {
                DemandayTeleMarketingContactsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateTimeEditor;
                var w2 = s.TextAreaEditor;
                var w3 = s.LookupEditor;

                Q.initFormType(DemandayTeleMarketingContactsForm, [
                    'Slot', w0,
                    'FirstName', w0,
                    'LastName', w0,
                    'Domain', w0,
                    'Title', w0,
                    'JobLevel', w0,
                    'JobFunctionRole', w0,
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
                    'Continents', w0,
                    'ProfileLink', w0,
                    'CompanyLink', w0,
                    'RevenueLink', w0,
                    'AdressLink', w0,
                    'ProspectUrl', w0,
                    'EmailFormat', w0,
                    'Link', w0,
                    'QaStatus', w0,
                    'DeliveryStatus', w0,
                    'Category', w0,
                    'CallDate', w1,
                    'DateAudited', w1,
                    'DeliveryDate', w1,
                    'AgentName', w0,
                    'QaName', w0,
                    'TlName', w0,
                    'PrimaryReason', w0,
                    'Comments', w2,
                    'Source', w0,
                    'VerificationMode', w0,
                    'Asset1', w0,
                    'Asset2', w0,
                    'Tenurity', w0,
                    'Code', w0,
                    'Md5', w0,
                    'OwnerId', w3
                ]);
            }
        }
    }
}
