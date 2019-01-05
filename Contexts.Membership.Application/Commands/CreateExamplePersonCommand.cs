using System;
using MediatR;
using SSar.Contexts.Common.Notifications;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateExamplePersonCommand : IRequest<OperationResult>
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
