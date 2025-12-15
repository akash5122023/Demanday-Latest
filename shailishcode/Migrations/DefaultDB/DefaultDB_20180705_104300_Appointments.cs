using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20180705104300)]
    public class DefaultDB_20180705_104300_Appointments : Migration
    {
        public override void Up()
        {
            //Appointment section in all modules
            Create.Table("EnquiryAppointments")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Details").AsString(2000)
                .WithColumn("AppointmentDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("RepresentativeId").AsInt32().NotNullable().ForeignKey("FK_EnquiryAppointment_UserId", "dbo", "Users", "UserId")
                .WithColumn("EnquiryId").AsInt32().NotNullable().ForeignKey("FK_Appointment_EnquiryId", "dbo", "Enquiry", "Id")
                ;

            Create.Table("InvoiceAppointments")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Details").AsString(2000)
                .WithColumn("AppointmentDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("RepresentativeId").AsInt32().NotNullable().ForeignKey("FK_InvoiceAppointment_UserId", "dbo", "Users", "UserId")
                .WithColumn("InvoiceId").AsInt32().NotNullable().ForeignKey("FK_Appointment_InvoiceId", "dbo", "Invoice", "Id")
                ;

            Create.Table("QuotationAppointments")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Details").AsString(2000)
                .WithColumn("AppointmentDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("RepresentativeId").AsInt32().NotNullable().ForeignKey("FK_QuotationAppointment_UserId", "dbo", "Users", "UserId")
                .WithColumn("QuotationId").AsInt32().NotNullable().ForeignKey("FK_Appointment_QuotationId", "dbo", "Quotation", "Id")
                ;

            Create.Table("TeleCallingAppointments")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Details").AsString(2000)
                .WithColumn("AppointmentDate").AsDateTime().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("RepresentativeId").AsInt32().NotNullable().ForeignKey("FK_TeleCallingAppointment_UserId", "dbo", "Users", "UserId")
                .WithColumn("TeleCallingId").AsInt32().NotNullable().ForeignKey("FK_Appointment_TeleCallingId", "dbo", "TeleCalling", "Id")
                ;
        }

        public override void Down()
        {

        }
    }
}