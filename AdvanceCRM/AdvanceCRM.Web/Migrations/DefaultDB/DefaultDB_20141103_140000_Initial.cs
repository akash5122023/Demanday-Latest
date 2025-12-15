using FluentMigrator;
using System;

namespace AdvanceCRM.Migrations.DefaultDB
{

    [Migration(20141103140000)]
    public class DefaultDB_20141103_140000_Initial : AutoReversingMigration
    {
        public override void Up()
        {
            this.CreateTableWithId32("Users", "UserId", s => s
                .WithColumn("Username").AsString(100).NotNullable()
                .WithColumn("DisplayName").AsString(100).NotNullable()
                .WithColumn("Email").AsString(100).Nullable()
                .WithColumn("Source").AsString(4).NotNullable()
                .WithColumn("PasswordHash").AsString(86).NotNullable()
                .WithColumn("PasswordSalt").AsString(10).NotNullable()
                .WithColumn("LastDirectoryUpdate").AsDateTime().Nullable()
                .WithColumn("UserImage").AsString(100).Nullable()
                .WithColumn("InsertDate").AsDateTime().NotNullable()
                .WithColumn("InsertUserId").AsInt32().NotNullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable()
                .WithColumn("IsActive").AsInt16().NotNullable().WithDefaultValue(1)
                .WithColumn("UpperLevel").AsInt32().NotNullable().ForeignKey("FK_UsersUserId_UserId", "dbo", "Users", "UserId").WithDefaultValue(1)
                .WithColumn("UpperLevel2").AsInt32().NotNullable().ForeignKey("FK_LVL2UserId_UserId", "dbo", "Users", "UserId").WithDefaultValue(1)
                .WithColumn("UpperLevel3").AsInt32().NotNullable().ForeignKey("FK_LVL3UserId_UserId", "dbo", "Users", "UserId").WithDefaultValue(1)
                .WithColumn("UpperLevel4").AsInt32().NotNullable().ForeignKey("FK_LVL4UserId_UserId", "dbo", "Users", "UserId").WithDefaultValue(1)
                .WithColumn("UpperLevel5").AsInt32().NotNullable().ForeignKey("FK_LVL5UserId_UserId", "dbo", "Users", "UserId").WithDefaultValue(1)
                .WithColumn("Host").AsString(200).Nullable()
                .WithColumn("Port").AsInt32().Nullable()
                .WithColumn("SSL").AsBoolean().Nullable()
                .WithColumn("EmailId").AsString(200).Nullable()
                .WithColumn("EmailPassword").AsString(200).Nullable()
                .WithColumn("Phone").AsString(50).Nullable()
                .WithColumn("MCSMTPServer").AsString(100).Nullable()
                .WithColumn("MCSMTPPort").AsInt32().Nullable()
                .WithColumn("MCIMAPServer").AsString(100).Nullable()
                .WithColumn("MCIMAPPort").AsInt32().Nullable()
                .WithColumn("MCUsername").AsString(100).Nullable()
                .WithColumn("MCPassword").AsString(100).Nullable()
                .WithColumn("StartTime").AsString(50).Nullable()
                .WithColumn("EndTime").AsString(50).Nullable()
                .WithColumn("UID").AsString(500).Nullable()
                .WithColumn("NonOperational").AsBoolean().Nullable()
                )
                ;

            Insert.IntoTable("Users").Row(new {
                Username = "admin",
                DisplayName = "admin",
                Email = "admin@dummy.com",
                Source = "site",
                PasswordHash = "krkONeKnBUpAbwMewgJjQj2iSrOTOgUrRHdSNXtfJrs+edCNKY5Z0Sb3mOYkelLOJqik0g8LcPyjt/AqQe2dhw",
                PasswordSalt = "[kn^_",
                InsertDate = new DateTime(2017, 1, 1),
                InsertUserId = 1,
                IsActive = 1
            });

            this.CreateTableWithId32("Languages", "Id", s => s
                .WithColumn("LanguageId").AsString(10).NotNullable()
                .WithColumn("LanguageName").AsString(50).NotNullable());

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "en",
                LanguageName = "English"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "ru",
                LanguageName = "Russian"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "es",
                LanguageName = "Spanish"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "tr",
                LanguageName = "Turkish"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "de",
                LanguageName = "German"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "zh-CN",
                LanguageName = "Chinese (Simplified)"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "it",
                LanguageName = "Italian"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "pt",
                LanguageName = "Portuguese"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "pt-BR",
                LanguageName = "Portuguese (Brazil)"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "fa",
                LanguageName = "Farsi"
            });

            Insert.IntoTable("Languages").Row(new
            {
                LanguageId = "vi-VN",
                LanguageName = "Vietnamese (Vietnam)"
            });
        }
    }
}