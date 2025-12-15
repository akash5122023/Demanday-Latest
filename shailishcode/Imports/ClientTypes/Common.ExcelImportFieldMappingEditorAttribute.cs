using Serenity;
using Serenity.ComponentModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdvanceCRM.Common
{
    public partial class ExcelImportFieldMappingEditorAttribute : CustomEditorAttribute
    {
        public const string Key = "AdvanceCRM.Common.ExcelImportFieldMappingEditor";

        public ExcelImportFieldMappingEditorAttribute()
            : base(Key)
        {
        }
    }
}
