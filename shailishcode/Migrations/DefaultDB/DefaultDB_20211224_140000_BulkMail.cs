using FluentMigrator;

namespace AdvanceCRM.Migrations.DefaultDB
{
    [Migration(20211224140000)]
    public class DefaultDB_20211224_140000_BulkMail : Migration
    {
        public override void Up()
        {
          
            Insert.IntoTable("BulkMailConfig").Row(new
            {
                Host = "SMTP.gmail.com",
                Port = "587",
                SSL = false
            });

           
        }

        public override void Down()
        {

        }
    }
}