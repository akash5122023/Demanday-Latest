
namespace AdvanceCRM.Toolkit {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class DemandayCompetitorDialog extends DialogBase<DemandayCompetitorRow, any> {
        protected getFormKey() { return DemandayCompetitorForm.formKey; }
        protected getIdProperty() { return DemandayCompetitorRow.idProperty; }
        protected getLocalTextPrefix() { return DemandayCompetitorRow.localTextPrefix; }
        protected getNameProperty() { return DemandayCompetitorRow.nameProperty; }
        protected getService() { return DemandayCompetitorService.baseUrl; }
        protected getDeletePermission() { return DemandayCompetitorRow.deletePermission; }
        protected getInsertPermission() { return DemandayCompetitorRow.insertPermission; }
        protected getUpdatePermission() { return DemandayCompetitorRow.updatePermission; }

        protected form = new DemandayCompetitorForm(this.idPrefix);

    }
}