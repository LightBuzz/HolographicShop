namespace HolographicShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddKeys : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProductInCarts", name: "Cart_Id", newName: "CartId");
            RenameColumn(table: "dbo.ProductInCarts", name: "Product_Id", newName: "ProductId");
            RenameIndex(table: "dbo.ProductInCarts", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameIndex(table: "dbo.ProductInCarts", name: "IX_Cart_Id", newName: "IX_CartId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductInCarts", name: "IX_CartId", newName: "IX_Cart_Id");
            RenameIndex(table: "dbo.ProductInCarts", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameColumn(table: "dbo.ProductInCarts", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.ProductInCarts", name: "CartId", newName: "Cart_Id");
        }
    }
}
