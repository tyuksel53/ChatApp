namespace YazlabII_Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastLoginTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastLoginTime");
        }
    }
}
