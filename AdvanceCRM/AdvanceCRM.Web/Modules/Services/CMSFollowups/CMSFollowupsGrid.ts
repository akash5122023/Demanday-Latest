
namespace AdvanceCRM.Services {

    @Serenity.Decorators.registerClass()
    export class CMSFollowupsGrid extends Serenity.EntityGrid<CMSFollowupsRow, any> {
        protected getColumnsKey() { return 'Services.CMSFollowups'; }
        protected getIdProperty() { return CMSFollowupsRow.idProperty; }
        protected getInsertPermission() { return CMSFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return CMSFollowupsRow.localTextPrefix; }
        protected getService() { return CMSFollowupsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
            this.element.find('.quick-filters-bar').toggle(false);
        }

        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ CMSId: this.CMSId });
        }

        protected getInitialTitle() {
            return null;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.CMSId;
        }

        private _CMSId: string;

        get CMSId() {
            return this._CMSId;
        }

        set CMSId(value: string) {
            if (this._CMSId !== value) {
                this._CMSId = value;
                this.setEquality('CMSId', value);
                this.refresh();
            }
        }
    }
}