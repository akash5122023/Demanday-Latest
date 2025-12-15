using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.ThirdParty
{
    public partial class IVRNumberEditorAttribute : LookupEditorBaseAttribute
    {
        public const string Key = "AdvanceCRM.ThirdParty.IVRNumberEditor";

        public IVRNumberEditorAttribute()
            : base(Key)
        {
        }
    }
}
