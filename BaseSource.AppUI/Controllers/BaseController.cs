using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BaseSource.AppUI.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private string _ipConnection;
        private string _userId;
        public string UserId
        {
            get { return _userId ?? User.FindFirstValue(ClaimTypes.NameIdentifier); }
            set { _userId = value; }
        }
        public string IpConnection
        {
            get
            {  // Lấy đối tượng HttpRequest từ Context
                var httpRequest = HttpContext.Request;
               return _ipConnection ?? httpRequest?.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            set { _ipConnection = value; }
        }
    }
}
