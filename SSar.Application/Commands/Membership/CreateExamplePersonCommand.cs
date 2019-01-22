using MediatR;
using SSar.Application.Commands.Infrastructure;

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
