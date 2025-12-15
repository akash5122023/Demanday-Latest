using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20190218110005)]
    public class DefaultDB_20190218_110005_IndiaMartDetails : Migration
    {
        public override void Up()
        {

            Create.Table("IndiaMartDetails")
              .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
              .WithColumn("Rn").AsInt32().Nullable()
              .WithColumn("QueryId").AsString(100).Nullable()
              .WithColumn("QueryType").AsString(10).Nullable()
              .WithColumn("SenderName").AsString(200).Nullable()
              .WithColumn("SenderEmail").AsString(200).Nullable()
              .WithColumn("Subject").AsString(500).Nullable()
              .WithColumn("DateRe").AsString(100).Nullable()
              .WithColumn("DateR").AsString(100).Nullable()
              .WithColumn("DateTimeRe").AsString(100).Nullable()
              .WithColumn("GlUserCompanyName").AsString(500).Nullable()
              .WithColumn("ReadStatus").AsInt32().Nullable()
              .WithColumn("SenderGLUserId").AsString(100).Nullable()
              .WithColumn("Mob").AsString(1024).Nullable()
              .WithColumn("CountryFlag").AsString(500).Nullable()
              .WithColumn("QueryModId").AsString(100).Nullable()
              .WithColumn("LogTime").AsString(100).Nullable()
              .WithColumn("QueryModRefId").AsString(100).Nullable()
              .WithColumn("DIRQueryModrefType").AsInt16().Nullable()
              .WithColumn("ORGSenderGLUserId").AsString(100).Nullable()
              .WithColumn("EnqMessage").AsString(1000).Nullable()
              .WithColumn("EnqAddress").AsString(1000).Nullable()
              .WithColumn("EnqCallDuration").AsString(100).Nullable()
              .WithColumn("EnqReceiverMob").AsString(100).Nullable()
              .WithColumn("EnqCity").AsString(100).Nullable()
              .WithColumn("EnqState").AsString(100).Nullable()
              .WithColumn("ProductName").AsString(500).Nullable()
              .WithColumn("CountryISO").AsString(100).Nullable()
              .WithColumn("EmailAlt").AsString(100).Nullable()
              .WithColumn("MobileAlt").AsString(100).Nullable()
              .WithColumn("Phone").AsString(100).Nullable()
              .WithColumn("PhoneAlt").AsString(100).Nullable()
              .WithColumn("ImmemberSince").AsString(100).Nullable()
              .WithColumn("TotalCnt").AsInt32().Nullable();
        }
        public override void Down()
        {
        }
    }
}