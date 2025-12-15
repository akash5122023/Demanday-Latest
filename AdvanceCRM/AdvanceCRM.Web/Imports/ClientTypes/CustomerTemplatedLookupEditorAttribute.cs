using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM
{
    public partial class CustomerTemplatedLookupEditorAttribute : LookupEditorBaseAttribute
    {
        public const string Key = "AdvanceCRM.CustomerTemplatedLookupEditor";

        public CustomerTemplatedLookupEditorAttribute()
            : base(Key)
        {
        }
    }
}
