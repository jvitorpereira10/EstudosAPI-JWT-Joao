namespace Ok.Tech.Domain.Notifications
{
  public class Notifications
  {
    public Notifications(string message)
    {
      Message = message;
    }
    public string Message { get; }
  }
}
