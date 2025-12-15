
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    export class ItineraryGrid extends Serenity.EntityGrid<ItineraryRow, any> {
        protected getColumnsKey() { return 'Products.Itinerary'; }
        protected getDialogType() { return ItineraryDialog; }
        protected getIdProperty() { return ItineraryRow.idProperty; }
        protected getInsertPermission() { return ItineraryRow.insertPermission; }
        protected getLocalTextPrefix() { return ItineraryRow.localTextPrefix; }
        protected getService() { return ItineraryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}