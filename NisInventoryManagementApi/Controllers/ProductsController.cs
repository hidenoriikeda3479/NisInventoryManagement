using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NisInventoryManagementApi.Data;
using NisInventoryManagementApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NisInventoryManagementApi.Controllers
{
    /// <summary>
    /// 商品マスタに関連するAPIコントローラー
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>d
        /// コンストラクタ。データベースコンテキストを注入
        /// </summary>
        /// <param name="context">アプリケーションのデータベースコンテキスト</param>
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 全ての商品を取得
        /// </summary>
        /// <returns>商品のリスト</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMaster>>> GetProducts()
        {
            // データベースからすべての商品を非同期で取得
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// 指定されたIDの商品を取得
        /// </summary>
        /// <param name="id">商品のID</param>
        /// <returns>該当する商品、または404エラー</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductMaster>> GetProduct(int id)
        {
            // 指定されたIDの商品をデータベースから検索
            var product = await _context.Products.FindAsync(id);

            // 商品が見つからない場合は404を返す
            if (product == null)
            {
                return NotFound();
            }

            // 見つかった商品を返す
            return product;
        }

        /// <summary>
        /// 商品名や単価で商品を検索
        /// </summary>
        /// <param name="name">商品名（部分一致）</param>
        /// <param name="price">単価</param>
        /// <returns>該当する商品のリスト</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductMaster>>> SearchProducts(
            [FromQuery] string? name, // null 許容のための ? を使用
            [FromQuery] decimal? price // null 許容のための ? を使用
        )
        {
            // 検索クエリを構築（すべてのProductsからスタート）
            var query = _context.Products.AsQueryable();

            // 商品名でフィルタリング（nameがnullでない場合）
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.ProductName.Contains(name));
            }

            // 単価でフィルタリング（priceがnullでない場合）
            if (price.HasValue)
            {
                query = query.Where(p => p.Price == price.Value);
            }

            // クエリを実行して結果を取得
            var products = await query.ToListAsync();

            // 結果が空の場合は404エラーを返す
            if (products == null || !products.Any())
            {
                return NotFound("該当する商品が見つかりませんでした。");
            }

            // 該当する商品を返す
            return products;
        }

        /// <summary>
        /// 新しい商品を作成
        /// </summary>
        /// <param name="product">新しく作成する商品情報</param>
        /// <returns>作成した商品の詳細とLocationヘッダー</returns>
        [HttpPost]
        public async Task<ActionResult<ProductMaster>> CreateProduct(ProductMaster product)
        {
            // データベースに新しい商品を追加
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // 新しく作成された商品の詳細とLocationヘッダーを返す
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        /// <summary>
        /// 指定されたIDの商品情報を更新
        /// </summary>
        /// <param name="id">更新する商品のID</param>
        /// <param name="product">更新された商品情報</param>
        /// <returns>ステータスコード（更新結果に応じて200や404を返す）</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductMaster product)
        {
            // 更新対象のIDと商品IDが一致しない場合はBadRequestを返す
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            // 商品情報を更新状態に設定
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                // データベースの変更を保存
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // 商品が存在しない場合は404エラーを返す
                if (!_context.Products.Any(e => e.ProductId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // 更新成功時にはNoContentを返す
            return NoContent();
        }

        /// <summary>
        /// 指定されたIDの商品を削除
        /// </summary>
        /// <param name="id">削除する商品のID</param>
        /// <returns>削除の結果を表すステータスコード</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // 指定されたIDの商品を検索
            var product = await _context.Products.FindAsync(id);

            // 商品が存在しない場合は404エラーを返す
            if (product == null)
            {
                return NotFound();
            }

            // 商品を削除し、データベースに変更を保存
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            // 削除成功時にはNoContentを返す
            return NoContent();
        }
    }
}
