using AdvanceCRM.Administration.Entities;
using Serenity.ComponentModel;
using System;

namespace AdvanceCRM.Administration.Forms
{
    [FormScript("Administration.Role")]
    [BasedOnRow(typeof(RoleRow), CheckNames = true)]
    public class RoleForm
    {
        public String RoleName { get; set; }
    }
}