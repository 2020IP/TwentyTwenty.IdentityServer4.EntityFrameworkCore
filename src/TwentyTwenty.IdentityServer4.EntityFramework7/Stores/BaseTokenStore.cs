using IdentityServer4.Core.Models;
using IdentityServer4.Core.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Serialization;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Stores
{
    // TODO: FindAsync is slated to be back in the RTM of 7.0. 
    //      For how, Where and FirstOrDefaultAsync will have to make due
    public abstract class BaseTokenStore<TEntity>
        where TEntity : class
    {
        protected readonly OperationalContext _context;
        protected readonly TokenType _tokenType;
        protected readonly IScopeStore _scopeStore;
        private readonly IClientStore _clientStore;

        protected IClientStore ClientStore
        {
            get
            {
                return _clientStore;
            }
        }

        protected BaseTokenStore(OperationalContext context, TokenType tokenType, IScopeStore scopeStore, IClientStore clientStore)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (scopeStore == null) throw new ArgumentNullException("scopeStore");
            if (clientStore == null) throw new ArgumentNullException("clientStore");

            _context = context;
            _tokenType = tokenType;
            _scopeStore = scopeStore;
            _clientStore = clientStore;
        }

        JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClaimConverter());
            settings.Converters.Add(new ClaimsPrincipalConverter());
            settings.Converters.Add(new ClientConverter(ClientStore));
            settings.Converters.Add(new ScopeConverter(_scopeStore));
            return settings;
        }

        protected string ConvertToJson(TEntity value)
        {
            return JsonConvert.SerializeObject(value, GetJsonSerializerSettings());
        }

        protected TEntity ConvertFromJson(string json)
        {
            return JsonConvert.DeserializeObject<TEntity>(json, GetJsonSerializerSettings());
        }

        public async Task<TEntity> GetAsync(string key)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == key && x.TokenType == _tokenType)
                .FirstOrDefaultAsync();

            if (token == null || token.Expiry < DateTime.UtcNow)
            {
                return null;
            }

            return ConvertFromJson(token.JsonCode);
        }

        public async Task RemoveAsync(string key)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == key && x.TokenType == _tokenType)
                .FirstOrDefaultAsync();

            if (token != null)
            {
                _context.Tokens.Remove(token);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ITokenMetadata>> GetAllAsync(string subject)
        {
            var tokens = await _context.Tokens.Where(x =>
                x.SubjectId == subject &&
                x.TokenType == _tokenType).ToArrayAsync();

            var results = tokens.Select(x => ConvertFromJson(x.JsonCode)).ToArray();
            return results.Cast<ITokenMetadata>();
        }

        public async Task RevokeAsync(string subject, string client)
        {
            var found = _context.Tokens.Where(x =>
                x.SubjectId == subject &&
                x.ClientId == client &&
                x.TokenType == _tokenType).ToArray();

            _context.Tokens.RemoveRange(found);
            await _context.SaveChangesAsync();
        }

        public abstract Task StoreAsync(string key, TEntity value);
    }
}