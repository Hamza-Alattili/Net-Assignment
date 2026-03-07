using Application.CQRS.Commands;
using Application.Repositories.Interface;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    /// <summary>
    /// Handler لـ UpdateCustomerCommand
    /// يتولى تنفيذ عملية تحديث بيانات عميل موجود
    /// 
    /// الخطوات:
    /// 1. استقبال الـ Command من Controller
    /// 2. التحقق من صحة البيانات (يتم بواسطة Validator)
    /// 3. البحث عن العميل في قاعدة البيانات
    /// 4. تحديث البيانات
    /// 5. حفظ التغييرات
    /// </summary>
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly IGenericRepository.IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IGenericRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            // البحث عن العميل
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.Id);

            if (customer == null)
            {
                throw new Exception($"العميل برقم {request.Id} غير موجود");
            }

            // تحديث البيانات
            customer.Name = request.Name;
            customer.Phone = request.Phone;

            // تحديث العميل في قاعدة البيانات
            _unitOfWork.Customers.Update(customer);

            // حفظ التغييرات
            await _unitOfWork.CompleteAsync();
        }
    }
}
