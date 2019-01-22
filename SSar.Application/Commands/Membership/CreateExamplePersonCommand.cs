using MediatR;
using SSar.Infrastructure.Commands;

namespace SSar.Application.Commands.Membership
{
    public class CreateExamplePersonCommand : IRequest<CommandResult>
    {
        public CreateExamplePersonCommand()
        {
        }

        public CreateExamplePersonCommand(string name, string emailAddress)
        {
            Name = name;
            EmailAddress = emailAddress;
        }

        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
