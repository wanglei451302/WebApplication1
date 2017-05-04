namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Materials");
        }
    }
}
