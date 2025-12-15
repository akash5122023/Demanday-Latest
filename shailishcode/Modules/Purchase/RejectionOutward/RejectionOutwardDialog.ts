
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class RejectionOutwardDialog extends DialogBase<RejectionOutwardRow, any> {
        protected getFormKey() { return RejectionOutwardForm.formKey; }
        protected getIdProperty() { return RejectionOutwardRow.idProperty; }
        protected getLocalTextPrefix() { return RejectionOutwardRow.localTextPrefix; }
        //protected getNameProperty() { return RejectionOutwardRow.nameProperty; }
        //protected getNameProperty() { return (RejectionOutwardRow as any).nameProperty; }
        protected getService() { return RejectionOutwardService.baseUrl; }
        protected getDeletePermission() { return RejectionOutwardRow.deletePermission; }
        protected getInsertPermission() { return RejectionOutwardRow.insertPermission; }
        protected getUpdatePermission() { return RejectionOutwardRow.updatePermission; }

        protected form = new RejectionOutwardForm(this.idPrefix);

    }
}