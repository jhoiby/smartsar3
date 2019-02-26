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
using SSar.Contexts.Membership.Domain.AggregateRoots.MemberOrganizations;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateMemberOrganizationCommandHandler : AppRequestHandler<CreateMemberOrganizationCommand, CommandResult>
    {
        private readonly AppDbContext _dbContext;

        public CreateMemberOrganizationCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        protected override async Task<CommandResult> HandleCore(CreateMemberOrganizationCommand request, CancellationToken cancellationToken)
        {
            return
                (await 
                    MemberOrganization
                        .Create(new OrganizationName(request.FullName, request.Nickname, request.ReportingCode))
                        .AddIfSucceeded(_dbContext))
                .ToCommandResult();
        }
    }
}
