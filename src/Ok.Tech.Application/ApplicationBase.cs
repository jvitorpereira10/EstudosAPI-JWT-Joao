using FluentValidation;
using FluentValidation.Results;
using Ok.Tech.Domain.Entities;
using Ok.Tech.Domain.Notifications;
using Ok.Tech.Domain.Repositories;
using System.Linq;

namespace Ok.Tech.Application
{
  public abstract class ApplicationBase
  {
    private readonly INotifier _notifier;

    public ApplicationBase(IUnitOfWork unitOfWork, INotifier notifier)
    {
      UnitOfWork = unitOfWork;
      _notifier = notifier;
    }

    protected IUnitOfWork UnitOfWork { get; }

    protected void Notify(ValidationResult validationResult)
    {
      validationResult.Errors.ToList().ForEach((e) => { Notify(e.ErrorMessage); });
    }

    protected void Notify(string message)
    {
      _notifier.Handle(new Notifications(message));
    }

    protected bool Validate<TValidator, TEntity>(TValidator validator, TEntity entity) where TValidator : AbstractValidator<TEntity> where TEntity : Entity
    {
      var validate = validator.Validate(entity);

      Notify(validate);

      return validate.IsValid;
    }
  }
}
