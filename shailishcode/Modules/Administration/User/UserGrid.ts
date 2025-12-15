namespace AdvanceCRM.Administration {

    import fld = UserRow.Fields;
    @Serenity.Decorators.registerClass()
    export class UserGrid extends GridBase<UserRow, any> {
        protected getColumnsKey() { return "Administration.User"; }
        protected getDialogType() { return UserDialog; }
        protected getIdProperty() { return UserRow.idProperty; }
        protected getIsActiveProperty() { return UserRow.isActiveProperty; }
        protected getLocalTextPrefix() { return UserRow.localTextPrefix; }
        protected getService() { return UserService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            //this.view.setGrouping(
            //    [{
            //        formatter: x => 'IsActive: ' + x.value + ' (' + x.count + ' items)',
            //        getter: fld.IsActive
            //    }])
        }

       //protected getColumns(): Slick.Column[] {
       //     var columns = super.getColumns();

       //     Q.first(columns, x => x.field == fld.IsActive).format =
       //         ctx => `<a href="javascript:;" class="state-link">${Q.htmlEncode(ctx.value)}</a>`;

       //     return columns;
       // }

        //protected onClick(e: JQueryEventObject, row: number, cell: number): void {

        //    // let base grid handle clicks for its edit links
        //    super.onClick(e, row, cell);

        //    // if base grid already handled, we shouldn"t handle it again
        //    if (e.isDefaultPrevented()) {
        //        return;
        //    }

        //    // get reference to current item
        //    var item = this.itemAt(row);

        //    // get reference to clicked element
        //    var target = $(e.target);

        //    if (target.hasClass("state-link")) {
        //        e.preventDefault();

        //        var s = Q.first(UserRow.getLookup().items,
        //            x => x.IsActive == item.IsActive);

        //        new UserDialog().loadByIdAndOpenDialog(s.IsActive);
        //    }
        //}
        protected getDefaultSortBy() {
            return [UserRow.Fields.Username];
        }
    }
}