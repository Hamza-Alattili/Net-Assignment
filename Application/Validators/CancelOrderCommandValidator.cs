using Application.CQRS.Commands;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator لـ CancelOrderCommand
    /// يتحقق من صحة البيانات المدخلة قبل إلغاء طلب
    /// </summary>
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("معرف الطلب مطلوب");
        }
    }
}
