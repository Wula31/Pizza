using FluentValidation;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;

namespace Pizza.Infrastructure.Validations;

public class RankHistoryValidation : AbstractValidator<RankHistory>
{
    public RankHistoryValidation(IUserRepository userRepository, IPizzaRepository pizzaRepository)
    {
        
        RuleFor(x => x.UserId)
            .MustAsync(async (userId,_) => await userRepository.EntityExistsAsync(userId)
                                           && await userRepository.EntityMarkedDeletedAsync(userId))
            .WithMessage("User does not exist or is marked deleted");
        RuleFor(x => x.PizzaId)
            .MustAsync(async (pizzaId,_) => await pizzaRepository.EntityExistsAsync(pizzaId)
                                           && await pizzaRepository.EntityMarkedDeletedAsync(pizzaId))
            .WithMessage("Pizza does not exist or is marked deleted");
        RuleFor(x => x.Rank)
            .Must(rank => rank is > 0 and <= 10)
            .WithMessage("Rank must be greater than 0 and less than or equal to 10");
    }
}