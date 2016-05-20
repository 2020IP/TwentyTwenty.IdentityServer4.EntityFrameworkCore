namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore
{
    class EfConstants
    {
        public class TableNames
        {
            public const string Client = "Clients";
            public const string ClientClaim = "ClientClaims";
            public const string ClientCustomGrantType = "ClientCustomGrantTypes";
            public const string ClientProviderRestriction = "ClientProviderRestrictions";
            public const string ClientPostLogoutRedirectUri = "ClientPostLogoutRedirectUris";
            public const string ClientRedirectUri = "ClientRedirectUris";
            public const string ClientScopes = "ClientScopes";
            public const string ClientSecret = "ClientSecrets";
            public const string ClientCorsOrigin = "ClientCorsOrigins";

            public const string Scope = "Scopes";
            public const string ScopeClaim = "ScopeClaims";
            public const string ScopeSecrets = "ScopeSecrets";

            public const string Consent = "Consents";
            public const string Token = "Tokens";
        }
    }
}