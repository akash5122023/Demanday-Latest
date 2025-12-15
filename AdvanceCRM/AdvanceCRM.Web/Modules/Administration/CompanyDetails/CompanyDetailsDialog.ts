
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class CompanyDetailsDialog extends DialogBase<CompanyDetailsRow, any> {
        protected getFormKey() { return CompanyDetailsForm.formKey; }
        protected getIdProperty() { return CompanyDetailsRow.idProperty; }
        protected getLocalTextPrefix() { return CompanyDetailsRow.localTextPrefix; }
        protected getNameProperty() { return CompanyDetailsRow.nameProperty; }
        protected getService() { return CompanyDetailsService.baseUrl; }
        protected getDeletePermission() { return CompanyDetailsRow.deletePermission; }
        protected getInsertPermission() { return CompanyDetailsRow.insertPermission; }
        protected getUpdatePermission() { return CompanyDetailsRow.updatePermission; }

        protected form = new CompanyDetailsForm(this.idPrefix);
        private BranchGrid: BranchGrid;
        constructor() {
            super();
            this.BranchGrid = new BranchGrid(this.byId('BranchGrid'));
        }

        loadEntity(entity: CompanyDetailsRow) {
            super.loadEntity(entity);

            Serenity.TabsExtensions.setDisabled(this.tabs, 'Branch', this.isNewOrDeleted());

            if (!this.isNewOrDeleted()) {
                this.BranchGrid.companyId = entity.Id.toString();
            }
        }
    }
}