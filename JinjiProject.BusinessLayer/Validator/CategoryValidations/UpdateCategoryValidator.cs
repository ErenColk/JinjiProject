﻿using FluentValidation;
using JinjiProject.Dtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Validator.CategoryValidations
{
	public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
	{
        public UpdateCategoryValidator()
        {
			RuleFor(category => category.Name).NotEmpty().WithMessage("Kategori adı boş geçilemez.").WithErrorCode("1");
			RuleFor(category => category.Name).MinimumLength(2).WithMessage("Kategori adı en az 2 karakter içermelidir.").WithErrorCode("1");
			RuleFor(category => category.Name).Must(IsNumber).WithMessage("Kategori adı sadece sayı içermemelidir.").WithErrorCode("1");


			RuleFor(category => category.Description).NotEmpty().WithMessage("Kategori açıklaması boş geçilemez.").WithErrorCode("2");
			RuleFor(category => category.Description).MinimumLength(3).WithMessage("Kategori açıklaması en az 3 karakter içermelidir.").WithErrorCode("2");
			RuleFor(category => category.Description).Must(IsNumber).WithMessage("Kategori açıklaması sadece sayı içermemelidir.").WithErrorCode("2");

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
