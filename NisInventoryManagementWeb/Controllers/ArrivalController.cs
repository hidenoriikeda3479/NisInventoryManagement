using Microsoft.AspNetCore.Mvc;
using NisInventoryManagementMvc.Models;
using NisInventoryManagementMvc.Services;
using NisInventoryManagementWeb.Services;
using System.Threading.Tasks;

namespace NisInventoryManagementWeb.Controllers
{
    /// <summary>
    /// 入荷情報を操作するコントローラー
    /// </summary>
    public class ArrivalController : Controller
    {

        /// <summary>
        /// 商品サービス
        /// </summary>
        private readonly ArrivalService _arrivalService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="arrivalService">入荷サービス</param>
        public ArrivalController(ArrivalService arrivalService)
        {
            _arrivalService = arrivalService;
        }

        /// <summary>
        /// 入荷リストを表示
        /// </summary>
        /// <returns>入荷一覧ページ</returns>
        public async Task<ActionResult> Index()
        {
            // サービスから入荷一覧を取得
            var arrival = await _arrivalService.GetArrivalAsync();

            // 取得した商品をビューに渡す
            return View(arrival);
        }

        /// <summary>
        /// 入荷情報検索
        /// </summary>
        /// <param name="productName">商品名</param>
        /// <param name="receiptDate">入荷日</param>
        /// <returns>入荷一覧ページ</returns>
        public async Task<ActionResult> Search(string productName, DateTime? receiptDate)
        {
            // 商品名と入荷日に当てはまる入荷情報を検索
            var arrival = await _arrivalService.GetArrivalByNameDateAsync(productName, receiptDate);

            // 取得した入荷情報をビューに渡す
            return View("Index",arrival);
        }

        /// <summary>
        /// 入荷情報更新
        /// </summary>
        /// <param name="receiptId">入荷ID</param>
        /// <param name="quantity">入荷数</param>
        /// <param name="receiptDate">入荷日</param>
        /// <returns>入荷情報更新</returns>
        public async Task<ActionResult> Update(int receiptId,int quantity, DateTime? receiptDate)
        {
            // 指定の入荷IDの情報を取得
            var arrival = await _arrivalService.GetArrivalByReceiptIdAsync(receiptId);

            // 変更後の入荷数と入荷日を代入
            arrival!.Quantity = quantity;
            arrival.ReceiptDate = receiptDate;

            // 変更内容を更新する
            var updateArrival = await _arrivalService.UpdateArrivalAsync(arrival);

            if (updateArrival.IsSuccessStatusCode)
            {
                // 更新成功時には入荷一覧ページにリダイレクト
                return RedirectToAction(nameof(Index));
            }

            // 更新失敗時更新画面に戻る
            return View(updateArrival);
        }

        /// <summary>
        /// 入荷詳細ページ
        /// </summary>
        /// <param name="receiptId">入荷ID</param>
        /// <returns>入荷情報編集ページ</returns>
        public　async Task<ActionResult> Details(int receiptId)
        {

            var arrival = await _arrivalService.GetArrivalByReceiptIdAsync(receiptId);

            return View(arrival);
        }

        /// <summary>
        /// 入荷登録
        /// </summary>
        /// <param name="arrival">入荷データ</param>
        /// <returns>成功時は入荷一覧画面、失敗時は登録画面</returns>
        [HttpPost]
        public async Task<IActionResult> Create(ArrivalViewModel arrival)
        {
            // 入力値の検証
            if (ModelState.IsValid)
            {
                // サービスを使用して入荷情報を作成
                var result = await _arrivalService.CreateArrivalAsync(arrival);
                if (result.IsSuccessStatusCode)
                {
                    // 登録成功時には入荷一覧ページにリダイレクト
                    return RedirectToAction("Index", "Products");
                }
            }

            // 登録失敗時には同じページを再表示
            return View(arrival);
        }

        /// <summary>
        /// 入荷登録フォームの表示
        /// </summary>
        /// <returns>入荷登録ページ</returns>
        public async Task<IActionResult> Create(int productId)
        {
            // 受取ったIDで商品情報取得
            var product = await _arrivalService.GetArrivalByProductIdAsync(productId);

            // 商品IDと商品名を格納
            var arrival = new ArrivalViewModel
            {
                ProductId = product!.ProductId,
                ProductName = product!.ProductName,
            };

            // 入荷登録ページを表示
            return View(arrival);
        }

        /// <summary>
        /// 入荷情報削除
        /// </summary>
        /// <param name="receiptId">商品ID</param>
        /// <returns>入荷情報一覧ページ</returns>
        public async Task<IActionResult> Delete(int receiptId)
        {
            // 指定IDの商品をサービスで削除
            var result = await _arrivalService.DeleteArrivalAsync(receiptId);
            if (result.IsSuccessStatusCode)
            {
                // 削除成功時には商品一覧ページにリダイレクト
                return RedirectToAction(nameof(Index));
            }

            // 削除失敗時には404エラー
            return NotFound();
        }
    }
}
