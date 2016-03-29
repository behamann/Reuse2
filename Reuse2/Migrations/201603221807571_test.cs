namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Anuncios", "interessadoID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notificacaos", "anuncioRelacionado_anuncioID", "dbo.Anuncios");
            DropForeignKey("dbo.Notificacaos", "destino_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notificacaos", "origem_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Anuncios", new[] { "interessadoID" });
            DropIndex("dbo.Notificacaos", new[] { "anuncioRelacionado_anuncioID" });
            DropIndex("dbo.Notificacaos", new[] { "destino_Id" });
            DropIndex("dbo.Notificacaos", new[] { "origem_Id" });
            CreateTable(
                "dbo.Interesses",
                c => new
                    {
                        anuncioID = c.Int(nullable: false),
                        userID = c.String(nullable: false, maxLength: 128),
                        texto = c.String(),
                        aceito = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.anuncioID, t.userID })
                .ForeignKey("dbo.AspNetUsers", t => t.userID, cascadeDelete: true)
                .ForeignKey("dbo.Anuncios", t => t.anuncioID, cascadeDelete: true)
                .Index(t => t.anuncioID)
                .Index(t => t.userID);
            
            AddColumn("dbo.Anuncios", "Interesses_anuncioID", c => c.Int());
            AddColumn("dbo.Anuncios", "Interesses_userID", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Interesses_anuncioID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Interesses_userID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Anuncios", new[] { "Interesses_anuncioID", "Interesses_userID" });
            CreateIndex("dbo.AspNetUsers", new[] { "Interesses_anuncioID", "Interesses_userID" });
            AddForeignKey("dbo.Anuncios", new[] { "Interesses_anuncioID", "Interesses_userID" }, "dbo.Interesses", new[] { "anuncioID", "userID" });
            AddForeignKey("dbo.AspNetUsers", new[] { "Interesses_anuncioID", "Interesses_userID" }, "dbo.Interesses", new[] { "anuncioID", "userID" });
            DropColumn("dbo.Anuncios", "interessadoID");
            DropTable("dbo.Notificacaos");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notificacaos",
                c => new
                    {
                        notificacaoID = c.Int(nullable: false, identity: true),
                        texto = c.String(),
                        visualizada = c.Boolean(nullable: false),
                        anuncioRelacionado_anuncioID = c.Int(),
                        destino_Id = c.String(maxLength: 128),
                        origem_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.notificacaoID);
            
            AddColumn("dbo.Anuncios", "interessadoID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Interesses", "anuncioID", "dbo.Anuncios");
            DropForeignKey("dbo.AspNetUsers", new[] { "Interesses_anuncioID", "Interesses_userID" }, "dbo.Interesses");
            DropForeignKey("dbo.Interesses", "userID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Anuncios", new[] { "Interesses_anuncioID", "Interesses_userID" }, "dbo.Interesses");
            DropIndex("dbo.AspNetUsers", new[] { "Interesses_anuncioID", "Interesses_userID" });
            DropIndex("dbo.Interesses", new[] { "userID" });
            DropIndex("dbo.Interesses", new[] { "anuncioID" });
            DropIndex("dbo.Anuncios", new[] { "Interesses_anuncioID", "Interesses_userID" });
            DropColumn("dbo.AspNetUsers", "Interesses_userID");
            DropColumn("dbo.AspNetUsers", "Interesses_anuncioID");
            DropColumn("dbo.Anuncios", "Interesses_userID");
            DropColumn("dbo.Anuncios", "Interesses_anuncioID");
            DropTable("dbo.Interesses");
            CreateIndex("dbo.Notificacaos", "origem_Id");
            CreateIndex("dbo.Notificacaos", "destino_Id");
            CreateIndex("dbo.Notificacaos", "anuncioRelacionado_anuncioID");
            CreateIndex("dbo.Anuncios", "interessadoID");
            AddForeignKey("dbo.Notificacaos", "origem_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Notificacaos", "destino_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Notificacaos", "anuncioRelacionado_anuncioID", "dbo.Anuncios", "anuncioID");
            AddForeignKey("dbo.Anuncios", "interessadoID", "dbo.AspNetUsers", "Id");
        }
    }
}
