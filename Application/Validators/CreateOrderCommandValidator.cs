using Application.CQRS.Commands;
using FluentValidation;

namespace Application.Validators
{
    
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("معرف العميل مطلوب");

            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("إجمالي الطلب يجب أن يكون أكبر من صفر");
        }
    }
}
