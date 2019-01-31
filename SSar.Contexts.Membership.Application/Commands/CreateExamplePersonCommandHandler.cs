using SSar.Contexts.Common.Application.Commands;
using SSar.Contexts.Common.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using SSar.Contexts.Common.Data.Extensions;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePersons;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateExamplePersonCommandHandler : AppRequestHandler<CreateExamplePersonCommand, CommandResult>
    {
        private AppDbContext _dbContext;

        public CreateExamplePersonCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override async Task<CommandResult> HandleCore(CreateExamplePersonCommand request, CancellationToken cancellationToken)
        {
            return
                (await
                    ExamplePerson
                        .Create(request.Name, request.EmailAddress)
                        .AddIfSucceeded(_dbContext))
                .ToCommandResult();
        }
    }
}
