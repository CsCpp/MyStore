namespace CrmBl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Base1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checks",
                c => new
                    {
                        CheckId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        SelerId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Seller_SellerId = c.Int(),
                    })
                .PrimaryKey(t => t.CheckId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.Seller_SellerId)
                .Index(t => t.CustomerId)
                .Index(t => t.Seller_SellerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        SellerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SellerId);
            
            CreateTable(
                "dbo.Sells",
                c => new
                    {
                        SellId = c.Int(nullable: false, identity: true),
                        ChekId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Check_CheckId = c.Int(),
                    })
                .PrimaryKey(t => t.SellId)
                .ForeignKey("dbo.Checks", t => t.Check_CheckId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.Check_CheckId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sells", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Sells", "Check_CheckId", "dbo.Checks");
            DropForeignKey("dbo.Checks", "Seller_SellerId", "dbo.Sellers");
            DropForeignKey("dbo.Checks", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Sells", new[] { "Check_CheckId" });
            DropIndex("dbo.Sells", new[] { "ProductId" });
            DropIndex("dbo.Checks", new[] { "Seller_SellerId" });
            DropIndex("dbo.Checks", new[] { "CustomerId" });
            DropTable("dbo.Products");
            DropTable("dbo.Sells");
            DropTable("dbo.Sellers");
            DropTable("dbo.Customers");
            DropTable("dbo.Checks");
        }
    }
}
