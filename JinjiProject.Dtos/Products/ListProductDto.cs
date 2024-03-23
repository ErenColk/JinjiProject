using JinjiProject.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Products
{
    public class ListProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public bool? IsDiscount { get; set; }
        public int Stock { get; set; }
        public string ImagePath { get; set; }
        public string? ImagePath2 { get; set; }
        public string? ImagePath3 { get; set; }
        public int? Capacity { get; set; }
        public Size? Size { get; set; }
        public string GenreName { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string StatusName { get; set; }
        public int MaterialId { get; set; }
        public int GenreId { get; set; }

    }
}
