using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NisInventoryManagementApi.Models
{
    /// <summary>
    /// 商品マスタ
    /// </summary>
    [Table("product_master")]
    public class ProductMaster
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Column("product_name")]
        public string ProductName { get; set; } = default!;

        /// <summary>
        /// 商品説明
        /// </summary>
        [MaxLength(500)]
        [Column("product_description")]
        public string? ProductDescription { get; set; }

        /// <summary>
        /// 単価
        /// </summary>
        [Required]
        [Column("price")]
        public decimal Price { get; set; }
    }
}
