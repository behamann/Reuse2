namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagens2 : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Imagems", "AnuncioId", "dbo.Anuncios");
            DropIndex("dbo.Imagems", new[] { "AnuncioId" });
            DropTable("dbo.Imagems");
        }
    }
}
