using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace SSar.Contexts.IdentityAuth.Application.Commands
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
