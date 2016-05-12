namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cnpjString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "cnpj", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "cnpj", c => c.Int(nullable: false));
        }
    }
}
