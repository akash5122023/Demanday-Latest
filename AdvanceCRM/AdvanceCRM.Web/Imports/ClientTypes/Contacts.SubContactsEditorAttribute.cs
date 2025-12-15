using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Contacts
{
    public partial class SubContactsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Contacts.SubContactsEditor";

        public SubContactsEditorAttribute()
            : base(Key)
        {
        }
    }
}
