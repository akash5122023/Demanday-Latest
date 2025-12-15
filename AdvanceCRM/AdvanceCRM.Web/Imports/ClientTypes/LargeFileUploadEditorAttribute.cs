using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM
{
    public partial class LargeFileUploadEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.LargeFileUploadEditor";

        public LargeFileUploadEditorAttribute()
            : base(Key)
        {
        }
    }
}
