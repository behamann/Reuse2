namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FUNCIONA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anuncios", "interessadoID", c => c.String(maxLength: 128));
            AddColumn("dbo.Anuncios", "dataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Anuncios", "status", c => c.String());
            CreateIndex("dbo.Anuncios", "interessadoID");
            AddForeignKey("dbo.Anuncios", "interessadoID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Anuncios", "interessadoID", "dbo.AspNetUsers");
            DropIndex("dbo.Anuncios", new[] { "interessadoID" });
            DropColumn("dbo.Anuncios", "status");
            DropColumn("dbo.Anuncios", "dataCriacao");
            DropColumn("dbo.Anuncios", "interessadoID");
        }
    }
}
