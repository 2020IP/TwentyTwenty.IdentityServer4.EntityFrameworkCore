using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;

namespace IdentityServer4.Models
{
    public static class MappingExtensions
    {
        public static Scope<TKey> ToEntity<TKey>(this Scope s, Scope<TKey> dest = null)
            where TKey : IEquatable<TKey>
        {
            dest = dest ?? new Scope<TKey>();

            dest.AllowUnrestrictedIntrospection = s.AllowUnrestrictedIntrospection;
            dest.ClaimsRule = s.ClaimsRule;
            dest.Description = s.Description;
            dest.DisplayName = s.DisplayName;
            dest.Emphasize = s.Emphasize;
            dest.Enabled = s.Enabled;
            dest.IncludeAllClaimsForUser = s.IncludeAllClaimsForUser;
            dest.Name = s.Name;
            dest.Required = s.Required;
            dest.ScopeClaims = s.Claims.ToEnumerableOrEmpty().Select(x => new ScopeClaim<TKey>
            {
                AlwaysIncludeInIdToken = x.AlwaysIncludeInIdToken,
                Description = x.Description,
                Name = x.Name,
            }).ToArray();
            dest.ScopeSecrets = s.ScopeSecrets.ToEnumerableOrEmpty().Select(x => new ScopeSecret<TKey>
            {
                Description = x.Description,
                Expiration = x.Expiration.HasValue ? x.Expiration.Value.UtcDateTime : default(DateTime?),
                Type = x.Type,
                Value = x.Value,
            }).ToArray();
            dest.ShowInDiscoveryDocument = s.ShowInDiscoveryDocument;
            dest.Type = (int)s.Type;
 
            return dest;
        }

        public static Client<TKey> ToEntity<TKey>(this Client s, Client<TKey> dest = null)
            where TKey : IEquatable<TKey>
        {
            dest = dest ?? new Client<TKey>();

            dest.AbsoluteRefreshTokenLifetime = s.AbsoluteRefreshTokenLifetime;
            dest.AccessTokenLifetime = s.AccessTokenLifetime;
            dest.AccessTokenType = s.AccessTokenType;
            dest.AllowAccessToAllScopes = s.AllowAccessToAllScopes;
            dest.AllowAccessTokensViaBrowser = s.AllowAccessTokensViaBrowser;
            dest.AllowedCorsOrigins = s.AllowedCorsOrigins.ToEnumerableOrEmpty().Select(x => new ClientCorsOrigin<TKey> { Origin = x }).ToArray();
            dest.AllowedGrantTypes = s.AllowedGrantTypes.ToEnumerableOrEmpty().Select(x => new ClientGrantType<TKey> { GrantType = x }).ToArray();
            dest.AllowedScopes = s.AllowedScopes.ToEnumerableOrEmpty().Select(x => new ClientScope<TKey> { Scope = x }).ToArray();
            dest.AllowPromptNone = s.AllowPromptNone;
            dest.AllowRememberConsent = s.AllowRememberConsent;
            dest.AlwaysSendClientClaims = s.AlwaysSendClientClaims;
            dest.AuthorizationCodeLifetime = s.AuthorizationCodeLifetime;
            dest.Claims = s.Claims.ToEnumerableOrEmpty().Select(x => new ClientClaim<TKey>
            {
                Type = x.Type,
                Value = x.Value,
            }).ToArray();
            dest.ClientId = s.ClientId;
            dest.ClientName = s.ClientName;
            dest.ClientSecrets = s.ClientSecrets.ToEnumerableOrEmpty().Select(x => new ClientSecret<TKey>
            {
                Description = x.Description,
                Expiration = x.Expiration.HasValue ? x.Expiration.Value.UtcDateTime : default(DateTime?),
                Type = x.Type,
                Value = x.Value,
            }).ToArray();
            dest.ClientUri = s.ClientUri;
            dest.Enabled = s.Enabled;
            dest.EnableLocalLogin = s.EnableLocalLogin;
            dest.IdentityProviderRestrictions = s.IdentityProviderRestrictions.ToEnumerableOrEmpty().Select(x => new ClientProviderRestriction<TKey> { Provider = x }).ToArray();
            dest.IdentityTokenLifetime = s.IdentityTokenLifetime;
            dest.IncludeJwtId = s.IncludeJwtId;
            dest.LogoUri = s.LogoUri;
            dest.LogoutSessionRequired = s.LogoutSessionRequired;
            dest.LogoutUri = s.LogoutUri;
            dest.PostLogoutRedirectUris = s.PostLogoutRedirectUris.ToEnumerableOrEmpty().Select(x => new ClientPostLogoutRedirectUri<TKey> { Uri = x }).ToArray();
            dest.PrefixClientClaims = s.PrefixClientClaims;
            dest.RedirectUris = s.RedirectUris.ToEnumerableOrEmpty().Select(x => new ClientRedirectUri<TKey> { Uri = x }).ToArray();
            dest.RefreshTokenExpiration = s.RefreshTokenExpiration;
            dest.RefreshTokenUsage = s.RefreshTokenUsage;
            dest.RequireConsent = s.RequireConsent;
            dest.SlidingRefreshTokenLifetime = s.SlidingRefreshTokenLifetime;
            dest.UpdateAccessTokenOnRefresh = s.UpdateAccessTokenClaimsOnRefresh;

            return dest;
        }
    }
}