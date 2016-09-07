using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwentyTwenty.IdentityServer4.EntityFrameworkCore.Interfaces;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Services
{
    public class ClientConfigurationCorsPolicyService<TKey> : ICorsPolicyService
        where TKey : IEquatable<TKey>
    {
        readonly IClientConfigurationContext<TKey> context;

        public ClientConfigurationCorsPolicyService(IClientConfigurationContext<TKey> ctx)
        {
            context = ctx;
        }

        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var urls = await context.Clients
                .SelectMany(x1 => x1.AllowedCorsOrigins).Select(x => x.Origin).ToArrayAsync();

            return urls.Select(x => x.GetOrigin()).Where(x => x != null).Distinct()
                .Contains(origin, StringComparer.OrdinalIgnoreCase);
        }
    }
}