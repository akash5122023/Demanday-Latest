using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Enquiry
{
    public partial class EnquiryProductsEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Enquiry.EnquiryProductsEditor";

        public EnquiryProductsEditorAttribute()
            : base(Key)
        {
        }
    }
}
