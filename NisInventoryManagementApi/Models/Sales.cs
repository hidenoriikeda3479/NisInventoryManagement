using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NisInventoryManagementApi.Models
{
    /// <summary>
    /// 売上情報
    /// </summary>
    [Table("sales")]
    public class Sales
    {
        /// <summary>
        /// 売上ID
        /// </summary>
        [Key]
        [Column("sales_id")]
        public int SalesId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        [ForeignKey("ProductMaster")]
        [Column("product_id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 売上数
        /// </summary>
        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 売上日
        /// </summary>
        [Required]
        [Column("sales_date")]
        public DateTime SalesDate { get; set; }

        public virtual ProductMaster Product { get; set; } = default!;
    }
}
