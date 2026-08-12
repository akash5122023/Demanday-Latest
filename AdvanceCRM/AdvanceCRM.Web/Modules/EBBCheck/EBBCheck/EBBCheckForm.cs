using Serenity.ComponentModel;
using System;
using System.ComponentModel;

namespace AdvanceCRM.EBBCheck.Forms
{
    [FormScript("EBBCheck.EBBCheck")]
    [BasedOnRow(typeof(EBBCheckRow), CheckNames = true)]
    public class EBBCheckForm
    {
        // FirstName / Email / Date are entered once on insert and locked afterwards –
        // the dialog makes them read-only in edit mode (EBBCheckDialog.ts) and the
        // server keeps the original values on update (EBBCheckSaveHandler).
        [Category("EBB Check")]
        [HalfWidth]
        public String FirstName { get; set; }
        [HalfWidth]
        public String Email { get; set; }
        [HalfWidth, DateTimeEditor]
        public DateTime Date { get; set; }
        // Editable only by the Quality team – disabled on the client for others
        // (see EBBCheckDialog.ts) and enforced on the server (EBBCheckSaveHandler).
        [HalfWidth]
        public EbbStatus Status { get; set; }
        // "Created By" – auto-stamped on create and kept read-only afterwards.
        [Category("Representatives")]
        [HalfWidth]
        [ReadOnly(true)]
        public Int32 OwnerId { get; set; }
    }
}
