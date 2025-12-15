
namespace AdvanceCRM.Common {

    @Serenity.Decorators.registerClass()
    export class NotificationUsersGrid extends Serenity.EntityGrid<NotificationUsersRow, any> {
        protected getColumnsKey() { return 'Common.NotificationUsers'; }
        protected getDialogType() { return null; }
        protected getIdProperty() { return NotificationUsersRow.idProperty; }
        protected getInsertPermission() { return NotificationUsersRow.insertPermission; }
        protected getLocalTextPrefix() { return NotificationUsersRow.localTextPrefix; }
        protected getService() { return NotificationUsersService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected onClick(e: JQueryEventObject, row: number, cell: number): void {

            // let base grid handle clicks for its edit links
            super.onClick(e, row, cell);

            // if base grid already handled, we shouldn"t handle it again
            if (e.isDefaultPrevented()) {
                return;
            }

            // get reference to current item
            var item = this.itemAt(row);
            e.preventDefault();
            window.open(item.NotificationsUrl, '_self');
        }

        getButtons() {
            var buttons = super.getButtons();
            buttons.shift();
            return buttons;
        }
    }
}