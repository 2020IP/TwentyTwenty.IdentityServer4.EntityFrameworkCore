using Microsoft.EntityFrameworkCore;
using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Interfaces;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts
{
    public class ClientConfigurationContext<TKey> : DbContext, IClientConfigurationContext<TKey>
        where TKey : IEquatable<TKey>
    {
        public ClientConfigurationContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Client<TKey>> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureClientContext<TKey>();

            base.OnModelCreating(modelBuilder);
        }
    }
}