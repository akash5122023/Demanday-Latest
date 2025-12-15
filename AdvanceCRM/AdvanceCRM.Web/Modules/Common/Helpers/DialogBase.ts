namespace AdvanceCRM {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.maximizable()
    export class DialogBase<TEntity, TOptions>
        //this comment is for preventing replacement 
        extends Serenity.EntityDialog<TEntity, TOptions> {

        private loadedState: string;
        protected form: any;

        constructor(opt?) {
            super(opt);
            DialogUtils.pendingChangesConfirmation(this.element, () => this.getSaveState() != this.loadedState);
        }

        protected updateInterface() {
            super.updateInterface();

            this.element.find('.category-links').hide();

            let $FirstCategory = this.element.find('.first-category > .category-title');

            if (Q.isEmptyOrNull($FirstCategory.find('.category-anchor').text()))
                $FirstCategory.hide();

        }
        protected getToolbarButtons(): Serenity.ToolButton[] {
            let buttons = super.getToolbarButtons();

            buttons.push({
                title: 'Refresh',
                icon: 'fa fa-refresh',
                onClick: () => {
                    this.onRefreshClick();
                }
            });

            try {
                buttons.push({
                    cssClass: 'btn-history',
                    icon: 'fa fa-history',
                    onClick: () => {
                        let entityId = this.entity[this.getIdProperty()];
                        if (entityId) {
                            let dlg = new _Ext.AuditLogViewerDialog({ FormKey: this.getFormKey(), EntityId: entityId });

                            dlg.dialogOpen();
                        } else {
                            Q.alert('No change log found for this entity.')
                        }
                    }
                });
            } catch (e) { }

            return buttons;
        }

        onRefreshClick() {
            this.reloadById();
        }

        protected getSaveState() {
            try {
                return $.toJSON(this.getSaveEntity());
            }
            catch (e) {
                return null;
            }
        }

        protected onSaveSuccess(response) {
            super.onSaveSuccess(response);
            isPageRefreshRequired = true;
            //Q.reloadLookup(this.getLookupKey());
        }

        loadResponse(data) {
            super.loadResponse(data);

            this.loadedState = this.getSaveState();
        }

        dialogOpen(asPanel) {
            super.dialogOpen(asPanel);
            if (asPanel === false) {
                this.fullContentArea();
            }
        }

        fullContentArea() {
            this.setDialogSize(screen.width - 250, screen.height - 250);
        }
        // set the dialog size relative to content area (to shrink use negative value)
        setDialogSize(width?, height?, top?, left?, $content?) {
            if (!$content) {
                $content = $('section.content');
            }
            if ($content.length == 0) {
                $content = $('.content-wrapper');
            }

            let dialogElement = this.element ? this.element.closest(".ui-dialog") : $(".ui-dialog");

            if ($content.length > 0 && dialogElement.length > 0) {

                let dialogWidth = width || ($content.width() + 30);
                let dialogHeight = height || ($content.height() + 30);

                this.element.dialog("option", "width", dialogWidth);
                this.element.dialog("option", "height", dialogHeight);

                let titleBarHeight = dialogElement.find('.ui-dialog-title').height() || 20;
                let toolBarHeight = dialogElement.find('.s-DialogToolbar.s-Toolbar').height() || 0;
                let tabBarHeight = dialogElement.find('.nav.nav-tabs.property-tabs').height() || 0;
                let categoryLinkHeight = dialogElement.find('.category-links').height() || 0;

                this.element.find('.categories').height(dialogHeight - titleBarHeight - toolBarHeight - tabBarHeight - categoryLinkHeight - 55);

                dialogElement.css({
                    left: $content.position().left + (left || 0),
                    top: (top || 50),
                });
            }

        }

    }
}