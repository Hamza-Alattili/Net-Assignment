using Application.CQRS.Commands;
using Application.Repositories.Interface;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    /// <summary>
    /// Handler لـ CreateCustomerCommand
    /// يتولى تنفيذ عملية إنشاء عميل جديد
    /// 
    /// الخطوات:
    /// 1. استقبال الـ Command من Controller
    /// 2. التحقق من صحة البيانات (يتم بواسطة Validator)
    /// 3. إنشاء entity جديد من Customer
    /// 4. إضافته إلى قاعدة البيانات
    /// 5. حفظ التغييرات
    /// 6. إرجاع معرف العميل الجديد
    /// </summary>
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly IGenericRepository.IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(IGenericRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // إنشاء entity جديد
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Phone = request.Phone,
                CreatedOn = DateTime.UtcNow
            };

            // إضافة العميل إلى قاعدة البيانات
            await _unitOfWork.Customers.AddAsync(customer);

            // حفظ التغييرات
            await _unitOfWork.CompleteAsync();

            // إرجاع معرف العميل الجديد
            return customer.Id;
        }
    }
}
