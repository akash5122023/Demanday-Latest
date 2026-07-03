using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20261019100000)]
    public class DefaultDB_20261019_100000_ChatGroups : Migration
    {
        public override void Up()
        {
            if (!Schema.Table("ChatGroup").Exists())
            {
                Create.Table("ChatGroup")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Name").AsString(150).NotNullable()
                    .WithColumn("CreatedBy").AsInt32().NotNullable().ForeignKey("FK_ChatGroup_CreatedBy_UserId", "dbo", "Users", "UserId")
                    .WithColumn("CreatedDate").AsDateTime().NotNullable()
                    .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(1);
            }

            if (!Schema.Table("ChatGroupMember").Exists())
            {
                Create.Table("ChatGroupMember")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("GroupId").AsInt32().NotNullable().ForeignKey("FK_ChatGroupMember_GroupId", "dbo", "ChatGroup", "Id")
                    .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("FK_ChatGroupMember_UserId", "dbo", "Users", "UserId");

                Create.Index("UX_ChatGroupMember_Group_User")
                    .OnTable("ChatGroupMember")
                    .OnColumn("GroupId").Ascending()
                    .OnColumn("UserId").Ascending()
                    .WithOptions().Unique();
            }

            if (!Schema.Table("ChatGroupMessage").Exists())
            {
                Create.Table("ChatGroupMessage")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("GroupId").AsInt32().NotNullable().ForeignKey("FK_ChatGroupMessage_GroupId", "dbo", "ChatGroup", "Id")
                    .WithColumn("SenderId").AsInt32().NotNullable().ForeignKey("FK_ChatGroupMessage_SenderId_UserId", "dbo", "Users", "UserId")
                    .WithColumn("Message").AsCustom("nvarchar(max)").Nullable()
                    .WithColumn("SentDate").AsDateTime().Nullable()
                    .WithColumn("AttachmentPath").AsString(500).Nullable()
                    .WithColumn("AttachmentName").AsString(255).Nullable()
                    .WithColumn("AttachmentType").AsString(20).Nullable();

                Create.Index("IX_ChatGroupMessage_Group_Id")
                    .OnTable("ChatGroupMessage")
                    .OnColumn("GroupId").Ascending()
                    .OnColumn("Id").Ascending();
            }

            if (!Schema.Table("ChatGroupRead").Exists())
            {
                Create.Table("ChatGroupRead")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("GroupId").AsInt32().NotNullable()
                    .WithColumn("UserId").AsInt32().NotNullable()
                    .WithColumn("LastReadId").AsInt32().NotNullable().WithDefaultValue(0);

                Create.Index("UX_ChatGroupRead_Group_User")
                    .OnTable("ChatGroupRead")
                    .OnColumn("GroupId").Ascending()
                    .OnColumn("UserId").Ascending()
                    .WithOptions().Unique();
            }

            if (!Schema.Table("ChatUserSetting").Exists())
            {
                Create.Table("ChatUserSetting")
                    .WithColumn("UserId").AsInt32().PrimaryKey().ForeignKey("FK_ChatUserSetting_UserId", "dbo", "Users", "UserId")
                    .WithColumn("ChatEnabled").AsBoolean().NotNullable().WithDefaultValue(1);
            }
        }

        public override void Down()
        {
            Delete.Table("ChatUserSetting");
            Delete.Table("ChatGroupRead");
            Delete.Table("ChatGroupMessage");
            Delete.Table("ChatGroupMember");
            Delete.Table("ChatGroup");
        }
    }
}
