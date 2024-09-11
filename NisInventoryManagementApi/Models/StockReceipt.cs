using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NisInventoryManagementApi.Models
{
    /// <summary>
    /// 入荷情報
    /// </summary>
    [Table("stock_receipt")]
    public class StockReceipt
    {
        /// <summary>
        /// 入荷ID
        /// </summary>
        [Key]
        [Column("receipt_id")]
        public int ReceiptId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        [ForeignKey("ProductMaster")]
        [Column("product_id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 入荷数
        /// </summary>
        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 入荷日
        /// </summary>
        [Required]
        [Column("receipt_date")]
        public DateTime ReceiptDate { get; set; }

        public virtual ProductMaster Product { get; set; } = default!;
    }
}
