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
            RuleFor(x => x.FullName)
                .NotEmpty();

            RuleFor(x => x.ShortName)
                .NotEmpty();

            RuleFor(x => x.ReportingCode)
                .NotEmpty().MaximumLength(8);
        }
    }
}
