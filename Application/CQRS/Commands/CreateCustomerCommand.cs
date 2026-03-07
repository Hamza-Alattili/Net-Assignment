using MediatR;

namespace Application.CQRS.Commands
{
    /// <summary>
    /// Command لإنشاء عميل جديد
    /// يتم تنفيذه بواسطة CreateCustomerCommandHandler
    /// </summary>
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
