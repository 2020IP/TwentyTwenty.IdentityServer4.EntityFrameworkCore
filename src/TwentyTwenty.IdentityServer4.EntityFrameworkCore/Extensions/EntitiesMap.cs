using System;
using System.Linq;
using System.Security.Claims;
using Models = IdentityServer4.Models;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public static class MappingExtensions
    {
        public static Models.Consent ToModel(this Consent s)
        {
            if (s == null)
            {
                return null;
            }

            return new Models.Consent
            {
                ClientId = s.ClientId,
                SubjectId = s.SubjectId,
                Scopes = s.Scopes.ParseScopes(),
            };
        }

        public static Models.Scope ToModel<TKey>(this Scope<TKey> s)
            where TKey : IEquatable<TKey>
        {
            if (s == null)
            {
                return null;
            }

            return new Models.Scope
            {
                AllowUnrestrictedIntrospection = s.AllowUnrestrictedIntrospection,
                ClaimsRule = s.ClaimsRule,
                Description = s.Description,
                DisplayName = s.DisplayName,
                ShowInDiscoveryDocument = s.ShowInDiscoveryDocument,
                Emphasize = s.Emphasize,
                Enabled = s.Enabled,
                IncludeAllClaimsForUser = s.IncludeAllClaimsForUser,
                Name = s.Name,
                Required = s.Required,
                Type = (Models.ScopeType)s.Type,
                Claims = s.ScopeClaims.ToEnumerableOrEmpty().Select(x => new Models.ScopeClaim
                {
                    AlwaysIncludeInIdToken = x.AlwaysIncludeInIdToken,
                    Description = x.Description,
                    Name = x.Name,
                }).ToList(),
                ScopeSecrets = s.ScopeSecrets.ToEnumerableOrEmpty().Select(x => new Models.Secret
                {
                    Description = x.Description,
                    Expiration = x.Expiration,
                    Type = x.Type,
                    Value = x.Value,
                }).ToList(),
            };
        }

        public static Models.Client ToModel<TKey>(this Client<TKey> s)
            where TKey : IEquatable<TKey>
        {
            if (s == null)
            {
                return null;
            }

            return new Models.Client
            {
                AbsoluteRefreshTokenLifetime = s.AbsoluteRefreshTokenLifetime,
                AccessTokenLifetime = s.AccessTokenLifetime,
                AccessTokenType = s.AccessTokenType,
                AllowAccessToAllScopes = s.AllowAccessToAllScopes,
                AllowAccessTokensViaBrowser = s.AllowAccessTokensViaBrowser,
                RequirePkce = s.RequirePkce,
                RequireClientSecret = s.RequireClientSecret,
                AllowRememberConsent = s.AllowRememberConsent,
                AlwaysSendClientClaims = s.AlwaysSendClientClaims,
                AuthorizationCodeLifetime = s.AuthorizationCodeLifetime,
                AllowedScopes = s.AllowedScopes.ToEnumerableOrEmpty().Select(x => x.Scope).ToList(),
                AllowedGrantTypes = s.AllowedGrantTypes.ToEnumerableOrEmpty().Select(x => x.GrantType).ToList(),
                AllowedCorsOrigins = s.AllowedCorsOrigins.ToEnumerableOrEmpty().Select(x => x.Origin).ToList(),
                Claims = s.Claims.ToEnumerableOrEmpty().Select(x => new Claim(x.Type, x.Value)).ToList(),
                ClientId = s.ClientId,
                ClientName = s.ClientName,
                ClientSecrets = s.ClientSecrets.ToEnumerableOrEmpty().Select(x => new Models.Secret
                {
                    Description = x.Description,
                    Expiration = x.Expiration,
                    Type = x.Type,
                    Value = x.Value,
                }).ToList(),
                ClientUri = s.ClientUri,
                Enabled = s.Enabled,
                EnableLocalLogin = s.EnableLocalLogin,
                IdentityProviderRestrictions = s.IdentityProviderRestrictions.ToEnumerableOrEmpty().Select(x => x.Provider).ToList(),
                IdentityTokenLifetime = s.IdentityTokenLifetime,
                IncludeJwtId = s.IncludeJwtId,
                LogoUri = s.LogoUri,
                LogoutSessionRequired = s.LogoutSessionRequired,
                LogoutUri = s.LogoutUri,
                PostLogoutRedirectUris = s.PostLogoutRedirectUris.ToEnumerableOrEmpty().Select(x => x.Uri).ToList(),
                PrefixClientClaims = s.PrefixClientClaims,
                RedirectUris = s.RedirectUris.ToEnumerableOrEmpty().Select(x => x.Uri).ToList(),
                RefreshTokenExpiration = s.RefreshTokenExpiration,
                RefreshTokenUsage = s.RefreshTokenUsage,
                RequireConsent = s.RequireConsent,
                SlidingRefreshTokenLifetime = s.SlidingRefreshTokenLifetime,
                UpdateAccessTokenClaimsOnRefresh = s.UpdateAccessTokenClaimsOnRefresh,
            };
        }
    }
}