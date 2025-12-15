namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class UserDialog extends DialogBase<UserRow, any> {
        protected getFormKey() { return UserForm.formKey; }
        protected getIdProperty() { return UserRow.idProperty; }
        protected getIsActiveProperty() { return UserRow.isActiveProperty; }
        protected getLocalTextPrefix() { return UserRow.localTextPrefix; }
        protected getNameProperty() { return UserRow.nameProperty; }
        protected getService() { return UserService.baseUrl; }

        protected form = new UserForm(this.idPrefix);

        constructor() {
            super();

            Common.NavigationService.MultiLocation({},
                response => {
                    if (response.Status != "Remove") {
                        this.form.BranchId.element.addClass(" required");
                    }
                }
            );

            this.form.Password.addValidationRule(this.uniqueName, e => {
                if (this.form.Password.value.length < 7)
                    return "Password must be at least 7 characters!";
            });

            this.form.PasswordConfirm.addValidationRule(this.uniqueName, e => {
                if (this.form.Password.value != this.form.PasswordConfirm.value)
                    return "The passwords entered doesn't match!";
            });

            var uid = Q.toId(Authorization.userDefinition.UserId);
            if (uid != 1) {
                this.form.CompanyId.readOnly = true;
            }
           // this.form.TenantId.set_readOnly(true);
            this.form.TenantId.readOnly=true;
            this.form.TenantId.element.closest('.field').hide();
           // this.form.Url.set_readOnly(true);
            this.form.Url.readOnly = true;
            this.form.Url.element.closest('.field').hide();
        }

        protected getToolbarButtons() {
            let buttons = super.getToolbarButtons();

            buttons.push({
                title: Q.text('Site.UserDialog.EditRolesButton'),
                cssClass: 'edit-roles-button',
                icon: 'icon-people text-blue',
                onClick: () => {
                    new UserRoleDialog({
                        userID: this.entity.UserId,
                        username: this.entity.Username
                    }).dialogOpen();
                },
                separator: true
            });

            buttons.push({
                title: Q.text('Site.UserDialog.EditPermissionsButton'),
                cssClass: 'edit-permissions-button',
                icon: 'icon-lock-open text-green',
                onClick: () => {
                    new UserPermissionDialog({
                        userID: this.entity.UserId,
                        username: this.entity.Username
                    }).dialogOpen();
                }
            });

            return buttons;
        }

        protected updateInterface() {
            super.updateInterface();

            this.toolbar.findButton('edit-roles-button').toggleClass('disabled', this.isNewOrDeleted());
            this.toolbar.findButton("edit-permissions-button").toggleClass("disabled", this.isNewOrDeleted());
        }

        protected afterLoadEntity() {
            super.afterLoadEntity();

            // these fields are only required in new record mode
            this.form.Password.element.toggleClass('required', this.isNew())
                .closest('.field').find('sup').toggle(this.isNew());
            this.form.PasswordConfirm.element.toggleClass('required', this.isNew())
                .closest('.field').find('sup').toggle(this.isNew());
        }

        onDialogOpen() {
            super.onDialogOpen();

            if (this.form.Username.value == 'admin') {
                Serenity.EditorUtils.setReadonly(this.form.Username.element, true);
            }

            if (this.form.CompanyId.value < "1") {
                this.form.CompanyId.value = Authorization.userDefinition.CompanyId.toString();
            }
        }

        /*
        protected getPropertyItems() {
            var items = super.getPropertyItems();
            if (!Q.Authorization.hasPermission("Administration:Company"))
                items = items.filter(x => x.name != UserRow.Fields.CompanyId);
            return items;
        }

        protected validateBeforeSave() {
            var cid = this.form.CompanyId.value;
            if (this.form.UpperLevel.value != '1') {
                if (UserRow.getLookup().itemById[this.form.UpperLevel.value].CompanyId.toString() != cid) {
                    Q.alert("Upper Level 1 has a different Company");
                    return false;
                }
            }
            if (this.form.UpperLevel.value != '1') {
                if (UserRow.getLookup().itemById[this.form.UpperLevel2.value].CompanyId.toString() != cid) {
                    Q.alert("Upper Level 2 has a different Company");
                    return false;
                }
            }
            if (this.form.UpperLevel.value != '1') {
                if (UserRow.getLookup().itemById[this.form.UpperLevel3.value].CompanyId.toString() != cid) {
                    Q.alert("Upper Level 3 has a different Company");
                    return false;
                }
            }
            if (this.form.UpperLevel.value != '1') {
                if (UserRow.getLookup().itemById[this.form.UpperLevel4.value].CompanyId.toString() != cid) {
                    Q.alert("Upper Level 4 has a different Company");
                    return false;
                }
            }
            if (this.form.UpperLevel.value != '1') {
                if (UserRow.getLookup().itemById[this.form.UpperLevel5.value].CompanyId.toString() != cid) {
                    Q.alert("Upper Level 5 has a different Company");
                    return false;
                }
            }
            return true;
        }*/
    }
}