namespace AdvanceCRM {
    @Serenity.Decorators.element('<input type="checkbox"/>')
    @Serenity.Decorators.registerClass([Serenity.IGetEditValue, Serenity.ISetEditValue])
    export class BSSwitchEditor
        extends Serenity.Widget<BootstrapSwitchOptions>
        implements Serenity.IGetEditValue, Serenity.ISetEditValue {

        constructor(element: JQuery, opt: BootstrapSwitchOptions) {
            super(element, opt);

            this.options.size = "mini";
            element.attr('type', 'checkbox').bootstrapSwitch(this.options);
        }

        public setEditValue(source: any, property: Serenity.PropertyItem): void {
            if (this.element.hasClass('required')) this.element.removeClass('required');
            this.element.bootstrapSwitch('state', source[property.name]);
        }

        public getEditValue(property: Serenity.PropertyItem, target: any): void {
            target[property.name] = this.element.bootstrapSwitch('state');
        }
    }
    export interface BootstrapSwitchOptions {
        state?: boolean;
        size?: string;
        animate?: boolean;
        disabled?: boolean;
        readonly?: boolean;
        indeterminate?: boolean;
        invers?: boolean;
        radioAllOff?: boolean;
        onColor?: string;
        offColor?: string;
        onText?: string;
        offText?: string;
        labelText?: string;
        handleWidth?: string;
        labelWidth?: string;
        baseClass?: string;
        wrapperClass?: string;
        onInit?: any;
        onSwitchChange?: any;
        toggleInverse(): JQuery;
        toggleAnimate(): JQuery;
        toggleDisabled(): JQuery;
        toggleReadonly(): JQuery;
        toggleIndeterminate(): JQuery;
        toggleState(skip?: boolean): JQuery;
        toggleDisabled(): JQuery;
        toggleReadOnly(): JQuery;
    }
}