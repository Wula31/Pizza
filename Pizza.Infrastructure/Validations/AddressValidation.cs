using System.Data;
using FluentValidation;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;
namespace Pizza.Infrastructure.Validations;


public class AddressValidation: AbstractValidator<Address>
{

    public AddressValidation(IUserRepository userRepository)
    {
        RuleFor(x => x.UserId)
            .MustAsync(async (userId,_) => await userRepository.UserExistsAsync(userId) 
                                           && !await userRepository.UserMarkedDeletedAsync(userId))
            .WithMessage("User does not exist or is marked deleted");
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required")
            .MaximumLength(15).WithMessage("There can only be 15 characters in a city");
        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required")
            .MaximumLength(15).WithMessage("There can only be 15 characters in a country");
        RuleFor(x => x.Region)
            .MaximumLength(15).WithMessage("There can only be 15 characters in a region");
        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("Description must be less than 100 characters");
    }
}




