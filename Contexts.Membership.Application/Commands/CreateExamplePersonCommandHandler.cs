using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SSar.Contexts.Common.AbstractClasses;
using SSar.Contexts.Common.Notifications;
using SSar.Contexts.Membership.Data;
using SSar.Contexts.Membership.Domain.Entities;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateExamplePersonCommandHandler : AppRequestHandler<CreateExamplePersonCommand, CommandResult>
    {
        private MembershipDbContext _dbContext;

        public CreateExamplePersonCommandHandler(MembershipDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override async Task<CommandResult> HandleCore(CreateExamplePersonCommand request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("\n\n*** Executing CreateExamplePersonCommandHandler.HandleCore()");

            //var aggregateResult = await Create<MembershipDbContext,ExamplePerson>(
            //    _dbContext, 
            //    Guid.NewGuid(), 
            //    () => ExamplePerson.Create(request.Name, request.EmailAddress));

            return new CommandResult();
        }
    }
}
