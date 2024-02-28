using FluentValidation;
using JinjiProject.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Validator.CategoryValidations
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {

        public CreateCategoryValidator()
        {
            RuleFor(category => category.Name).NotEmpty().NotNull().WithMessage("Kategori alanı boş geçilemez.").WithErrorCode("1");
            RuleFor(category => category.Name).MinimumLength(2).NotNull().WithMessage("Kategori adı en az 2 karakter içermelidir.").WithErrorCode("1");
            RuleFor(category => category.Name).Must(IsNumber).NotNull().WithMessage("Kategori adı sadece sayı içermemelidir.").WithErrorCode("1");
            RuleFor(category => category.Description).NotEmpty().NotNull().WithMessage("Kategori açıklama alanı boş geçilemez.").WithErrorCode("2");
            RuleFor(category => category.Description).MinimumLength(3).NotNull().WithMessage("Kategori açıklaması en az 3 karakter içermelidir.").WithErrorCode("2");
            RuleFor(category => category.Description).Must(IsNumber).NotNull().WithMessage("Kategori açıklaması sadece sayı içermemelidir.").WithErrorCode("2");

        }

       private static bool IsNumber(string description)
        {
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
