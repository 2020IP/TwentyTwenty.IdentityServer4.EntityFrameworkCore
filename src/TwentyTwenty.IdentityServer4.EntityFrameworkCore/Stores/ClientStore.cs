using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.DbContexts;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Entities;
using Models = IdentityServer4.Models;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Stores
{
    public class ClientStore<TKey> : IClientStore
        where TKey : IEquatable<TKey>
    {
        private readonly ClientConfigurationContext<TKey> context;

        public ClientStore(ClientConfigurationContext<TKey> context)
        {
            if (context == null) throw new ArgumentNullException("context");

            this.context = context;
        }

        public async Task<Models.Client> FindClientByIdAsync(string clientId)
        {
            var client = await context.Clients
                .Include(x => x.ClientSecrets)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .Include(x => x.AllowedScopes)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.Claims)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.AllowedGrantTypes)
                .SingleOrDefaultAsync(x => x.ClientId == clientId);

            return client.ToModel();
        }
    }
}