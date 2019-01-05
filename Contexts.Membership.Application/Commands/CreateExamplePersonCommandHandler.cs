using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SSar.Contexts.Common.Notifications;
using SSar.Contexts.Membership.Data;
using SSar.Contexts.Membership.Domain.Entities;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateExamplePersonCommandHandler : IRequestHandler<CreateExamplePersonCommand, OperationResult>
    {
        private MembershipDbContext _dbContext;

        public CreateExamplePersonCommandHandler(MembershipDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult> Handle(CreateExamplePersonCommand request, CancellationToken cancellationToken)
        {
            var exPerson = ExamplePerson.CreateFromData(request.Name, request.EmailAddress);
            _dbContext.Add(exPerson);

            return OperationResult.Successful(exPerson.Id);
        }
    }
}
