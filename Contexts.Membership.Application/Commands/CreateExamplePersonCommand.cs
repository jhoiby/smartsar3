using System;
using MediatR;
using SSar.Contexts.Common.Notifications;
using SSar.Contexts.Membership.Domain.Entities;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateExamplePersonCommand : IRequest<CommandResult>
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
