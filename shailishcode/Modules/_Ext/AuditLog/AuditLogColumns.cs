
namespace _Ext.Columns
{
    using _Ext.Entities;
    using Serenity.ComponentModel;
    using System;
    using System.ComponentModel;

    [ColumnsScript("_Ext.AuditLog")]
    [BasedOnRow(typeof(AuditLogRow))]
    public class AuditLogColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight, Hidden]
        public Int64 Id { get; set; }
        public String EntityTableName { get; set; }
        public AuditActionType ActionType { get; set; }
        public DateTime ActionDate { get; set; }
        public Int64 EntityId { get; set; }
        public String Changes { get; set; }
        [Width(200, Min = 150)]
        public Int64 UserId { get; set; }
        public String IpAddress { get; set; }
    }
}