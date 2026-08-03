using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.EBBCheck.Columns
{
    [ColumnsScript("EBBCheck.EBBCheck")]
    [BasedOnRow(typeof(EBBCheckRow), CheckNames = true)]
    public class EBBCheckColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        [EditLink]
        public String FirstName { get; set; }
        public String Email { get; set; }
        [EnumEditor]
        public EbbStatus Status { get; set; }
        [DisplayName("User Name")]
        public String UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Created By")]
        public String OwnerUsername { get; set; }
    }
}
