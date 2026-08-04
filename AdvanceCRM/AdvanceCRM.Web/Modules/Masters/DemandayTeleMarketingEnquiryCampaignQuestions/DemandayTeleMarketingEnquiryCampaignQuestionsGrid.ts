
namespace AdvanceCRM.Masters {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingEnquiryCampaignQuestionsGrid extends GridBase<DemandayTeleMarketingEnquiryCampaignQuestionsRow, any> {
        protected getColumnsKey() { return "Masters.DemandayTeleMarketingEnquiryCampaignQuestions"; }
        protected getDialogType() { return DemandayTeleMarketingEnquiryCampaignQuestionsDialog; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.idProperty; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryCampaignQuestionsRow.localTextPrefix; }
        protected getService() { return DemandayTeleMarketingEnquiryCampaignQuestionsService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected addButtonsToToolbar(): void {
            super.addButtonsToToolbar();

            this.toolbar.prependButton({
                title: 'Download Template',
                cssClass: 'export-excel-button',
                onClick: () => {
                    window.location.href = DemandayTeleMarketingEnquiryCampaignQuestionsService.baseUrl + '/DownloadTemplate';
                }
            });

            this.toolbar.prependButton({
                title: 'Import Questions & Answers from Excel',
                cssClass: 'import-button',
                onClick: () => {
                    let dialog = new ExcelImportQuestionsAnswersDialog();
                    dialog.dialogOpen();
                    dialog.element.closest(".ui-dialog").on("dialogclose", () => {
                        this.refresh();
                    });
                }
            });
        }
    }
}