using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureClientContext<TKey>(this ModelBuilder modelBuilder)
            where TKey : IEquatable<TKey>
        {
            modelBuilder.Entity<Client<TKey>>(b =>
            {
                b.ToTable(EfConstants.TableNames.Client);
                b.HasMany(e => e.ClientSecrets).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.RedirectUris).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.PostLogoutRedirectUris).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.AllowedScopes).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.IdentityProviderRestrictions).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.ClientSecrets).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.AllowedCorsOrigins).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasMany(e => e.AllowedGrantTypes).WithOne(e => e.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasIndex(e => e.ClientId).IsUnique();
                b.Property(e => e.ClientId).IsRequired().HasMaxLength(200);
                b.Property(e => e.ClientName).IsRequired().HasMaxLength(200);
                b.Property(e => e.ClientUri).HasMaxLength(2000);
            });

            modelBuilder.Entity<ClientClaim<TKey>>(b =>
            {
                b.ToTable(EfConstants.TableNames.ClientClaim);
                b.Property(e => e.Type).IsRequired().HasMaxLength(250);
                b.Property(e => e.Value).IsRequired().HasMaxLength(250);
            });

            modelBuilder.Entity<ClientCorsOrigin<TKey>>()
                .ToTable(EfConstants.TableNames.ClientCorsOrigin)
                .Property(e => e.Origin).IsRequired().HasMaxLength(150);

            modelBuilder.Entity<ClientGrantType<TKey>>()
                .ToTable(EfConstants.TableNames.ClientGrantType)
                .Property(e => e.GrantType).IsRequired().HasMaxLength(150);

            modelBuilder.Entity<ClientPostLogoutRedirectUri<TKey>>()
                .ToTable(EfConstants.TableNames.ClientPostLogoutRedirectUri)
                .Property(e => e.Uri).IsRequired().HasMaxLength(2000);

            modelBuilder.Entity<ClientProviderRestriction<TKey>>()
                .ToTable(EfConstants.TableNames.ClientProviderRestriction)
                .Property(e => e.Provider).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<ClientRedirectUri<TKey>>()
                .ToTable(EfConstants.TableNames.ClientRedirectUri)
                .Property(e => e.Uri).IsRequired().HasMaxLength(2000);

            modelBuilder.Entity<ClientScope<TKey>>()
                .ToTable(EfConstants.TableNames.ClientScopes)
                .Property(e => e.Scope).IsRequired().HasMaxLength(200);

            modelBuilder.Entity<ClientSecret<TKey>>(b =>
            {
                b.ToTable(EfConstants.TableNames.ClientSecret);
                b.Property(e => e.Value).IsRequired().HasMaxLength(250);
                b.Property(e => e.Type).HasMaxLength(250);
                b.Property(e => e.Description).HasMaxLength(2000);
            });
        }

        public static void ConfigureOperationalContext(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consent>(b =>
            {
                b.ToTable(EfConstants.TableNames.Consent);
                b.Property(e => e.SubjectId).HasMaxLength(200);
                b.Property(e => e.ClientId).HasMaxLength(200);
                b.Property(e => e.Scopes).IsRequired().HasMaxLength(2000);
                b.HasKey(e => new { e.SubjectId, e.ClientId });
            });

            modelBuilder.Entity<Token>(b =>
            {
                b.ToTable(EfConstants.TableNames.Token);
                b.Property(e => e.SubjectId).HasMaxLength(200);
                b.Property(e => e.ClientId).IsRequired().HasMaxLength(200);
                b.Property(e => e.JsonCode).IsRequired();
                b.Property(e => e.Expiry).IsRequired();
                b.HasKey(e => new { e.Key, e.TokenType });
            });
        }

        public static void ConfigureScopeConfigurationContext<TKey>(this ModelBuilder modelBuilder)
            where TKey : IEquatable<TKey>
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
    }
}
