namespace BuyalotWebShoppingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuyalotDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Address1 = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PostalCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AddressID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        customerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(),
                        State = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.customerID);
            
            CreateTable(
                "dbo.Billing",
                c => new
                    {
                        BillingID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        CardNumber = c.Int(nullable: false),
                        CardType = c.String(nullable: false),
                        CVV = c.Int(nullable: false),
                        ExpDate = c.DateTime(nullable: false),
                        CardHolderName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BillingID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        ShippingDate = c.DateTime(nullable: false),
                        ShippingAddress = c.String(),
                        Status = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        orderDetailsID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        QuantityOrdered = c.Int(nullable: false),
                        PriceEach = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.orderDetailsID)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        ProductDescription = c.String(nullable: false),
                        ProdCategoryID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Vendor = c.String(nullable: false),
                        QuantityInStock = c.Int(nullable: false),
                        ProductImage = c.Binary(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.ProductCategory", t => t.ProdCategoryID, cascadeDelete: true)
                .Index(t => t.ProdCategoryID);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        RecordId = c.Int(nullable: false, identity: true),
                        CartId = c.String(),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RecordId)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        ProdCategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProdCategoryID);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        PaymentDate = c.DateTime(nullable: false),
                        CustomerID = c.Int(nullable: false),
                        OrderID = c.Int(nullable: false),
                        PaymentType = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Customer", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        adminID = c.Int(nullable: false, identity: true),
                        adminName = c.String(nullable: false),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        confirmPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.adminID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        roleId = c.Int(nullable: false, identity: true),
                        roleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.roleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 12),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.userId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Payment", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Product", "ProdCategoryID", "dbo.ProductCategory");
            DropForeignKey("dbo.OrderDetail", "ProductID", "dbo.Product");
            DropForeignKey("dbo.Cart", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "OrderID", "dbo.Order");
            DropForeignKey("dbo.Order", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Billing", "CustomerID", "dbo.Customer");
            DropForeignKey("dbo.Address", "CustomerID", "dbo.Customer");
            DropIndex("dbo.Payment", new[] { "OrderID" });
            DropIndex("dbo.Payment", new[] { "CustomerID" });
            DropIndex("dbo.Cart", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "ProdCategoryID" });
            DropIndex("dbo.OrderDetail", new[] { "ProductID" });
            DropIndex("dbo.OrderDetail", new[] { "OrderID" });
            DropIndex("dbo.Order", new[] { "CustomerID" });
            DropIndex("dbo.Billing", new[] { "CustomerID" });
            DropIndex("dbo.Address", new[] { "CustomerID" });
            DropTable("dbo.User");
            DropTable("dbo.Role");
            DropTable("dbo.Admin");
            DropTable("dbo.Payment");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Cart");
            DropTable("dbo.Product");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.Billing");
            DropTable("dbo.Customer");
            DropTable("dbo.Address");
        }
    }
}
