using FluentValidation;
using Ok.Tech.Domain.Entities;

namespace Ok.Tech.Domain.Validations
{
  public class ProductValidation : AbstractValidator<Product>
  {
    public ProductValidation()
    {
      RuleFor(p => p.Name)
      .NotEmpty().WithMessage("The {PropertyName} must be supplied")
      .Length(3, 200).WithMessage("The {PropertyName} length must be between {MinLength} and {MaxLength}");

      RuleFor(p => p.Description)
      .NotEmpty().WithMessage("The {PropertyName} must be supplied")
      .Length(3, 1000).WithMessage("The {PropertyName} length must be between {MinLength} and {MaxLength}");
    }
  }
}
