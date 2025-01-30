namespace Pizza.Infrastructure.Validations;
using Domain.Entities;
using FluentValidation;

public class PizzaValidator : AbstractValidator<PizzaE>
{
    public PizzaValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(3,20).WithMessage("Name must be between 3 and 20 characters");
        RuleFor(x => x.Price)
            .Must(x => x > 0).WithMessage("Price must be greater than 0")
            .NotEmpty().WithMessage("Price is required");
        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("Description must be less than 100 characters");
        RuleFor(x => x.CaloryCount)
            .Must(x => x > 0).WithMessage("Caloric count must be greater than 0")
            .NotEmpty().WithMessage("Caloric count is required");
    }
}