using NisInventoryManagementMvc.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NisInventoryManagementMvc.Services
{
    /// <summary>
    /// WebAPI呼び出し用サービス
    /// </summary>
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// APIから全商品を取得
        /// </summary>
        /// <returns>商品のリスト</returns>
        public async Task<IEnumerable<ProductViewModel>?> GetProductsAsync()
        {
            // Web APIから商品リストを取得
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>("https://localhost:7129/api/products");
        }

        /// <summary>
        /// 商品IDを指定して商品を取得
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns>指定された商品の情報</returns>
        public async Task<ProductViewModel?> GetProductByIdAsync(int id)
        {
            // Web APIから指定IDの商品を取得
            return await _httpClient.GetFromJsonAsync<ProductViewModel>($"https://localhost:7129/api/products/{id}");
        }

        /// <summary>
        /// 新規商品を登録
        /// </summary>
        /// <param name="product">新規商品情報</param>
        /// <returns>HTTPレスポンス</returns>
        public async Task<HttpResponseMessage> CreateProductAsync(ProductViewModel product)
        {
            // Web APIに対して商品情報を送信し、新規登録
            return await _httpClient.PostAsJsonAsync("https://localhost:7129/api/products", product);
        }

        /// <summary>
        /// 商品名とIDで商品を検索
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <param name="productName">商品名</param>
        /// <returns>HTTPレスポンス</returns>
        public async Task<IEnumerable<ProductViewModel>?> SearchProductsAsync(int? id, string? productName)
        {
            // クエリパラメータ付きでAPIにリクエストを送信
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>($"https://localhost:7129/api/products?productName={productName}&id={id}");
        }

        /// <summary>
        /// 商品を削除
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns>HTTPレスポンス</returns>
        public async Task<HttpResponseMessage> DeleteProductAsync(int id)
        {
            // Web APIに対して指定IDの商品を削除
            return await _httpClient.DeleteAsync($"https://localhost:7129/api/products/{id}");
        }
    }
}
