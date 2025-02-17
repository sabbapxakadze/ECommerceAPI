using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLibrary.DTOs.Product
{
    public class ProductUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        //public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}