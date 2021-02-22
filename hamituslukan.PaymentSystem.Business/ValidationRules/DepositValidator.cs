using FluentValidation;
using hamituslukan.PaymentSystem.Dto.Concrete;

namespace hamituslukan.PaymentSystem.Business.ValidationRules
{
    public class DepositValidator : AbstractValidator<DepositDto>
    {
        public DepositValidator()
        {
            RuleFor(x => x.Amount).NotNull();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.ReceiveDate).NotNull();
        }
    }
}