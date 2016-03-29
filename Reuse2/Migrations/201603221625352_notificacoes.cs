namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificacoes : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.notificacaoID)
                .ForeignKey("dbo.Anuncios", t => t.anuncioRelacionado_anuncioID)
                .ForeignKey("dbo.AspNetUsers", t => t.destino_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.origem_Id)
                .Index(t => t.anuncioRelacionado_anuncioID)
                .Index(t => t.destino_Id)
                .Index(t => t.origem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notificacaos", "origem_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notificacaos", "destino_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notificacaos", "anuncioRelacionado_anuncioID", "dbo.Anuncios");
            DropIndex("dbo.Notificacaos", new[] { "origem_Id" });
            DropIndex("dbo.Notificacaos", new[] { "destino_Id" });
            DropIndex("dbo.Notificacaos", new[] { "anuncioRelacionado_anuncioID" });
            DropTable("dbo.Notificacaos");
        }
    }
}
