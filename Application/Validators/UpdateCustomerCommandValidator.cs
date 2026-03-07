using Application.CQRS.Commands;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator لـ UpdateCustomerCommand
    /// يتحقق من صحة البيانات المدخلة قبل تحديث بيانات عميل
    /// </summary>
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("معرف العميل مطلوب");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("اسم العميل مطلوب")
                .MinimumLength(3).WithMessage("اسم العميل يجب أن يكون 3 أحرف على الأقل")
                .MaximumLength(100).WithMessage("اسم العميل لا يجب أن يتجاوز 100 حرف");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("رقم الهاتف مطلوب")
                .Matches(@"^[0-9\-\+\(\)\s]+$").WithMessage("رقم الهاتف غير صحيح");
        }
    }
}
