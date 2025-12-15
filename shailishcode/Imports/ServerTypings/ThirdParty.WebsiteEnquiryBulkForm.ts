namespace AdvanceCRM.ThirdParty {
    export interface WebsiteEnquiryBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class WebsiteEnquiryBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.WebsiteEnquiryBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!WebsiteEnquiryBulkForm.init)  {
                WebsiteEnquiryBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(WebsiteEnquiryBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
