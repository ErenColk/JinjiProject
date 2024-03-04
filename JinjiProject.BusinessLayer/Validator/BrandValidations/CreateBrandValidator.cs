using FluentValidation;
using JinjiProject.Dtos.Brands;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Validator.BrandValidations
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandValidator()
        {
            RuleFor(brand => brand.Name).NotEmpty().WithMessage("Marka adı boş geçilemez.").WithErrorCode("1");
            RuleFor(brand => brand.Name).MinimumLength(2).WithMessage("Marka adı en az 2 karakter içermelidir.").WithErrorCode("1");
            RuleFor(brand => brand.Name).Must(IsNumber).WithMessage("Marka adı sadece sayı içermemelidir.").WithErrorCode("1");


            RuleFor(brand => brand.Description).NotEmpty().WithMessage("Marka açıklaması boş geçilemez.").WithErrorCode("2");
            RuleFor(brand => brand.Description).MinimumLength(3).WithMessage("Marka açıklaması en az 3 karakter içermelidir.").WithErrorCode("2");
            RuleFor(brand => brand.Description).Must(IsNumber).WithMessage("Marka açıklaması sadece sayı içermemelidir.").WithErrorCode("2");

        }

        private static bool IsNumber(string description)
        {

            if (description == null)
            {
                return true;
            }
            foreach (var item in description)
            {
                if (!char.IsDigit(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
