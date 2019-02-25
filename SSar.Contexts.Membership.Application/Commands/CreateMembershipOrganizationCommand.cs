using MediatR;
using SSar.Contexts.Common.Application.Commands;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateMembershipOrganizationCommand : IRequest<CommandResult>
    {
        public CreateMembershipOrganizationCommand()
        {
        }

        public CreateMembershipOrganizationCommand(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
