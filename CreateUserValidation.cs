using FluentValidation;

namespace ActionFilterValidator;

public class CreateUserValidation : AbstractValidator<CreateUserDto>
{
    public CreateUserValidation()
    {
        RuleFor(t => t.Name)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(t => t.Age)
            .NotEmpty()
            .GreaterThan(18);
    }
}