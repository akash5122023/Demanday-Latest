
namespace AdvanceCRM.ThirdParty {
    export class IVRCallOptions {
        CustomerNumber: string;
        ServiceURL: string;
    }

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class IVRCallDialog extends Serenity.PropertyDialog<IVRCallOptions, any> {

        private form: IVRCallForm;

        constructor(opt?: IVRCallOptions) {
            super(opt);

            this.form = new IVRCallForm(this.idPrefix);
            this.form.CustomerNumber.value = opt.CustomerNumber;


        }

        protected getDialogTitle(): string {
            return "IVR Click to Call";
        }

        protected getDialogButtons(): Serenity.DialogButton[] {
            return [
                {
                    text: 'Call',
                    click: () => {
                        Q.serviceRequest(
                            this.options.ServiceURL + '/ClickToCall',
                            {
                                IVRNumber: this.form.IVRNumber.text,
                                AgentNumber: this.form.AgentNumber.value,
                                CustomerNumber: this.form.CustomerNumber.value
                              //  CallID: this.form.CallID.value
                                
                            },
                            response => {Q.notifyInfo(response.Status);}
                            );

                        this.dialogClose();   
                    }

                }
            ];
        }
    }
}