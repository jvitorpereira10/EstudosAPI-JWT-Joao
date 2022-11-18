using System.Collections.Generic;
using System.Linq;

namespace Ok.Tech.Domain.Notifications
{
  public class Notifier : INotifier
  {
    private readonly IList<Notifications> _notifications;
    public Notifier()
    {
      _notifications = new List<Notifications>();
    }

    public bool HasNotifications()
    {
      return _notifications.Any();
    }

    public IEnumerable<Notifications> GetNotifications()
    {
      return _notifications;
    }

    public void Handle(Notifications notifications)
    {
      _notifications.Add(notifications);
    }
  }
}
