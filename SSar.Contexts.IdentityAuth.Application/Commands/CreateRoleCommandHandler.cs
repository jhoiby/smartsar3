using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SSar.Contexts.Common.Application.Commands;
using SSar.Contexts.Common.Data;
using SSar.Contexts.IdentityAuth.Application.Extensions;
using SSar.Contexts.IdentityAuth.Domain.Entities;

namespace SSar.Contexts.IdentityAuth.Application.Commands
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CommandResult>
    {
        private AppDbContext _dbContext;
        private RoleManager<ApplicationRole> _roleManager;

        public CreateRoleCommandHandler(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<CommandResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            IdentityResult x;

            var identityResult = await _roleManager.CreateAsync(
                new ApplicationRole(request.Name, request.Description, DateTime.UtcNow));

            return identityResult.ToCommandResult();
        }
    }
}
