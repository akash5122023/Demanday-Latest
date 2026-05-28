
namespace AdvanceCRM.Demanday {

    @Serenity.Decorators.registerClass()
    export class DemandayTeleMarketingEnquiryGrid extends GridBase<DemandayTeleMarketingEnquiryRow, any> {
        protected getColumnsKey() { return 'Demanday.DemandayTeleMarketingEnquiry' }
        protected getDialogType() { return DemandayTeleMarketingEnquiryDialog; }
        protected getIdProperty() { return DemandayTeleMarketingEnquiryRow.idProperty; }
        protected getInsertPermission() { return DemandayTeleMarketingEnquiryRow.insertPermission; }
        protected getLocalTextPrefix() { return DemandayTeleMarketingEnquiryRow.localTextPrefix; }
        protected getService() { return DemandayTeleMarketingEnquiryService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getColumns() {
            let columns = super.getColumns();

            let attachmentsCol = columns.find(x => x.field === 'Attachments');
            if (attachmentsCol) {
                attachmentsCol.format = (ctx) => {
                    if (!ctx.value)
                        return '';

                    let files = ctx.value.split('|').filter(f => f.trim());
                    if (files.length === 0)
                        return '';

                    let html = '<div class="audio-player-container">';
                    files.forEach(file => {
                        let filename = file.split('/').pop();
                        html += `
                            <div class="audio-item" style="margin: 5px 0;">
                                <audio controls preload="none" style="height: 30px; max-width: 200px;">
                                    <source src="${Q.resolveUrl('~/upload/')}${file}" type="audio/mpeg">
                                    Your browser does not support audio.
                                </audio>
                                <a href="${Q.resolveUrl('~/upload/')}${file}" download="${filename}"
                                   class="btn btn-sm btn-info" style="margin-left: 5px;" title="Download">
                                    <i class="fa fa-download"></i>
                                </a>
                            </div>`;
                    });
                    html += '</div>';
                    return html;
                };
            }

            return columns;
        }

        protected getButtons(): Serenity.ToolButton[] {
            let buttons = super.getButtons();

            return buttons;
        }
    }
}