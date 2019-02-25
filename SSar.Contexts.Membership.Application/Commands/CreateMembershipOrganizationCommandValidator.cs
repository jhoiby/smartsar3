using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateMembershipOrganizationCommandValidator : AbstractValidator<CreateMembershipOrganizationCommand>
    {
        public CreateMembershipOrganizationCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
