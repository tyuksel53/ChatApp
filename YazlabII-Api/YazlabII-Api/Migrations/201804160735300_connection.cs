namespace YazlabII_Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class connection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Connections",
                c => new
                    {
                        ConnectionID = c.String(nullable: false, maxLength: 128),
                        UserAgent = c.String(),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.ConnectionID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Connections");
        }
    }
}
