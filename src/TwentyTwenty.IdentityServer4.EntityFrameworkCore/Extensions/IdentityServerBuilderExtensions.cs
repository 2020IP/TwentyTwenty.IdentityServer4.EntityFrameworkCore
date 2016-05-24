using Microsoft.Extensions.DependencyInjection;
using System;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore.Extensions
{
    public static class IdentityServerBuilderExtensions
    {
        public static EntityFrameworkOptions ConfigureEntityFramework<TKey>(this IIdentityServerBuilder builder)
            where TKey : IEquatable<TKey>
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            return new EntityFrameworkOptions(builder);
        }
    }
}