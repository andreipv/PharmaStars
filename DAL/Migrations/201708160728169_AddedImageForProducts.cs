namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageForProducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImgPath", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImgPath");
        }
    }
}
