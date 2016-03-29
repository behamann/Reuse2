namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correcoes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Imagems", "anuncioID", "dbo.Anuncios");
            DropIndex("dbo.Imagems", new[] { "anuncioID" });
            DropTable("dbo.Imagems");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.imagemID);
            
            CreateIndex("dbo.Imagems", "anuncioID");
            AddForeignKey("dbo.Imagems", "anuncioID", "dbo.Anuncios", "anuncioID", cascadeDelete: true);
        }
    }
}
