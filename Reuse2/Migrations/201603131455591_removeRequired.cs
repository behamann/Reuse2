namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "endereco", c => c.String());
            AlterColumn("dbo.AspNetUsers", "cep", c => c.String());
            AlterColumn("dbo.AspNetUsers", "cidade", c => c.String());
            AlterColumn("dbo.AspNetUsers", "estado", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "estado", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "cidade", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "cep", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "endereco", c => c.String(nullable: false));
        }
    }
}
