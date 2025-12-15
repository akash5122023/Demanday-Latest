
namespace _Ext
{

    //@Serenity.Decorators.registerClass()
    //export class TitleCaseEditor extends Serenity.StringEditor {

    //    constructor(defaultValue?: JQuery) {
    //        super(defaultValue);
    //    }

    //    protected validate(value: string): string {
    //        if (/^[A-Za-z\s]+$/.test(value)) {
    //            throw new Error('Only characters and spaces are allowed');
    //        }

    //        return value.trim().replace(/\b(\w)/g, (_, letter) => letter.toUpperCase());
    //    }
    //}


    @Serenity.Decorators.registerEditor()
    export class TitleCaseEditor extends Serenity.StringEditor {

        constructor(input: JQuery) {
            super(input);
            this.addValidationRule(this.uniqueName, e => {
                var value = Q.trimToNull(this.get_value());
                if (value == null) {
                    return null;
                }
                var error = TitleCaseEditor.validate(value);
                if (error) {
                    return error;
                }
                else {
                    // clear validation error message
                    //this.clearValidationErrors();
                    return null;
                }
            });
       

            input.bind('change', e => {
                if (!Serenity.WX.hasOriginalEvent(e)) {
                    return;
                }
                this.formatValue();
            });

            input.bind('blur', e => {
                if (this.element.hasClass('valid')) {
                    this.formatValue();
                }
            });
        }

        protected formatValue(): void {
            this.element.val(this.getFormattedValue());
        }

        protected getFormattedValue(): string {
            var value = this.element.val();
            return TitleCaseEditor.format(value);
        }

        get_value() {
            return this.getFormattedValue();
        }

        set_value(value: string) {
            this.element.val(value);
        }

       
        static validate(value: string) {
            if (Q.isEmptyOrNull(value)) {
                return "Please enter a value.";
            } else if (!TitleCaseEditor.isValid(value)) {
                return "Please enter only alphabetical characters.";
            }
           
            return null;
        }
        
        //static isValid(value: string) {
        //    return /^[a-zA-Z]*$/.test(value);
        //}
        static isValid(value: string) {
            return /^[a-zA-Z ]*$/g.test(value);
        }
        static format(value: string) {
            value = value.replace(/[^a-zA-Z ]/g, "");
            value = value.toLowerCase();
            value = value.replace(/\b\w/g, function (m) {
                return m.toUpperCase();
            });
            return value;
        }

        
    }

}