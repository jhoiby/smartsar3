using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Notifications;
using SSar.Contexts.Membership.Data;
using SSar.Contexts.Membership.Domain.Entities;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateExamplePersonCommandHandler : AppRequestHandler<CreateExamplePersonCommand, OperationResult>
    {
        private MembershipDbContext _dbContext;

        public CreateExamplePersonCommandHandler(MembershipDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override async Task<OperationResult> HandleCore(CreateExamplePersonCommand request, CancellationToken cancellationToken)
        {
            var test = ExamplePerson.CreateFromData(request.Name, request.EmailAddress);

            var exPerson = Create<MembershipDbContext,ExamplePerson>(
                _dbContext, 
                Guid.NewGuid(), 
                () => ExamplePerson.CreateFromData(request.Name, request.EmailAddress));

            return OperationResult.Successful(exPerson.Id);
        }
    }
}
