
namespace AdvanceCRM {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.panel()
    export class ConfigurationDialog<TEntity> extends Serenity.EntityDialog<TEntity, any> {

        constructor() {
            super();
        }

        protected getToolbarButtons(): Serenity.ToolButton[] {

            let btns = super.getToolbarButtons();

            //Removing delete button
            btns.splice(Q.indexOf(btns, x => x.cssClass == "delete-button"), 1);
            btns.shift();
            btns[Q.indexOf(btns, x => x.cssClass == "apply-changes-button")].title = Q.text("Controls.EntityDialog.SaveButton");
            return btns;
        }

        dialogOpen() {
            super.dialogOpen();
            $('.panel-titlebar').hide();
        }
    }
}