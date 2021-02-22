using FluentValidation;
using hamituslukan.PaymentSystem.Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace hamituslukan.PaymentSystem.Business.ValidationRules
{
    public class SubscriberValidator : AbstractValidator<SubscriberDto>
    {
        public SubscriberValidator()
        {
            RuleFor(x => x.Type).NotNull();
            RuleFor(x => x.IdentityNumber).Length<SubscriberDto>(x => x.Type.IdentityLength);
            RuleFor(x => x.BeginDate).NotNull();
            RuleFor(x => x.Deposit).NotNull();
            RuleFor(x => x.User).NotNull();
        }
    }
}