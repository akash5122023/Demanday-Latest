using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Administration
{
    public partial class UserEditorAttribute : LookupEditorBaseAttribute
    {
        public const string Key = "AdvanceCRM.Administration.UserEditor";

        public UserEditorAttribute()
            : base(Key)
        {
        }
    }
}
