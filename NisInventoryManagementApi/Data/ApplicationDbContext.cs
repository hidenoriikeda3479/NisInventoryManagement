using Microsoft.EntityFrameworkCore;
using NisInventoryManagementApi.Models;

namespace NisInventoryManagementApi.Data
{
    /// <summary>
    /// アプリケーションのデータベースコンテキストを表します。
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 商品マスタテーブルを取得または設定します。
        /// </summary>
        public DbSet<ProductMaster> Products { get; set; }

        /// <summary>
        /// 在庫テーブルを取得または設定します。
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; }

        /// <summary>
        /// 入荷テーブルを取得または設定します。
        /// </summary>
        public DbSet<StockReceipt> StockReceipts { get; set; }

        /// <summary>
        /// 売上テーブルを取得または設定します。
        /// </summary>
        public DbSet<Sales> Sales { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // パラメータレスコンストラクタを追加（設計時に使用されます）
        public ApplicationDbContext() : base()
        {
        }

        /// <summary>
        /// モデルの構築時にカラムやテーブルの設定を指定します。
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</ m>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 商品マスタシードデータ
            var products = new ProductMaster[]
            {
                new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m },
                new ProductMaster { ProductId = 2, ProductName = "Smartphone", Price = 799.99m },
                new ProductMaster { ProductId = 3, ProductName = "Tablet", Price = 499.99m },
                new ProductMaster { ProductId = 4, ProductName = "Monitor", Price = 199.99m },
                new ProductMaster { ProductId = 5, ProductName = "Keyboard", Price = 49.99m },
                new ProductMaster { ProductId = 6, ProductName = "Mouse", Price = 29.99m },
                new ProductMaster { ProductId = 7, ProductName = "Printer", Price = 149.99m },
                new ProductMaster { ProductId = 8, ProductName = "Scanner", Price = 89.99m },
                new ProductMaster { ProductId = 9, ProductName = "Speaker", Price = 69.99m },
                new ProductMaster { ProductId = 10, ProductName = "Headphone", Price = 39.99m },
                new ProductMaster { ProductId = 11, ProductName = "Webcam", Price = 59.99m },
                new ProductMaster { ProductId = 12, ProductName = "Router", Price = 99.99m },
                new ProductMaster { ProductId = 13, ProductName = "Hard Drive", Price = 89.99m },
                new ProductMaster { ProductId = 14, ProductName = "USB Flash Drive", Price = 19.99m },
                new ProductMaster { ProductId = 15, ProductName = "External SSD", Price = 129.99m },
                new ProductMaster { ProductId = 16, ProductName = "Power Bank", Price = 49.99m },
                new ProductMaster { ProductId = 17, ProductName = "Charger", Price = 25.99m },
                new ProductMaster { ProductId = 18, ProductName = "Projector", Price = 299.99m },
                new ProductMaster { ProductId = 19, ProductName = "VR Headset", Price = 399.99m },
                new ProductMaster { ProductId = 20, ProductName = "Smartwatch", Price = 199.99m }
            };
            modelBuilder.Entity<ProductMaster>().HasData(products);

            // 在庫シードデータ
            var inventories = new Inventory[]
            {
                new Inventory { InventoryId = 1, ProductId = 1, Quantity = 100, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 2, ProductId = 2, Quantity = 200, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 3, ProductId = 3, Quantity = 150, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 4, ProductId = 4, Quantity = 300, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 5, ProductId = 5, Quantity = 120, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 6, ProductId = 6, Quantity = 50, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 7, ProductId = 7, Quantity = 80, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 8, ProductId = 8, Quantity = 90, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 9, ProductId = 9, Quantity = 70, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 10, ProductId = 10, Quantity = 110, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 11, ProductId = 11, Quantity = 60, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 12, ProductId = 12, Quantity = 50, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 13, ProductId = 13, Quantity = 80, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 14, ProductId = 14, Quantity = 120, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 15, ProductId = 15, Quantity = 150, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 16, ProductId = 16, Quantity = 130, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 17, ProductId = 17, Quantity = 100, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 18, ProductId = 18, Quantity = 60, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 19, ProductId = 19, Quantity = 90, LastUpdated = DateTime.Now },
                new Inventory { InventoryId = 20, ProductId = 20, Quantity = 50, LastUpdated = DateTime.Now }
            };
            modelBuilder.Entity<Inventory>().HasData(inventories);

            // 入荷シードデータ
            var stockReceipts = new StockReceipt[]
            {
                new StockReceipt { ReceiptId = 1, ProductId = 1, Quantity = 50, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 2, ProductId = 2, Quantity = 70, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 3, ProductId = 3, Quantity = 60, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 4, ProductId = 4, Quantity = 40, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 5, ProductId = 5, Quantity = 55, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 6, ProductId = 6, Quantity = 45, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 7, ProductId = 7, Quantity = 30, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 8, ProductId = 8, Quantity = 60, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 9, ProductId = 9, Quantity = 40, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 10, ProductId = 10, Quantity = 50, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 11, ProductId = 11, Quantity = 45, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 12, ProductId = 12, Quantity = 35, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 13, ProductId = 13, Quantity = 70, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 14, ProductId = 14, Quantity = 90, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 15, ProductId = 15, Quantity = 60, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 16, ProductId = 16, Quantity = 70, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 17, ProductId = 17, Quantity = 45, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 18, ProductId = 18, Quantity = 50, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 19, ProductId = 19, Quantity = 55, ReceiptDate = DateTime.Now },
                new StockReceipt { ReceiptId = 20, ProductId = 20, Quantity = 40, ReceiptDate = DateTime.Now }
            };
            modelBuilder.Entity<StockReceipt>().HasData(stockReceipts);

            // 売上シードデータ
            var sales = new Sales[]
            {
                new Sales { SalesId = 1, ProductId = 1, Quantity = 10, SalesDate = DateTime.Now },
                new Sales { SalesId = 2, ProductId = 2, Quantity = 20, SalesDate = DateTime.Now },
                new Sales { SalesId = 3, ProductId = 3, Quantity = 15, SalesDate = DateTime.Now },
                new Sales { SalesId = 4, ProductId = 4, Quantity = 12, SalesDate = DateTime.Now },
                new Sales { SalesId = 5, ProductId = 5, Quantity = 18, SalesDate = DateTime.Now },
                new Sales { SalesId = 6, ProductId = 6, Quantity = 9, SalesDate = DateTime.Now },
                new Sales { SalesId = 7, ProductId = 7, Quantity = 14, SalesDate = DateTime.Now },
                new Sales { SalesId = 8, ProductId = 8, Quantity = 13, SalesDate = DateTime.Now },
                new Sales { SalesId = 9, ProductId = 9, Quantity = 17, SalesDate = DateTime.Now },
                new Sales { SalesId = 10, ProductId = 10, Quantity = 16, SalesDate = DateTime.Now },
                new Sales { SalesId = 11, ProductId = 11, Quantity = 11, SalesDate = DateTime.Now },
                new Sales { SalesId = 12, ProductId = 12, Quantity = 10, SalesDate = DateTime.Now },
                new Sales { SalesId = 13, ProductId = 13, Quantity = 14, SalesDate = DateTime.Now },
                new Sales { SalesId = 14, ProductId = 14, Quantity = 19, SalesDate = DateTime.Now },
                new Sales { SalesId = 15, ProductId = 15, Quantity = 12, SalesDate = DateTime.Now },
                new Sales { SalesId = 16, ProductId = 16, Quantity = 20, SalesDate = DateTime.Now },
                new Sales { SalesId = 17, ProductId = 17, Quantity = 9, SalesDate = DateTime.Now },
                new Sales { SalesId = 18, ProductId = 18, Quantity = 10, SalesDate = DateTime.Now },
                new Sales { SalesId = 19, ProductId = 19, Quantity = 8, SalesDate = DateTime.Now },
                new Sales { SalesId = 20, ProductId = 20, Quantity = 12, SalesDate = DateTime.Now }
            };
            modelBuilder.Entity<Sales>().HasData(sales);
        }
    }
}