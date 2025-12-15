using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Default
{
    public partial class ApplicantStatusEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Default.ApplicantStatusEditor";

        public ApplicantStatusEditorAttribute()
            : base(Key)
        {
        }
    }
}
