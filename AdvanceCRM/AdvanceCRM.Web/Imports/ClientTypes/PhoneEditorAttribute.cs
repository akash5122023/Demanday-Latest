using Serenity.ComponentModel;
using System;

namespace AdvanceCRM
{
    public partial class PhoneEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "Serenity.PhoneEditor";

        public PhoneEditorAttribute()
            : base(Key)
        {
        }
    }
}
