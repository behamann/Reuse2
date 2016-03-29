namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "endereco", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "cep", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "bairro", c => c.String());
            AddColumn("dbo.AspNetUsers", "cidade", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "estado", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "telefone", c => c.String());
            AddColumn("dbo.AspNetUsers", "itensDoados", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "itensPedidos", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "itensPedidos");
            DropColumn("dbo.AspNetUsers", "itensDoados");
            DropColumn("dbo.AspNetUsers", "telefone");
            DropColumn("dbo.AspNetUsers", "estado");
            DropColumn("dbo.AspNetUsers", "cidade");
            DropColumn("dbo.AspNetUsers", "bairro");
            DropColumn("dbo.AspNetUsers", "cep");
            DropColumn("dbo.AspNetUsers", "endereco");
        }
    }
}
