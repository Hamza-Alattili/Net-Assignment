using MediatR;

namespace Application.CQRS.Commands
{
    
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
    }
}
