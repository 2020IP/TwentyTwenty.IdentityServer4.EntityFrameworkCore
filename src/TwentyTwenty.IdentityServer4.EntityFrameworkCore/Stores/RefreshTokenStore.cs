using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Stores
{
    // TODO: FindAsync is slated to be back in the RTM of 7.0. 
    //      For how, Where and FirstOrDefaultAsync will have to make due
    public class RefreshTokenStore : BaseTokenStore<RefreshToken>, IRefreshTokenStore
    {
        public RefreshTokenStore(OperationalContext context, IScopeStore scopeStore, IClientStore clientStore)
            : base(context, TokenType.RefreshToken, scopeStore, clientStore)
        {
        }

        public override async Task StoreAsync(string key, RefreshToken value)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == key && x.TokenType == _tokenType)
                .FirstOrDefaultAsync();

            if (token == null)
            {
                token = new Entities.Token
                {
                    Key = key,
                    SubjectId = value.SubjectId,
                    ClientId = value.ClientId,
                    JsonCode = ConvertToJson(value),
                    TokenType = _tokenType
                };
                _context.Tokens.Add(token);
            }

            token.Expiry = DateTime.UtcNow.AddSeconds(value.LifeTime);
            await _context.SaveChangesAsync();
        }
    }
}