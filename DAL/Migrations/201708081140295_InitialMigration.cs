namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProdCateg_Assoc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ID_CATEG = c.Int(nullable: false),
                        ID_PROD = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ID_PROD)
                .ForeignKey("dbo.Categories", t => t.ID_CATEG)
                .Index(t => t.ID_CATEG)
                .Index(t => t.ID_PROD);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255, unicode: false),
                        ID_MRF = c.Int(),
                        Quantity = c.Int(),
                        Description = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Manufacturers", t => t.ID_MRF)
                .Index(t => t.ID_MRF);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255, unicode: false),
                        Adress = c.String(maxLength: 255, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProdCateg_Assoc", "ID_CATEG", "dbo.Categories");
            DropForeignKey("dbo.ProdCateg_Assoc", "ID_PROD", "dbo.Products");
            DropForeignKey("dbo.Products", "ID_MRF", "dbo.Manufacturers");
            DropIndex("dbo.Products", new[] { "ID_MRF" });
            DropIndex("dbo.ProdCateg_Assoc", new[] { "ID_PROD" });
            DropIndex("dbo.ProdCateg_Assoc", new[] { "ID_CATEG" });
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Products");
            DropTable("dbo.ProdCateg_Assoc");
            DropTable("dbo.Categories");
        }
    }
}
