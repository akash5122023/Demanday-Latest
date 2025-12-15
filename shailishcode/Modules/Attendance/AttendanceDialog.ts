
namespace AdvanceCRM.Attendance {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()
    export class AttendanceDialog extends DialogBase<AttendanceRow, any> {
        protected getFormKey() { return AttendanceForm.formKey; }
        protected getIdProperty() { return AttendanceRow.idProperty; }
        protected getLocalTextPrefix() { return AttendanceRow.localTextPrefix; }
        protected getNameProperty() { return AttendanceRow.nameProperty; }
        protected getService() { return AttendanceService.baseUrl; }
        protected getDeletePermission() { return AttendanceRow.deletePermission; }
        protected getInsertPermission() { return AttendanceRow.insertPermission; }
        protected getUpdatePermission() { return AttendanceRow.updatePermission; }

        protected form = new AttendanceForm(this.idPrefix);

        constructor() {
            super();

        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            if (Authorization.hasPermission("Attendance:Can Approve")) {
                buttons.push({
                    title: 'Approve',
                    icon: 'fa-check',
                    hint: 'Approve this Attendance',
                    onClick: () => {
                        AttendanceService.Approve({
                            Id: Q.toId(this.entityId)
                        },
                            response => {
                                if (response.Status == "Approved") {
                                    Q.notifySuccess(response.Status);
                                    Serenity.SubDialogHelper.triggerDataChange(this);
                                }
                                else {
                                    Q.notifyError(response.Status);
                                }
                            }
                        );
                    },
                    separator: true
                });
            }

            buttons.push({
                title: 'Get Location',
                icon: 'fa-map-marker  text-red',
                hint: 'Get Current Location',
                onClick: () => {
                    let boundCallback = this.locationCallback.bind(this);
                    if (navigator.geolocation) {
                        navigator.geolocation.getCurrentPosition(boundCallback);
                    } else {
                        Q.notifyError("Sorry, your browser does not support HTML5 geolocation.");
                    }
                },
                separator: true
            });

            buttons.push({
                title: 'On Map',
                icon: 'fa-map-o text-primary',
                hint: 'Show Location on map',
                onClick: () => {
                    var url = 'http://maps.google.com/maps?q=' + this.form.Coordinates.value;
                    window.open(
                        url,
                        '_blank' // <- This is what makes it open in a new window.
                    );
                }
            });

            return buttons;
        }

        protected updateInterface() {
            super.updateInterface();

            this.toolbar.findButton('fa-check').toggle(this.isEditMode());
        }

        private locationCallback(position) {
            this.form.Coordinates.value = position.coords.latitude + "," + position.coords.longitude;
        }
    }
}