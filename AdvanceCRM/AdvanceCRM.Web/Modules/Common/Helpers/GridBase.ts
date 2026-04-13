namespace AdvanceCRM {
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.filterable()

    export class GridBase<TEntity, TOptions> extends Serenity.EntityGrid<TEntity, TOptions> {

        public rowSelection = new Serenity.GridRowSelectionMixin(this);

        constructor(container: JQuery) {
            super(container);
        }

        protected getQuickFilters(): Serenity.QuickFilter<Serenity.Widget<any>, any>[] {
            let filters = super.getQuickFilters();

            for (let f of filters) {
                if (f.type === Serenity.StringEditor) {
                    const origHandler = f.handler;
                    f.handler = (h) => {
                        let value = (h.widget as Serenity.StringEditor).value;
                        if (value != null) value = value.trim();

                        if (value && value.indexOf(',') >= 0) {
                            let parts = value.split(',').map(s => s.trim()).filter(s => s.length > 0);
                            if (parts.length > 1) {
                                let orCrit: any[] = null;
                                for (let part of parts) {
                                    let crit: any[] = [Serenity.Criteria(h.field), 'like', '%' + part + '%'];
                                    orCrit = orCrit == null ? crit : Serenity.Criteria.or(orCrit, crit);
                                }
                                h.request.Criteria = Serenity.Criteria.and(
                                    h.request.Criteria,
                                    Serenity.Criteria.paren(orCrit)
                                );
                                h.active = true;
                                return;
                            }
                        }

                        if (origHandler) {
                            origHandler(h);
                        }
                    };
                }
            }

            return filters;
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
                    let orCrit: any[] = group[0];
                    for (let i = 1; i < group.length; i++) {
                        orCrit = Serenity.Criteria.or(orCrit, group[i]);
                    }
                    resultParts.push(Serenity.Criteria.paren(orCrit));
                } else {
                    resultParts.push(group[0]);
                }
            }

            if (resultParts.length === 0) return criteria;

            let combined = resultParts[0];
            for (let i = 1; i < resultParts.length; i++) {
                combined = Serenity.Criteria.and(combined, resultParts[i]);
            }

            return combined;
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
        }
    }
}