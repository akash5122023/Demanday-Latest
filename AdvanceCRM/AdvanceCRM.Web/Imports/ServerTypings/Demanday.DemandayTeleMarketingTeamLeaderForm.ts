namespace AdvanceCRM.Demanday {
    export interface DemandayTeleMarketingTeamLeaderForm {
        CampaignId: Serenity.StringEditor;
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
        ZoomInfoIndustry: Serenity.StringEditor;
        SubIndustry: Serenity.StringEditor;
        ZoomInfoEmployeeSize: Serenity.StringEditor;
        Asset: Serenity.StringEditor;
        CallStatus: Serenity.StringEditor;
        Street: Serenity.StringEditor;
        City: Serenity.StringEditor;
        State: Serenity.StringEditor;
        ZipCode: Serenity.StringEditor;
        Country: Serenity.StringEditor;
        ProfileLink: Serenity.StringEditor;
        CompanyLink: Serenity.StringEditor;
        RevenueLink: Serenity.StringEditor;
        AddressLink: Serenity.StringEditor;
        Link: Serenity.StringEditor;
        EmailFormat: Serenity.StringEditor;
        Tenurity: Serenity.StringEditor;
        Code: Serenity.StringEditor;
        Md5: Serenity.StringEditor;
        Date: Serenity.DateTimeEditor;
        AdditionalNotes: Serenity.StringEditor;
        Attachments: Serenity.ImageUploadEditor;
        QADetails: DemandayTeleMarketingEnquiryQADetailsEditor;
        OwnerId: Serenity.LookupEditor;
    }

    export class DemandayTeleMarketingTeamLeaderForm extends Serenity.PrefixedContext {
        static formKey = 'Demanday.DemandayTeleMarketingTeamLeader';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayTeleMarketingTeamLeaderForm.init)  {
                DemandayTeleMarketingTeamLeaderForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.DateTimeEditor;
                var w2 = s.ImageUploadEditor;
                var w3 = DemandayTeleMarketingEnquiryQADetailsEditor;
                var w4 = s.LookupEditor;

                Q.initFormType(DemandayTeleMarketingTeamLeaderForm, [
                    'CampaignId', w0,
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
                    'ZoomInfoIndustry', w0,
                    'SubIndustry', w0,
                    'ZoomInfoEmployeeSize', w0,
                    'Asset', w0,
                    'CallStatus', w0,
                    'Street', w0,
                    'City', w0,
                    'State', w0,
                    'ZipCode', w0,
                    'Country', w0,
                    'ProfileLink', w0,
                    'CompanyLink', w0,
                    'RevenueLink', w0,
                    'AddressLink', w0,
                    'Link', w0,
                    'EmailFormat', w0,
                    'Tenurity', w0,
                    'Code', w0,
                    'Md5', w0,
                    'Date', w1,
                    'AdditionalNotes', w0,
                    'Attachments', w2,
                    'QADetails', w3,
                    'OwnerId', w4
                ]);
            }
        }
    }
}
