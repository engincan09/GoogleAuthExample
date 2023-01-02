using Microsoft.AspNetCore.Identity;

namespace GoogleAuthentication.Api.Entities.Users
{
    public class User : IdentityUser<string>
    {
        public string Provider { get; set; }
    }
}
