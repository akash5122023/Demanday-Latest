namespace AdvanceCRM.Demanday {
    export interface DemandayTeleMarketingEnquiryForm {
        MasterAccountId: Serenity.LookupEditor;
        CampaignId: Serenity.LookupEditor;
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

    export class DemandayTeleMarketingEnquiryForm extends Serenity.PrefixedContext {
        static formKey = 'Demanday.DemandayTeleMarketingEnquiry';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayTeleMarketingEnquiryForm.init)  {
                DemandayTeleMarketingEnquiryForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateTimeEditor;
                var w3 = s.ImageUploadEditor;
                var w4 = DemandayTeleMarketingEnquiryQADetailsEditor;

                Q.initFormType(DemandayTeleMarketingEnquiryForm, [
                    'MasterAccountId', w0,
                    'CampaignId', w0,
                    'FirstName', w1,
                    'LastName', w1,
                    'Title', w1,
                    'Email', w1,
                    'WorkPhone', w1,
                    'AlternativeNumber', w1,
                    'CompanyName', w1,
                    'Industry', w1,
                    'Revenue', w1,
                    'CompanyEmployeeSize', w1,
                    'ZoomInfoIndustry', w1,
                    'SubIndustry', w1,
                    'ZoomInfoEmployeeSize', w1,
                    'Asset', w1,
                    'CallStatus', w1,
                    'Street', w1,
                    'City', w1,
                    'State', w1,
                    'ZipCode', w1,
                    'Country', w1,
                    'ProfileLink', w1,
                    'CompanyLink', w1,
                    'RevenueLink', w1,
                    'AddressLink', w1,
                    'EmailFormat', w1,
                    'Tenurity', w1,
                    'Code', w1,
                    'Md5', w1,
                    'Date', w2,
                    'AdditionalNotes', w1,
                    'Attachments', w3,
                    'QADetails', w4,
                    'OwnerId', w0
                ]);
            }
        }
    }
}
