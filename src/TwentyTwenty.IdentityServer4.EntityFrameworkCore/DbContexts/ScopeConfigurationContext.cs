using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts
{
    public class ScopeConfigurationContext<TKey> : BaseContext
        where TKey : IEquatable<TKey>
    {
        public ScopeConfigurationContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Scope<TKey>> Scopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScopeClaim<TKey>>(b =>
            {
                b.ToTable(EfConstants.TableNames.ScopeClaim);
                b.Property(e => e.Name).IsRequired().HasMaxLength(200);
                b.Property(e => e.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<ScopeSecret<TKey>>(b =>
            {
                b.ToTable(EfConstants.TableNames.ScopeSecrets);
                b.Property(e => e.Description).HasMaxLength(1000);
                b.Property(e => e.Type).HasMaxLength(250);
                b.Property(e => e.Value).IsRequired().HasMaxLength(250);
            });

            modelBuilder.Entity<Scope<TKey>>(b =>
            {
                b.ToTable(EfConstants.TableNames.Scope);
                b.HasMany(e => e.ScopeClaims).WithOne(e => e.Scope).OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.ScopeSecrets).WithOne(e => e.Scope).OnDelete(DeleteBehavior.Cascade);
                b.Property(e => e.Name).IsRequired().HasMaxLength(200);
                b.Property(e => e.DisplayName).HasMaxLength(200);
                b.Property(e => e.Description).HasMaxLength(1000);
                b.Property(e => e.ClaimsRule).HasMaxLength(200);
            });
        }
        //protected override void ConfigureChildCollections()
        //{
        //    this.Set<Scope>().Local.CollectionChanged +=
        //        delegate (object sender, NotifyCollectionChangedEventArgs e)
        //        {
        //            if (e.Action == NotifyCollectionChangedAction.Add)
        //            {
        //                foreach (Scope item in e.NewItems)
        //                {
        //                    RegisterDeleteOnRemove(item.ScopeClaims);
        //                }
        //            }
        //        };
        //}
    }
}