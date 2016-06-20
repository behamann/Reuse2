namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perguntas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perguntas",
                c => new
                    {
                        perguntaID = c.Int(nullable: false, identity: true),
                        descricao = c.String(),
                        questionadorId = c.Int(nullable: false),
                        AnuncioId = c.Int(nullable: false),
                        dataDeCriacao = c.DateTime(nullable: false),
                        resposta = c.String(),
                        questionador_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.perguntaID)
                .ForeignKey("dbo.Anuncios", t => t.AnuncioId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.questionador_Id)
                .Index(t => t.AnuncioId)
                .Index(t => t.questionador_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Perguntas", "questionador_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Perguntas", "AnuncioId", "dbo.Anuncios");
            DropIndex("dbo.Perguntas", new[] { "questionador_Id" });
            DropIndex("dbo.Perguntas", new[] { "AnuncioId" });
            DropTable("dbo.Perguntas");
        }
    }
}
