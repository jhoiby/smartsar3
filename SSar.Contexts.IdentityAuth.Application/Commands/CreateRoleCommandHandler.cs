using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SSar.Contexts.Common.Application.Commands;
using SSar.Contexts.Common.Data;
using SSar.Contexts.IdentityAccess.Application.Extensions;
using SSar.Contexts.IdentityAccess.Domain.Entities;

namespace SSar.Contexts.IdentityAccess.Application.Commands
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CommandResult>
    {
        private RoleManager<ApplicationRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<CommandResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var identityResult = await _roleManager.CreateAsync(
                new ApplicationRole(request.Name, request.Description, DateTime.UtcNow));

            return identityResult.ToCommandResult();
        }
    }
}
