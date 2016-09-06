using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities
{
    public class Client<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual string ClientId { get; set; }

        public virtual ICollection<ClientSecret<TKey>> ClientSecrets { get; set; }

        /// <summary>
        /// If set to false, no client secret is needed to request tokens at the token endpoint (defaults to true)
        /// </summary>
        public bool RequireClientSecret { get; set; }

        public virtual string ClientName { get; set; }

        public virtual string ClientUri { get; set; }

        public virtual string LogoUri { get; set; }

        public string LogoutUri { get; set; }

        public bool LogoutSessionRequired { get; set; }

        public virtual bool RequireConsent { get; set; }

        /// <summary>
        /// Specifies whether a proof key is required for authorization code based token requests
        /// </summary>
        public virtual bool RequirePkce { get; set; }

        public virtual bool AllowRememberConsent { get; set; }

        public virtual ICollection<ClientGrantType<TKey>> AllowedGrantTypes { get; set; }

        public virtual ICollection<ClientRedirectUri<TKey>> RedirectUris { get; set; }

        public virtual ICollection<ClientPostLogoutRedirectUri<TKey>> PostLogoutRedirectUris { get; set; }

        public virtual bool AllowAccessToAllScopes { get; set; }

        public virtual ICollection<ClientScope<TKey>> AllowedScopes { get; set; }

        // Seconds
        [Range(0, int.MaxValue)]
        public virtual int IdentityTokenLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public virtual int AccessTokenLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public virtual int AuthorizationCodeLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public virtual int AbsoluteRefreshTokenLifetime { get; set; }

        [Range(0, int.MaxValue)]
        public virtual int SlidingRefreshTokenLifetime { get; set; }

        public virtual TokenUsage RefreshTokenUsage { get; set; }

        public virtual bool AllowPromptNone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the access token (and its claims) should be updated on a refresh token request.
        /// </summary>
        /// <value>
        /// <c>true</c> if the token should be updated; otherwise, <c>false</c>.
        /// </value>
        public virtual bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        public virtual TokenExpiration RefreshTokenExpiration { get; set; }

        public virtual AccessTokenType AccessTokenType { get; set; }

        public virtual bool EnableLocalLogin { get; set; }

        public virtual ICollection<ClientProviderRestriction<TKey>> IdentityProviderRestrictions { get; set; }

        public virtual bool IncludeJwtId { get; set; }

        public virtual ICollection<ClientClaim<TKey>> Claims { get; set; }

        public virtual bool AlwaysSendClientClaims { get; set; }

        public virtual bool PrefixClientClaims { get; set; }

        public virtual ICollection<ClientCorsOrigin<TKey>> AllowedCorsOrigins { get; set; }

        public bool AllowAccessTokensViaBrowser { get; set; }
    }
}