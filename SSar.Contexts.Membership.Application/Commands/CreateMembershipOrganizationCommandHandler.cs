using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SSar.Contexts.Common.Application.Commands;
using SSar.Contexts.Common.Data;
using SSar.Contexts.Common.Data.Extensions;
using SSar.Contexts.Common.Domain.ValueTypes;
using SSar.Contexts.Membership.Domain.AggregateRoots.MembershipOrganizations;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateMembershipOrganizationCommandHandler : AppRequestHandler<CreateMembershipOrganizationCommand, CommandResult>
    {
        private readonly AppDbContext _dbContext;

        public CreateMembershipOrganizationCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override async Task<CommandResult> HandleCore(CreateMembershipOrganizationCommand request, CancellationToken cancellationToken)
        {
            return
                (await 
                    MembershipOrganization
                        .Create(new OrganizationName(request.Name))
                        .AddIfSucceeded(_dbContext))
                .ToCommandResult();
        }
    }
}
