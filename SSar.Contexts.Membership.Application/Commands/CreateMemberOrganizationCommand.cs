using MediatR;
using SSar.Contexts.Common.Application.Commands;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateMemberOrganizationCommand : IRequest<CommandResult>
    {
        public CreateMemberOrganizationCommand()
        {
        }

        public CreateMemberOrganizationCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
