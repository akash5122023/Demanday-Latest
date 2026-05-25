namespace AdvanceCRM.Demanday {
    export interface DemandayEnquiryForm {
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
        ZoomInfoEmployeeSize: Serenity.StringEditor;
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
        Tenurity: Serenity.StringEditor;
        Code: Serenity.StringEditor;
        Md5: Serenity.StringEditor;
        Date: Serenity.DateTimeEditor;
        OwnerId: Administration.UserEditor;
    }

    export class DemandayEnquiryForm extends Serenity.PrefixedContext {
        static formKey = 'Demanday.DemandayEnquiry';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DemandayEnquiryForm.init)  {
                DemandayEnquiryForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;
                var w1 = s.StringEditor;
                var w2 = s.DateTimeEditor;
                var w3 = Administration.UserEditor;

                Q.initFormType(DemandayEnquiryForm, [
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
                    'ZoomInfoEmployeeSize', w1,
                    'Street', w1,
                    'City', w1,
                    'State', w1,
                    'ZipCode', w1,
                    'Country', w1,
                    'ProfileLink', w1,
                    'CompanyLink', w1,
                    'RevenueLink', w1,
                    'AdressLink', w1,
                    'EmailFormat', w1,
                    'Tenurity', w1,
                    'Code', w1,
                    'Md5', w1,
                    'Date', w2,
                    'OwnerId', w3
                ]);
            }
        }
    }
}
