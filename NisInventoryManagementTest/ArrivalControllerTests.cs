using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NisInventoryManagementApi.Controllers;
using NisInventoryManagementApi.Data;
using NisInventoryManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisInventoryManagementTest
{
    /// <summary>
    /// 入荷コントローラーのテストクラス
    /// </summary>
    public class ArrivalControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        /// <summary>
        /// GetArrivalメソッドのテスト
        /// すべての入荷データを正しく取得するか検証
        /// </summary>
        [Fact]
        public async Task GetArrival_ReturnsAllArrival()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ArrivalController(context);

            // サンプルデータの追加
            context.StockReceipts.Add(new StockReceipt
            {
                ReceiptId = 1,
                ProductId = 1,
                Quantity = 50,
                ReceiptDate = DateTime.Now,
                Product = new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m }
            });
            context.StockReceipts.Add(new StockReceipt
            {
                ReceiptId = 2,
                ProductId = 2,
                Quantity = 30,
                ReceiptDate = DateTime.Now,
                Product = new ProductMaster { ProductId = 2, ProductName = "Smartphone", Price = 799.99m }
            });
            context.SaveChanges();

            // Act: すべての商品を取得
            var result = await controller.GetArrival();

            // Assert: 取得した商品の数が2件であることを検証
            var arrival = Assert.IsAssignableFrom<IEnumerable<StockReceiptResponseModel>>(result.Value);
            Assert.Equal(2, arrival.Count());
        }

        /// <summary>
        /// GetProductメソッドの正常系テスト
        /// 指定されたIDの商品が正しく取得されるか検証
        /// </summary>
        [Fact]
        public async Task GetArrival_ReturnsArrivalById()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ArrivalController(context);

            // サンプルデータの追加
            // サンプルデータの追加
            context.StockReceipts.Add(new StockReceipt
            {
                ReceiptId = 1,
                ProductId = 1,
                Quantity = 50,
                ReceiptDate = DateTime.Now,
                Product = new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m }
            }); context.SaveChanges();

            // Act: 指定されたIDの商品を取得
            var result = await controller.GetArrival(1);

            // Assert: 取得した入荷情報が期待した内容であることを検証
            var arrival = Assert.IsAssignableFrom<StockReceiptResponseModel>(result.Value);
            Assert.Equal(1, arrival.ReceiptId);
            Assert.Equal("Laptop", arrival.ProductName);
            Assert.Equal(50, arrival.Quantity);
            Assert.Equal(DateTime.Now.Date, arrival.ReceiptDate.Date);
        }

        /// <summary>
        /// CreateArrivalメソッドの正常系テスト
        /// 指定されたIDの入荷数と入荷日が正しく取得されるか検証
        /// </summary>
        [Fact]
        public async Task CreateArrivalAddNewArrival()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ArrivalController(context);
            var newArrival = new StockReceipt
            {
                ReceiptId = 1,
                ProductId = 1,
                Quantity = 50,
                ReceiptDate = DateTime.Now,
                Product = new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m }
            };

            var result = await controller.CreateArrival(newArrival);

            // Assert: データベースに入荷情報が追加されたことを検証
            var arrival = await context.StockReceipts.FindAsync(1);
            Assert.NotNull(arrival);
            Assert.Equal("Laptop", arrival.Product!.ProductName);
            Assert.Equal(50, arrival.Quantity);
            Assert.Equal(DateTime.Now.Date, arrival.ReceiptDate.Date);

        }
    }
}
