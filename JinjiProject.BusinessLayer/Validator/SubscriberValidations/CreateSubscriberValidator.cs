using FluentValidation;
using JinjiProject.Dtos.Subscribers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.BusinessLayer.Validator.SubscriberValidations
{
    public class CreateSubscriberValidator : AbstractValidator<CreateSubscriberDto>
    {
        public CreateSubscriberValidator()
        {
            RuleFor(subscriber => subscriber.FullName).NotEmpty().WithMessage("Abone adı boş geçilemez.").WithErrorCode("1");
            RuleFor(subscriber => subscriber.Email).NotEmpty().WithMessage("Email boş geçilemez.").WithErrorCode("2");
        }
    }
}
