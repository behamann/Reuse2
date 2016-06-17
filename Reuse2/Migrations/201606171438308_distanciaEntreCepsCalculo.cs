namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class distanciaEntreCepsCalculo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DistanciaEntreCeps", "distanciaCalc", c => c.Double(nullable: false));
            AddColumn("dbo.DistanciaEntreCeps", "duracaoCalc", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DistanciaEntreCeps", "duracaoCalc");
            DropColumn("dbo.DistanciaEntreCeps", "distanciaCalc");
        }
    }
}
