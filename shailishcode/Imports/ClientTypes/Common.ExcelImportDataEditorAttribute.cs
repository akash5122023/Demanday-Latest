using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Common
{
    public partial class ExcelImportDataEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Common.ExcelImportDataEditor";

        public ExcelImportDataEditorAttribute()
            : base(Key)
        {
        }
    }
}
