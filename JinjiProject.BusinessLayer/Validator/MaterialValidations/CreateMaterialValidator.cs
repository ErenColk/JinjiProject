using FluentValidation;
using JinjiProject.Dtos.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Validator.MaterialValidations
{
    public class CreateMaterialValidator : AbstractValidator<CreateMaterialDto>
    {
        public CreateMaterialValidator()
        {
            RuleFor(material => material.Name).NotEmpty().WithMessage("Malzeme adı boş geçilemez.").WithErrorCode("1");
            RuleFor(material => material.Name).MinimumLength(2).WithMessage("Malzeme adı en az 2 karakter içermelidir.").WithErrorCode("1");
            RuleFor(material => material.Name).Must(IsNumber).WithMessage("Malzeme adı sadece sayı içermemelidir.").WithErrorCode("1");


            RuleFor(material => material.Description).NotEmpty().WithMessage("Malzeme açıklaması boş geçilemez.").WithErrorCode("2");
            RuleFor(material => material.Description).MinimumLength(3).WithMessage("Malzeme açıklaması en az 3 karakter içermelidir.").WithErrorCode("2");
            RuleFor(material => material.Description).Must(IsNumber).WithMessage("Malzeme açıklaması sadece sayı içermemelidir.").WithErrorCode("2");

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
