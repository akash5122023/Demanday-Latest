namespace AdvanceCRM.Demanday {
    export interface DemandayContactsForm {
        CampaignId: Serenity.LookupEditor;
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
        ZoomInfoIndustry: Serenity.StringEditor;
        ZoomInfoEmployeeSize: Serenity.StringEditor;
        Date: Serenity.DateTimeEditor;
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

    export class DemandayContactsForm extends Serenity.PrefixedContext {
        static formKey = 'Demanday.DemandayContacts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayContactsForm.init)  {
                DemandayContactsForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateTimeEditor;
                var w3 = s.TextAreaEditor;

                Q.initFormType(DemandayContactsForm, [
                    'CampaignId', w0,
                    'Slot', w1,
                    'FirstName', w1,
                    'LastName', w1,
                    'Domain', w1,
                    'Title', w1,
                    'JobLevel', w1,
                    'JobFunctionRole', w1,
                    'Email', w1,
                    'WorkPhone', w1,
                    'AlternativeNumber', w1,
                    'CompanyName', w1,
                    'Industry', w1,
                    'Revenue', w1,
                    'CompanyEmployeeSize', w1,
                    'ZoomInfoIndustry', w1,
                    'ZoomInfoEmployeeSize', w1,
                    'Date', w2,
                    'Street', w1,
                    'City', w1,
                    'State', w1,
                    'ZipCode', w1,
                    'Country', w1,
                    'Continents', w1,
                    'ProfileLink', w1,
                    'CompanyLink', w1,
                    'RevenueLink', w1,
                    'AdressLink', w1,
                    'ProspectUrl', w1,
                    'EmailFormat', w1,
                    'Link', w1,
                    'QaStatus', w1,
                    'DeliveryStatus', w1,
                    'Category', w1,
                    'CallDate', w2,
                    'DateAudited', w2,
                    'DeliveryDate', w2,
                    'AgentName', w1,
                    'QaName', w1,
                    'TlName', w1,
                    'PrimaryReason', w1,
                    'Comments', w3,
                    'Source', w1,
                    'VerificationMode', w1,
                    'Asset1', w1,
                    'Asset2', w1,
                    'Tenurity', w1,
                    'Code', w1,
                    'Md5', w1,
                    'OwnerId', w0
                ]);
            }
        }
    }
}
