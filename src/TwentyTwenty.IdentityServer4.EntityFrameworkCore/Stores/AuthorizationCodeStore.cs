using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Threading.Tasks;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Stores
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