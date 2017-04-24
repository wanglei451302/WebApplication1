namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AA : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Admins", "created_at", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Admins", "created_at", c => c.String());
        }
    }
}
