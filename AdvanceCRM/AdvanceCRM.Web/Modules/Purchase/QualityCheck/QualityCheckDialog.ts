
namespace AdvanceCRM.Purchase {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class QualityCheckDialog extends DialogBase<QualityCheckRow, any> {
        protected getFormKey() { return QualityCheckForm.formKey; }
        protected getIdProperty() { return QualityCheckRow.idProperty; }
        protected getLocalTextPrefix() { return QualityCheckRow.localTextPrefix; }
        protected getNameProperty() { return QualityCheckRow.nameProperty; }
        protected getService() { return QualityCheckService.baseUrl; }
        protected getDeletePermission() { return QualityCheckRow.deletePermission; }
        protected getInsertPermission() { return QualityCheckRow.insertPermission; }
        protected getUpdatePermission() { return QualityCheckRow.updatePermission; }

        protected form = new QualityCheckForm(this.idPrefix);

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            if (Authorization.hasPermission("QualityCheck:Move to Rejection Outward")) {
                buttons.push({
                    title: 'To Rejection Outward',
                    icon: 'fa fa-share-square text-red',
                    hint: 'Move to Rejection Outward',
                    onClick: () => {
                        QualityCheckService.MoveToRejectionOutward({
                            Id: this.get_entityId()
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                                new RejectionOutwardDialog().loadByIdAndOpenDialog(response.Id);
                            }
                        );
                    }
                });
            }

            return buttons;
        }


        onDialogOpen() {
            super.onDialogOpen();
            this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();

            if (this.form.QcNumber.value < 1) {
                // call our service, see CustomerEndpoint.cs and CustomerRepository.cs
                QualityCheckService.GetNextNumber({
                    Prefix: '',
                    Length: 5
                }, response => {
                    this.form.QcNumber.value = Q.toId(response.Serial);
                });
            }


        }

    }
}