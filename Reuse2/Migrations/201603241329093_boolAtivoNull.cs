namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolAtivoNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Interesses", "aceito", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Interesses", "aceito", c => c.Boolean(nullable: false));
        }
    }
}
