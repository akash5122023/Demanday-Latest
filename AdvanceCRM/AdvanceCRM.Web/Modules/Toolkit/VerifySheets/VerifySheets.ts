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

    // Hard cap on rows fetched per sheet. A suppression list can hold millions of rows; asking the
    // server for all of them makes the List query time out (and would never render usefully), so we
    // pull one bounded page and show the true total from the server's TotalCount.
    var VsLoadLimit = 200;

    // Open Campaign is added to by several users at once, so its section refreshes itself on this
    // interval — a domain added by anyone shows up for everyone without pressing anything.
    var VsLivePollMs = 10000;

    // Wait for typing to settle before re-querying the server for a filter.
    var VsFilterDebounceMs = 350;

    // Formats a date/time value as "hh:mm:ss AM/PM DD/MM/YYYY" (e.g. "01:24:27 AM 15/07/2026").
    // Falls back to the raw text if the value isn't a parseable date.
    function vsFormatTimeStamp(value: any): string {
        if (value == null || value === '')
            return '';
        var d = new Date(value);
        if (isNaN(d.getTime()))
            return String(value);
        var pad = (n: number) => (n < 10 ? '0' + n : '' + n);
        var h = d.getHours();
        var ampm = h >= 12 ? 'PM' : 'AM';
        var h12 = h % 12; if (h12 === 0) h12 = 12;
        var time = pad(h12) + ':' + pad(d.getMinutes()) + ':' + pad(d.getSeconds()) + ' ' + ampm;
        var date = pad(d.getDate()) + '/' + pad(d.getMonth() + 1) + '/' + d.getFullYear();
        return time + ' ' + date;
    }

    // Formats a date value as "DD/MM/YYYY" (falls back to the raw text if unparseable).
    function vsFormatDate(value: any): string {
        if (value == null || value === '')
            return '';
        var d = new Date(value);
        if (isNaN(d.getTime()))
            return String(value);
        var pad = (n: number) => (n < 10 ? '0' + n : '' + n);
        return pad(d.getDate()) + '/' + pad(d.getMonth() + 1) + '/' + d.getFullYear();
    }

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
        /** True total row count on the server per sheet (we only fetch a bounded page of it). */
        private totals: { [key: string]: number } = {};
        /** Whether a section is showing all its rows (true) or just the first few + "more ++". */
        private expanded: { [key: string]: boolean } = {};
        private searchTerm = '';

        // Master Suppression's own filters (it is account-scoped, so these narrow it client-side
        // to a Campaign and/or a Date range without re-querying the server).
        private msCampaignEditor: Serenity.LookupEditor;
        private msCampaignHolder: JQuery;
        private msCampaignFilter: number = null;
        private msDateFrom = '';
        private msDateTo = '';
        /** Master Suppression company-name filter (contains match, applied server-side). */
        private msCompany = '';
        private msCompanyTimer: any;

        /** Timer that keeps the Open Campaign section in sync with other users' additions. */
        private livePollTimer: any;

        /** Sr No sort direction per sheet — descending (newest first) until the header is clicked. */
        private sortDesc: { [key: string]: boolean } = {};

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
                    columns: [
                        { field: 'SrNo', title: 'Sr No' },
                        { field: 'CompanyName', title: 'Company Name' },
                        { field: 'FirstName', title: 'First Name' },
                        { field: 'LastName', title: 'Last Name' },
                        { field: 'Email', title: 'Email' }
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
                        { field: 'Reason', title: 'Reason' },
                        { field: 'Cpc', title: 'CPC' }
                    ]
                },
                {
                    key: 'MasterSuppression', title: 'Master Suppression',
                    list: MasterSupressionService.List,
                    newDialog: () => new MasterSupressionDialog(),
                    scope: 'account',
                    // CampaignId / CampaignCampaignId / Date drive this sheet's own Campaign + Date filter.
                    includeColumns: ['CampaignId', 'CampaignCampaignId', 'Date'],
                    columns: [
                        { field: 'SrNo', title: 'Sr No' },
                        { field: 'CampaignCampaignId', title: 'Campaign ID' },
                        { field: 'CompanyName', title: 'Company Name' },
                        { field: 'FirstName', title: 'First Name' },
                        { field: 'LastName', title: 'Last Name' },
                        { field: 'Email', title: 'Email' },
                        { field: 'Domain', title: 'Domain' },
                        { field: 'Date', title: 'Date' }
                    ]
                },
                {
                    key: 'OpenCampaign', title: 'Open Campaign',
                    list: OpenCampaignService.List,
                    newDialog: () => new OpenCampaignDialog(),
                    quickAddDomain: true,
                    includeColumns: ['DemandayUserDisplayName', 'CampaignIdValue'],
                    columns: [
                        { field: 'SrNo', title: 'Sr No' },
                        { field: 'Domain', title: 'Domain' },
                        { field: 'DemandayUserDisplayName', title: 'Demanday User' },
                        { field: 'CampaignIdValue', title: 'Campaign ID' },
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
            var canImport = Q.Authorization.hasPermission('Toolkit:VerifySheets:Import');

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
                if (canImport) {
                    $('<button type="button" class="vs-item-upload"><i class="fa fa-upload"></i></button>')
                        .attr('title', 'Import Excel into ' + s.title)
                        .appendTo(item)
                        .on('click', e => {
                            e.stopPropagation();
                            ddMenu.hide();
                            this.doUpload(s.key);
                        });
                }
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
                // Re-showing a section loads it if its own prerequisite is met — an account-scoped
                // sheet needs only the Master Account, the rest need the Campaign.
                var shown = this.sheetByKey(key);
                if (this.visible[key] && shown) {
                    var ready = shown.scope === 'account'
                        ? this.getAccountId() != null
                        : this.getCampaignId() != null;
                    if (ready)
                        this.loadSheet(shown);
                }
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
                    // Fetched through the progress panel rather than a plain navigation, so a big
                    // export shows how far along the download is instead of looking frozen.
                    AdvanceCRM.Common.TransferProgress.download({
                        url: '~/Toolkit/VerifySheets/ExportExcel?campaignId=' + campaignId,
                        title: 'Exporting Verify Sheets',
                        preparingText: 'Building the sheets on the server…',
                        fileName: 'VerifySheets.zip'
                    });
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

                // Open Campaign keeps its fast "type a domain + Enter" box; the generic per-section
                // "+ Add" button was removed, so other modules are populated via import only.
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

                // Master Suppression gets its own Campaign + Date filter (it is account-scoped, so the
                // main Campaign selector does not apply to it). The campaign editor is instantiated
                // later, once the account editor exists to cascade from.
                if (s.key === 'MasterSuppression') {
                    var msFilter = $('<div class="vs-ms-filter"></div>').appendTo(head);
                    $('<span class="vs-ms-label">Company</span>').appendTo(msFilter);
                    $('<input type="text" class="vs-ms-company" placeholder="Contains…">').appendTo(msFilter)
                        .on('input', (e: any) => {
                            // Debounced so a re-query fires once the user stops typing.
                            var v = String($(e.target).val() || '');
                            if (this.msCompanyTimer)
                                clearTimeout(this.msCompanyTimer);
                            this.msCompanyTimer = setTimeout(() => {
                                this.msCompany = v.trim();
                                this.loadSheet(s);
                            }, VsFilterDebounceMs);
                        });
                    $('<span class="vs-ms-label">Campaign</span>').appendTo(msFilter);
                    this.msCampaignHolder = $('<span class="vs-ms-campaign"></span>').appendTo(msFilter);
                    $('<span class="vs-ms-label">Date</span>').appendTo(msFilter);
                    $('<input type="date" class="vs-ms-date" title="From date">').appendTo(msFilter)
                        .on('change', (e: any) => { this.msDateFrom = String($(e.target).val() || ''); this.loadSheet(s); });
                    $('<span class="vs-ms-dash">–</span>').appendTo(msFilter);
                    $('<input type="date" class="vs-ms-date" title="To date">').appendTo(msFilter)
                        .on('change', (e: any) => { this.msDateTo = String($(e.target).val() || ''); this.loadSheet(s); });
                    $('<button type="button" class="vs-ms-clear" title="Clear filters">✕</button>').appendTo(msFilter)
                        .on('click', () => this.clearMsFilter(s));
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

            // Sr No header toggles ascending / descending. The sort runs on the server, so it
            // orders the whole sheet, not just the page currently loaded.
            el.on('click', '.vs-sort-toggle', e => {
                e.preventDefault();
                var key = String($(e.currentTarget).attr('data-key'));
                this.sortDesc[key] = this.sortDesc[key] === false;
                var sheet = this.sheetByKey(key);
                if (sheet)
                    this.loadSheet(sheet);
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

            // Account editor drives the Campaign cascade, and on its own is enough to load the
            // account-scoped sheets (Master Suppression).
            this.accountEditor = Serenity.Widget.create({
                type: Serenity.LookupEditor,
                element: e => e.appendTo(el.find('.vs-account-holder')).attr('id', 'vsAccountId').attr('placeholder', 'Account'),
                options: <Serenity.LookupEditorOptions>{ lookupKey: 'Masters.DemandayMasterAccount' }
            });
            this.accountEditor.changeSelect2(() => this.loadAll());

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

            // Master Suppression's own Campaign filter — cascaded from the same Account, but it only
            // narrows the already-loaded rows client-side (no server re-query).
            if (this.msCampaignHolder) {
                this.msCampaignEditor = Serenity.Widget.create({
                    type: Serenity.LookupEditor,
                    element: e => e.appendTo(this.msCampaignHolder).attr('placeholder', 'All campaigns'),
                    options: <any>{
                        lookupKey: 'Masters.DemandayCampaignId',
                        cascadeFrom: 'vsAccountId',
                        cascadeField: 'DemandayMasterAccountId'
                    }
                });
                this.msCampaignEditor.changeSelect2(() => {
                    var v = this.msCampaignEditor.value;
                    var id = Q.isEmptyOrNull(v) ? NaN : parseInt(v, 10);
                    this.msCampaignFilter = isNaN(id) ? null : id;
                    var sheet = this.sheetByKey('MasterSuppression');
                    if (sheet) this.loadSheet(sheet);
                });
            }

            // Seed each section with its own "what's missing" message.
            this.loadAll();
        }

        /** Resets the Master Suppression Campaign + Date filters back to "all". */
        private clearMsFilter(sheet: VsSheet) {
            this.msCampaignFilter = null;
            this.msDateFrom = '';
            this.msDateTo = '';
            this.msCompany = '';
            if (this.msCompanyTimer)
                clearTimeout(this.msCompanyTimer);
            if (this.msCampaignEditor)
                this.msCampaignEditor.value = null;
            this.element.find('.vs-ms-date').val('');
            this.element.find('.vs-ms-company').val('');
            this.loadSheet(sheet);
        }

        /**
         * Server-side criteria for the Master Suppression Company + Date filters. The date "to"
         * bound is exclusive on the next day so the whole selected day is included regardless of
         * its time part.
         */
        private msDateCriteria(): any {
            var crit: any = null;
            // Company name is a "contains" match, so it filters the whole table server-side.
            if (this.msCompany)
                crit = [Serenity.Criteria('CompanyName'), 'like', '%' + this.msCompany + '%'];
            if (this.msDateFrom) {
                var from: any = [Serenity.Criteria('Date'), '>=', this.msDateFrom];
                crit = crit ? Serenity.Criteria.and(crit, from) : from;
            }
            if (this.msDateTo) {
                var to = new Date(this.msDateTo + 'T00:00:00');
                to.setDate(to.getDate() + 1);
                var pad = (n: number) => (n < 10 ? '0' + n : '' + n);
                var next = to.getFullYear() + '-' + pad(to.getMonth() + 1) + '-' + pad(to.getDate());
                var upper: any = [Serenity.Criteria('Date'), '<', next];
                crit = crit ? Serenity.Criteria.and(crit, upper) : upper;
            }
            return crit;
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
                    // Sent through the progress panel so the user can watch the upload percentage
                    // of a large sheet, then see that the server is still importing it.
                    AdvanceCRM.Common.TransferProgress.upload({
                        url: '~/Toolkit/VerifySheets/ImportExcel',
                        formData: fd,
                        title: 'Importing ' + (sheetDef ? sheetDef.title : sheetKey),
                        processingText: 'Uploaded. Importing rows on the server…',
                        onSuccess: msg => {
                            alert(msg || 'Import completed.');
                            // An import can create new Campaign IDs; the lookup is cached, so force a
                            // fresh fetch and the Campaign dropdowns pick them up without a page reload.
                            Q.reloadLookupAsync('Masters.DemandayCampaignId');
                            var sheet = this.sheetByKey(sheetKey);
                            if (sheet) this.loadSheet(sheet);
                        },
                        onError: msg => alert('Upload failed: ' + msg)
                    });
                }
                if (fileInput.parentNode) document.body.removeChild(fileInput);
            };
            fileInput.click();
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

        /**
         * Account-scoped sheets (Master Suppression) only need a Master Account, so they load as
         * soon as one is picked — no Campaign required. Campaign-scoped sheets still wait for a
         * Campaign, and each section shows its own message about what is missing.
         */
        private loadAll() {
            var campaignId = this.getCampaignId();
            var accountId = this.getAccountId();

            this.sheets.forEach(s => {
                if (!this.visible[s.key])
                    return;
                if (s.scope === 'account') {
                    if (accountId != null)
                        this.loadSheet(s);
                    else
                        this.setSheetPlaceholder(s, 'Select a Master Account to view this sheet.');
                }
                else {
                    if (campaignId != null)
                        this.loadSheet(s);
                    else
                        this.setSheetPlaceholder(s, 'Select a Campaign ID to view this sheet.');
                }
            });

            if (campaignId != null)
                this.startLivePoll();
            else
                this.stopLivePoll();
        }

        /** Clears one section and shows why it has nothing to display. */
        private setSheetPlaceholder(sheet: VsSheet, message: string) {
            delete this.loaded[sheet.key];
            delete this.totals[sheet.key];
            var sec = this.element.find('.vs-section[data-key="' + sheet.key + '"]');
            sec.find('.vs-count').text('0');
            sec.find('.vs-card-body').html('<div class="vs-empty">' + Q.htmlEncode(message) + '</div>');
        }

        /**
         * Keeps Open Campaign live: it re-reads that one section on a timer so a domain added by
         * another user appears here on its own. The refresh is silent — no "Loading…" flash and the
         * section's expanded / collapsed state is left exactly as the user left it.
         */
        private startLivePoll() {
            this.stopLivePoll();
            this.livePollTimer = setInterval(() => {
                if (this.getCampaignId() == null)
                    return;
                var s = this.sheetByKey('OpenCampaign');
                if (s && this.visible[s.key] && !this.collapsed[s.key])
                    this.loadSheet(s, true);
            }, VsLivePollMs);
        }

        private stopLivePoll() {
            if (this.livePollTimer) {
                clearInterval(this.livePollTimer);
                this.livePollTimer = null;
            }
        }

        /** @param silent background refresh: no loading flash, keeps the expanded/collapsed state. */
        private loadSheet(sheet: VsSheet, silent?: boolean) {
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

            // Master Suppression's own filters run on the server, so they work against the whole
            // table rather than only the page of rows that happens to be loaded.
            if (sheet.key === 'MasterSuppression' && this.msCampaignFilter != null)
                filter.CampaignId = this.msCampaignFilter;

            var bodyEl = this.element.find('.vs-section[data-key="' + sheet.key + '"] .vs-card-body');
            if (!silent)
                bodyEl.html('<div class="vs-loading">Loading…</div>');

            var request = <Serenity.ListRequest>{
                EqualityFilter: filter,
                // Sorted by the Sr No column header; newest first until the user flips it.
                Sort: [this.sortDesc[sheet.key] === false ? 'SrNo' : 'SrNo DESC'],
                // Bounded page — never stream a multi-million row table into the browser.
                Take: VsLoadLimit
            };
            // Ask for joined view columns (usernames, account numbers) — omitted by default.
            if (sheet.includeColumns)
                request.IncludeColumns = sheet.includeColumns;

            if (sheet.key === 'MasterSuppression') {
                var crit = this.msDateCriteria();
                if (crit)
                    request.Criteria = crit;
            }

            sheet.list(request,
                response => {
                    this.loaded[sheet.key] = response.Entities || [];
                    // TotalCount is the real row count on the server (we only fetched a page of it).
                    this.totals[sheet.key] = response.TotalCount != null
                        ? response.TotalCount
                        : (response.Entities || []).length;
                    // A fresh load starts collapsed to the 5-row preview; a silent poll must not
                    // yank the view back while the user is reading an expanded list.
                    if (!silent)
                        this.expanded[sheet.key] = false;
                    this.renderSheet(sheet);
                },
                <Q.ServiceOptions<any>>{
                    blockUI: false,
                    onError: () => {
                        // A failed background poll should leave the current data on screen.
                        if (silent)
                            return;
                        delete this.loaded[sheet.key];
                        bodyEl.html('<div class="vs-error">Unable to load (you may not have permission for this sheet).</div>');
                    }
                });
        }

        private renderSheet(sheet: VsSheet) {
            // Filtering (campaign / date) happens server-side; these are the rows of the page we hold.
            var all = this.loaded[sheet.key] || [];
            var entities = all.filter(row => this.matchesSearch(sheet, row));
            // The server's real row count — the loaded page may be only a slice of it.
            var total = this.totals[sheet.key] != null ? this.totals[sheet.key] : all.length;

            var sec = this.element.find('.vs-section[data-key="' + sheet.key + '"]');
            // While searching, show how much of the loaded page is being hidden.
            sec.find('.vs-count').text(this.searchTerm
                ? entities.length + ' / ' + total
                : String(total));
            var bodyEl = sec.find('.vs-card-body');

            if (!entities.length) {
                bodyEl.html('<div class="vs-empty">' + (this.searchTerm
                    ? 'No rows match “' + Q.htmlEncode(this.searchTerm) + '”.'
                    : 'No records for this selection.') + '</div>');
                return;
            }

            // Show only the first few rows until "more ++" is clicked, so a big sheet stays compact.
            var isExpanded = !!this.expanded[sheet.key];
            var hasMore = entities.length > VsPreviewLimit;
            var visibleRows = (isExpanded || !hasMore) ? entities : entities.slice(0, VsPreviewLimit);

            var html = '';
            // Be explicit when the table holds far more than the page we fetched.
            if (total > all.length) {
                html += '<div class="vs-grid-note">Showing the newest ' + all.length +
                    ' of ' + total + ' records. Use the filters above or Export to Excel for the full list.</div>';
            }
            html += '<div class="vs-table-wrap"><table class="vs-table"><thead><tr>';
            var desc = this.sortDesc[sheet.key] !== false;
            sheet.columns.forEach(c => {
                // Sr No header sorts the whole sheet server-side; the arrow shows the direction.
                if (c.field === 'SrNo') {
                    html += '<th><a href="#" class="vs-sort-toggle" data-key="' + sheet.key + '" ' +
                        'title="Sort by Sr No">' + vsEscape(c.title) +
                        ' <span class="vs-sort-arrow">' + (desc ? '▼' : '▲') + '</span></a></th>';
                }
                else {
                    html += '<th>' + vsEscape(c.title) + '</th>';
                }
            });
            html += '</tr></thead><tbody>';
            visibleRows.forEach(row => {
                html += '<tr>';
                sheet.columns.forEach(c => {
                    // Sr No opens the record's own dialog; needs the row Id to load it.
                    if (c.field === 'SrNo' && row['Id'] != null) {
                        html += '<td><a href="#" class="vs-edit-link" data-key="' + sheet.key +
                            '" data-id="' + row['Id'] + '">' + vsEscape(row['SrNo']) + '</a></td>';
                    }
                    else if (c.field === 'TimeStamp') {
                        html += '<td>' + vsEscape(vsFormatTimeStamp(row[c.field])) + '</td>';
                    }
                    else if (c.field === 'Date') {
                        html += '<td>' + vsEscape(vsFormatDate(row[c.field])) + '</td>';
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
