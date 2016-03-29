namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anuncioUserIdString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Anuncios", new[] { "pessoa_Id" });
            DropColumn("dbo.Anuncios", "pessoaID");
            RenameColumn(table: "dbo.Anuncios", name: "pessoa_Id", newName: "pessoaID");
            AlterColumn("dbo.Anuncios", "pessoaID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Anuncios", "pessoaID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Anuncios", new[] { "pessoaID" });
            AlterColumn("dbo.Anuncios", "pessoaID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Anuncios", name: "pessoaID", newName: "pessoa_Id");
            AddColumn("dbo.Anuncios", "pessoaID", c => c.Int(nullable: false));
            CreateIndex("dbo.Anuncios", "pessoa_Id");
        }
    }
}
