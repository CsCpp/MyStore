namespace CrmBl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Checks", "Seller_SellerId", "dbo.Sellers");
            DropForeignKey("dbo.Sells", "Check_CheckId", "dbo.Checks");
            DropIndex("dbo.Checks", new[] { "Seller_SellerId" });
            DropIndex("dbo.Sells", new[] { "Check_CheckId" });
            RenameColumn(table: "dbo.Checks", name: "Seller_SellerId", newName: "SellerId");
            RenameColumn(table: "dbo.Sells", name: "Check_CheckId", newName: "CheckId");
            AddColumn("dbo.Checks", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Checks", "SellerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sells", "CheckId", c => c.Int(nullable: false));
            CreateIndex("dbo.Checks", "SellerId");
            CreateIndex("dbo.Sells", "CheckId");
            AddForeignKey("dbo.Checks", "SellerId", "dbo.Sellers", "SellerId", cascadeDelete: true);
            AddForeignKey("dbo.Sells", "CheckId", "dbo.Checks", "CheckId", cascadeDelete: true);
            DropColumn("dbo.Checks", "SelerId");
            DropColumn("dbo.Sells", "ChekId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sells", "ChekId", c => c.Int(nullable: false));
            AddColumn("dbo.Checks", "SelerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Sells", "CheckId", "dbo.Checks");
            DropForeignKey("dbo.Checks", "SellerId", "dbo.Sellers");
            DropIndex("dbo.Sells", new[] { "CheckId" });
            DropIndex("dbo.Checks", new[] { "SellerId" });
            AlterColumn("dbo.Sells", "CheckId", c => c.Int());
            AlterColumn("dbo.Checks", "SellerId", c => c.Int());
            DropColumn("dbo.Checks", "Price");
            RenameColumn(table: "dbo.Sells", name: "CheckId", newName: "Check_CheckId");
            RenameColumn(table: "dbo.Checks", name: "SellerId", newName: "Seller_SellerId");
            CreateIndex("dbo.Sells", "Check_CheckId");
            CreateIndex("dbo.Checks", "Seller_SellerId");
            AddForeignKey("dbo.Sells", "Check_CheckId", "dbo.Checks", "CheckId");
            AddForeignKey("dbo.Checks", "Seller_SellerId", "dbo.Sellers", "SellerId");
        }
    }
}
