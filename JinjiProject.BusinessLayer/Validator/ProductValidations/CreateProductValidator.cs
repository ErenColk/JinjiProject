using FluentValidation;
using JinjiProject.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Validator.ProductValidations
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {

        public CreateProductValidator()
        {
            
        }
    }
}
