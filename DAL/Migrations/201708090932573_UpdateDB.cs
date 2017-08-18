namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProdCateg_Assoc", "ID_PROD", "dbo.Products");
            DropForeignKey("dbo.ProdCateg_Assoc", "ID_CATEG", "dbo.Categories");
            DropIndex("dbo.ProdCateg_Assoc", new[] { "ID_CATEG" });
            DropIndex("dbo.ProdCateg_Assoc", new[] { "ID_PROD" });
            CreateTable(
                "dbo.Prod_Categ_Assoc",
                c => new
                    {
                        ID_CATEG = c.Int(nullable: false),
                        ID_PROD = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID_CATEG, t.ID_PROD })
                .ForeignKey("dbo.Products", t => t.ID_CATEG, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.ID_PROD, cascadeDelete: true)
                .Index(t => t.ID_CATEG)
                .Index(t => t.ID_PROD);
            
            DropTable("dbo.ProdCateg_Assoc");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProdCateg_Assoc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ID_CATEG = c.Int(nullable: false),
                        ID_PROD = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Prod_Categ_Assoc", "ID_PROD", "dbo.Categories");
            DropForeignKey("dbo.Prod_Categ_Assoc", "ID_CATEG", "dbo.Products");
            DropIndex("dbo.Prod_Categ_Assoc", new[] { "ID_PROD" });
            DropIndex("dbo.Prod_Categ_Assoc", new[] { "ID_CATEG" });
            DropTable("dbo.Prod_Categ_Assoc");
            CreateIndex("dbo.ProdCateg_Assoc", "ID_PROD");
            CreateIndex("dbo.ProdCateg_Assoc", "ID_CATEG");
            AddForeignKey("dbo.ProdCateg_Assoc", "ID_CATEG", "dbo.Categories", "ID");
            AddForeignKey("dbo.ProdCateg_Assoc", "ID_PROD", "dbo.Products", "ID");
        }
    }
}
