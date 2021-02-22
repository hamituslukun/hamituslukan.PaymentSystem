using FluentValidation;
using hamituslukan.PaymentSystem.Dto.Concrete;

namespace hamituslukan.PaymentSystem.Business.ValidationRules
{
    public class SubscriberTypeValidator : AbstractValidator<SubscriberTypeDto>
    {
        public SubscriberTypeValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.IdentityLength).NotNull();
        }
    }
}