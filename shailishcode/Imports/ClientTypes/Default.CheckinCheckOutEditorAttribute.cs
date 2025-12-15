using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Default
{
    public partial class CheckinCheckOutEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Default.CheckinCheckOutEditor";

        public CheckinCheckOutEditorAttribute()
            : base(Key)
        {
        }
    }
}
