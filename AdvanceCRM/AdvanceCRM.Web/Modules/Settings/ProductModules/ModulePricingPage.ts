namespace AdvanceCRM.Settings {

    @Serenity.Decorators.registerClass()
    export class ModulePricingPage {
        private container: JQuery;
        private tableBody: JQuery;
        private saveButton: JQuery;

        constructor(container: JQuery) {
            this.container = container;
            this.tableBody = container.find('tbody');
            this.saveButton = container.find('.js-save');

            this.bindHandlers();
            this.load();
        }

        private bindHandlers(): void {
            this.saveButton.on('click', e => {
                e.preventDefault();
                this.save();
            });
        }

        private load(): void {
            this.setLoading(true);

            ProductModulesService.List({}, response => {
                this.renderRows(response.Entities || []);
            }).always(() => this.setLoading(false));
        }

        private renderRows(modules: ProductModuleRow[]): void {
            this.tableBody.empty();

            if (!modules || modules.length === 0) {
                const message = Q.htmlEncode(Q.text("Controls.DataGrid.NoRecords") || 'No modules found.');
                this.tableBody.append(`<tr class="no-results"><td colspan="4" class="text-center">${message}</td></tr>`);
                return;
            }

            for (const module of modules) {
                if (!module) {
                    continue;
                }

                const moduleId = module.Id ?? 0;
                const name = module.Name || '';
                const displayName = module.DisplayName || name;
                const priceValue = module.Price != null ? module.Price.toFixed(2) : '';
                const currencyValue = module.Currency ? module.Currency.trim() : '';

                const row = $(
                    `<tr data-id="${moduleId}">
                        <td class="module-name">${Q.htmlEncode(displayName)}</td>
                        <td class="module-key text-muted">${Q.htmlEncode(name)}</td>
                        <td class="module-price-input">
                            <input type="number" class="form-control input-price" min="0" step="0.01" value="${priceValue}" />
                        </td>
                        <td class="module-currency-input">
                            <input type="text" class="form-control input-currency" maxlength="16" value="${Q.htmlEncode(currencyValue)}" />
                        </td>
                    </tr>`);

                this.tableBody.append(row);
            }
        }

        private save(): void {
            const modules: ProductModulesService.ModulePriceUpdateItem[] = [];
            let hasError = false;

            this.tableBody.find('tr').each((_, element) => {
                const row = $(element);
                row.removeClass('has-error');

                const idAttr = row.attr('data-id');
                if (!idAttr) {
                    return;
                }

                const moduleId = parseInt(idAttr, 10);
                if (isNaN(moduleId)) {
                    return;
                }

                const priceInput = row.find('input.input-price');
                const currencyInput = row.find('input.input-currency');

                const priceValueRaw = priceInput.val() as string;
                const priceRaw = Q.trimToNull(priceValueRaw ?? '');
                let price: number | undefined = undefined;

                if (priceRaw != null) {
                    const parsed = Q.parseDecimal(priceRaw);
                    if (parsed == null || isNaN(parsed)) {
                        row.addClass('has-error');
                        hasError = true;
                        return;
                    }

                    price = parsed;
                }

                const currencyValueRaw = currencyInput.val() as string;
                const currency = Q.trimToNull(currencyValueRaw ?? '');

                modules.push({
                    Id: moduleId,
                    Price: price,
                    Currency: currency ?? undefined
                });
            });

            if (hasError) {
                Q.notifyError('Please correct the highlighted prices before saving.');
                return;
            }

            this.setLoading(true);

            ProductModulesService.UpdatePrices({ Modules: modules }, () => {
                Q.notifySuccess('Module pricing updated successfully.');
                this.load();
            }).always(() => this.setLoading(false));
        }

        private setLoading(isLoading: boolean): void {
            this.container.toggleClass('is-loading', isLoading);
            this.saveButton.prop('disabled', isLoading);
        }
    }
}
