namespace DemoApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContentObject",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 50),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        AuthToken = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Published = c.DateTime(nullable: false),
                        Copyright = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentObject", t => t.Id)
                .ForeignKey("dbo.Person", t => t.AuthorId)
                .Index(t => t.Id)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ContentObject", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Person", "Id", "dbo.ContentObject");
            DropForeignKey("dbo.Book", "AuthorId", "dbo.Person");
            DropForeignKey("dbo.Book", "Id", "dbo.ContentObject");
            DropIndex("dbo.Person", new[] { "Id" });
            DropIndex("dbo.Book", new[] { "AuthorId" });
            DropIndex("dbo.Book", new[] { "Id" });
            DropTable("dbo.Person");
            DropTable("dbo.Book");
            DropTable("dbo.User");
            DropTable("dbo.ContentObject");
        }
    }
}
