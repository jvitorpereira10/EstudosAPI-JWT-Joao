using System.Collections.Generic;

namespace Ok.Tech.Domain.Notifications
{
  public interface INotifier
  {
    bool HasNotifications();

    IEnumerable<Notifications> GetNotifications();

    void Handle(Notifications notifications);
  }
}
