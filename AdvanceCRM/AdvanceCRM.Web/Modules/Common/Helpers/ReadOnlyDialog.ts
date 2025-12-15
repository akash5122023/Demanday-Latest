
namespace AdvanceCRM {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.maximizable()
    export class ReadOnlyDialog<TEntity> extends Serenity.EntityDialog<TEntity, any>{

        constructor() {
            super();
        }

        protected getToolbarButtons(): Serenity.ToolButton[] {
            return [];
        }

        protected updateInterface(): void {
            super.updateInterface();
            Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
            this.element.find('sup').hide();
            this.toolbar.element.toggle(false);
        }

        protected getEntityTitle(): string {

            if (!this.isEditMode()) {
                return "How did you manage to open this dialog in new record mode?";
            }
            else {
                var entityType = super.getEntitySingular();
                let name = this.getEntityNameFieldValue() || "";
                return 'View ' + entityType + " (" + name + ")";
            }
        }

        protected updateTitle(): void {
            super.updateTitle();
        }
    }
}