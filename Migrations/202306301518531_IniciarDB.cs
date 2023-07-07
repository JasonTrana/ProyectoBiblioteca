namespace BibliotecaVirtual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IniciarDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id_author = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastName = c.String(),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id_author);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id_book = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Author_id = c.Int(nullable: false),
                        Editorial = c.String(),
                        Type = c.Int(nullable: false),
                        QuantityAvailable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_book)
                .ForeignKey("dbo.Author", t => t.Author_id, cascadeDelete: true)
                .Index(t => t.Author_id);
            
            CreateTable(
                "dbo.Loan",
                c => new
                    {
                        Id_loan = c.Int(nullable: false, identity: true),
                        Book_id = c.Int(nullable: false),
                        User_id = c.Int(nullable: false),
                        LoanDate = c.DateTime(nullable: false),
                        ExpectedReturnDate = c.DateTime(nullable: false),
                        ActualReturnDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id_loan)
                .ForeignKey("dbo.Book", t => t.Book_id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_id, cascadeDelete: true)
                .Index(t => t.Book_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id_user = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LastName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Roles = c.Int(nullable: false), 
                    })
                .PrimaryKey(t => t.Id_user);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loan", "User_id", "dbo.User");
            DropForeignKey("dbo.Loan", "Book_id", "dbo.Book");
            DropForeignKey("dbo.Book", "Author_id", "dbo.Author");
            DropIndex("dbo.Loan", new[] { "User_id" });
            DropIndex("dbo.Loan", new[] { "Book_id" });
            DropIndex("dbo.Book", new[] { "Author_id" });
            DropTable("dbo.User");
            DropTable("dbo.Loan");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
