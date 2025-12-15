
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class AdditionalInfoGrid extends Serenity.EntityGrid<AdditionalInfoRow, any> {
        protected getColumnsKey() { return 'Wte.AdditionalInfo'; }
        protected getDialogType() { return AdditionalInfoDialog; }
        protected getIdProperty() { return AdditionalInfoRow.idProperty; }
        protected getInsertPermission() { return AdditionalInfoRow.insertPermission; }
        protected getLocalTextPrefix() { return AdditionalInfoRow.localTextPrefix; }
        protected getService() { return AdditionalInfoService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}