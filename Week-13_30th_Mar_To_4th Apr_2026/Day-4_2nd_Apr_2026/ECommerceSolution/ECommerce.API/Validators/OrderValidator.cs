using ECommerce.API.DTOs;
using FluentValidation;

namespace ECommerce.API.Validators;

public class OrderValidator : AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be valid");
    }
}