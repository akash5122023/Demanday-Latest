using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Services
{
    public partial class AMCProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Services.AMCProductsEditor";

        public AMCProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
