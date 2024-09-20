using NisInventoryManagementMvc.Models;
using NisInventoryManagementWeb.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NisInventoryManagementWeb.Services
{
    /// <summary>
    /// WebAPI呼び出し用サービス
    /// </summary>
    public class ArrivalService
    {
        private readonly HttpClient _httpClient;

        public ArrivalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// APIから全入荷情報を取得
        /// </summary>
        /// <returns>入荷のリスト</returns>
        public async Task<IEnumerable<ArrivalViewModel>?> GetArrivalAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ArrivalViewModel>>("https://localhost:7129/api/arrival");
        }

        /// <summary>
        /// 商品名と入荷日を指定して入荷情報を取得
        /// </summary>
        /// <param name="name">商品名</param>
        /// <param name="date">入荷日</param>
        /// <returns>指定された商品の情報</returns>
        public async Task<IEnumerable<ArrivalViewModel>?> GetArrivalByNameDateAsync(string name, DateTime? date)
        {
            // Web APIから指定IDの商品を取得
            return await _httpClient.GetFromJsonAsync<IEnumerable<ArrivalViewModel>>($"https://localhost:7129/api/arrival/search?name={name}&date={date}");
        }

        /// <summary>
        /// 入荷IDで入荷情報を取得
        /// </summary>
        /// <param name="name">商品名</param>
        /// <param name="date">入荷日</param>
        /// <returns>指定された商品の情報</returns>
        public async Task<ArrivalViewModel?> GetArrivalByIdAsync(int id)
        {
            // Web APIから指定IDの商品を取得
            return await _httpClient.GetFromJsonAsync<ArrivalViewModel>($"https://localhost:7129/api/arrival/{id}");
        }

        /// <summary>
        /// 商品IDで商品情報を取得
        /// </summary>
        /// <param name="id">商品ID</param>
        /// <returns>指定された商品の情報</returns>
        public async Task<ProductViewModel?> GetArrivalByProductAsync(int id)
        {
            // Web APIから指定IDの商品を取得
            return await _httpClient.GetFromJsonAsync<ProductViewModel>($"https://localhost:7129/api/products/{id}");
        }

        /// <summary>
        /// 入荷を登録
        /// </summary>
        /// <param name="arrival">入荷登録情報</param>
        /// <returns>HTTPレスポンス</returns>
        public async Task<HttpResponseMessage> CreateArrivalAsync(ArrivalViewModel arrival)
        {
            // Web APIに対して入荷情報を送信し、新規登録
            return await _httpClient.PostAsJsonAsync("https://localhost:7129/api/arrival", arrival);
        }

        /// <summary>
        /// 入荷を登録
        /// </summary>
        /// <param name="quantity">入荷登録情報</param>
        /// <returns>HTTPレスポンス</returns>
        public async Task<HttpResponseMessage> UpdateArrivalAsync(ArrivalViewModel arrival)
        {
            // Web APIに対して入荷情報を送信し、新規登録
            return await _httpClient.PutAsJsonAsync("https://localhost:7129/api/arrival", arrival);
        }

        /// <summary>
        /// 入荷情報を削除
        /// </summary>
        /// <param name="id">入荷ID</param>
        /// <returns>HTTPレスポンス</returns>
        public async Task<HttpResponseMessage> DeleteArrivalAsync(int id)
        {
            // Web APIに対して指定IDの入荷情報を削除
            return await _httpClient.DeleteAsync($"https://localhost:7129/api/arrival/{id}");
        }
    }
}
