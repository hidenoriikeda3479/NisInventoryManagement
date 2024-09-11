using Microsoft.AspNetCore.Mvc;
using NisInventoryManagementMvc.Models;
using NisInventoryManagementMvc.Services;
using System.Threading.Tasks;

namespace NisInventoryManagementMvc.Controllers
{
    /// <summary>
    /// 商品情報を操作するコントローラー
    /// </summary>
    public class ProductsController : Controller
    {
        /// <summary>
        /// 商品サービス
        /// </summary>
        private readonly ProductService _productService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="productService">商品サービス</param>
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 商品リストを表示
        /// </summary>
        /// <returns>商品一覧ページ</returns>
        public async Task<IActionResult> Index()
        {
            // サービスから商品一覧を取得
            var products = await _productService.GetProductsAsync();

            // 取得した商品をビューに渡す
            return View(products);
        }

        /// <summary>
        /// 商品詳細を表示
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns>商品詳細ページ</returns>
        public async Task<IActionResult> Details(int id)
        {
            // 指定IDの商品をサービスから取得
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                // 商品が見つからない場合は404エラー
                return NotFound();
            }

            // 商品詳細ページを表示
            return View(product);
        }

        /// <summary>
        /// 新規商品登録フォームの表示
        /// </summary>
        /// <returns>商品登録ページ</returns>
        public IActionResult Create()
        {
            // 商品作成ページを表示
            return View();
        }

        /// <summary>
        /// 新規商品を登録
        /// </summary>
        /// <param name="product">新規商品情報</param>
        /// <returns>商品リストページ</returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            // 入力値の検証
            if (ModelState.IsValid)
            {
                // サービスを使用して商品を作成
                var result = await _productService.CreateProductAsync(product);
                if (result.IsSuccessStatusCode)
                {
                    // 登録成功時には商品一覧ページにリダイレクト
                    return RedirectToAction(nameof(Index));
                }
            }

            // 登録失敗時には同じページを再表示
            return View(product);
        }

        /// <summary>
        /// 商品削除
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns>商品リストページ</returns>
        public async Task<IActionResult> Delete(int id)
        {
            // 指定IDの商品をサービスで削除
            var result = await _productService.DeleteProductAsync(id);
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

