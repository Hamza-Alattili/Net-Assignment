using MediatR;

namespace Application.CQRS.Commands
{
    
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
