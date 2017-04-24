namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmin : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Admins");
            AlterColumn("dbo.Admins", "name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Admins", "name");
            DropColumn("dbo.Admins", "id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "id", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Admins");
            AlterColumn("dbo.Admins", "name", c => c.String());
            AddPrimaryKey("dbo.Admins", "id");
        }
    }
}
