using FluentValidation;
using hamituslukan.PaymentSystem.Dto.Concrete;

namespace hamituslukan.PaymentSystem.Business.ValidationRules
{
    public class InvoiceValidator : AbstractValidator<InvoiceDto>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.StartDate).NotNull();
            RuleFor(x => x.EndDate).NotNull();
            RuleFor(x => x.DueDate).NotNull();
            RuleFor(x => x.Amount).NotNull();
            RuleFor(x => x.Subscriber).NotNull();
        }
    }
}