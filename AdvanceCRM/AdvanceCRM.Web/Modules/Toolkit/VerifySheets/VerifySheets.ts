namespace AdvanceCRM.Toolkit {

    interface VsColumn {
        field: string;
        title: string;
    }

    interface VsSheet {
        key: string;
        title: string;
        list: (request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<any>) => void, opt?: Q.ServiceOptions<any>) => JQueryXHR;
        columns: VsColumn[];
    }

    function vsEscape(value: any): string {
        if (value == null)
            return '';
        return Q.htmlEncode(String(value));
    }

    /**
     * Verify Sheets page: pick a Campaign and view its data pulled from every Tool Kit
     * sub-module (Specification, Email Suppression, Competitor, TAL, Master Suppression)
     * each in its own section.
     */
    export class VerifySheetsPage {

        private element: JQuery;
        private campaignEditor: Serenity.LookupEditor;
        private sheets: VsSheet[];
        private visible: { [key: string]: boolean } = {};

        constructor(element: JQuery) {
            this.element = element;
            this.sheets = this.getSheets();
            this.sheets.forEach(s => this.visible[s.key] = true);
            this.render();
        }

        private getSheets(): VsSheet[] {
            return [
                {
                    key: 'Specification', title: 'Specification',
                    list: DemandaySpecsService.List,
                    columns: [
                        { field: 'Id', title: 'ID' },
                        { field: 'OrderId', title: 'Order ID' },
                        { field: 'JobTitle', title: 'Job Title' },
                        { field: 'JobLevel', title: 'Job Level' },
                        { field: 'JobFunction', title: 'Job Function' },
                        { field: 'Industry', title: 'Industry' },
                        { field: 'City', title: 'City' },
                        { field: 'Country', title: 'Country' }
                    ]
                },
                {
                    key: 'EmailSuppression', title: 'Email Suppression',
                    list: ClientSupressionService.List,
                    columns: [
                        { field: 'Id', title: 'ID' },
                        { field: 'CompanyName', title: 'Company Name' },
                        { field: 'FirstName', title: 'First Name' },
                        { field: 'LastName', title: 'Last Name' },
                        { field: 'Email', title: 'Email' },
                        { field: 'Domain', title: 'Domain' }
                    ]
                },
                {
                    key: 'CompetitorList', title: 'Competitor List',
                    list: DemandayCompetitorService.List,
                    columns: [
                        { field: 'Id', title: 'ID' },
                        { field: 'CompanyName', title: 'Company Name' },
                        { field: 'Domain', title: 'Domain' },
                        { field: 'Email', title: 'Email' },
                        { field: 'Cpc', title: 'CPC' }
                    ]
                },
                {
                    key: 'TALList', title: 'TAL List',
                    list: TalCampaignService.List,
                    columns: [
                        { field: 'Id', title: 'ID' },
                        { field: 'CompanyName', title: 'Company Name' },
                        { field: 'Domain', title: 'Domain' },
                        { field: 'AgentDisplayName', title: 'Agent' }
                    ]
                },
                {
                    key: 'MasterSuppression', title: 'Master Suppression',
                    list: MasterSupressionService.List,
                    columns: [
                        { field: 'Id', title: 'ID' },
                        { field: 'CompanyName', title: 'Company Name' },
                        { field: 'FirstName', title: 'First Name' },
                        { field: 'LastName', title: 'Last Name' },
                        { field: 'Email', title: 'Email' },
                        { field: 'Domain', title: 'Domain' }
                    ]
                }
            ];
        }

        private render() {
            var el = this.element;
            el.addClass('verify-sheets-page');

            var toolbar = $('<div class="vs-toolbar"></div>').appendTo(el);
            $('<div class="vs-campaign-holder"></div>').appendTo(toolbar);

            // Verify Sheets multi-select dropdown
            var ddWrap = $('<div class="vs-dropdown"></div>').appendTo(toolbar);
            var ddBtn = $('<button type="button" class="vs-verify-btn">Verify Sheets <span class="caret"></span></button>').appendTo(ddWrap);
            var ddMenu = $('<div class="vs-verify-menu"></div>').appendTo(ddWrap);
            this.sheets.forEach(s => {
                var item = $('<label class="vs-verify-item"></label>').appendTo(ddMenu);
                $('<input type="checkbox" checked>').attr('data-key', s.key).appendTo(item);
                $('<span></span>').text(' ' + s.title).appendTo(item);
            });
            ddBtn.on('click', e => { e.stopPropagation(); ddMenu.toggle(); });
            $(document).on('click', () => ddMenu.hide());
            ddMenu.on('click', e => e.stopPropagation());
            ddMenu.find('input[type=checkbox]').on('change', e => {
                var cb = $(e.target);
                var key = cb.attr('data-key');
                this.visible[key] = cb.is(':checked');
                var sec = el.find('.vs-section[data-key="' + key + '"]');
                sec.toggle(this.visible[key]);
                if (this.visible[key] && this.getCampaignId() != null)
                    this.loadSheet(this.sheetByKey(key));
            });

            $('<button type="button" class="vs-export-btn"><i class="fa fa-download"></i> Export to Excel</button>')
                .appendTo(toolbar)
                .on('click', () => {
                    var campaignId = this.getCampaignId();
                    if (campaignId == null) {
                        Q.notifyError('Please select a Campaign first.');
                        return;
                    }
                    window.location.href = Q.resolveUrl('~/Toolkit/VerifySheets/ExportExcel?campaignId=' + campaignId);
                });

            // Sections
            var body = $('<div class="vs-sections"></div>').appendTo(el);
            this.sheets.forEach(s => {
                var sec = $('<div class="vs-section"></div>').attr('data-key', s.key).appendTo(body);
                var head = $('<div class="vs-card-head"></div>').appendTo(sec);
                $('<span class="vs-title"></span>').text(s.title).appendTo(head);
                $('<span class="vs-count-badge">0</span>').appendTo(head).addClass('vs-count');
                $('<div class="vs-card-body"></div>').appendTo(sec);
            });

            // Campaign editor
            this.campaignEditor = Serenity.Widget.create({
                type: Serenity.LookupEditor,
                element: e => e.appendTo(el.find('.vs-campaign-holder')).attr('placeholder', 'Campaign ID'),
                options: <Serenity.LookupEditorOptions>{ lookupKey: 'Masters.DemandayCampaignId' }
            });
            this.campaignEditor.changeSelect2(() => this.loadAll());

            this.setPlaceholder('Select a Campaign ID to view its sheets.');
        }

        private sheetByKey(key: string): VsSheet {
            return this.sheets.filter(s => s.key === key)[0];
        }

        private getCampaignId(): number {
            var v = this.campaignEditor.value;
            if (Q.isEmptyOrNull(v))
                return null;
            var id = parseInt(v, 10);
            return isNaN(id) ? null : id;
        }

        private setPlaceholder(message: string) {
            this.sheets.forEach(s => {
                this.element.find('.vs-section[data-key="' + s.key + '"] .vs-card-body')
                    .html('<div class="vs-empty">' + Q.htmlEncode(message) + '</div>');
            });
        }

        private loadAll() {
            var campaignId = this.getCampaignId();
            if (campaignId == null) {
                this.setPlaceholder('Select a Campaign ID to view its sheets.');
                return;
            }
            this.sheets.forEach(s => {
                if (this.visible[s.key])
                    this.loadSheet(s);
            });
        }

        private loadSheet(sheet: VsSheet) {
            var campaignId = this.getCampaignId();
            if (campaignId == null)
                return;

            var bodyEl = this.element.find('.vs-section[data-key="' + sheet.key + '"] .vs-card-body');
            bodyEl.html('<div class="vs-loading">Loading…</div>');

            var request = <Serenity.ListRequest>{
                EqualityFilter: { CampaignId: campaignId }
            };

            sheet.list(request,
                response => this.renderSheet(sheet, response.Entities || []),
                <Q.ServiceOptions<any>>{
                    blockUI: false,
                    onError: () => bodyEl.html('<div class="vs-error">Unable to load (you may not have permission for this sheet).</div>')
                });
        }

        private renderSheet(sheet: VsSheet, entities: any[]) {
            var sec = this.element.find('.vs-section[data-key="' + sheet.key + '"]');
            sec.find('.vs-count').text(String(entities.length));
            var bodyEl = sec.find('.vs-card-body');

            if (!entities.length) {
                bodyEl.html('<div class="vs-empty">No records for this campaign.</div>');
                return;
            }

            var html = '<div class="vs-table-wrap"><table class="vs-table"><thead><tr>';
            sheet.columns.forEach(c => html += '<th>' + vsEscape(c.title) + '</th>');
            html += '</tr></thead><tbody>';
            entities.forEach(row => {
                html += '<tr>';
                sheet.columns.forEach(c => html += '<td>' + vsEscape(row[c.field]) + '</td>');
                html += '</tr>';
            });
            html += '</tbody></table></div>';
            bodyEl.html(html);
        }
    }
}
