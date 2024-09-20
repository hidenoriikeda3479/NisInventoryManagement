using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NisInventoryManagementMvc.Models
{
    /// <summary>
    /// 商品名を取得するためのビューモデル
    /// </summary>
    public class ProductNameViewModel
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Display(Name = "商品ID")]
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        [Display(Name = "商品名")]
        [Required(ErrorMessage = "商品名を入力してください。")]
        [StringLength(100, ErrorMessage = "商品名は100文字以内で入力してください。")]
        public string ProductName { get; set; } = default!;
    }
}
