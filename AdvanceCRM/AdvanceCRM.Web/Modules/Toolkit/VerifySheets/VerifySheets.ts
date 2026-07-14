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
        /** Opens the sub-module's own dialog, so "+ Add" reuses its validation and permissions. */
        newDialog: () => Serenity.EntityDialog<any, any>;
        /**
         * What the sheet is keyed by. Everything is campaign-wise except Master Suppression,
         * which is maintained per Master Account and carries no CampaignId.
         */
        scope?: 'campaign' | 'account';
        /**
         * Joined (view) fields the list must be asked for explicitly — the List service only
         * returns table columns by default, so usernames/account numbers come back blank without this.
         */
        includeColumns?: string[];
        /** Open Campaign gets a lightweight inline "type a domain + Add" box above its table. */
        quickAddDomain?: boolean;
    }

    function vsEscape(value: any): string {
        if (value == null)
            return '';
        return Q.htmlEncode(String(value));
    }

    // A section shows at most this many rows until the user clicks "more ++" to reveal the rest.
    var VsPreviewLimit = 5;

    /**
     * Verify Sheets page: pick a Campaign and view its data pulled from every Tool Kit
     * sub-module (Specification, Email Suppression, Competitor, TAL, Master Suppression,
     * Open Campaign) each in its own section.
     */
    export class VerifySheetsPage {

        private element: JQuery;
        private accountEditor: Serenity.LookupEditor;
        private campaignEditor: Serenity.LookupEditor;
        private searchInput: JQuery;
        private collapseAllBtn: JQuery;
        private sheets: VsSheet[];
        private visible: { [key: string]: boolean } = {};
        /** Whether a section's body is collapsed, so tall sheets don't force long scrolling. */
        private collapsed: { [key: string]: boolean } = {};
        /** Rows exactly as loaded from the server, before the search box filters them. */
        private loaded: { [key: string]: any[] } = {};
        /** Whether a section is showing all its rows (true) or just the first few + "more ++". */
        private expanded: { [key: string]: boolean } = {};
        private searchTerm = '';

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
                    newDialog: () => new DemandaySpecsDialog(),
                    columns: [
                        { field: 'SrNo', title: 'Sr No' },
                        { field: 'OrderId', title: 'Order ID' },
                        { field: 'JobTitle', title: 'Job Title' },
                        { field: 'JobLevel', title: 'Job Level' },
                        { field: 'JobFunction', title: 'Job Function' },
                        { field: 'Industry', title: 'Industry' },
                        { field: 'CompanyEmployeeSize', title: 'Company Employee Size' },
                        { field: 'AnnualRevenue', title: 'Annual Revenue' },
                        { field: 'ExcludeCompany', title: 'Exclude Company' },
                        { field: 'Address', title: 'Address' },
                        { field: 'City', title: 'City' },
                        { field: 'State', title: 'State' },
                        { field: 'ZipCode', title: 'Zip Code' },
                        { field: 'Country', title: 'Country' },
                        { field: 'Comments', title: 'Comments' },
                        { field: 'AdditionalNotes', title: 'Additional Notes' }
                    ]
                },
                {
                    key: 'EmailSuppression', title: 'Email Suppression',
                    list: ClientSupressionService.List,
                    newDialog: () => new ClientSupressionDialog(),
                    columns: [                        { field: 'SrNo', title: 'Sr No' },
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
                    newDialog: () => new DemandayCompetitorDialog(),
                    columns: [                        { field: 'SrNo', title: 'Sr No' },
                        { field: 'CompanyName', title: 'Company Name' },
                        { field: 'Domain', title: 'Domain' },
                        { field: 'Email', title: 'Email' },
                        { field: 'Cpc', title: 'CPC' }
                    ]
                },
                {
                    key: 'TALList', title: 'TAL List',
                    list: TalCampaignService.List,
                    newDialog: () => new TalCampaignDialog(),
                    includeColumns: ['AgentDisplayName'],
                    columns: [
                        { field: 'SrNo', title: 'Sr No' },
                        { field: 'CompanyName', title: 'Company Name' },
                        { field: 'Domain', title: 'Domain' },
                        { field: 'AgentDisplayName', title: 'Agent' },
                        { field: 'Reason', title: 'Reason' }
                    ]
                },
                {
                    key: 'MasterSuppression', title: 'Master Suppression',
                    list: MasterSupressionService.List,
                    newDialog: () => new MasterSupressionDialog(),
                    scope: 'account',
                    columns: [                        { field: 'SrNo', title: 'Sr No' },
                        { field: 'CompanyName', title: 'Company Name' },
                        { field: 'FirstName', title: 'First Name' },
                        { field: 'LastName', title: 'Last Name' },
                        { field: 'Email', title: 'Email' },
                        { field: 'Domain', title: 'Domain' }
                    ]
                },
                {
                    key: 'OpenCampaign', title: 'Open Campaign',
                    list: OpenCampaignService.List,
                    newDialog: () => new OpenCampaignDialog(),
                    quickAddDomain: true,
                    includeColumns: ['DemandayUserUsername', 'MasterAccountAccountNumber'],
                    columns: [                        { field: 'SrNo', title: 'Sr No' },
                        { field: 'Domain', title: 'Domain' },
                        { field: 'DemandayUserUsername', title: 'Demanday User' },
                        { field: 'MasterAccountAccountNumber', title: 'Account Number' },
                        { field: 'TimeStamp', title: 'Time Stamp' }
                    ]
                }
            ];
        }

        private render() {
            var el = this.element;
            el.addClass('verify-sheets-page');

            // Adding rows on this page is gated by its own permission; without it the "+ Add"
            // buttons (and Open Campaign's quick "Add domain" box) are not rendered at all.
            var canAdd = Q.Authorization.hasPermission('Toolkit:VerifySheets:Add');

            var toolbar = $('<div class="vs-toolbar"></div>').appendTo(el);
            $('<div class="vs-account-holder"></div>').appendTo(toolbar);
            $('<div class="vs-campaign-holder"></div>').appendTo(toolbar);

            // One dropdown drives both jobs: the checkbox shows/hides a sheet's section, and the
            // upload icon on the same row imports an Excel into that sheet.
            var ddWrap = $('<div class="vs-dropdown"></div>').appendTo(toolbar);
            var ddBtn = $('<button type="button" class="vs-verify-btn">Verify Sheets <span class="caret"></span></button>').appendTo(ddWrap);
            var ddMenu = $('<div class="vs-verify-menu"></div>').appendTo(ddWrap);
            this.sheets.forEach(s => {
                var item = $('<div class="vs-verify-item"></div>').appendTo(ddMenu);
                // The checkbox lives in its own label so the upload button does not toggle it.
                var lbl = $('<label class="vs-verify-label"></label>').appendTo(item);
                $('<input type="checkbox" checked>').attr('data-key', s.key).appendTo(lbl);
                $('<span></span>').text(' ' + s.title).appendTo(lbl);
                $('<button type="button" class="vs-item-upload"><i class="fa fa-upload"></i></button>')
                    .attr('title', 'Import Excel into ' + s.title)
                    .appendTo(item)
                    .on('click', e => {
                        e.stopPropagation();
                        ddMenu.hide();
                        this.doUpload(s.key);
                    });
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

            // Search box: filters the already-loaded rows of every visible sheet, matching
            // the term anywhere inside any displayed column.
            var searchWrap = $('<div class="vs-search"></div>').appendTo(toolbar);
            this.searchInput = $('<input type="text" class="vs-search-input" placeholder="Search all sheets…">')
                .appendTo(searchWrap)
                .on('input', () => {
                    this.searchTerm = String(this.searchInput.val() || '').trim().toLowerCase();
                    this.renderAllLoaded();
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

            // Collapse / expand every section at once — the fast way past a tall sheet.
            this.collapseAllBtn = $('<button type="button" class="vs-collapse-all"><i class="fa fa-compress"></i> Collapse all</button>')
                .appendTo(toolbar)
                .on('click', () => this.toggleCollapseAll());

            // Sections
            var body = $('<div class="vs-sections"></div>').appendTo(el);
            this.sheets.forEach(s => {
                var sec = $('<div class="vs-section"></div>').attr('data-key', s.key).appendTo(body);
                var head = $('<div class="vs-card-head"></div>').appendTo(sec);

                // Caret + title toggle the section; the buttons/input to the right keep working.
                var toggle = $('<span class="vs-head-toggle"></span>').appendTo(head)
                    .on('click', () => this.toggleCollapse(s.key));
                $('<i class="fa fa-chevron-down vs-caret"></i>').appendTo(toggle);
                $('<span class="vs-title"></span>').text(s.title).appendTo(toggle);
                $('<span class="vs-count-badge">0</span>').appendTo(head).addClass('vs-count');

                // Open Campaign: a fast "type a domain + Enter" box just left of the full Add button,
                // so a new domain can be added without opening the whole dialog.
                if (canAdd && s.quickAddDomain) {
                    var quick = $('<div class="vs-quick-add"></div>').appendTo(head);
                    var domainInput = $('<input type="text" class="vs-quick-input" placeholder="New domain…">')
                        .appendTo(quick)
                        .on('keydown', (e: any) => {
                            if (e.which === 13) { e.preventDefault(); this.quickAddDomain(s, domainInput); }
                        });
                    $('<button type="button" class="vs-quick-btn">Add domain</button>')
                        .appendTo(quick)
                        .on('click', () => this.quickAddDomain(s, domainInput));
                }

                if (canAdd) {
                    $('<button type="button" class="vs-add-btn"><i class="fa fa-plus"></i> Add</button>')
                        .appendTo(head)
                        .on('click', () => this.addRecord(s));
                }
                $('<div class="vs-card-body"></div>').appendTo(sec);
            });

            // The Sr No cells are edit links. Delegated once here so re-rendered tables keep working.
            el.on('click', '.vs-edit-link', e => {
                e.preventDefault();
                var link = $(e.currentTarget);
                var sheet = this.sheetByKey(String(link.attr('data-key')));
                var id = parseInt(String(link.attr('data-id')), 10);
                if (sheet && !isNaN(id))
                    this.openRecord(sheet, id);
            });

            // "more ++" / "less --" toggles a section between its 5-row preview and the full list.
            el.on('click', '.vs-more-toggle', e => {
                e.preventDefault();
                var key = String($(e.currentTarget).attr('data-key'));
                this.expanded[key] = !this.expanded[key];
                var sheet = this.sheetByKey(key);
                if (sheet)
                    this.renderSheet(sheet);
            });

            // Account editor drives the Campaign cascade.
            this.accountEditor = Serenity.Widget.create({
                type: Serenity.LookupEditor,
                element: e => e.appendTo(el.find('.vs-account-holder')).attr('id', 'vsAccountId').attr('placeholder', 'Account'),
                options: <Serenity.LookupEditorOptions>{ lookupKey: 'Masters.DemandayMasterAccount' }
            });

            // Campaign editor — cascaded from the selected Account.
            this.campaignEditor = Serenity.Widget.create({
                type: Serenity.LookupEditor,
                element: e => e.appendTo(el.find('.vs-campaign-holder')).attr('placeholder', 'Campaign ID'),
                options: <any>{
                    lookupKey: 'Masters.DemandayCampaignId',
                    cascadeFrom: 'vsAccountId',
                    cascadeField: 'DemandayMasterAccountId'
                }
            });
            this.campaignEditor.changeSelect2(() => this.loadAll());

            this.setPlaceholder('Select a Campaign ID to view its sheets.');
        }

        /** Collapses/expands one section's body; the header (with its count) stays visible. */
        private toggleCollapse(key: string) {
            this.collapsed[key] = !this.collapsed[key];
            this.applyCollapsed(key);
            this.syncCollapseAllButton();
        }

        private applyCollapsed(key: string) {
            var sec = this.element.find('.vs-section[data-key="' + key + '"]');
            var isCollapsed = !!this.collapsed[key];
            sec.toggleClass('vs-collapsed', isCollapsed);
            sec.find('.vs-card-body').toggle(!isCollapsed);
        }

        /** If every visible section is collapsed, the button offers "Expand all"; otherwise "Collapse all". */
        private toggleCollapseAll() {
            var visibleKeys = this.sheets.filter(s => this.visible[s.key]).map(s => s.key);
            var anyExpanded = visibleKeys.some(k => !this.collapsed[k]);

            visibleKeys.forEach(k => {
                this.collapsed[k] = anyExpanded; // collapse them all, or expand them all
                this.applyCollapsed(k);
            });
            this.syncCollapseAllButton();
        }

        private syncCollapseAllButton() {
            if (!this.collapseAllBtn)
                return;
            var visibleKeys = this.sheets.filter(s => this.visible[s.key]).map(s => s.key);
            var allCollapsed = visibleKeys.length > 0 && visibleKeys.every(k => this.collapsed[k]);
            this.collapseAllBtn.html(allCollapsed
                ? '<i class="fa fa-expand"></i> Expand all'
                : '<i class="fa fa-compress"></i> Collapse all');
        }

        private sheetByKey(key: string): VsSheet {
            return this.sheets.filter(s => s.key === key)[0];
        }

        private doUpload(sheetKey: string) {
            var sheetDef = this.sheetByKey(sheetKey);
            var campaignId = this.getCampaignId();
            var accountId = this.getAccountId();

            // Master Suppression only needs the Master Account; the rest need the Campaign.
            if (sheetDef && sheetDef.scope === 'account') {
                if (accountId == null) {
                    Q.notifyError('Please select a Master Account first.');
                    return;
                }
            }
            else if (campaignId == null) {
                Q.notifyError('Please select a Campaign first.');
                return;
            }

            var fileInput = document.createElement('input');
            fileInput.type = 'file';
            fileInput.accept = '.xlsx';
            fileInput.style.display = 'none';
            document.body.appendChild(fileInput);
            fileInput.onchange = () => {
                if (fileInput.files && fileInput.files.length > 0) {
                    var fd = new FormData();
                    fd.append('file', fileInput.files[0]);
                    fd.append('campaignId', String(campaignId != null ? campaignId : 0));
                    fd.append('masterAccountId', String(accountId != null ? accountId : 0));
                    fd.append('sheet', sheetKey);
                    fetch(Q.resolveUrl('~/Toolkit/VerifySheets/ImportExcel'), { method: 'POST', body: fd })
                        .then(r => r.text().then(msg => {
                            alert(msg || 'Import completed.');
                            var sheet = this.sheetByKey(sheetKey);
                            if (sheet) this.loadSheet(sheet);
                        }))
                        .catch(err => alert('Upload failed: ' + err));
                }
                if (fileInput.parentNode) document.body.removeChild(fileInput);
            };
            fileInput.click();
        }

        /** Opens the sub-module's own dialog, pre-tagged with the selected Campaign/Account. */
        private addRecord(sheet: VsSheet) {
            var campaignId = this.getCampaignId();
            var accountId = this.getAccountId();
            var entity: any = {};

            if (sheet.scope === 'account') {
                if (accountId == null) {
                    Q.notifyError('Please select a Master Account first.');
                    return;
                }
                // Account-wise sheet: deliberately no CampaignId.
                entity.MasterAccountId = accountId;
            }
            else {
                if (campaignId == null) {
                    Q.notifyError('Please select a Campaign first.');
                    return;
                }
                entity.CampaignId = campaignId;
                if (accountId != null)
                    entity.MasterAccountId = accountId;
            }

            var dlg = sheet.newDialog();
            // EntityDialog fires this after a successful save or delete.
            dlg.element.on('ondatachange', () => this.loadSheet(sheet));

            // Passing an entity without an Id makes the dialog open in "new record" mode.
            dlg.loadEntityAndOpenDialog(entity);
        }

        /** Inserts one Open Campaign row straight from the inline domain box. */
        private quickAddDomain(sheet: VsSheet, input: JQuery) {
            var campaignId = this.getCampaignId();
            if (campaignId == null) {
                Q.notifyError('Please select a Campaign first.');
                return;
            }

            var domain = String(input.val() || '').trim();
            if (!domain) {
                Q.notifyError('Please enter a domain.');
                return;
            }

            var entity: any = { CampaignId: campaignId, Domain: domain };
            var accountId = this.getAccountId();
            if (accountId != null)
                entity.MasterAccountId = accountId;

            OpenCampaignService.Create({ Entity: entity }, () => {
                input.val('');
                input.focus();
                Q.notifySuccess('Domain added.');
                this.loadSheet(sheet);
            });
        }

        private getCampaignId(): number {
            var v = this.campaignEditor.value;
            if (Q.isEmptyOrNull(v))
                return null;
            var id = parseInt(v, 10);
            return isNaN(id) ? null : id;
        }

        private getAccountId(): number {
            var v = this.accountEditor.value;
            if (Q.isEmptyOrNull(v))
                return null;
            var id = parseInt(v, 10);
            return isNaN(id) ? null : id;
        }

        private setPlaceholder(message: string) {
            this.loaded = {};
            this.sheets.forEach(s => {
                var sec = this.element.find('.vs-section[data-key="' + s.key + '"]');
                sec.find('.vs-count').text('0');
                sec.find('.vs-card-body').html('<div class="vs-empty">' + Q.htmlEncode(message) + '</div>');
            });
        }

        /** True when the term appears in any column the sheet actually displays. */
        private matchesSearch(sheet: VsSheet, row: any): boolean {
            if (!this.searchTerm)
                return true;

            for (var i = 0; i < sheet.columns.length; i++) {
                var value = row[sheet.columns[i].field];
                if (value == null)
                    continue;
                if (String(value).toLowerCase().indexOf(this.searchTerm) >= 0)
                    return true;
            }
            return false;
        }

        /** Re-renders from the cache — the search box never re-queries the server. */
        private renderAllLoaded() {
            this.sheets.forEach(s => {
                if (this.visible[s.key] && this.loaded[s.key])
                    this.renderSheet(s);
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
            var filter: any;
            if (sheet.scope === 'account') {
                var accountId = this.getAccountId();
                if (accountId == null)
                    return;
                filter = { MasterAccountId: accountId };
            }
            else {
                var campaignId = this.getCampaignId();
                if (campaignId == null)
                    return;
                filter = { CampaignId: campaignId };
            }

            var bodyEl = this.element.find('.vs-section[data-key="' + sheet.key + '"] .vs-card-body');
            bodyEl.html('<div class="vs-loading">Loading…</div>');

            var request = <Serenity.ListRequest>{
                EqualityFilter: filter
            };
            // Ask for joined view columns (usernames, account numbers) — omitted by default.
            if (sheet.includeColumns)
                request.IncludeColumns = sheet.includeColumns;

            sheet.list(request,
                response => {
                    this.loaded[sheet.key] = response.Entities || [];
                    // A fresh load starts collapsed to the 5-row preview.
                    this.expanded[sheet.key] = false;
                    this.renderSheet(sheet);
                },
                <Q.ServiceOptions<any>>{
                    blockUI: false,
                    onError: () => {
                        delete this.loaded[sheet.key];
                        bodyEl.html('<div class="vs-error">Unable to load (you may not have permission for this sheet).</div>');
                    }
                });
        }

        private renderSheet(sheet: VsSheet) {
            var all = this.loaded[sheet.key] || [];
            var entities = all.filter(row => this.matchesSearch(sheet, row));

            var sec = this.element.find('.vs-section[data-key="' + sheet.key + '"]');
            // While searching, show how much of the campaign's data is being hidden.
            sec.find('.vs-count').text(this.searchTerm
                ? entities.length + ' / ' + all.length
                : String(all.length));
            var bodyEl = sec.find('.vs-card-body');

            if (!entities.length) {
                bodyEl.html('<div class="vs-empty">' + (this.searchTerm
                    ? 'No rows match “' + Q.htmlEncode(this.searchTerm) + '”.'
                    : 'No records for this campaign.') + '</div>');
                return;
            }

            // Show only the first few rows until "more ++" is clicked, so a big sheet stays compact.
            var isExpanded = !!this.expanded[sheet.key];
            var hasMore = entities.length > VsPreviewLimit;
            var visibleRows = (isExpanded || !hasMore) ? entities : entities.slice(0, VsPreviewLimit);

            var html = '<div class="vs-table-wrap"><table class="vs-table"><thead><tr>';
            sheet.columns.forEach(c => html += '<th>' + vsEscape(c.title) + '</th>');
            html += '</tr></thead><tbody>';
            visibleRows.forEach(row => {
                html += '<tr>';
                sheet.columns.forEach(c => {
                    // Sr No opens the record's own dialog; needs the row Id to load it.
                    if (c.field === 'SrNo' && row['Id'] != null) {
                        html += '<td><a href="#" class="vs-edit-link" data-key="' + sheet.key +
                            '" data-id="' + row['Id'] + '">' + vsEscape(row['SrNo']) + '</a></td>';
                    }
                    else {
                        html += '<td>' + vsEscape(row[c.field]) + '</td>';
                    }
                });
                html += '</tr>';
            });

            // A full-width row carrying the "more ++" / "less --" toggle when the sheet has 5+ rows.
            if (hasMore) {
                var colCount = sheet.columns.length;
                var toggleText = isExpanded
                    ? 'less --'
                    : 'more ++ (' + (entities.length - VsPreviewLimit) + ' more)';
                html += '<tr class="vs-more-row"><td colspan="' + colCount + '">' +
                    '<a href="#" class="vs-more-toggle" data-key="' + sheet.key + '">' +
                    vsEscape(toggleText) + '</a></td></tr>';
            }

            html += '</tbody></table></div>';
            bodyEl.html(html);
        }

        /** Opens an existing record in the sub-module's own dialog; reloads the sheet on save/delete. */
        private openRecord(sheet: VsSheet, id: number) {
            var dlg = sheet.newDialog();
            dlg.element.on('ondatachange', () => this.loadSheet(sheet));
            dlg.loadByIdAndOpenDialog(id);
        }
    }
}
