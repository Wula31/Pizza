using FluentValidation;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;

namespace Pizza.Infrastructure.Validations;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(2,20).WithMessage("First name must be between 2 and 20 characters");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2,30).WithMessage("Last name must be between 2 and 30 characters");
        RuleFor(x=> x.Email)
            .NotEmpty().WithMessage("Email is required")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Email is not a valid email address");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Phone number is not valid Phone Number");
        RuleFor(x => x.Address)
            .Must(address => address == null || address.Length > 0)
            .WithMessage("Address must not be an empty array if provided");
    }
}