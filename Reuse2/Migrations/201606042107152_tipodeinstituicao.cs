namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tipodeinstituicao : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "tipo_tipoDeInstituicaoID", "dbo.TipoDeInstituicaos");
            DropIndex("dbo.AspNetUsers", new[] { "tipo_tipoDeInstituicaoID" });
            RenameColumn(table: "dbo.AspNetUsers", name: "tipo_tipoDeInstituicaoID", newName: "tipoDeInstituicaoID");
            AlterColumn("dbo.AspNetUsers", "tipoDeInstituicaoID", c => c.Int(nullable: true));
            CreateIndex("dbo.AspNetUsers", "tipoDeInstituicaoID");
            AddForeignKey("dbo.AspNetUsers", "tipoDeInstituicaoID", "dbo.TipoDeInstituicaos", "tipoDeInstituicaoID", cascadeDelete: true);
            DropColumn("dbo.AspNetUsers", "tipoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "tipoID", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "tipoDeInstituicaoID", "dbo.TipoDeInstituicaos");
            DropIndex("dbo.AspNetUsers", new[] { "tipoDeInstituicaoID" });
            AlterColumn("dbo.AspNetUsers", "tipoDeInstituicaoID", c => c.Int());
            RenameColumn(table: "dbo.AspNetUsers", name: "tipoDeInstituicaoID", newName: "tipo_tipoDeInstituicaoID");
            CreateIndex("dbo.AspNetUsers", "tipo_tipoDeInstituicaoID");
            AddForeignKey("dbo.AspNetUsers", "tipo_tipoDeInstituicaoID", "dbo.TipoDeInstituicaos", "tipoDeInstituicaoID");
        }
    }
}
