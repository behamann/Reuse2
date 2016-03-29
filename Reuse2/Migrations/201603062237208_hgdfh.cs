namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hgdfh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Anuncios",
                c => new
                    {
                        anuncioID = c.Int(nullable: false, identity: true),
                        pessoaID = c.Int(nullable: false),
                        categoriaID = c.Int(nullable: false),
                        subCategoria = c.String(nullable: false),
                        condicao = c.String(),
                        titulo = c.String(nullable: false),
                        descricao = c.String(nullable: false),
                        tipo = c.String(),
                        video = c.String(),
                        ativo = c.Boolean(nullable: false),
                        pessoa_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.anuncioID)
                .ForeignKey("dbo.Categorias", t => t.categoriaID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.pessoa_Id)
                .Index(t => t.categoriaID)
                .Index(t => t.pessoa_Id);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        categoriaID = c.Int(nullable: false, identity: true),
                        titulo = c.String(),
                    })
                .PrimaryKey(t => t.categoriaID);
            
            CreateTable(
                "dbo.Subcategorias",
                c => new
                    {
                        subCategoriaID = c.Int(nullable: false, identity: true),
                        categoriaID = c.Int(nullable: false),
                        titulo = c.String(),
                    })
                .PrimaryKey(t => t.subCategoriaID)
                .ForeignKey("dbo.Categorias", t => t.categoriaID, cascadeDelete: true)
                .Index(t => t.categoriaID);
            
            CreateTable(
                "dbo.Imagems",
                c => new
                    {
                        imagemID = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        anuncioID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.imagemID)
                .ForeignKey("dbo.Anuncios", t => t.anuncioID, cascadeDelete: true)
                .Index(t => t.anuncioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Anuncios", "pessoa_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Imagems", "anuncioID", "dbo.Anuncios");
            DropForeignKey("dbo.Anuncios", "categoriaID", "dbo.Categorias");
            DropForeignKey("dbo.Subcategorias", "categoriaID", "dbo.Categorias");
            DropIndex("dbo.Imagems", new[] { "anuncioID" });
            DropIndex("dbo.Subcategorias", new[] { "categoriaID" });
            DropIndex("dbo.Anuncios", new[] { "pessoa_Id" });
            DropIndex("dbo.Anuncios", new[] { "categoriaID" });
            DropTable("dbo.Imagems");
            DropTable("dbo.Subcategorias");
            DropTable("dbo.Categorias");
            DropTable("dbo.Anuncios");
        }
    }
}
