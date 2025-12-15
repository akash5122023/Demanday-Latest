namespace AdvanceCRM.Common {
    export interface BulkTransferForm {
        From: Administration.UserEditor;
        To: Administration.UserEditor;
        Module: Serenity.EnumEditor;
    }

    export class BulkTransferForm extends Serenity.PrefixedContext {
        static formKey = 'Common.BulkTransfer';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!BulkTransferForm.init)  {
                BulkTransferForm.init = true;

                var s = Serenity;
                var w0 = Administration.UserEditor;
                var w1 = s.EnumEditor;

                Q.initFormType(BulkTransferForm, [
                    'From', w0,
                    'To', w0,
                    'Module', w1
                ]);
            }
        }
    }
}
