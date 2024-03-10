using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Core.Entities.Concrete
{
    public class Genre : BaseEntity
    {
        public Genre()
        {
            Products = new List<Product>(); 
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsOnHomePage { get; set; }
        public int? Order { get; set; }
        [NotMapped]
        public IFormFile UploadPath{ get; set; }
        public string ImagePath { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
