using Microsoft.Extensions.DependencyInjection;
using System;

namespace TwentyTwenty.IdentityServer4.EntityFrameworkCore
{
    public static class IdentityServerBuilderExtensions
    {
        public static EntityFrameworkOptions ConfigureEntityFramework(this IIdentityServerBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            return new EntityFrameworkOptions(builder);
        }
    }
}