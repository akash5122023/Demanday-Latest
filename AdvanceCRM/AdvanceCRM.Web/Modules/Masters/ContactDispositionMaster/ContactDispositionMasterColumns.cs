using Serenity;
using Serenity.ComponentModel;
using Serenity.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;

namespace AdvanceCRM.Masters.Columns
{
    [ColumnsScript("Masters.ContactDispositionMaster")]
    [BasedOnRow(typeof(ContactDispositionMasterRow), CheckNames = true)]
    public class ContactDispositionMasterColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String Name { get; set; }
    }
}