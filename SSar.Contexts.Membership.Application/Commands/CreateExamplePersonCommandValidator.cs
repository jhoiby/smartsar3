using FluentValidation;

namespace SSar.Contexts.Membership.Application.Commands
{
    public class CreateExamplePersonCommandValidator : AbstractValidator<CreateExamplePersonCommand>
    {
        public CreateExamplePersonCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Validator: Name must not be empty.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .WithMessage("Validator: Email address must not be empty.");
        }
    }
}
