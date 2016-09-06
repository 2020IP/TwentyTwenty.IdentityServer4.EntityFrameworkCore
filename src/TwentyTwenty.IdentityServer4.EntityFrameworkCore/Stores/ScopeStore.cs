using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = IdentityServer4.Models;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Stores;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Stores
{
    public class ScopeStore<TKey> : IScopeStore
        where TKey : IEquatable<TKey>
    {
        private readonly ScopeConfigurationContext<TKey> _context;

        public ScopeStore(ScopeConfigurationContext<TKey> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public async Task<IEnumerable<Models.Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            var scopes = _context.Scopes
                .Include(e => e.ScopeClaims)
                .Include( e => e.ScopeSecrets )
                .AsQueryable();

            if (scopeNames != null && scopeNames.Any())
            {
                scopes = scopes.Where(e => scopeNames.Contains(e.Name));
            }

            var list = await scopes.ToListAsync();
            return list.Select(e => e.ToModel());
        }

        public async Task<IEnumerable<Models.Scope>> GetScopesAsync(bool publicOnly = true)
        {
            var scopes = _context.Scopes
                .Include(e => e.ScopeClaims)
                .Include(e => e.ScopeSecrets)
                .AsQueryable();

            if (publicOnly)
            {
                scopes = scopes.Where(e => e.ShowInDiscoveryDocument);
            }

            var list = await scopes.ToListAsync();
            return list.Select(x => x.ToModel());
        }
    }
}