using JinjiProject.Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string SizeId { get; set; }
        public string ImagePath { get; set; }
        public IFormFile UploadPath { get; set; }
        public int? Capacity { get; set; }
        public  string MaterialId { get; set; }
        public  string CategoryId{ get; set; }
        public  string BrandId { get; set; }
    }
}
