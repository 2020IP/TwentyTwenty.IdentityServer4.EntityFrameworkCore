using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using IdentityServer4.Stores.Serialization;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Serialization;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Stores
{
    public class PersistedGrantService : IPersistedGrantService
    {
        private readonly IOperationalContext _context;
        private readonly IScopeStore _scopeStore;
        private readonly IClientStore _clientStore;

        public PersistedGrantService(IOperationalContext context, IScopeStore scopeStore, IClientStore clientStore)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (!(context is DbContext)) throw new ArgumentException("Operational context is not a Database Context", nameof(context));
            if (scopeStore == null) throw new ArgumentNullException(nameof(scopeStore));
            if (clientStore == null) throw new ArgumentNullException(nameof(clientStore));

            _context = context;
            _scopeStore = scopeStore;
            _clientStore = clientStore;
        }

        public async Task<IEnumerable<Consent>> GetAllGrantsAsync(string subjectId)
        {
            var found = await _context.Consents
                .Where(x => x.SubjectId == subjectId).ToArrayAsync();

            var results = found.Select(x => new Consent
            {
                SubjectId = x.SubjectId,
                ClientId = x.ClientId,
                Scopes = x.Scopes.ParseScopes()
            });

            return results.ToArray().AsEnumerable();
        }

        public async Task<AuthorizationCode> GetAuthorizationCodeAsync(string code)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == code && x.TokenType == Entities.TokenType.AuthorizationCode)
                .FirstOrDefaultAsync();

            if (token == null || token.Expiry < DateTime.UtcNow)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<AuthorizationCode>(token.JsonCode, GetJsonSerializerSettings());
        }

        public async Task<Token> GetReferenceTokenAsync(string handle)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == handle && x.TokenType == Entities.TokenType.TokenHandle)
                .FirstOrDefaultAsync();

            if (token == null || token.Expiry < DateTime.UtcNow)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Token>(token.JsonCode, GetJsonSerializerSettings());
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string refreshTokenHandle)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == refreshTokenHandle && x.TokenType == Entities.TokenType.RefreshToken)
                .FirstOrDefaultAsync();

            if (token == null || token.Expiry < DateTime.UtcNow)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<RefreshToken>(token.JsonCode, GetJsonSerializerSettings());
        }

        public async Task<Consent> GetUserConsentAsync(string subjectId, string clientId)
        {
            var consent = await _context.Consents
                .Where(x => x.SubjectId == subjectId && x.ClientId == clientId)
                .FirstOrDefaultAsync();

            if (consent == null)
            {
                return null;
            }

            return new Consent
            {
                SubjectId = consent.SubjectId,
                ClientId = consent.ClientId,
                Scopes = consent.Scopes.ParseScopes()
            };
        }

        public async Task RemoveAllGrantsAsync(string subjectId, string clientId)
        {
            var tokens = _context.Tokens.ToArray();
            _context.Tokens.RemoveRange(tokens);
            await ((DbContext)_context).SaveChangesAsync();
        }

        public async Task RemoveAuthorizationCodeAsync(string code)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == code
                    && x.TokenType == Entities.TokenType.AuthorizationCode)
                .FirstOrDefaultAsync();

            if (token != null)
            {
                _context.Tokens.Remove(token);
                await ((DbContext)_context).SaveChangesAsync();
            }
        }

        public async Task RemoveReferenceTokenAsync(string handle)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == handle
                    && x.TokenType == Entities.TokenType.TokenHandle)
                .FirstOrDefaultAsync();

            if (token != null)
            {
                _context.Tokens.Remove(token);
                await ((DbContext)_context).SaveChangesAsync();
            }
        }

        public async Task RemoveReferenceTokensAsync(string subjectId, string clientId)
        {
            var found = _context.Tokens.Where(x =>
                x.SubjectId == subjectId &&
                x.ClientId == clientId &&
                x.TokenType == Entities.TokenType.TokenHandle).ToArray();

            _context.Tokens.RemoveRange(found);
            await ((DbContext)_context).SaveChangesAsync();
        }

        public async Task RemoveRefreshTokenAsync(string refreshTokenHandle)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == refreshTokenHandle
                    && x.TokenType == Entities.TokenType.RefreshToken)
                .FirstOrDefaultAsync();

            if (token != null)
            {
                _context.Tokens.Remove(token);
                await ((DbContext)_context).SaveChangesAsync();
            }
        }

        public async Task RemoveRefreshTokensAsync(string subjectId, string clientId)
        {
            var found = _context.Tokens.Where(x =>
                x.SubjectId == subjectId &&
                x.ClientId == clientId &&
                x.TokenType == Entities.TokenType.RefreshToken).ToArray();

            _context.Tokens.RemoveRange(found);
            await ((DbContext)_context).SaveChangesAsync();
        }

        public async Task RemoveUserConsentAsync(string subjectId, string clientId)
        {
            var found = await _context.Consents
                .Where(x => x.SubjectId == subjectId && x.ClientId == clientId)
                .FirstOrDefaultAsync();

            if (found != null)
            {
                _context.Consents.Remove(found);
                await ((DbContext)_context).SaveChangesAsync();
            }
        }

        public async Task StoreAuthorizationCodeAsync(string handle, AuthorizationCode code)
        {
            var token = new Entities.Token
            {
                Key = handle,
                SubjectId = code.Subject.Claims.FirstOrDefault(c => c.Type == "sub")?.Value,
                ClientId = code.ClientId,
                JsonCode = JsonConvert.SerializeObject(code, GetJsonSerializerSettings()),
                Expiry = DateTime.UtcNow.AddSeconds(code.Lifetime),
                TokenType = Entities.TokenType.AuthorizationCode,
            };

            _context.Tokens.Add(token);
            await ((DbContext)_context).SaveChangesAsync();
        }

        public async Task StoreReferenceTokenAsync(string handle, Token token)
        {
            var storeToken = new Entities.Token
            {
                Key = handle,
                SubjectId = token.SubjectId,
                ClientId = token.ClientId,
                JsonCode = JsonConvert.SerializeObject(token, GetJsonSerializerSettings()),
                Expiry = DateTime.UtcNow.AddSeconds(token.Lifetime),
                TokenType = Entities.TokenType.TokenHandle
            };

            _context.Tokens.Add(storeToken);
            await ((DbContext)_context).SaveChangesAsync();
        }

        public async Task StoreRefreshTokenAsync(string handle, RefreshToken refreshToken)
        {
            var token = await _context.Tokens
                .Where(x => x.Key == handle && x.TokenType == Entities.TokenType.RefreshToken)
                .FirstOrDefaultAsync();

            if (token == null)
            {
                token = new Entities.Token
                {
                    Key = handle,
                    SubjectId = refreshToken.SubjectId,
                    ClientId = refreshToken.ClientId,
                    JsonCode = JsonConvert.SerializeObject(refreshToken, GetJsonSerializerSettings()),
                    TokenType = Entities.TokenType.RefreshToken,
                };
                _context.Tokens.Add(token);
            }

            token.Expiry = DateTime.UtcNow.AddSeconds(refreshToken.Lifetime);
            await ((DbContext)_context).SaveChangesAsync();
        }

        public async Task StoreUserConsentAsync(Consent consent)
        {
            var item = await _context.Consents
                .Where(x => x.SubjectId == consent.SubjectId && x.ClientId == consent.ClientId)
                .FirstOrDefaultAsync();

            if (item == null)
            {
                item = new Entities.Consent
                {
                    SubjectId = consent.SubjectId,
                    ClientId = consent.ClientId
                };
                _context.Consents.Add(item);
            }

            if (consent.Scopes == null || !consent.Scopes.Any())
            {
                _context.Consents.Remove(item);
            }

            item.Scopes = consent.Scopes.StringifyScopes();

            await ((DbContext)_context).SaveChangesAsync();
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ClaimConverter());
            settings.Converters.Add(new ClaimsPrincipalConverter());
            settings.Converters.Add(new ClientConverter(_clientStore));
            settings.Converters.Add(new ScopeConverter(_scopeStore));
            return settings;
        }
    }
}