namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Imagems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        AnuncioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Anuncios", t => t.AnuncioId, cascadeDelete: true)
                .Index(t => t.AnuncioId);
            
            AddColumn("dbo.AspNetUsers", "avatar", c => c.String());
            AlterColumn("dbo.Anuncios", "condicao", c => c.String(nullable: false));
            AlterColumn("dbo.Anuncios", "tipo", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "endereco", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "cep", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "cidade", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "estado", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Imagems", "AnuncioId", "dbo.Anuncios");
            DropIndex("dbo.Imagems", new[] { "AnuncioId" });
            AlterColumn("dbo.AspNetUsers", "estado", c => c.String());
            AlterColumn("dbo.AspNetUsers", "cidade", c => c.String());
            AlterColumn("dbo.AspNetUsers", "cep", c => c.String());
            AlterColumn("dbo.AspNetUsers", "endereco", c => c.String());
            AlterColumn("dbo.Anuncios", "tipo", c => c.String());
            AlterColumn("dbo.Anuncios", "condicao", c => c.String());
            DropColumn("dbo.AspNetUsers", "avatar");
            DropTable("dbo.Imagems");
        }
    }
}
