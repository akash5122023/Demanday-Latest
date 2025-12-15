
namespace AdvanceCRM.ThirdParty {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class RawTelecallDialog extends DialogBase<RawTelecallRow, any> {
        protected getFormKey() { return RawTelecallForm.formKey; }
        protected getIdProperty() { return RawTelecallRow.idProperty; }
        protected getLocalTextPrefix() { return RawTelecallRow.localTextPrefix; }
        protected getNameProperty() { return RawTelecallRow.nameProperty; }
        protected getService() { return RawTelecallService.baseUrl; }
        protected getDeletePermission() { return RawTelecallRow.deletePermission; }
        protected getInsertPermission() { return RawTelecallRow.insertPermission; }
        protected getUpdatePermission() { return RawTelecallRow.updatePermission; }

        protected form = new RawTelecallForm(this.idPrefix);



        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.CreatedBy.value < "1") {
                this.form.CreatedBy.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedTo.value < "1") {
                this.form.AssignedTo.value = Q.toId(Authorization.userDefinition.UserId);
            }
        }
        
        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

           

            if(Authorization.hasPermission("IVR:Click To Call")) {
        buttons.push({
            title: 'Call',
            icon: 'fa-phone text-blue',
            onClick: () => {
                if (Q.toId(this.form.Name.value) != null) {
                    var phn = this.form.Phone.value;
                   

                    var dialog = new ThirdParty.IVRCallDialog({
                        CustomerNumber: phn,
                        ServiceURL: this.getService()
                    });

                    dialog.dialogOpen();
                }
                else
                    Q.notifyError("Contact Invalid!!");
            }
        });
            }

           
                buttons.push({
                    title: 'To Enquiry',
                    icon: 'fa fa-share-square text-red',
                    hint: 'Move To Enquiry',
                    onClick: () => {
                        RawTelecallService.MoveToEnquiry({
                            Id: this.get_entityId()
                        },
                            response => {
                                Q.notifyInfo(response.Status);
                                new Enquiry.EnquiryDialog().loadByIdAndOpenDialog(response.Id);
                            }
                        );
                    }
                });

          
            
            

            return buttons;
        }
    
    }
}