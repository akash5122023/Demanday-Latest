using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20210718110000)]
    public class DefaultDB_20210718_110000_Employee : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists("Department", table => table
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Department").AsString(200).NotNullable());

            Create.Table("Employee").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("EmpCode").AsString(20).NotNullable()
                .WithColumn("DepartmentId").AsInt32().Nullable().ForeignKey("FK_EDepartment_DepartmentId", "dbo", "Department", "Id")
                .WithColumn("Name").AsString(150).NotNullable()
                .WithColumn("Phone").AsString(100).Nullable()
                .WithColumn("Email").AsString(100).Nullable()
                .WithColumn("Address").AsString(500).Nullable()
                .WithColumn("ProfessionalEmail").AsString(100).Nullable()
                .WithColumn("CityId").AsInt32().Nullable().ForeignKey("FK_ECity_CityId", "dbo", "City", "Id")
                .WithColumn("StateId").AsInt32().Nullable().ForeignKey("FK_EState_StateId", "dbo", "State", "Id")
                .WithColumn("Pin").AsString(50).Nullable()
                .WithColumn("Country").AsInt32().Nullable()
                .WithColumn("AdditionalInfo").AsString(5000).Nullable()               
                .WithColumn("Gender").AsInt32().Nullable()
                .WithColumn("Religion").AsInt32().Nullable()
                .WithColumn("AreaId").AsInt32().Nullable().ForeignKey("FK_EArea_AreaId", "dbo", "Area", "Id")
                .WithColumn("MaritalStatus").AsInt32().Nullable()
                .WithColumn("MarriageAnniversary").AsDateTime().Nullable()
                .WithColumn("Birthdate").AsDateTime().Nullable()
                .WithColumn("DateOfJoining").AsDateTime().Nullable()
                .WithColumn("CompanyId").AsInt32().NotNullable().ForeignKey("FK_ECompany_CompanyDetailsId","dbo","CompanyDetails","Id")
                .WithColumn("RolesId").AsInt32().Nullable().ForeignKey("FK_ERoles_RolesId", "dbo", "Roles", "RoleId")
                .WithColumn("OwnerId").AsInt32().NotNullable().ForeignKey("FK_EmpUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AdhaarNo").AsString(50).Nullable()
                .WithColumn("PANNo").AsString(50).Nullable()               
                .WithColumn("Attachment").AsString(500).Nullable()                
                .WithColumn("BankName").AsString(100).Nullable()
                .WithColumn("AccountNumber").AsString(100).Nullable()
                .WithColumn("IFSC").AsString(100).Nullable()
                .WithColumn("BankType").AsString(100).Nullable()
                .WithColumn("Branch").AsString(100).Nullable()  
                .WithColumn("TehsilId").AsInt32().Nullable().ForeignKey("FK_Employee_TehsilId", "dbo", "Tehsil", "Id")
                .WithColumn("VillageId").AsInt32().Nullable().ForeignKey("FK_Employee_VillageId", "dbo", "Village", "Id")
                ;

            

        }

        public override void Down()
        {

        }
    }
}