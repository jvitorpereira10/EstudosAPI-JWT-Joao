using Microsoft.AspNetCore.Mvc;
using Ok.Tech.Api.Models.Error;
using Ok.Tech.Domain.Notifications;
using System.Linq;

namespace Ok.Tech.Api.Controllers
{
  [ApiController]
  public abstract class MainController : ControllerBase
  {
    private readonly INotifier _notifier;

    public MainController(INotifier notifier)
    {
      _notifier = notifier;
    }

    protected ActionResult CreatedResponse(string actionName, object routeValues, object value = null)
    {
      if (!IsOperationValid())
      {
        return CreatedAtAction(actionName, routeValues, value);
      }

      return BadRequestResponse();
    }
    protected ActionResult OKResponse(object value)
    {
      if (!IsOperationValid())
      {
        return Ok(value);
      }

      return BadRequestResponse();
    }

    protected ActionResult OKResponse()
    {
      if (!IsOperationValid())
      {
        return Ok();
      }

      return BadRequestResponse();
    }

    protected ActionResult BadRequestResponse()
    {
      var errorModel = new ErrorModel { Messages = _notifier.GetNotifications().Select(n => n.Message) };

      return BadRequest(errorModel);
    }

    protected bool IsOperationValid()
    {
      return _notifier.HasNotifications();
    }

    protected void NotifyError(string errorMessage)
    {
      _notifier.Handle(new Notifications(errorMessage));
    }

    protected bool IsModelValid()
    {
      if (ModelState.IsValid)
      {
        return true;
      }
      
      NotifyModelStateErrors();

      return false;
    }

    protected void NotifyModelStateErrors()
    {
      var errors = ModelState.Values.SelectMany(e => e.Errors);

      foreach (var error in errors)
      {
        string errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
        NotifyError(errorMessage);
      }
    }
  }
}
