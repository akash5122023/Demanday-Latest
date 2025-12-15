
namespace AdvanceCRM.Administration {

    @Serenity.Decorators.registerClass()
    export class CompanyDetailsGrid extends Serenity.EntityGrid<CompanyDetailsRow, any> {
        protected getColumnsKey() { return 'Administration.CompanyDetails'; }
        protected getDialogType() { return CompanyDetailsDialog; }
        protected getIdProperty() { return CompanyDetailsRow.idProperty; }
        protected getInsertPermission() { return CompanyDetailsRow.insertPermission; }
        protected getLocalTextPrefix() { return CompanyDetailsRow.localTextPrefix; }
        protected getService() { return CompanyDetailsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }


        getButtons() {
            var buttons = super.getButtons();
            Common.NavigationService.MultiCompany({},
                response => {
                    if (response.Status == "Remove") {
                        buttons.shift();
                    }
                });

            buttons.push({

                title: 'Backup',
                cssClass: 'send-button',
                hint: 'Take Database backup',
                onClick: () => {
                    Q.confirm(
                        "Confirm Backup Generation?",
                        () => {
                            var filename = "Backup.bak";

                            $.ajax({
                                type: "POST",
                                url: 'Services/Administration/CompanyDetails/Backup',

                                success: function (data) {

                                    var file = new Blob([data], { type: 'application/octet-stream' });
                                    if (window.navigator.msSaveOrOpenBlob) // IE10+
                                        window.navigator.msSaveOrOpenBlob(file, "Backup.bak");
                                    else { // Others
                                        var a = document.createElement("a"),
                                            url = URL.createObjectURL(file);
                                        a.href = url;
                                        a.download = filename;
                                        document.body.appendChild(a);
                                        a.click();
                                        setTimeout(function () {
                                            document.body.removeChild(a);
                                            window.URL.revokeObjectURL(url);
                                        }, 0);
                                    }
                                }
                            });
                        },
                        {
                            title: 'Database Backup'
                        });
                }
            });

            return buttons;
        }
    }
}