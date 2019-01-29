using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using MediatR;
using SSar.Contexts.Common.Application.Commands;

namespace SSar.Contexts.IdentityAuth.Application.Commands
{
    public class CreateRoleCommand : IRequest<CommandResult>
    {
        public CreateRoleCommand()
        {  // For aspnetcore razor pages
        }

        public CreateRoleCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
