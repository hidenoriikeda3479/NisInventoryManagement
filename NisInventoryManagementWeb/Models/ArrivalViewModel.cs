using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NisInventoryManagementMvc.Models
{
    /// <summary>
    /// 入荷ビュー用のモデル
    /// </summary>
    public class ArrivalViewModel
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
        /// 入荷数
        /// </summary>
        [Required(ErrorMessage = "入荷数を入力してください")]
        [Range(1, 1000, ErrorMessage ="入荷数は1～1,000の範囲で入力してください。")]
        [DisplayName("入荷数")]
        public int? Quantity { get; set; }

        /// <summary>
        /// 入荷日
        /// </summary>
        [Required(ErrorMessage ="日付を設定してください")]
        [DisplayName("入荷日")]
        public DateTime? ReceiptDate { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        [Display(Name = "商品名")]
        public string ProductName { get; set; } = default!;
    }
}
