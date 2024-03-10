using JinjiProject.Core.Entities.Abstract;
using JinjiProject.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Core.Entities.Concrete
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string ImagePath { get; set; }
        public int? Capacity { get; set; }
        public float? Height { get; set; }
        public float? Width { get; set; }
        public float? Length { get; set; }

        [NotMapped]
        public IFormFile UploadPath { get; set; }
        public Size? Size { get; set; }

        public int MaterialId { get; set; }
        public int GenreId{ get; set; }
        public int BrandId { get; set; }
        public virtual Material Material { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
