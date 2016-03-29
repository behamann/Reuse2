namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeComInteressados : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Anuncios", "contemInteressados");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Anuncios", "contemInteressados", c => c.Boolean(nullable: false));
        }
    }
}
