using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.EBBCheck.Forms
{
    [FormScript("EBBCheck.EBBCheck")]
    [BasedOnRow(typeof(EBBCheckRow), CheckNames = true)]
    public class EBBCheckForm
    {
        [Category("EBB Check")]
        [HalfWidth]
        public String FirstName { get; set; }
        [HalfWidth]
        public String Email { get; set; }
        [HalfWidth, DateTimeEditor]
        public DateTime Date { get; set; }
        // Editable only by the Quality team – disabled on the client for others
        // (see EBBCheckDialog.ts) and enforced on the server (EBBCheckSaveHandler).
        [FullWidth]
        public EbbStatus Status { get; set; }
        // "Created By" – auto-stamped, read-only.
        [Category("Representatives")]
        [HalfWidth]
        public Int32 OwnerId { get; set; }
    }
}
