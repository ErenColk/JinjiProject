﻿using FluentValidation;
using JinjiProject.VMs.ResetPassword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Validator.ResetPasswordValidations
{
    public class ResetPasswordVMValidator : AbstractValidator<ResetPasswordVM>
    {
        public ResetPasswordVMValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez!").Equal(x => x.ConfirmPassword).WithMessage("Şifre ve Şifre Tekrarı aynı olmalıdır!");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre tekrarı boş geçilemez!");
        }
    }
}
