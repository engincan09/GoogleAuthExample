using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GoogleAuthentication.Api.Entities.Users;

namespace GoogleAuthentication.Api.Entities
{

    public class GoogleAuthDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public GoogleAuthDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
