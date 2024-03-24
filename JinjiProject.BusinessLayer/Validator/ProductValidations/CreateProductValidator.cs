using FluentValidation;
using JinjiProject.Dtos.Products;
using Microsoft.AspNetCore.Http;
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
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı boş geçilemez!").WithErrorCode("1");
            RuleFor(x => x.Name).MaximumLength(150).WithMessage("Ürün adı en fazla 20 karakter olabilir!").WithErrorCode("1");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Ürün fiyatı boş geçilemez!").WithErrorCode("2");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Ürün stoğu boş geçilemez!").WithErrorCode("3");
            RuleFor(x => x.Color).NotEmpty().WithMessage("Ürün rengi boş geçilemez!").WithErrorCode("4");
            RuleFor(x => x.Color).MaximumLength(30).WithMessage("Ürün rengi en fazla 30 karakter olabilir!").WithErrorCode("4");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Ürün açıklaması boş geçilemez!").WithErrorCode("5");
            RuleFor(x => x.BrandId).NotEmpty().NotNull().WithMessage("Ürün markası boş geçilemez!").WithErrorCode("6");
            RuleFor(x => x.CategoryId).NotEmpty().NotNull().WithMessage("Kategori boş geçilemez!").WithErrorCode("7");
            RuleFor(x => x.GenreId).NotEmpty().NotNull().WithMessage("Kategori Türü boş geçilemez!").WithErrorCode("8");
            RuleFor(x => x.MaterialId).NotEmpty().NotNull().WithMessage("Malzeme boş geçilemez!").WithErrorCode("9");
            RuleFor(x => x.UploadPath).NotEmpty().NotNull().WithMessage("Fotoğraf boş geçilemez!").WithErrorCode("10");
            RuleFor(x => x.UploadPath).Must(FileExtensions.IsImage).WithMessage("Dosya sadece .jpg .jpeg veya .png uzantılı olmalıdır!").WithErrorCode("10");
            RuleFor(x => x.UploadPathSecond).Must(file => file == null || file.IsImage()).WithMessage("Dosya sadece .jpg .jpeg veya .png uzantılı olmalıdır!").WithErrorCode("11");
            RuleFor(x => x.UploadPathThirth).Must(file => file == null || file.IsImage()).WithMessage("Dosya sadece .jpg .jpeg veya .png uzantılı olmalıdır!").WithErrorCode("12");
        }
       
    }

}
