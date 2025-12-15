using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20170123131300)]
    public class DefaultDB_20170123_131300_Tasks : Migration
    {
        public override void Up()
        {

            Create.Table("TaskType")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Type").AsString(50).NotNullable()
                ;


            Create.Table("TaskStatus").WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Status").AsString(200).NotNullable();

            Create.Table("Tasks")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("Task").AsString(150).NotNullable()
                .WithColumn("Details").AsString(350).Nullable()
                .WithColumn("CreationDate").AsDateTime().NotNullable()
                .WithColumn("ExpectedCompletion").AsDateTime().Nullable()
                .WithColumn("AssignedBy").AsInt32().NotNullable().ForeignKey("FK_TABUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("AssignedTo").AsInt32().NotNullable().ForeignKey("FK_TATUserId_UserId", "dbo", "Users", "UserId")
                .WithColumn("StatusId").AsInt32().NotNullable().ForeignKey("FK_Tasks_StatusId", "dbo", "TaskStatus", "Id")
                .WithColumn("CompletionDate").AsDateTime().Nullable()
                .WithColumn("TypeId").AsInt32().Nullable().ForeignKey("FK_TaskType_TaskTypeId", "dbo", "TaskType", "Id")
                .WithColumn("Attachments").AsString(1024).Nullable()
                .WithColumn("Priority").AsInt32().NotNullable().WithDefaultValue(1)
                .WithColumn("Resolution").AsString(2048).Nullable()
                .WithColumn("ContactsId").AsInt32().Nullable().ForeignKey("FK_Tasks_ContactsId", "dbo", "Contacts", "Id")
                .WithColumn("ProductId").AsInt32().Nullable().ForeignKey("FK_Tasks_ProductsId", "dbo", "Products", "Id")
                ;

            Create.Table("TaskWatcher")
                 .WithColumn("Id").AsInt32().Identity().PrimaryKey().NotNullable()
                 .WithColumn("AssignedId").AsInt32().NotNullable().ForeignKey("FK_TaskWatcher_UserId", "dbo", "Users", "UserId")
                 .WithColumn("TasksId").AsInt32().NotNullable().ForeignKey("FK_TaskWatcher_TasksId", "dbo", "Tasks", "Id")
                 ;



            Insert.IntoTable("TaskType").Row(new { Type = "General" });
            Insert.IntoTable("TaskStatus").Row(new { Status = "Open" });
            Insert.IntoTable("TaskStatus").Row(new { Status = "Closed" });
        }

        public override void Down()
        {

        }
    }
}