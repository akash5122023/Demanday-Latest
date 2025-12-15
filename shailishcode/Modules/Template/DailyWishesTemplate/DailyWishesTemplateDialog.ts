
/// <reference path="../../Common/Helpers/ConfigurationDialog.ts" />
namespace AdvanceCRM.Template {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.panel()
    export class DailyWishesTemplateDialog extends ConfigurationDialog<DailyWishesTemplateRow> {
        protected getFormKey() { return DailyWishesTemplateForm.formKey; }
        protected getIdProperty() { return DailyWishesTemplateRow.idProperty; }
        protected getLocalTextPrefix() { return DailyWishesTemplateRow.localTextPrefix; }
        protected getNameProperty() { return DailyWishesTemplateRow.nameProperty; }
        protected getService() { return DailyWishesTemplateService.baseUrl; }

        protected form = new DailyWishesTemplateForm(this.idPrefix);

        constructor() {
            super();
        }
    }
}