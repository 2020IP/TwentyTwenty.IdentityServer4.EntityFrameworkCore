using Microsoft.EntityFrameworkCore;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Interfaces;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts
{
    public class OperationalContext : DbContext, IOperationalContext
    {
        public OperationalContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Consent> Consents { get; set; }

        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureOperationalContext();

            base.OnModelCreating(modelBuilder);
        }
    }
}