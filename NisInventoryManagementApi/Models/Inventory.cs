using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NisInventoryManagementApi.Models
{
    /// <summary>
    /// 在庫情報
    /// </summary>
    [Table("inventory")]
    public class Inventory
    {
        /// <summary>
        /// 在庫ID
        /// </summary>
        [Key]
        [Column("inventory_id")]
        public int InventoryId { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [Required]
        [ForeignKey("ProductMaster")]
        [Column("product_id")]
        public int ProductId { get; set; }

        /// <summary>
        /// 在庫数
        /// </summary>
        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 最終更新日
        /// </summary>
        [Column("last_updated")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public virtual ProductMaster Product { get; set; } = default!;
    }
}
