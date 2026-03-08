using Application.CQRS.Commands;
using FluentValidation;

namespace Application.Validators
{
    
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("معرف الطلب مطلوب");
        }
    }
}
