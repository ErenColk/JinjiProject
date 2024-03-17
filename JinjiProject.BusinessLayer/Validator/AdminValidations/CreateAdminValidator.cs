using FluentValidation;
using JinjiProject.BusinessLayer.Validator.ProductValidations;
using JinjiProject.Core.Entities.Concrete;
using JinjiProject.Dtos.Admins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Validator.AdminValidations
{
    public class CreateAdminValidator : AbstractValidator<CreateAdminDto>
    {
        public CreateAdminValidator()
        {
            RuleFor(admin => admin.FirstName).NotEmpty().WithMessage("Ad boş geçilemez.").MinimumLength(2).WithMessage("Admin adı en az 2 karakter içermelidir.").Must(IsNumber).WithMessage("Admin adı sadece sayı içermemelidir.").WithErrorCode("1");

            RuleFor(admin => admin.LastName).NotEmpty().WithMessage("Soyad boş geçilemez.").MinimumLength(2).WithMessage("Admin soyadı en az 2 karakter içermelidir.").Must(IsNumber).WithMessage("Admin soyadı sadece sayı içermemelidir.").WithErrorCode("2");


            RuleFor(admin => admin.BirthDate).NotEmpty().WithMessage("Doğum Tarihi boş geçilemez.").WithErrorCode("3");

            RuleFor(admin => admin.Email).NotEmpty().WithMessage("Email adresi boş geçilemez.").WithErrorCode("4");

            RuleFor(admin => admin.Gender).NotNull().WithMessage("Cinsiyet boş geçilemez.").WithErrorCode("5");

            RuleFor(admin => admin.UploadPath).NotNull().WithMessage("Fotoğraf boş geçilemez.").WithErrorCode("6");
            RuleFor(x => x.UploadPath).Must(FileExtensions.IsImage).WithMessage("Dosya sadece .jpg .jpeg veya .png uzantılı olmalıdır!").WithErrorCode("6");

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
