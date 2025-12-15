
namespace AdvanceCRM.Tasks {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.panel()

    export class TasksDialog extends DialogBase<TasksRow, any> {
        protected getFormKey() { return TasksForm.formKey; }
        protected getIdProperty() { return TasksRow.idProperty; }
        protected getLocalTextPrefix() { return TasksRow.localTextPrefix; }
        protected getNameProperty() { return TasksRow.nameProperty; }
        protected getService() { return TasksService.baseUrl; }
        protected getDeletePermission() { return TasksRow.deletePermission; }
        protected getInsertPermission() { return TasksRow.insertPermission; }
        protected getUpdatePermission() { return TasksRow.updatePermission; }

        protected form = new TasksForm(this.idPrefix);

        constructor() {
            super();
            this.byId('NoteList').closest('.field').hide().end().appendTo(this.byId('TabNotes'));
            this.byId('Timeline').closest('.field').hide().end().appendTo(this.byId('TabTimeline'));

            this.form.CompletionDate.getGridField().toggle(false);
            this.form.Period.getGridField().toggle(false);

            this.form.StatusId.changeSelect2(e => {
                if (this.form.StatusId.value == '2') {
                    this.form.CompletionDate.getGridField().toggle(true)
                    if (this.form.CompletionDate.value == null) {
                        this.form.CompletionDate.value = "now";
                    }
                }
                else {
                    this.form.CompletionDate.getGridField().toggle(false)
                }
            });

            this.form.Recurring.change(e => {
                if (this.form.Recurring.value == true) {
                    this.form.Period.getGridField().toggle(true);
                }
                else {
                    this.form.Period.getGridField().toggle(false);
                }
            });
        }

        protected updateInterface() {
            super.updateInterface();

            this.toolbar.findButton('mail-button').toggle(this.isEditMode());
            this.toolbar.findButton('send-button').toggle(this.isEditMode());
        }

        getToolbarButtons() {
            var buttons = super.getToolbarButtons();

            buttons.push({
                title: 'Mail',
                cssClass: 'mail-button', icon: 'fa fa-envelope-o text-blue',
                hint: 'Send task details by Mail',
                onClick: () => {
                    TasksService.SendMail({
                        Id: this.get_entityId()
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                },
                separator: true
            });

            buttons.push({
                title: 'SMS',
                cssClass: 'send-button', icon: 'fa-comments-o text-green',
                hint: 'Send task details by SMS',
                onClick: () => {
                    TasksService.SendSMS({
                        Id: Q.toId(this.entityId)
                    },
                        response => {
                            Q.notifyInfo(response.Status);
                        }
                    );
                }
            });


            //buttons.push({
            //    title: 'BizWA',
            //    cssClass: 'send-button', icon: 'fa-brands fa-whatsapp',
            //    hint: 'Send thankyou Whatsapp to customer',
            //    onClick: () => {
            //        TasksService.SendWati({
            //            Id: Q.toId(this.entityId)
            //        },
            //            response => {
            //                Q.notifyInfo(response.Status);
            //            }
            //        );
            //    }
            //});

            return buttons;
        }

        onDialogOpen() {
            super.onDialogOpen();
            this.element.closest(".ui-dialog").find(".ui-icon-maximize-window").click();
            if (this.form.AssignedBy.value < "1") {
                this.form.AssignedBy.value = Q.toId(Authorization.userDefinition.UserId);
            }
            if (this.form.AssignedTo.value < "1") {
                this.form.AssignedTo.value = Q.toId(Authorization.userDefinition.UserId);
            }

            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].TaskMasterInTask == true) {
            //    this.form.TaskId.element.closest('.category').toggle(false);
            //    this.form.TaskId.getGridField().toggle(false);
            //}

            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].TaskTitleInTask == true) {
            //    this.form.TaskTitle.element.closest('.category').toggle(false);
            //    this.form.TaskTitle.getGridField().toggle(false);
            //}



            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].TaskTitleInTask == true) {
            //    this.form.TaskTitle.getGridField().toggle(false);
            //}

            //if (Administration.CompanyDetailsRow.getLookup().itemById[1].TaskMasterInTask == true) {
            //    this.form.TaskId.getGridField().toggle(false); // Only hides the field, not category
            //}

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].TaskTitleInTask != true) {
                this.form.TaskTitle.getGridField().toggle(false); // Hide if false
            }

            if (Administration.CompanyDetailsRow.getLookup().itemById[1].TaskMasterInTask != true) {
                this.form.TaskId.getGridField().toggle(false); // Hide if false
            }


            if (this.form.StatusId.value == '2') {
                this.form.CompletionDate.getGridField().toggle(true)
                if (this.form.CompletionDate.value == null) {
                    this.form.CompletionDate.value = "now";
                }
            }
            else {
                this.form.CompletionDate.getGridField().toggle(false)
            }

            Serenity.EditorUtils.setReadonly(this.form.AssignedBy.element, true);
        }

        loadEntity(entity: TasksRow) {
            super.loadEntity(entity);

            Serenity.TabsExtensions.setDisabled(this.tabs, 'Timeline', this.isNewOrDeleted());
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Notes', this.isNewOrDeleted());
        }

    }
}