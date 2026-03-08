using Application.CQRS.Commands;
using Application.Repositories.Interface;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    
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

            await _unitOfWork.Customers.AddAsync(customer);

            await _unitOfWork.CompleteAsync();

            return customer.Id;
        }
    }
}
