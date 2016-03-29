namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conteminteressados : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anuncios", "contemInteressados", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Anuncios", "contemInteressados");
        }
    }
}
