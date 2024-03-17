using FluentValidation;
using JinjiProject.BusinessLayer.Validator.ProductValidations;
using JinjiProject.Dtos.Genres;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Validator.GenreValidations
{
    public class CreateGenreValidator : AbstractValidator<CreateGenreDto>
    {
        public CreateGenreValidator()
        {
            RuleFor(genre => genre.Name).NotEmpty().WithMessage("Kategori türünün adı boş geçilemez.").WithErrorCode("1");
            RuleFor(genre => genre.Name).MinimumLength(2).WithMessage("Kategori türünün adı en az 2 karakter içermelidir.").WithErrorCode("1");
            RuleFor(genre => genre.Name).Must(IsNumber).WithMessage("Kategori türünün adı sadece sayı içermemelidir.").WithErrorCode("1");


            RuleFor(genre => genre.Description).NotEmpty().WithMessage("Kategori türünün açıklaması boş geçilemez.").WithErrorCode("2");
            RuleFor(genre => genre.Description).MinimumLength(3).WithMessage("Kategori türünün açıklaması en az 3 karakter içermelidir.").WithErrorCode("2");
            RuleFor(genre => genre.Description).Must(IsNumber).WithMessage("Kategori türünün açıklaması sadece sayı içermemelidir.").WithErrorCode("2");
            RuleFor(x => x.UploadPath).NotEmpty().WithMessage("Fotoğraf boş geçilemez!").NotNull().WithMessage("Fotoğraf boş geçilemez!").WithErrorCode("3");
            RuleFor(x => x.UploadPath).Must(FileExtensions.IsImage).WithMessage("Dosya sadece .jpg .jpeg veya .png uzantılı olmalıdır!").WithErrorCode("3");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Kategori geçilemez!").NotNull().WithMessage("Kategori geçilemez!").WithErrorCode("4");
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
