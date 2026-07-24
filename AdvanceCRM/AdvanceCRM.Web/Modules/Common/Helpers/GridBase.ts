namespace AdvanceCRM {
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.filterable()

    export class GridBase<TEntity, TOptions> extends Serenity.EntityGrid<TEntity, TOptions> {

        public rowSelection = new Serenity.GridRowSelectionMixin(this);

        constructor(container: JQuery) {
            super(container);
        }

        // Shared filter experience for every Demanday & Toolkit grid: multi-value paste text filters
        // with Include/Exclude, master-backed dropdowns (Country / Job Level / Job Function /
        // Sub Industry / Employee Size), the Domain paste box, and the Master Account -> Campaign Id
        // cascade. Each piece only kicks in when the grid actually has that field, so grids without a
        // given field are unaffected.
        protected getQuickFilters(): Serenity.QuickFilter<Serenity.Widget<any>, any>[] {
            let filters = super.getQuickFilters();

            // Domain paste box (wherever a "Domain" column exists).
            this.addDomainQuickFilter(filters);

            let items = this.getPropertyItems() || [];
            let has = (n: string) => items.some(i => i && i.name === n);

            // Ensure the known dropdown fields appear wherever the grid actually has that column,
            // even if they weren't explicitly marked as quick filters.
            let ensure = (fld: string, title: string) => {
                if (has(fld) && !filters.some(f => f.field === fld))
                    filters.push({ field: fld, title: title });
            };
            ensure('Country', 'Country');
            ensure('JobLevel', 'Job Level');
            ensure('JobFunctionRole', 'Job Function');
            ensure('SubIndustry', 'Sub Industry');
            ensure('CompanyEmployeeSize', 'Company Employee Size');

            // Text (StringEditor) filters -> multi-value paste boxes with Include/Exclude.
            this.makeMultiValueTextFilters(filters);

            // Known fields -> master-backed multi-select dropdowns (values from the Masters menu).
            this.makeLookupDropdownFilters(filters, {
                Country: 'Masters.DemandayCountryMaster',
                JobLevel: 'Masters.DemandayJobLevelMaster',
                JobFunctionRole: 'Masters.DemandayJobFunctionMaster',
                SubIndustry: 'Masters.DemandaySubIndustryMaster',
                CompanyEmployeeSize: 'Masters.DemandayEmployeeSizeMaster'
            });

            // Master Account + Campaign cascade for grids that store CampaignId as a string (Demanday
            // style). Toolkit grids have their own int-FK MasterAccountId cascade, so we skip those.
            if (has('CampaignId') && !has('MasterAccountId'))
                this.addMasterAccountAndCampaignFilters(filters);

            return this.orderQuickFilters(filters);
        }

        // Fixed display order for the filter panel (fields not present are simply skipped; anything
        // not listed keeps its original order at the end).
        protected orderQuickFilters(filters: Serenity.QuickFilter<Serenity.Widget<any>, any>[]) {
            const order = ['MasterAccountId', 'CampaignId', 'Date', 'Country', 'Title',
                'JobLevel', 'JobFunctionRole', 'Domain', 'CompanyEmployeeSize', 'Industry', 'SubIndustry'];
            let ordered: Serenity.QuickFilter<Serenity.Widget<any>, any>[] = [];
            for (let fld of order)
                for (let f of filters)
                    if (f.field === fld && ordered.indexOf(f) < 0) ordered.push(f);
            for (let f of filters)
                if (ordered.indexOf(f) < 0) ordered.push(f);
            return ordered;
        }

        private addDomainQuickFilter(filters: Serenity.QuickFilter<Serenity.Widget<any>, any>[]) {
            let items = this.getPropertyItems() || [];
            let hasDomain = items.some(i => i && i.name === 'Domain');
            if (!hasDomain) return;
            if (filters.some(f => f.field === 'Domain')) return;   // don't duplicate

            filters.push({
                field: 'Domain',
                type: Serenity.TextAreaEditor,
                options: { rows: 4 },
                title: 'Domain(s)',
                cssClass: 'domain-multi-filter',
                // Apply the filter shortly after the user pastes/types, without needing to blur.
                init: (w) => {
                    GridBase.addIncludeExcludeToggle(w);
                    let el = w.element;
                    let timer: any = null;
                    el.on('input paste', () => {
                        if (timer) clearTimeout(timer);
                        timer = setTimeout(() => el.triggerHandler('change'), 400);
                    });
                },
                handler: (h) => {
                    let raw = (h.widget as Serenity.TextAreaEditor).value;
                    let domains = GridBase.parseDomains(raw);
                    if (!domains.length) { h.active = false; return; }

                    let exclude = (h.widget as any).dvExclude && (h.widget as any).dvExclude();

                    // Match on "contains" so a stored value like "admin@gmail.com" or "gmail.com"
                    // is both matched by the "gmail.com" domain.
                    // Include => record matches ANY domain (OR of like).
                    // Exclude => record matches NONE (AND of not-like). Balanced tree so a large
                    // pasted domain list stays within the server's JSON depth limit.
                    let crits = domains.map(d => exclude
                        ? [Serenity.Criteria('Domain'), 'not like', '%' + d + '%']
                        : [Serenity.Criteria('Domain'), 'like', '%' + d + '%']);
                    let combined = GridBase.combineBalanced(crits,
                        (a, b) => exclude ? Serenity.Criteria.and(a, b) : Serenity.Criteria.or(a, b));
                    h.request.Criteria = Serenity.Criteria.and(
                        h.request.Criteria,
                        Serenity.Criteria.paren(combined)
                    );
                    h.active = true;
                }
            });
        }

        // Split a pasted blob (newline/comma/semicolon/space separated) into a unique list of domains.
        private static parseDomains(raw: string): string[] {
            if (!raw) return [];
            let tokens = raw.split(/[\s,;]+/);
            let seen: { [k: string]: boolean } = {};
            let out: string[] = [];
            for (let t of tokens) {
                let d = GridBase.extractDomain(t);
                if (d && !seen[d]) { seen[d] = true; out.push(d); }
            }
            return out;
        }

        // Combine a list of criteria into a BALANCED (bushy) binary tree instead of a linear
        // left-nested chain. A linear chain adds one JSON nesting level per value, so pasting ~60+
        // values pushes the request past the server's JSON reader depth (Newtonsoft MaxDepth = 64)
        // and the whole List POST fails before the handler runs. A balanced tree keeps depth at
        // ~log2(n) (e.g. 500 values -> ~9 levels), so hundreds/thousands of pasted values apply
        // fine. OR/AND are associative, so the rows matched are identical either way.
        private static combineBalanced(
            crits: any[], combine: (a: any, b: any) => any[]): any[] {
            if (!crits || !crits.length) return null;
            let level = crits.slice();
            while (level.length > 1) {
                let next: any[] = [];
                for (let i = 0; i < level.length; i += 2)
                    next.push(i + 1 < level.length ? combine(level[i], level[i + 1]) : level[i]);
                level = next;
            }
            return level[0];
        }

        // Turns text (StringEditor) quick filters into multi-value paste boxes that behave like the
        // Domain filter: paste many values (one per line, or comma/semicolon separated) and any of them
        // is matched with LIKE (OR). Auto-applies shortly after pasting. Optionally limit to given fields.
        protected makeMultiValueTextFilters(
            filters: Serenity.QuickFilter<Serenity.Widget<any>, any>[], onlyFields?: string[]) {
            for (let f of filters) {
                if (f.type !== Serenity.StringEditor) continue;
                if (!f.field) continue;
                if (onlyFields && onlyFields.indexOf(f.field) < 0) continue;

                let field = f.field;
                f.type = Serenity.TextAreaEditor;
                f.options = { rows: 3 };
                f.cssClass = (f.cssClass ? f.cssClass + ' ' : '') + 'multi-text-filter';
                f.init = (w) => {
                    GridBase.addIncludeExcludeToggle(w);
                    let el = w.element;
                    let timer: any = null;
                    el.on('input paste', () => {
                        if (timer) clearTimeout(timer);
                        timer = setTimeout(() => el.triggerHandler('change'), 400);
                    });
                };
                f.handler = (h) => {
                    let raw = (h.widget as Serenity.TextAreaEditor).value;
                    let vals = GridBase.parseMultiValues(raw);
                    if (!vals.length) { h.active = false; return; }

                    let exclude = (h.widget as any).dvExclude && (h.widget as any).dvExclude();

                    // "Contains" match so typing part of a value works (e.g. "Director" matches
                    // "Finance Director", "IT Director"). Include => matches ANY (OR of like).
                    // Exclude => matches NONE (AND of not-like). Balanced tree so 500+ pasted
                    // values stay well under the server's JSON depth limit.
                    let crits = vals.map(v => exclude
                        ? [Serenity.Criteria(field), 'not like', '%' + v + '%']
                        : [Serenity.Criteria(field), 'like', '%' + v + '%']);
                    let combined = GridBase.combineBalanced(crits,
                        (a, b) => exclude ? Serenity.Criteria.and(a, b) : Serenity.Criteria.or(a, b));
                    h.request.Criteria = Serenity.Criteria.and(
                        h.request.Criteria,
                        Serenity.Criteria.paren(combined)
                    );
                    h.active = true;
                };
            }
        }

        // Turns the given quick filters into multi-select dropdowns whose options come from a master
        // table (lookup). Selecting values applies the same Include/Exclude logic as the text filters
        // (OR of "=" to include, AND of "!=" to exclude). Only master values can be selected.
        protected makeLookupDropdownFilters(
            filters: Serenity.QuickFilter<Serenity.Widget<any>, any>[],
            map: { [field: string]: string }) {

            for (let f of filters) {
                if (!f.field || !map[f.field]) continue;
                let field = f.field;
                let lookupKey = map[field];

                f.type = Serenity.SelectEditor;
                f.options = { multiple: true } as any;
                f.cssClass = 'distinct-dropdown-filter';
                f.init = (w) => {
                    GridBase.addIncludeExcludeToggle(w);
                    let addUnique = (val: string): string => {
                        let lk = ('' + val).toLowerCase();
                        let items = (w as any).items || [];
                        for (let it of items) {
                            if (('' + it.id).toLowerCase() === lk) return it.id;
                        }
                        (w as any).addOption(val, val);
                        return val;
                    };
                    let findExisting = (val: string): string => {
                        let lk = ('' + val).toLowerCase();
                        let items = (w as any).items || [];
                        for (let it of items) {
                            if (('' + it.id).toLowerCase() === lk) return it.id;
                        }
                        return null;
                    };

                    // reloadLookupAsync forces a fresh fetch so newly-added master values show up
                    // without needing a lookup-cache refresh.
                    Q.reloadLookupAsync(lookupKey).then(() => Q.getLookupAsync(lookupKey)).then((lookup: any) => {
                        let tf = (lookup && lookup.textField) || 'Name';
                        let vals = ((lookup && lookup.items) || [])
                            .map(it => '' + it[tf])
                            .filter(v => v != null && ('' + v).trim().length);
                        // Numeric-aware sort: values starting with a number (e.g. employee-size ranges
                        // "0-1", "2-10", "10001+") sort by that leading number so they read 0 -> up;
                        // everything else falls back to alphabetical.
                        let lead = (s: string) => { let m = ('' + s).match(/^\s*(\d+)/); return m ? parseInt(m[1], 10) : NaN; };
                        vals.sort((a, b) => {
                            let na = lead(a), nb = lead(b);
                            if (!isNaN(na) && !isNaN(nb)) { if (na !== nb) return na - nb; }
                            else if (!isNaN(na)) return -1;
                            else if (!isNaN(nb)) return 1;
                            return a.toLowerCase().localeCompare(b.toLowerCase());
                        });
                        for (let v of vals) addUnique(v);
                    }, () => { /* lookup missing -> empty dropdown */ });

                    // Bulk paste selects many at once, but only values that exist in the master.
                    let $item = w.element.closest('.quick-filter-item');
                    $item.on('paste', 'input.select2-input', function (this: HTMLInputElement) {
                        let inp = this;
                        window.setTimeout(() => {
                            let tokens = (inp.value || '').split(/[\r\n;,]+/)
                                .map(s => ('' + s).trim()).filter(s => s.length);
                            if (tokens.length < 2) return;
                            let cur: string[] = (((w as any).get_values && (w as any).get_values()) || []).slice();
                            let curSet: { [k: string]: boolean } = {};
                            cur.forEach(v => curSet[('' + v).toLowerCase()] = true);
                            for (let t of tokens) {
                                let lk = t.toLowerCase();
                                let canonical = findExisting(t);
                                if (canonical != null && !curSet[lk]) { cur.push(canonical); curSet[lk] = true; }
                            }
                            (w as any).set_values(cur);
                            inp.value = '';
                            w.element.triggerHandler('change');
                        }, 0);
                    });
                };
                f.handler = (h) => {
                    let vals: string[] = (h.widget as any).get_values ? (h.widget as any).get_values() : [];
                    vals = (vals || []).filter(v => v != null && ('' + v).length);
                    if (!vals.length) { h.active = false; return; }

                    let exclude = (h.widget as any).dvExclude && (h.widget as any).dvExclude();
                    let crits = vals.map(v => exclude
                        ? [Serenity.Criteria(field), '!=', v]
                        : [Serenity.Criteria(field), '=', v]);
                    let combined = GridBase.combineBalanced(crits,
                        (a, b) => exclude ? Serenity.Criteria.and(a, b) : Serenity.Criteria.or(a, b));
                    h.request.Criteria = Serenity.Criteria.and(
                        h.request.Criteria,
                        Serenity.Criteria.paren(combined)
                    );
                    h.active = true;
                };
            }
        }

        // Adds a "Master Account" dropdown that cascades a "Campaign Id" dropdown (only its campaigns).
        // Filtering is on the grid's CampaignId string column; Master Account is a pure cascade driver.
        protected addMasterAccountAndCampaignFilters(
            filters: Serenity.QuickFilter<Serenity.Widget<any>, any>[]) {

            // Drop the auto CampaignId text filter; re-add it as a cascaded dropdown after Master Account.
            for (let i = 0; i < filters.length; i++) {
                if (filters[i].field === 'CampaignId') { filters.splice(i, 1); break; }
            }

            filters.push({
                field: 'MasterAccountId',
                type: Serenity.LookupEditor,
                title: 'Master Account',
                options: { lookupKey: 'Masters.DemandayMasterAccount' } as any,
                handler: (h) => {
                    let v = (h.widget as any).value;
                    h.active = v != null && ('' + v).length > 0;
                }
            });

            filters.push({
                field: 'CampaignId',
                type: Serenity.LookupEditor,
                title: 'Campaign Id',
                cssClass: 'distinct-dropdown-filter',
                options: {
                    lookupKey: 'Masters.DemandayCampaignId',
                    cascadeFrom: 'MasterAccountId',
                    cascadeField: 'DemandayMasterAccountId',
                    multiple: true
                } as any,
                init: (w) => { GridBase.addIncludeExcludeToggle(w); },
                handler: (h) => {
                    let ids: any[] = (h.widget as any).get_values ? (h.widget as any).get_values() : [];
                    if (!ids || !ids.length) { h.active = false; return; }

                    // The lookup value is the campaign master's Id; map it to the CampaignId string.
                    let strings: string[] = [];
                    try {
                        let lk: any = Q.getLookup('Masters.DemandayCampaignId');
                        for (let id of ids) {
                            let it = lk && lk.itemById ? lk.itemById[id] : null;
                            if (it && it.CampaignId != null && ('' + it.CampaignId).length) strings.push('' + it.CampaignId);
                        }
                    } catch (e) { }
                    if (!strings.length) { h.active = false; return; }

                    let exclude = (h.widget as any).dvExclude && (h.widget as any).dvExclude();
                    let crits = strings.map(s => exclude
                        ? [Serenity.Criteria('CampaignId'), '!=', s]
                        : [Serenity.Criteria('CampaignId'), '=', s]);
                    let combined = GridBase.combineBalanced(crits,
                        (a, b) => exclude ? Serenity.Criteria.and(a, b) : Serenity.Criteria.or(a, b));
                    h.request.Criteria = Serenity.Criteria.and(
                        h.request.Criteria,
                        Serenity.Criteria.paren(combined)
                    );
                    h.active = true;
                }
            });
        }

        // Split a pasted blob into unique values. Splits on newline / comma / semicolon only, so
        // multi-word values (e.g. "New York", "Vice President") stay intact. Case-insensitive dedupe.
        private static parseMultiValues(raw: string): string[] {
            if (!raw) return [];
            let tokens = raw.split(/[\r\n,;]+/);
            let seen: { [k: string]: boolean } = {};
            let out: string[] = [];
            for (let t of tokens) {
                let v = ('' + t).trim();
                if (!v.length) continue;
                let key = v.toLowerCase();
                if (!seen[key]) { seen[key] = true; out.push(v); }
            }
            return out;
        }

        // Injects a "+ Include / − Exclude" segmented toggle just above a quick-filter editor and
        // stashes a getter on the widget (widget.dvExclude()) that the field's handler reads to decide
        // whether to keep (Include) or drop (Exclude) the matching records.
        protected static addIncludeExcludeToggle(widget: Serenity.Widget<any>) {
            let $el = widget.element;
            if (!$el || !$el.length) return;

            let $host = $el.closest('.quick-filter-item');
            if (!$host.length) $host = $el.parent();
            if ($host.find('.dv-inc-exc').length) return;   // don't add twice

            let $toggle = $(
                '<div class="dv-inc-exc">' +
                    '<button type="button" class="dv-ie-btn dv-inc active" data-mode="inc" title="Show only these values">+ Include All</button>' +
                    '<button type="button" class="dv-ie-btn dv-exc" data-mode="exc" title="Hide these values, show everything else">&minus; Exclude All</button>' +
                '</div>');
            $host.append($toggle);   // buttons sit below the input
            $host.addClass('dv-mode-include');   // default mode; drives the header background colour

            let state = { exclude: false };
            $toggle.on('click', '.dv-ie-btn', function () {
                state.exclude = $(this).attr('data-mode') === 'exc';
                $toggle.find('.dv-ie-btn').removeClass('active');
                $(this).addClass('active');
                $host.removeClass('dv-mode-include dv-mode-exclude')
                    .addClass(state.exclude ? 'dv-mode-exclude' : 'dv-mode-include');
                $el.triggerHandler('change');   // re-apply the filter in the new mode
            });

            (widget as any).dvExclude = () => state.exclude;
        }

        // Reduce a single token to its bare domain: strips the local part of emails,
        // any scheme/path of URLs and a leading "www.".
        private static extractDomain(token: string): string {
            if (!token) return '';
            let s = ('' + token).trim().toLowerCase();
            if (!s.length) return '';
            if (s.indexOf('@') >= 0) s = s.substring(s.lastIndexOf('@') + 1);
            s = s.replace(/^https?:\/\//, '');
            let slash = s.indexOf('/');
            if (slash >= 0) s = s.substring(0, slash);
            s = s.replace(/^www\./, '');
            return s.trim();
        }

        protected onViewSubmit() {
            if (!super.onViewSubmit()) return false;

            var request = this.view.params as Serenity.ListRequest;
            if (request.Criteria && !Serenity.Criteria.isEmpty(request.Criteria)) {
                request.Criteria = this.convertSameFieldContainsToOr(request.Criteria);
            }

            return true;
        }

        private convertSameFieldContainsToOr(criteria: any[]): any[] {
            let parts = this.flattenCriteria(criteria, 'and');

            let likeGroups: { [field: string]: any[][] } = {};
            let otherParts: any[][] = [];

            for (let part of parts) {
                let unwrapped = part;
                if (Array.isArray(unwrapped) && unwrapped.length === 2 && unwrapped[0] === '()') {
                    unwrapped = unwrapped[1];
                }

                let field = this.extractLikeField(unwrapped);
                if (field) {
                    if (!likeGroups[field]) likeGroups[field] = [];
                    likeGroups[field].push(unwrapped);
                } else {
                    otherParts.push(part);
                }
            }

            let resultParts: any[][] = otherParts.slice();

            for (let field in likeGroups) {
                let group = likeGroups[field];
                if (group.length > 1) {
                    let orCrit = GridBase.combineBalanced(group, (a, b) => Serenity.Criteria.or(a, b));
                    resultParts.push(Serenity.Criteria.paren(orCrit));
                } else {
                    resultParts.push(group[0]);
                }
            }

            if (resultParts.length === 0) return criteria;

            return GridBase.combineBalanced(resultParts, (a, b) => Serenity.Criteria.and(a, b));
        }

        private flattenCriteria(criteria: any[], op: string): any[][] {
            if (!Array.isArray(criteria) || criteria.length !== 3) return [criteria];
            if (criteria[1] === op) {
                return [
                    ...this.flattenCriteria(criteria[0], op),
                    ...this.flattenCriteria(criteria[2], op)
                ];
            }
            return [criteria];
        }

        private extractLikeField(criteria: any[]): string {
            if (Array.isArray(criteria) && criteria.length === 3 &&
                Array.isArray(criteria[0]) && criteria[0].length === 1 &&
                typeof criteria[0][0] === 'string' &&
                criteria[1] === 'like') {
                return criteria[0][0];
            }
            return null;
        }

        protected getButtons() {
            var buttons = super.getButtons();

            //buttons.push(AdvanceCRM.Common.PdfExportHelper.createToolButton({
            //    grid: this,
            //    onViewSubmit: () => this.onViewSubmit(),
            //    separator: true
            //}));
            if (Authorization.hasPermission(this.displayName + ":Export")) {
                buttons.push(_Ext.PdfExportHelper.createToolButton({
                    grid: this,
                    onViewSubmit: () => this.onViewSubmit(),
                    separator: true

                }));
            }

            buttons.push(AdvanceCRM.Common.ExcelExportHelper.createToolButton({
                grid: this,
                onViewSubmit: () => this.onViewSubmit(),
                service: this.getService() + '/ListExcel'
            }));

            buttons.push({
                title: 'Delete',
                hint: 'Delete',
                icon: "fa-trash-o text-red",
                onClick: () => {
                    var action = new Common.BulkDeleteAction(this.getService());
                    action.done = () => this.rowSelection.resetCheckedAndRefresh();
                    action.execute(this.rowSelection.getSelectedKeys());
                    separator: true
                }
            });

            buttons.push({
                separator: true,
                title: 'Filters',
                cssClass: 'show-quick-filters',
                icon: 'fa-filter text-primary',
                onClick: () => {

                    const qfBar = this.ensureQuickFilterBar();
                    if (qfBar && qfBar.element)
                        qfBar.element.toggle(500, this.refreshGrid.bind(this));

                },
                hint: 'Click here to display Quick Filters'
            });

            return buttons;
        }


        protected getColumns(): Slick.Column[] {
            let columns = super.getColumns();

            // Render URL/link columns as clickable hyperlinks (Demanday & Toolkit modules).
            let svc = (this.getService && this.getService()) || '';
            let isLinkModule = svc.indexOf('Demanday/') === 0 || svc.indexOf('Toolkit/') === 0;
            const linkFields: { [key: string]: boolean } = {
                ProfileLink: true, CompanyLink: true, RevenueLink: true,
                AddressLink: true, AdressLink: true, Link: true, Domain: true
            };

            columns.forEach(column => {
                column.cssClass = column.cssClass || '';

                if (column.sourceItem) {
                    let formatterType = column.sourceItem.formatterType;
                    //width and cssClass
                    if (column.sourceItem.filteringType == "Lookup") {
                        column.cssClass += ' align-left';
                    } else if (formatterType == "Date") {
                        column.cssClass += ' align-center';
                    } else if (formatterType == "DateTime") {
                        column.cssClass += ' align-center';
                    } else if (formatterType == "Number") {
                        column.cssClass += ' align-right';

                    } else if (formatterType == "Checkbox") {
                        column.cssClass += ' align-center';
                    } else {
                        column.cssClass += ' align-left';
                    }

                    //formatter
                    if (column.sourceItem.editorType == "Lookup") {
                        if (!column.sourceItem.editorParams.autoComplete) {
                            (column as any).lookup = Q.getLookup(column.sourceItem.editorParams.lookupKey)
                            column.formatter = (row, cell, value, columnDef: any, dataContext) => {
                                let item = columnDef.lookup.itemById[value];
                                if (item) return item[columnDef.lookup.textField];
                                else return '-';
                            };
                        }
                    } else if (formatterType == "Enum") {

                        column.formatter = (row, cell, value, columnDef: any, dataContext) => {
                            let enumKey = columnDef.sourceItem.editorParams.enumKey
                            let text = Serenity.EnumFormatter.format(Serenity.EnumTypeRegistry.get(enumKey), Q.toId(value));
                            if (text) return text;
                            else return '-';
                        };
                    } else if (column.sourceItem.editorType == "Decimal") {

                        let formatSrt = '#,##0.00';

                        if (column.sourceItem.editorParams) {
                            let decimals = column.sourceItem.editorParams['decimals'];
                            if (decimals) {
                                formatSrt = '#,##0.'
                                for (let i = 0; i < decimals; i++) {
                                    formatSrt += '0'
                                }
                            }
                            else if (column.sourceItem.editorParams['minValue']) {
                                let splitedMinValue = (column.sourceItem.editorParams['minValue'] as string).split('.');
                                if (splitedMinValue.length > 1) {
                                    formatSrt = '#,##0.' + splitedMinValue[1];
                                } else {
                                    formatSrt = '#,##0';

                                }
                            }
                        }

                        column.format = ctx => Serenity.NumberFormatter.format(ctx.value, formatSrt);
                    }
                }

                if (isLinkModule && !column.format && !column.formatter && column.field && linkFields[column.field]) {
                    column.format = ctx => {
                        if (ctx.value == null) return '';
                        let text = ('' + ctx.value).trim();
                        if (!text.length) return '';
                        let href = text;
                        if (!/^(https?:|mailto:|tel:)/i.test(href)) href = 'https://' + href;
                        return '<a href="' + encodeURI(href) + '" target="_blank" rel="noopener">' + Q.htmlEncode(text) + '</a>';
                    };
                }
            });

            let rowSelectionCol = Serenity.GridRowSelectionMixin.createSelectColumn(() => this.rowSelection);
            rowSelectionCol.width = rowSelectionCol.minWidth = rowSelectionCol.maxWidth = 22
            columns.unshift(rowSelectionCol);

            return columns;
        }

        protected onClick(e: JQueryEventObject, row: number, cell: number) {
            super.onClick(e, row, cell);

            if (e.isDefaultPrevented())
                return;

            var item = this.itemAt(row) as TItem;
            let recordId = item[this.getIdProperty()];
            var target = $(e.target);

            // if user clicks "i" element, e.g. icon
            if (target.parent().hasClass('inline-action') || target.parent().hasClass('inline-actions') || target.parent().hasClass('inline-btn'))
                target = target.parent();

            if (target.hasClass('inline-action') || target.hasClass('inline-actions') || target.hasClass('inline-btn')) {
                e.preventDefault();
                this.onInlineActionClick(target, recordId, item);

            }
        }

        protected onInlineActionClick(target, recordId, item): void { }

        private refreshGrid() {
            this.refresh();
            this.element.triggerHandler('layout');
        }

        protected createToolbarExtensions() {
            super.createToolbarExtensions();

            new Serenity.FavoriteViewsMixin({
                grid: this
            });

            // Import / Export are permission-gated per sub-module (e.g. "DemandayContacts:Import",
            // "DemandayContacts:Export"). Hide the buttons when the user lacks the permission.
            let svc = (this.getService && this.getService()) || '';
            let prefix = svc.split('/').pop();
            if (prefix) {
                setTimeout(() => {
                    if (!Authorization.hasPermission(prefix + ':Import'))
                        this.element.find('.tool-button.import-excel-button, .tool-button.download-template-button').hide();
                    if (!Authorization.hasPermission(prefix + ':Export'))
                        this.element.find('.tool-button.export-excel-button, .tool-button.export-xlsx-button').hide();
                }, 0);
            }
        }
    }
}