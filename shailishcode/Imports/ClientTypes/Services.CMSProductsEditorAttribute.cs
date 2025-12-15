using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Services
{
    public partial class CMSProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Services.CMSProductsEditor";

        public CMSProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
