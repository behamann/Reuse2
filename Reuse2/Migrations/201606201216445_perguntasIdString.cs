namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perguntasIdString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Perguntas", new[] { "questionador_Id" });
            DropColumn("dbo.Perguntas", "questionadorId");
            RenameColumn(table: "dbo.Perguntas", name: "questionador_Id", newName: "questionadorId");
            AlterColumn("dbo.Perguntas", "questionadorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Perguntas", "questionadorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Perguntas", new[] { "questionadorId" });
            AlterColumn("dbo.Perguntas", "questionadorId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Perguntas", name: "questionadorId", newName: "questionador_Id");
            AddColumn("dbo.Perguntas", "questionadorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Perguntas", "questionador_Id");
        }
    }
}
