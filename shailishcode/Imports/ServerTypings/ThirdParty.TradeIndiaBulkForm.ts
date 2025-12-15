namespace AdvanceCRM.ThirdParty {
    export interface TradeIndiaBulkForm {
        UIds: Serenity.LookupEditor;
    }

    export class TradeIndiaBulkForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.TradeIndiaBulk';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!TradeIndiaBulkForm.init)  {
                TradeIndiaBulkForm.init = true;

                var s = Serenity;
                var w0 = s.LookupEditor;

                Q.initFormType(TradeIndiaBulkForm, [
                    'UIds', w0
                ]);
            }
        }
    }
}
