namespace AdvanceCRM.DNCContact {
    export interface DncContactsForm {
        FirstName: Serenity.StringEditor;
        LastName: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        DncStatus: Serenity.StringEditor;
        Number: Serenity.StringEditor;
        CampaignId: Serenity.IntegerEditor;
        MasterAccountId: Serenity.IntegerEditor;
    }

    export class DncContactsForm extends Serenity.PrefixedContext {
        static formKey = 'DNCContact.DncContacts';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!DncContactsForm.init)  {
                DncContactsForm.init = true;

                var s = Serenity;
                var w0 = s.StringEditor;
                var w1 = s.IntegerEditor;

                Q.initFormType(DncContactsForm, [
                    'FirstName', w0,
                    'LastName', w0,
                    'Email', w0,
                    'DncStatus', w0,
                    'Number', w0,
                    'CampaignId', w1,
                    'MasterAccountId', w1
                ]);
            }
        }
    }
}
