
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DealerDialog extends DialogBase<DealerRow, any> {
        protected getFormKey() { return DealerForm.formKey; }
        protected getIdProperty() { return DealerRow.idProperty; }
        protected getLocalTextPrefix() { return DealerRow.localTextPrefix; }
        protected getNameProperty() { return DealerRow.nameProperty; }
        protected getService() { return DealerService.baseUrl; }
        protected getDeletePermission() { return DealerRow.deletePermission; }
        protected getInsertPermission() { return DealerRow.insertPermission; }
        protected getUpdatePermission() { return DealerRow.updatePermission; }

        protected form = new DealerForm(this.idPrefix);

    }
}