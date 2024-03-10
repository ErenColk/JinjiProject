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
        public int Price { get; set; }
        public int Stock { get; set; }
        public string ImagePath { get; set; }
        public int? Capacity { get; set; }
        public string Size { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Status Status { get; set; }
    }
}
