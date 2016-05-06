namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarioInstituicao3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoDeInstituicaos",
                c => new
                    {
                        tipoDeInstituicaoID = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                    })
                .PrimaryKey(t => t.tipoDeInstituicaoID);
            
            AddColumn("dbo.AspNetUsers", "tipo_tipoDeInstituicaoID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "tipo_tipoDeInstituicaoID");
            AddForeignKey("dbo.AspNetUsers", "tipo_tipoDeInstituicaoID", "dbo.TipoDeInstituicaos", "tipoDeInstituicaoID");
            DropColumn("dbo.AspNetUsers", "tipo_tipoID");
            DropColumn("dbo.AspNetUsers", "tipo_nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "tipo_nome", c => c.String());
            AddColumn("dbo.AspNetUsers", "tipo_tipoID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "tipo_tipoDeInstituicaoID", "dbo.TipoDeInstituicaos");
            DropIndex("dbo.AspNetUsers", new[] { "tipo_tipoDeInstituicaoID" });
            DropColumn("dbo.AspNetUsers", "tipo_tipoDeInstituicaoID");
            DropTable("dbo.TipoDeInstituicaos");
        }
    }
}
