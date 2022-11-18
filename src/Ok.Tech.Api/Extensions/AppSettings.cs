namespace Ok.Tech.Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public int ExpirationInHours { get; set; }

        public string Emitter { get; set; }

        public string ValidIn { get; set; }
    }
}