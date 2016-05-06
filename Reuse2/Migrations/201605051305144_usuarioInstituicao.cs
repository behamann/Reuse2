namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarioInstituicao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "role", c => c.String());
            AddColumn("dbo.AspNetUsers", "cnpj", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "nomeDoResponsavel", c => c.String());
            AddColumn("dbo.AspNetUsers", "tipo_tipoID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "tipo_nome", c => c.String());
            AddColumn("dbo.AspNetUsers", "descricaoDaCausa", c => c.String());
            AddColumn("dbo.AspNetUsers", "itensNecessitados", c => c.String());
            AddColumn("dbo.AspNetUsers", "metodoDeColeta", c => c.String());
            AddColumn("dbo.AspNetUsers", "areaDeCobertura", c => c.String());
            AddColumn("dbo.AspNetUsers", "restricoesDeColeta", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "restricoesDeColeta");
            DropColumn("dbo.AspNetUsers", "areaDeCobertura");
            DropColumn("dbo.AspNetUsers", "metodoDeColeta");
            DropColumn("dbo.AspNetUsers", "itensNecessitados");
            DropColumn("dbo.AspNetUsers", "descricaoDaCausa");
            DropColumn("dbo.AspNetUsers", "tipo_nome");
            DropColumn("dbo.AspNetUsers", "tipo_tipoID");
            DropColumn("dbo.AspNetUsers", "nomeDoResponsavel");
            DropColumn("dbo.AspNetUsers", "cnpj");
            DropColumn("dbo.AspNetUsers", "role");
        }
    }
}
