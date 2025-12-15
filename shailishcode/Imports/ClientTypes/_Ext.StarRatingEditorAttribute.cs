using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace _Ext
{
    public partial class StarRatingEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "_Ext.StarRatingEditor";

        public StarRatingEditorAttribute()
            : base(Key)
        {
        }
    }
}
