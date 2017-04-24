namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addadmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "salt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "salt");
        }
    }
}
