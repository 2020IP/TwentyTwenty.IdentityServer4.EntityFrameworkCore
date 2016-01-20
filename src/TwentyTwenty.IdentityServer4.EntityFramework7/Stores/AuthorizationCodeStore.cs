using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;
using System;
using System.Threading.Tasks;
using TwentyTwenty.IdentityServer4.EntityFramework7.DbContexts;
using TwentyTwenty.IdentityServer4.EntityFramework7.Entities;

namespace TwentyTwenty.IdentityServer4.EntityFramework7.Stores
{
    public class AuthorizationCodeStore : BaseTokenStore<AuthorizationCode>, IAuthorizationCodeStore
    {
        public AuthorizationCodeStore(OperationalContext context, IScopeStore scopeStore, IClientStore clientStore)
            : base(context, TokenType.AuthorizationCode, scopeStore, clientStore)
        {
        }

        public override async Task StoreAsync(string key, AuthorizationCode code)
        {
            var efCode = new Entities.Token
            {
                Key = key,
                SubjectId = code.SubjectId,
                ClientId = code.ClientId,
                JsonCode = ConvertToJson(code),
                Expiry = DateTime.UtcNow.AddSeconds(code.Client.AuthorizationCodeLifetime),
                TokenType = _tokenType
            };

            _context.Tokens.Add(efCode);
            await _context.SaveChangesAsync();
        }
    }
}