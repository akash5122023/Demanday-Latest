using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM
{
    public partial class BSSwitchEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.BSSwitchEditor";

        public BSSwitchEditorAttribute()
            : base(Key)
        {
        }

        public Boolean Animate
        {
            get { return GetOption<Boolean>("animate"); }
            set { SetOption("animate", value); }
        }

        public String BaseClass
        {
            get { return GetOption<String>("baseClass"); }
            set { SetOption("baseClass", value); }
        }

        public Boolean Disabled
        {
            get { return GetOption<Boolean>("disabled"); }
            set { SetOption("disabled", value); }
        }

        public String HandleWidth
        {
            get { return GetOption<String>("handleWidth"); }
            set { SetOption("handleWidth", value); }
        }

        public Boolean Indeterminate
        {
            get { return GetOption<Boolean>("indeterminate"); }
            set { SetOption("indeterminate", value); }
        }

        public Boolean Invers
        {
            get { return GetOption<Boolean>("invers"); }
            set { SetOption("invers", value); }
        }

        public String LabelText
        {
            get { return GetOption<String>("labelText"); }
            set { SetOption("labelText", value); }
        }

        public String LabelWidth
        {
            get { return GetOption<String>("labelWidth"); }
            set { SetOption("labelWidth", value); }
        }

        public String OffColor
        {
            get { return GetOption<String>("offColor"); }
            set { SetOption("offColor", value); }
        }

        public String OffText
        {
            get { return GetOption<String>("offText"); }
            set { SetOption("offText", value); }
        }

        public String OnColor
        {
            get { return GetOption<String>("onColor"); }
            set { SetOption("onColor", value); }
        }

        public object OnInit
        {
            get { return GetOption<object>("onInit"); }
            set { SetOption("onInit", value); }
        }

        public object OnSwitchChange
        {
            get { return GetOption<object>("onSwitchChange"); }
            set { SetOption("onSwitchChange", value); }
        }

        public String OnText
        {
            get { return GetOption<String>("onText"); }
            set { SetOption("onText", value); }
        }

        public Boolean RadioAllOff
        {
            get { return GetOption<Boolean>("radioAllOff"); }
            set { SetOption("radioAllOff", value); }
        }

        public Boolean Readonly
        {
            get { return GetOption<Boolean>("readonly"); }
            set { SetOption("readonly", value); }
        }

        public String Size
        {
            get { return GetOption<String>("size"); }
            set { SetOption("size", value); }
        }

        public Boolean State
        {
            get { return GetOption<Boolean>("state"); }
            set { SetOption("state", value); }
        }

        public object ToggleState
        {
            get { return GetOption<Boolean>("toggleState"); }
            set { SetOption("toggleState", value); }
        }

        public String WrapperClass
        {
            get { return GetOption<String>("wrapperClass"); }
            set { SetOption("wrapperClass", value); }
        }
    }
}
