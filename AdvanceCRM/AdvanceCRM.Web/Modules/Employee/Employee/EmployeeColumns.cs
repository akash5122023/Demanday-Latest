
namespace AdvanceCRM.Employee.Columns
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [ColumnsScript("Employee.Employee")]
    [BasedOnRow(typeof(EmployeeRow), CheckNames = true)]
    public class EmployeeColumns
    {
        [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
        public Int32 Id { get; set; }
        
        public String EmpCode { get; set; }
        [QuickFilter]
        public String Department { get; set; }
        [EditLink]
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String ProfessionalEmail { get; set; }
        [QuickFilter]
        public String City { get; set; }
        [QuickFilter]
        public String State { get; set; }
        public String Pin { get; set; }
        public Int32 Country { get; set; }
        public String AdditionalInfo { get; set; }
        [QuickFilter]
        public Int32 Gender { get; set; }
        public Int32 Religion { get; set; }
        public String Area { get; set; }
        public Int32 MaritalStatus { get; set; }
        public DateTime MarriageAnniversary { get; set; }
        public DateTime Birthdate { get; set; }
        [QuickFilter]
        public DateTime DateOfJoining { get; set; }
        [QuickFilter]
        public String CompanyName { get; set; }
        [QuickFilter]
        public String RolesRoleName { get; set; }
        [QuickFilter]
        public String OwnerUsername { get; set; }
        public String AdhaarNo { get; set; }
        public String PanNo { get; set; }
        public String Attachment { get; set; }
        public String BankName { get; set; }
        public String AccountNumber { get; set; }
        public String Ifsc { get; set; }
        public String BankType { get; set; }
        public String Branch { get; set; }
        public String Tehsil { get; set; }
        public String Village { get; set; }
    }
}