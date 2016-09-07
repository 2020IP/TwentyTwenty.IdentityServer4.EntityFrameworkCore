using Microsoft.EntityFrameworkCore;
using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Interfaces;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts
{
    public class ScopeConfigurationContext<TKey> : DbContext, IScopeConfigurationContext<TKey>
        where TKey : IEquatable<TKey>
    {
        public ScopeConfigurationContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Scope<TKey>> Scopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureScopeConfigurationContext<TKey>();

            base.OnModelCreating(modelBuilder);
        }
    }
}