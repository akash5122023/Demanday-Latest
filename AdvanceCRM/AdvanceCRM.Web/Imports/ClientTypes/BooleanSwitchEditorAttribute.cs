using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM
{
    public partial class BooleanSwitchEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.BooleanSwitchEditor";

        public BooleanSwitchEditorAttribute()
            : base(Key)
        {
        }

        public String Css
        {
            get { return GetOption<String>("css"); }
            set { SetOption("css", value); }
        }
    }
}
