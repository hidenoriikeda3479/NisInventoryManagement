using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NisInventoryManagementApi.Controllers;
using NisInventoryManagementApi.Data;
using NisInventoryManagementApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NisInventoryManagementApi.Tests
{
    /// <summary>
    /// 商品コントローラーのテストクラス
    /// </summary>
    public class ProductsControllerTests
    {
        /// <summary>
        /// InMemoryデータベースを使用するためのコンテキストを取得
        /// </summary>
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        /// <summary>
        /// GetProductsメソッドのテスト
        /// すべての商品データを正しく取得するか検証
        /// </summary>
        [Fact]
        public async Task GetProducts_ReturnsAllProducts()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);

            // サンプルデータの追加
            context.Products.Add(new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m });
            context.Products.Add(new ProductMaster { ProductId = 2, ProductName = "Smartphone", Price = 799.99m });
            context.SaveChanges();

            // Act: すべての商品を取得
            var result = await controller.GetProducts();

            // Assert: 取得した商品の数が2件であることを検証
            var products = Assert.IsAssignableFrom<IEnumerable<ProductMaster>>(result.Value);
            Assert.Equal(2, products.Count());
        }

        /// <summary>
        /// GetProductメソッドの正常系テスト
        /// 指定されたIDの商品が正しく取得されるか検証
        /// </summary>
        [Fact]
        public async Task GetProduct_ReturnsProductById()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);

            // サンプルデータの追加
            context.Products.Add(new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m });
            context.SaveChanges();

            // Act: 指定されたIDの商品を取得
            var result = await controller.GetProduct(1);

            // Assert: 取得した商品が期待した内容であることを検証
            var product = Assert.IsAssignableFrom<ProductMaster>(result.Value);
            Assert.Equal(1, product.ProductId);
            Assert.Equal("Laptop", product.ProductName);
        }

        /// <summary>
        /// GetProductメソッドのNotFoundテスト
        /// 存在しないIDでNotFoundが返されるか検証
        /// </summary>
        [Fact]
        public async Task GetProduct_ReturnsNotFound_ForInvalidId()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);

            // サンプルデータは追加しない

            // Act: 存在しないIDの商品を取得
            var result = await controller.GetProduct(999); // 存在しないID

            // Assert: NotFoundが返されることを検証
            Assert.IsType<NotFoundResult>(result.Result);
        }

        /// <summary>
        /// SearchProductsメソッドの正常系テスト
        /// 商品名での検索結果が正しいか検証
        /// </summary>
        [Fact]
        public async Task SearchProducts_ReturnsCorrectProductByName()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);

            // サンプルデータの追加
            context.Products.Add(new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m });
            context.Products.Add(new ProductMaster { ProductId = 2, ProductName = "Smartphone", Price = 799.99m });
            context.SaveChanges();

            // Act: 商品名でフィルタリングして商品を取得
            var result = await controller.SearchProducts("Laptop", null);

            // Assert: フィルタリング結果が1件で、商品名が「Laptop」であることを検証
            var products = Assert.IsAssignableFrom<IEnumerable<ProductMaster>>(result.Value);
            Assert.Single(products);
            Assert.Equal("Laptop", products.First().ProductName);
        }

        /// <summary>
        /// SearchProductsメソッドのNotFoundテスト
        /// 該当商品がない場合にNotFoundが返されるか検証
        /// </summary>
        [Fact]
        public async Task SearchProducts_ReturnsNotFound_ForNoMatches()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);

            // サンプルデータの追加
            context.Products.Add(new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m });
            context.SaveChanges();

            // Act: 存在しない商品名で検索
            var result = await controller.SearchProducts("NonExistentProduct", null);

            // Assert: NotFoundが返されることを検証
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        /// <summary>
        /// CreateProductメソッドのテスト
        /// 新規商品が正しく追加されるか検証
        /// </summary>
        [Fact]
        public async Task CreateProduct_AddsNewProduct()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);
            var newProduct = new ProductMaster { ProductId = 3, ProductName = "Tablet", Price = 499.99m };

            // Act: 新規商品を追加
            var result = await controller.CreateProduct(newProduct);

            // Assert: データベースに新規商品が追加されたことを検証
            Assert.IsType<NoContentResult>(result);
            var product = await context.Products.FindAsync(3);
            Assert.NotNull(product);
            Assert.Equal("Tablet", product.ProductName);
        }

        /// <summary>
        /// UpdateProductメソッドの正常系テスト
        /// 商品情報が正しく更新されるか検証
        /// </summary>
        [Fact]
        public async Task UpdateProduct_UpdatesExistingProduct()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);

            // サンプルデータの追加
            context.Products.Add(new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m });
            context.SaveChanges();

            // Act: 商品情報の更新
            var updatedProduct = new ProductMaster { ProductId = 1, ProductName = "Updated Laptop", Price = 1099.99m };
            var result = await controller.UpdateProduct(1, updatedProduct);

            // Assert: 更新が成功したことを検証
            Assert.IsType<NoContentResult>(result);
            var product = await context.Products.FindAsync(1);
            Assert.Equal("Updated Laptop", product?.ProductName);
            Assert.Equal(1099.99m, product?.Price);
        }

        /// <summary>
        /// UpdateProductメソッドのBadRequestテスト
        /// IDが一致しない場合にBadRequestが返されるか検証
        /// </summary>
        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_ForMismatchedId()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);

            // サンプルデータの追加
            context.Products.Add(new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m });
            context.SaveChanges();

            // Act: 一致しないIDで更新
            var updatedProduct = new ProductMaster { ProductId = 2, ProductName = "Updated Laptop", Price = 1099.99m };
            var result = await controller.UpdateProduct(1, updatedProduct);

            // Assert: BadRequestが返されることを検証
            Assert.IsType<BadRequestResult>(result);
        }

        /// <summary>
        /// DeleteProductメソッドのテスト
        /// 指定されたIDの商品が正しく削除されるか検証
        /// </summary>
        [Fact]
        public async Task DeleteProduct_RemovesProduct()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);

            // サンプルデータの追加
            context.Products.Add(new ProductMaster { ProductId = 1, ProductName = "Laptop", Price = 999.99m });
            context.SaveChanges();

            // Act: 指定された商品を削除
            var result = await controller.DeleteProduct(1);

            // Assert: 削除が成功したことを検証
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await context.Products.FindAsync(1));
        }

        /// <summary>
        /// DeleteProductメソッドのNotFoundテスト
        /// 存在しないIDで削除を試みた場合にNotFoundが返されるか検証
        /// </summary>
        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_ForInvalidId()
        {
            // Arrange: InMemoryデータベースとProductsControllerのセットアップ
            var context = GetInMemoryDbContext();
            var controller = new ProductsController(context);

            // サンプルデータは追加しない

            // Act: 存在しないIDの商品を削除
            var result = await controller.DeleteProduct(999); // 存在しないID

            // Assert: NotFoundが返されることを検証
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
