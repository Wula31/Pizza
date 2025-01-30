using FluentValidation;
using Pizza.Application.Common.Interfaces;
using Pizza.Domain.Entities;

namespace Pizza.Infrastructure.Validations;

public class OrderValidation : AbstractValidator<Order>
{
    public OrderValidation(IUserRepository userRepository, IPizzaRepository pizzaRepository)
    {
        RuleFor(x => x.UserId)
            .MustAsync(async (userId,_) => await userRepository.EntityExistsAsync(userId)
            && await userRepository.EntityMarkedDeletedAsync(userId))
            .WithMessage("User does not exist or is marked deleted");
        RuleFor(x => x.Pizzas)
            .NotEmpty().WithMessage("Pizzas are required")
            .MustAsync(async (pizzas, _) =>
            {
                var results = await Task.WhenAll(pizzas.Select
                (async pizza => await pizzaRepository.EntityExistsAsync(pizza.Id) &&
                                !await pizzaRepository.EntityMarkedDeletedAsync(pizza.Id)));
                return results.All(result => result);
            }).WithMessage("Pizza does not exist or is marked deleted");
    }
}
