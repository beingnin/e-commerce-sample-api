using ECommerce.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        protected virtual User CurrentUser { get => GetCurrentUser(); }
        private User GetCurrentUser()
        {
            if (this.User != null && this.User.Claims != null && this.User.Claims.Any())
            {
                var claims = this.User.Claims;
                User user = new();
                user.UserId = Convert.ToInt32(claims.First(x => x.Type == "user-id").Value);
                user.FirstName = claims.First(x => x.Type == "first-name").Value;
                user.LastName = claims.First(x => x.Type == "last-name").Value;
                user.UserName = claims.First(x => x.Type == "user-name").Value;
                return user;
            }
            return null;
        }
    }
}
