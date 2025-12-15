namespace AdvanceCRM {
    /**
     * BooleanSwitchEditor. Inspired by http://bootsnipp.com/snippets/featured/material-design-switch
     *  
     */
    @Serenity.Decorators.element("<div/>")
    @Serenity.Decorators.registerEditor([Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly])
    export class BooleanSwitchEditor extends Serenity.Widget<BooleanSwitchOptions>
        implements Serenity.IGetEditValue, Serenity.ISetEditValue, Serenity.IReadOnly {

        private _value: boolean;
        private _input: JQuery;
        private _label: JQuery;

        constructor(container: JQuery, options: BooleanSwitchOptions) {
            super(container, options);

            if (!options.css) {
                this.options.css = "label-success";
            }
            var id = container.prop('id') + '_bsw';
            this._input = jQuery('<input type="checkbox"/>').prop('id', id);
            this._input.on('click', (e) => {
                if (this._input.hasClass('readonly') || this.element.hasClass('readonly'))
                    e.preventDefault();
            });


            this._label = jQuery('<label/>').prop('for', id).addClass(this.options.css);

            container.removeClass('flexify');
            container.append(this._input);
            container.append(this._label);
        }





        public setEditValue(source: any, property: Serenity.PropertyItem): void {
            this._input.prop('checked', source[property.name]);
        }

        public getEditValue(property: Serenity.PropertyItem, target: any): void {
            target[property.name] = this._input.is(":checked");
        }

        get_readOnly(): boolean {
            return this._input.hasClass(":readonly");
        }

        //We could also do it by disabling input checkbox
        set_readOnly(value: boolean) {
            if (value) {
                if (!this._input.hasClass("readonly"))
                    this._input.addClass("readonly");

            }
            else
                this._input.removeClass("readonly");
        }


        public get value(): boolean {
            return this._input.is(":checked");
        }

        public set value(data: boolean) {
            this._input.prop("checked", data)
        }

        public changeCss(className: string) {
            if (className && className.length > 0)
                this._label.removeClass(this.options.css).addClass(className);
        }
    }
    export interface BooleanSwitchOptions {
        css?: string;

    }

}