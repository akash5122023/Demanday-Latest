
namespace AdvanceCRM.Enquiry {

    @Serenity.Decorators.registerClass()
    export class EnquiryFollowupsGrid extends Serenity.EntityGrid<EnquiryFollowupsRow, any> {
        protected getColumnsKey() { return 'Enquiry.EnquiryFollowups'; }
        protected getIdProperty() { return EnquiryFollowupsRow.idProperty; }
        protected getInsertPermission() { return EnquiryFollowupsRow.insertPermission; }
        protected getLocalTextPrefix() { return EnquiryFollowupsRow.localTextPrefix; }
        protected getService() { return EnquiryFollowupsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
            this.element.find('.quick-filters-bar').toggle(false);
        }

        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ EnquiryId: this.enquiryId });
        }

        protected getInitialTitle() {
            return null;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.enquiryId;
        }

        private _enquiryId: string;

        get enquiryId() {
            return this._enquiryId;
        }

        set enquiryId(value: string) {
            if (this._enquiryId !== value) {
                this._enquiryId = value;
                this.setEquality('EnquiryId', value);
                this.refresh();
            }
        }
    }
}