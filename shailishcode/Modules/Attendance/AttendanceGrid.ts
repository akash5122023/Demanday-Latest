
namespace AdvanceCRM.Attendance {
    import fld = AttendanceRow.Fields;
    @Serenity.Decorators.registerClass()
    export class AttendanceGrid extends GridBase<AttendanceRow, any> {
        protected getColumnsKey() { return 'Attendance.Attendance'; }
        protected getDialogType() { return AttendanceDialog; }
        protected getIdProperty() { return AttendanceRow.idProperty; }
        protected getInsertPermission() { return AttendanceRow.insertPermission; }
        protected getLocalTextPrefix() { return AttendanceRow.localTextPrefix; }
        protected getService() { return AttendanceService.baseUrl; }

        constructor(container: JQuery) {
            super(container);

            var q = Q.parseQueryString();
            if (q["Today"]) {
              //  this.findQuickFilter(Serenity.DateEditor, fld.DateNTime).value = Date.now.toString("yyyy-MM-dd");
            }
           
        }



        getButtons() {
            var buttons = super.getButtons();

            if (!Authorization.hasPermission("Attendance:Manual Entry")) {
                buttons.shift();
            }

            return buttons;
        }
    }
}