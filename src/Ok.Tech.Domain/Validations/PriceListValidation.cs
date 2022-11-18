using FluentValidation;
using Ok.Tech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ok.Tech.Domain.Validations
{
    public class PriceListValidation : AbstractValidator<PriceList>
    {
        public PriceListValidation()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("The {PropertyName} must be supplied")
                .Length(3, 200).WithMessage("The {PropertyName} lenght must be between {MinLenght} and {MaxLenght}");
        }
    }
}
