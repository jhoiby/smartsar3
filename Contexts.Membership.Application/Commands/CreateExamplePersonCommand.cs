using System;
using MediatR;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateExamplePersonCommand : IRequest<Guid>
    {
        public CreateExamplePersonCommand()
        {
        }

        //public CreateExamplePersonCommand(string name, string emailAddress)
        //{
        //    Name = name;
        //    EmailAddress = emailAddress;
        //}

        public string Name { get; set; }
        public string EmailAddress { get; set; }

    }
}
