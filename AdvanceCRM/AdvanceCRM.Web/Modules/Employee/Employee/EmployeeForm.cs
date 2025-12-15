
namespace AdvanceCRM.Employee.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;  using Microsoft.AspNetCore.Mvc;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("Employee.Employee")]
    [BasedOnRow(typeof(EmployeeRow), CheckNames = true)]
    public class EmployeeForm
    {
        [Tab("General")]
        [Category("Basic Details")]
        [HalfWidth(UntilNext =true)]
         
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
      
        public Int32 Country { get; set; }
        public Int32 StateId { get; set; }
        public Int32 CityId { get; set; }       
        public Int32 TehsilId { get; set; }
        public Int32 VillageId { get; set; }
        public String Pin { get; set; }
        public String AdditionalInfo { get; set; }
        
        [Category("Official")]
        public Int32 CompanyId { get; set; }
        public String EmpCode { get; set; }
        public String ProfessionalEmail { get; set; }
        public Int32 DepartmentId { get; set; }
        public Int32 RolesId { get; set; }
        public Int32 OwnerId { get; set; }
        [Tab("Additional Details")]
        public Int32 Gender { get; set; }
        public Int32 Religion { get; set; }
        public Int32 AreaId { get; set; }
        public Int32 MaritalStatus { get; set; }
        public DateTime MarriageAnniversary { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime DateOfJoining { get; set; }    
        public String Attachment { get; set; }
        [Tab("Bank Details")]
        public String AdhaarNo { get; set; }
        public String PanNo { get; set; }
        public String BankName { get; set; }
        public String AccountNumber { get; set; }
        public String Ifsc { get; set; }
        public String BankType { get; set; }
        public String Branch { get; set; }
       
    }
}