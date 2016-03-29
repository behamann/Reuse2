namespace Reuse2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wut : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Anuncios", name: "Interesses_anuncioID", newName: "Interesse_anuncioID");
            RenameColumn(table: "dbo.Anuncios", name: "Interesses_userID", newName: "Interesse_userID");
            RenameColumn(table: "dbo.AspNetUsers", name: "Interesses_anuncioID", newName: "Interesse_anuncioID");
            RenameColumn(table: "dbo.AspNetUsers", name: "Interesses_userID", newName: "Interesse_userID");
            RenameIndex(table: "dbo.Anuncios", name: "IX_Interesses_anuncioID_Interesses_userID", newName: "IX_Interesse_anuncioID_Interesse_userID");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Interesses_anuncioID_Interesses_userID", newName: "IX_Interesse_anuncioID_Interesse_userID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Interesse_anuncioID_Interesse_userID", newName: "IX_Interesses_anuncioID_Interesses_userID");
            RenameIndex(table: "dbo.Anuncios", name: "IX_Interesse_anuncioID_Interesse_userID", newName: "IX_Interesses_anuncioID_Interesses_userID");
            RenameColumn(table: "dbo.AspNetUsers", name: "Interesse_userID", newName: "Interesses_userID");
            RenameColumn(table: "dbo.AspNetUsers", name: "Interesse_anuncioID", newName: "Interesses_anuncioID");
            RenameColumn(table: "dbo.Anuncios", name: "Interesse_userID", newName: "Interesses_userID");
            RenameColumn(table: "dbo.Anuncios", name: "Interesse_anuncioID", newName: "Interesses_anuncioID");
        }
    }
}
