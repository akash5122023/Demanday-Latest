namespace AdvanceCRM.ThirdParty {
    export interface IVRCallForm {
        IVRNumber: IVRNumberEditor;
        AgentNumber: Serenity.LookupEditor;
        CustomerNumber: Serenity.StringEditor;
    }

    export class IVRCallForm extends Serenity.PrefixedContext {
        static formKey = 'ThirdParty.IVRCall';
        private static init: boolean;

        constructor(prefix: string) {
            super(prefix);

            if (!IVRCallForm.init)  {
                IVRCallForm.init = true;

                var s = Serenity;
                var w0 = IVRNumberEditor;
                var w1 = s.LookupEditor;
                var w2 = s.StringEditor;

                Q.initFormType(IVRCallForm, [
                    'IVRNumber', w0,
                    'AgentNumber', w1,
                    'CustomerNumber', w2
                ]);
            }
        }
    }
}
