namespace AdvanceCRM.BizMail {
    export interface BmSubscribersForm {
        SubscriberId: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Status: Serenity.StringEditor;
        Source: Serenity.StringEditor;
        IpAddress: Serenity.StringEditor;
        DateAdded: Serenity.StringEditor;
        ListName: Serenity.StringEditor;
        ListId: Serenity.StringEditor;
        Phone: Serenity.StringEditor;
        IsMoved: Serenity.BooleanEditor;
    }

    export class BmSubscribersForm extends Serenity.PrefixedContext {
        static formKey = 'BizMail.BmSubscribers';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BmSubscribersForm.init)  {
                BmSubscribersForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.BooleanEditor;

                Q.initFormType(BmSubscribersForm, [
                    'SubscriberId', w0,
                    'Email', w0,
                    'FirstName', w0,
                    'LastName', w0,
                    'Status', w0,
                    'Source', w0,
                    'IpAddress', w0,
                    'DateAdded', w0,
                    'ListName', w0,
                    'ListId', w0,
                    'Phone', w0,
                    'IsMoved', w1
                ]);
            }
        }
    }
}
