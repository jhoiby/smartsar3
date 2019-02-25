using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateMemberOrganizationCommandValidator : AbstractValidator<CreateMemberOrganizationCommand>
    {
        public CreateMemberOrganizationCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
