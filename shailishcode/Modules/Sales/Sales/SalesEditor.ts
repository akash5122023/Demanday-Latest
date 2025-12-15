namespace AdvanceCRM.Sales {

    @Serenity.Decorators.registerEditor()
    export class SalesEditor extends Serenity.LookupEditorBase<Serenity.LookupEditorOptions, SalesRow> {

        constructor(hidden: JQuery) {
            super(hidden);
        }

        protected getLookupKey() {
            return SalesRow.lookupKey;
        }

        protected getItemText(item, lookup) {
            return item.Date.substring(0, 4) + "/" + item.InvoiceNo;
        }
    }
}