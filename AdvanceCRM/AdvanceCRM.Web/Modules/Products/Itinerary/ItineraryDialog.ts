
namespace AdvanceCRM.Products {

    @Serenity.Decorators.registerClass()
    export class ItineraryDialog extends Serenity.EntityDialog<ItineraryRow, any> {
        protected getFormKey() { return ItineraryForm.formKey; }
        protected getIdProperty() { return ItineraryRow.idProperty; }
        protected getLocalTextPrefix() { return ItineraryRow.localTextPrefix; }
        protected getNameProperty() { return ItineraryRow.nameProperty; }
        protected getService() { return ItineraryService.baseUrl; }
        protected getDeletePermission() { return ItineraryRow.deletePermission; }
        protected getInsertPermission() { return ItineraryRow.insertPermission; }
        protected getUpdatePermission() { return ItineraryRow.updatePermission; }

        protected form = new ItineraryForm(this.idPrefix);

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push(AdvanceCRM.Common.ReportHelper.createToolButton({
                title: 'Preview',
                cssClass: 'export-pdf-button',
                reportKey: 'Itinerary.PrintItinerary', //Change PrintQuotationUnique, PrintQuotation 
                getParams: () => ({ Id: this.get_entityId() }),
                separator: true
            }));

            return buttons;
        }

    }
}