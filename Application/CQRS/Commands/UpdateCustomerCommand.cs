using MediatR;

namespace Application.CQRS.Commands
{
    /// <summary>
    /// Command لتحديث بيانات عميل موجود
    /// يتم تنفيذه بواسطة UpdateCustomerCommandHandler
    /// </summary>
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
    }
}
