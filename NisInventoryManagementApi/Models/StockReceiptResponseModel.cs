using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NisInventoryManagementApi.Models
{
    /// <summary>
    /// 入荷レスポンスモデル
    /// </summary>
    public class StockReceiptResponseModel
    {
        /// <summary>
        /// 入荷ID
        /// </summary>
        public int ReceiptId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string ProductName { get; set; } = default!;

        /// <summary>
        /// 入荷数
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 入荷日
        /// </summary>
        public DateTime ReceiptDate { get; set; }

    }
}
