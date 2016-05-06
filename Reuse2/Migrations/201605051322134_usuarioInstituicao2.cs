namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarioInstituicao2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "tipoID", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "tipoID");
        }
    }
}
