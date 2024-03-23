using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Size = JinjiProject.Core.Enums.Size;

namespace JinjiProject.Dtos.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal? Price { get; set; }
        public int Stock { get; set; }
        public float? Height { get; set; }
        public float? Width { get; set; }
        public float? Length { get; set; }
        public Size? Size{ get; set; }
        public string ImagePath { get; set; }
        public string? ImagePath2 { get; set; }
        public string? ImagePath3 { get; set; }
        public IFormFile UploadPath { get; set; }
        public IFormFile? UploadPath2 { get; set; }
        public IFormFile? UploadPath3 { get; set; }
        public int? Capacity { get; set; }
        public  string MaterialId { get; set; }
        public  string CategoryId{ get; set; }
        public  string BrandId { get; set; }
        public  string GenreId { get; set; }
    }
}
