using FluentValidation;
using Ok.Tech.Domain.Entities;

namespace Ok.Tech.Domain.Validations
{
    public class PriceValidation : AbstractValidator<Price>
    {
        public PriceValidation()
        {
            RuleFor(s => s.Value)
                .NotEmpty()
                .WithMessage("The {PropertyName} must be supplied");

            RuleFor(s => s.Value)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The {PropertyName} must be greater or equal to 0");
        }
    }
}