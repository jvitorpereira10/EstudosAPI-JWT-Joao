using Microsoft.AspNetCore.Mvc;
using Ok.Tech.Api.Extensions.Security;
using Ok.Tech.Domain.Notifications;
using System.Threading.Tasks;

namespace Ok.Tech.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly ITokenManager _tokenManager;
        public AuthController(ITokenManager tokenManager, INotifier notifier) : base(notifier)
        {
            _tokenManager = tokenManager;
        }

        [HttpPost("signin")]
        public async Task<ActionResult> SignIn()
        {
            return OKResponse(_tokenManager.CreateToken());
        }
    }
}