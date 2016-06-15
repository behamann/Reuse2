namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class distanciaEntreCeps : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DistanciaEntreCeps",
                c => new
                    {
                        distanciaEntreCepsID = c.Int(nullable: false, identity: true),
                        cep1 = c.String(),
                        cep2 = c.String(),
                        distancia = c.String(),
                        duracao = c.String(),
                    })
                .PrimaryKey(t => t.distanciaEntreCepsID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DistanciaEntreCeps");
        }
    }
}
