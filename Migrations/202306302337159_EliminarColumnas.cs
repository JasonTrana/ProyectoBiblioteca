namespace BibliotecaVirtual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarColumnas : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Author", "LastName");
            DropColumn("dbo.Author", "RegisterDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Author", "RegisterDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Author", "LastName", c => c.String());
        }
    }
}
