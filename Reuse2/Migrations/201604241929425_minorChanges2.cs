namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minorChanges2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Anuncios", "condicao", c => c.String(nullable: false));
            AlterColumn("dbo.Anuncios", "tipo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Anuncios", "tipo", c => c.String());
            AlterColumn("dbo.Anuncios", "condicao", c => c.String());
        }
    }
}
