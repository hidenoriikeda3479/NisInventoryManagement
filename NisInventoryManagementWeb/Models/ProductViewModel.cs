﻿using System.ComponentModel.DataAnnotations;

namespace NisInventoryManagementMvc.Models
{
    /// <summary>
    /// 商品のビュー用モデル
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        [Required(ErrorMessage = "商品名を入力してください。")]
        [StringLength(100, ErrorMessage = "商品名は100文字以内で入力してください。")]
        public string ProductName { get; set; } = default!;

        /// <summary>
        /// 価格
        /// </summary>
        [Required(ErrorMessage = "価格を入力してください。")]
        [Range(0.01, 10000.00, ErrorMessage = "価格は0.01から10,000の範囲で入力してください。")]
        public decimal? Price { get; set; }
    }
}