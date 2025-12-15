interface BootstrapSwitchOptions {
    state?: boolean;
    size?: string;
    animate?: boolean;
    disabled?: boolean;
    readonly?: boolean;
    indeterminate?: boolean;
    inverse?: boolean;
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
}

interface JQuery {
    bootstrapSwitch(): JQuery;
    bootstrapSwitch(options: BootstrapSwitchOptions): JQuery;
    bootstrapSwitch(method: 'state'): boolean;
    bootstrapSwitch(method: 'state', value: boolean): JQuery;
    bootstrapSwitch(method: string, value?: any): any;
}
