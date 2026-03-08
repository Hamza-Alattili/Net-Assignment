using Application.CQRS.Commands;
using Application.Repositories.Interface;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly IGenericRepository.IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IGenericRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.Id);

            if (customer == null)
            {
                throw new Exception($"العميل برقم {request.Id} غير موجود");
            }

            customer.Name = request.Name;
            customer.Phone = request.Phone;

            _unitOfWork.Customers.Update(customer);

            await _unitOfWork.CompleteAsync();
        }

        Task<bool> IRequestHandler<UpdateCustomerCommand, bool>.Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
