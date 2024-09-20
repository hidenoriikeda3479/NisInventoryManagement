using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NisInventoryManagementApi.Data;
using NisInventoryManagementApi.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NisInventoryManagementApi.Controllers
{
    /// <summary>
    /// 入荷マスタに関連するAPIコントローラー
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>d
        /// コンストラクタ。データベースコンテキストを注入
        /// </summary>
        /// <param name="context">アプリケーションのデータベースコンテキスト</param>
        public ArrivalController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 全ての入荷を取得
        /// </summary>
        /// <returns>入荷のリスト</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockReceiptResponseModel>>> GetArrival()
        {
            // データベースからすべての入荷情報を非同期で取得
            return await _context.StockReceipts.Include(n => n.Product).Select(n => new StockReceiptResponseModel()
            {
                ReceiptId = n.ReceiptId,
                ProductId = n.ProductId,
                ProductName = n.Product!.ProductName,
                Quantity= n.Quantity,
                ReceiptDate = n.ReceiptDate
            }).ToListAsync();
        }

        /// <summary>
        /// 商品名や入荷日で入荷情報を検索
        /// </summary>
        /// <param name="name">商品名（部分一致）</param>
        /// <param name="date">入荷日</param>
        /// <returns>該当する入荷のリスト</returns>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<StockReceiptResponseModel>>> Search(
            [FromQuery] string? name, // null 許容のための ? を使用
            [FromQuery] DateTime? date // null 許容のための ? を使用
        )
        {
            // 検索クエリを構築（すべてのStockReceiptsからスタート）
            var query = _context.StockReceipts.Include(n => n.Product).Select(n => new StockReceiptResponseModel()
            {
                ReceiptId = n.ReceiptId,
                ProductId = n.ProductId,
                ProductName = n.Product!.ProductName,
                Quantity = n.Quantity,
                ReceiptDate = n.ReceiptDate
            }).AsQueryable();

            // 商品名でフィルタリング（nameがnullでない場合）
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.ProductName.Contains(name));
            }

            // 入荷日でフィルタリング（dateがnullでない場合）
            if (date.HasValue)
            {
                query = query.Where(p => p.ReceiptDate.Date == date);
            }

            // クエリを実行して結果を取得
            var arrival = await query.ToListAsync();

            // 結果が空の場合は404エラーを返す
            if (arrival == null || !arrival.Any())
            {
                return NotFound("該当する入荷情報が見つかりませんでした。");
            }

            // 該当する入荷情報を返す
            return arrival;
        }

        /// <summary>
        /// 指定されたIDの入荷情報を取得
        /// </summary>
        /// <param name="id">入荷のID</param>
        /// <returns>該当する入荷情報、または404エラー</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StockReceiptResponseModel>?> GetArrival(int id)
        {
            // 指定されたIDの入荷情報をデータベースから検索
            var arrival = await _context.StockReceipts.Include(n => n.Product).FirstOrDefaultAsync(s => s.ReceiptId == id);

            // 入荷情報が見つからない場合は404を返す
            if (arrival == null)
            {
                return NotFound();
            }

            // StockReceiptをカスタムレスポンスモデルに変換して返す
            var responseModel = new StockReceiptResponseModel
            {
                ReceiptId = arrival.ReceiptId,
                ProductId = arrival.Product!.ProductId,
                ProductName = arrival.Product!.ProductName,
                Quantity = arrival.Quantity,
                ReceiptDate = arrival.ReceiptDate
            };

            // 見つかった入荷情報を返す
            return responseModel;
        }

        /// <summary>
        /// 新しい入荷情報を作成
        /// </summary>
        /// <param name="arrival">新しく作成する入荷情報</param>
        /// <returns>作成した入荷情報の詳細とLocationヘッダー</returns>
        [HttpPost]
        public async Task<ActionResult<StockReceipt>> CreateArrival(StockReceipt arrival)
        {
            // データベースに新しい入荷情報を追加
            _context.StockReceipts.Add(arrival);
            await _context.SaveChangesAsync();

            // 新しく作成された入荷情報の詳細とLocationヘッダーを返す
            return CreatedAtAction(nameof(GetArrival), new { id = arrival.ProductId }, arrival);
        }

        /// <summary>
        /// 入荷情報の更新
        /// </summary>
        /// <param name="arrival">更新する入荷情報</param>
        /// <returns>作成した入荷の詳細とLocationヘッダー</returns>
        [HttpPut]
        public async Task<ActionResult<IEnumerable<StockReceiptResponseModel>>> UpdateArrival(StockReceipt arrival)
        {
            // 指定されたIDの入荷情報をデータベースから検索
            var before = await _context.StockReceipts.Include(n => n.Product).FirstOrDefaultAsync(s => s.ReceiptId == arrival.ReceiptId);

            // 商品が見つからない場合は404を返す
            if (arrival == null)
            {
                return NotFound();
            }

            // 入荷数と入荷日を更新
            before!.Quantity = arrival.Quantity;
            before.ReceiptDate = arrival.ReceiptDate;

            // 更新したデータをコミット
            await _context.SaveChangesAsync();
            
            var aaa = await _context.StockReceipts.Include(n => n.Product).Select(n => new StockReceiptResponseModel()
            {
                ReceiptId = n.ReceiptId,
                ProductId = n.ProductId,
                ProductName = n.Product!.ProductName,
                Quantity = n.Quantity,
                ReceiptDate = n.ReceiptDate
            }).ToListAsync();

            // データベースからすべての入荷情報を非同期で取得
            return aaa;
        }

        /// <summary>
        /// 指定されたIDの入荷情報を削除
        /// </summary>
        /// <param name="id">削除する入荷のID</param>
        /// <returns>削除の結果を表すステータスコード</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArrival(int id)
        {
            // 指定されたIDの入荷情報を検索
            var arrival = await _context.StockReceipts.FindAsync(id);

            // 入荷情報が存在しない場合は404エラーを返す
            if (arrival == null)
            {
                return NotFound();
            }

            // 入荷情報を削除し、データベースに変更を保存
            _context.StockReceipts.Remove(arrival);
            await _context.SaveChangesAsync();

            // 削除成功時にはNoContentを返す
            return NoContent();
        }
    }

}
