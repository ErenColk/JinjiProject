using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.Dtos.Products
{
    public class UpdateProductDiscountDto
    {
        public int Id { get; set; }
        public decimal  OldPrice { get; set; }
        public bool? IsDiscount { get; set; }
    }
}
