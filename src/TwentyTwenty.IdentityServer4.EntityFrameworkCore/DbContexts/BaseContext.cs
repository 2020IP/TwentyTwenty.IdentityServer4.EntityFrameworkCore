using Microsoft.EntityFrameworkCore;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) 
            : base(options)
        { }
    }
}